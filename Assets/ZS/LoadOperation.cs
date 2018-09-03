/*
name:
desc:LoadOperation 下载选项
author:ZS
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace ZS.Loader
{
	// 定义抽象类 IEnumerator 表明该类是一个协程
    public abstract class LoadOperation : IEnumerator, IReleaseToPool{
        internal LoadOperation next;// 使用链表结构连接对象
        private System.Func<bool> m_OnUpdate;//定义两个委托来回调
        private System.Func<bool> m_IsDone;
        //每一个LoadOperation都包含一个CRequest 用于下载
        internal CRequest cRequest { get; private set;}
        protected bool pool { get; set; }// 用于表示是否已创建对象池，


        //定义继承的虚函数
        public virtual void ReleaseToPool(){}
        //定义保护注册委托函数  给委托赋值
        protected void RegisterEvent(System.Func<bool> onUpdate, System.Func<bool> isDone)
        { this.m_OnUpdate = onUpdate; this.m_IsDone = isDone; }
        // 该方法本应用虚函数来实现  virtual function is bad for il2cpp so we use Action
        public bool Update(){ return m_OnUpdate(); }
        public bool IsDone(){ return m_IsDone(); }

        // CRequest 管理函数
        public void SetRequest(CRequest req){ this.cRequest = req;}
        public static void SetRequestData(CRequest req, object data){
            if( !req.isDisposed)
                req.data = data;
        }

        // 链表管理函数
        public bool MoveNext(){ return !IsDone(); }


        // 重置函数 直接将变量置空  
        // 是因为CRequest、LoadOperation都继承ObjectPool，不需要显示释放内存
        public virtual void Reset(){
            pool = false;
            cRequest = null;
            next = null;
        }
    }

    // http request
    public abstract class HttpLoadOperation : LoadOperation {
        protected bool isDone;
        // 资源数据存储
        protected object m_Data;
        
        public string error { get; protected set; }

        public object GetAsset(){ return m_Data; }

        // 下载回调委托
        private System.Action m_BeginDownLoad;
        protected void RegisterBeginDownLoad(System.Action beginDownLoad){
            m_BeginDownLoad = beginDownLoad;
        }
        public void BeginDownLoad(){
            if (m_BeginDownLoad != null)
                m_BeginDownLoad();
        }

        public override void Reset(){
            base.Reset();
            error = null;
            isDone = false;
        }
        protected bool _IsDone(){ return isDone; }
    }

    public sealed class WWWRequestOperation : HttpLoadOperation{
        // 使用下载小文件
        private WWW m_webrequest;

        public WWWRequestOperation(){
            RegisterEvent(_Update,_IsDone);
            RegisterBeginDownLoad(_BeginDownLoad);
        }

        private bool _Update(){
            if(!isDone && downloadIsDone){
                FinishDownload();
                isDone = true;
            }
            return !isDone;
        }
        private void _BeginDownLoad(){
            m_Data = null;
            var head = cRequest.head;
            string url = CUtils.CheckWWWUrl(CRequest.url);
            bool isOverrideHost = string.IsNullOrEmpty(cRequest.overrideHost);

            if (head is WWWForm){
                m_webrequest = new WWW(url, (WWWForm) head);
            }else if( head is string){
                var bytes = LuaHelper.GetBytes(head.ToString());
                m_webrequest = new WWW(url, bytes);
            }else if( isOverrideHost ){
                System.Array bytes = null;
                if(head is System.Array) bytes = (byte[])head;
                Dictionary<string, string> headers = new Dictionary<string, string>();
                if( cRequest.headers != null){
                    foreach( var kv in cRequest.headers){
                        headers[kv.Key] = kv.Value;
                    }
                }
                headers["host"] = cRequest.overrideHost;
                m_webrequest = new WWW(url, (byte[])head, cRequest.headers);
            }else
                m_webrequest = new WWW(url);
        }
        private void FinishDownload(){
            if(m_webrequest == nul){
                error = string.Format("webrequest is null, {0}", cRequest.key);
                return;
            }
            error = m_webrequest.error;

            if(!string.IsNullOrEmpty(error)){
                Debug.LogWarningFormat("url:{0},erro:{1}", cRequest.url, error);
                return;
            }
            var type = cRequest.assetType;

            if(CacheManager.Typeof_AudioClip.Equals(type)){
                #if UNITY_2017
                    m_Data = WWWAudioExtensions.GetAudioClip(m_webrequest);
                #else
                    m_Data = m_webrequest.GetAudioClip(false);
                #endif
            }else if(CacheManager.Typeof_Texture2D.Equals(type)){
                if(!string.IsNullOrEmpty(cRequest.assetName)&&cRequest.assetName.Equals("textureNonReadable"))
                    m_Data = m_webrequest.textureNonReadable;
                else
                    m_Data = m_webrequest.texture;
            }else if(CacheManager.Typeof_AssetBundle.Equals(type)){
                m_Data = m_webrequest.assetBundle;
            }else if(CacheManager.Typeof_Bytes.Equals(type)){
                m_Data = m_webrequest.bytes;
            }else
                m_Data = m_webrequest.text;
            
            UriGroup.CheckWWWComplete(cRequest, m_webrequest);

            cRequest.data = m_Data;
            m_webrequest.Dispose();
            m_webrequest = null;
        }



        public override void Reset(){
            base.Reset();
            m_Data = null;
        }
        private bool downloadIsDone{
            get{ return m_webrequest == null || m_webrequest.isDone; }
        }

        //缓存池
        static ObjectPool<WWWRequestOperation> webOperationPool = new ObjectPool<WWWRequestOperation>(m_ActionOnGet, m_ActionOnRelease);
        private static void m_ActionOnGet(WWWRequestOperation op){ op.pool = true; }
        private static void m_ActionOnRelease(WWWRequestOperation op){ op.Reset(); }
        public static WWWRequestOperation Get(){ return webOperationPool.Get(); }
        public static void Release(WWWRequestOperation toRelease){ webOperationPool.Release(toRelease); }
    }

    public sealed class WebRequestOperation : HttpLoadOperation{
        // 适用于下载大文件  具备断点续传机制
        private UnityWebRequest m_webrequest;
        private AsyncOperation m_asyncOperation;

        public WebRequestOperation(){
            RegisterEvent(_Update, _IsDone);
            RegisterBeginDownLoad(_BeginDownLoad);
        }
        private bool _Update(){
            if(!isDone && downloadIsDone){
                FinishDownload();
                isDone = true;
            }
            return !isDone;
        }
        // 之所以分类型 是对应类型的处理接口  效率更高
        private void _BeginDownLoad(){
            m_Data = null;
            var type = cRequest.assetType;
            var head = cRequest.head;
            if(CacheManager.Typeof_AudioClip.Equals(type)){
                AudioType au = AudioType.MOD;
                if(cRequest.head is AudioType)
                    au = (AudioType)cRequest.head;
                    #if UNITY_2017
                        m_webrequest = UnityWebRequestMultimedia.GetAudioClip(cRequest.url, au);
                    #else
                        m_webrequest = UnityWebRequest.GetAudioClip(cRequest.url, au);
                    #endif
            }else if(CacheManager.Typeof_AssetBundle.Equals(type)){
                m_webrequest = UnityWebRequest.GetAssetBundle(cRequest.url);
            }else if(CacheManager.Typeof_Texture2D.Equals(type)){
                #if UNITY_2017
                    if(cRequest.head is bool)
                        m_webrequest = UnityWebRequestTexture.GetTexture(cRequest.url, (bool)cRequest.head);
                    else
                        m_webrequest = UnityWebRequestTexture.GetTexture(cRequest.url);
                #else
                    if(cRequest.head is bool)
                        m_webrequest = UnityWebRequest.GetTexture(cRequest.url, (bool)cRequest.head);
                    else
                        m_webrequest = UnityWebRequest.GetTexture(cRequest.url);
                #endif
            }else if(head is WWWForm)
                m_webrequest = UnityWebRequest.Post(cRequest.url, (WWWForm)head);
            else if(head is string){
                var bytes = LuaHelper.GetBytes(head.ToString());
                m_webrequest = UnityWebRequest.Put(cRequest.url, bytes);
            }else if(head is System.Array)
                m_webrequest = UnityWebRequest.Put(cRequest.url, (byte[])head);
            else
                m_webrequest = UnityWebRequest.Get(cRequest.url);

            if(cRequest.headers != null){
                var headers = cRequest.headers;
                foreach(var kv in headers)
                    m_webrequest.SetRequestHeader(kv.Key, kv.Value);
            }

            if( !string.IsNullOrEmpty(cRequest.overrideHost)){
                m_webrequest.SetRequestHeader("host", cRequest.overrideHost);
            }

            m_asyncOperation = m_webrequest.Send();
        }


        private void FinishDownload(){
            if(m_webrequest == null){
                error = string.Format("webrequest is null, key={0},url={1}", cRequest.key, cRequest.url);
                return;
            }
            cRequest.responseHeaders = m_webrequest.GetResponseHeaders();
            #if UNITY_2017
                if(m_webrequest.isNetworkError)
            #else
                if(m_webrequest.isError)
            #endif
            {
                error = string.Format("response error code={0},url={1}", m_webrequest.responseCode, cRequest.url);//m_webrequest.error
                Debug.LogWarning(error);
                return;
            }

            var type = cRequest.assetType;
            if(CacheManager.Typeof_AudioClip.Equals(type)){
                m_Data = DownloadHandlerAudioClip.GetContent(m_webrequest);
            }else if(CacheManager.Typeof_Texture2D.Equals(type)){
                m_Data = DownloadHandlerTexture.GetContent(m_webrequest);
            }else if(CacheManager.Typeof_Bytes.Equals(type)){
                m_Data = m_webrequest.downloadHandler.data;
            }else if(CacheManager.Typeof_AssetBundle.Equals(type)){
                m_Data = DownloadHandlerAssetBundle.GetContent(m_webrequest);
            }else
                m_Data = DownloadHandlerBuffer.GetContent(m_webrequest);

            if(!CacheManager.Typeof_AssetBundle.Equals(type))
                UriGroup.CheckWWWComplete(cRequest, m_webrequest);

            cRequest.data = m_Data;
            m_webrequest.Dispose();
            m_webrequest = null;
            m_asyncOperation = null;
        }

        public override void Reset(){
            base.Reset();
            m_Data = null;
        }

        private bool downloadIsDone{
            get{ return m_webrequest == null || m_webrequest.isDone; }
        }


        // 缓存池
        static ObjectPool<WebRequestOperation> webOperationPool = new ObjectPool<WebRequestOperation>(m_ActionOnGet,m_ActionOnRelease);
        private static void m_ActionOnGet(WebRequestOperation op){ op.pool = true; }
        private static void m_ActionOnRelease(WebRequestOperation op){ op.Reset(); }
        public static WebRequestOperation Get(){ return webOperationPool.Get(); }
        public static void Release(WebRequestOperation toRelease){ webOperationPool.Release(toRelease);}

    }

    // 以上是加载资源包 读取AssetBundle
    // 以下是从AssetBundle 读取资源

    // load asset
    
    public abstract class AssetBundleLoadAssetOperation : LoadOperation{
        protected object m_Data;
        public string error { get; protected set; }
        public T GetAsset<T>() where T : UnityEngine.Object{
            return m_Data as T;
        }
        public override void Reset(){
            base.Reset();
            error = null;
            m_Data = null;
        }
    }

    public sealed class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation{
        private AssetBundleRequest m_Requst = null;

        public AssetBundleLoadAssetOperationFull(){
            RegisterEvent(_Update,_IsDone);
        }

        //return true if more Update calls are required.
        private bool _Update(){
            if(m_Requst != null){
                if(cRequest.OnComplete != null)// wait asset complete
                    return !_IsDone();
            }

            CacheData bundle = CacheManager.TryGetCache(cRequest.keyHashCode);
            if(bundle != null && bundle.isDone && CacheManager.CheckDependenciesComplete(cRequest)){
                if(bundle.isError || !bundle.canUse){
                    error = string.Format("load asset{0} from bundle({1}) error", cRequest.assetName, cRequest.key);
                    return false;
                }else{

                    var type = cRequest.assetType;
                    bool loadAll = CacheManager.Typeof_ABAllAssets.Equals(type);

                    if(cRequest.async){
                        if(loadAll)
                            m_Requst = bundle.assetBundle.LoadAllAssetsAsync();
                        else
                            m_Requst = bundle.assetBundle.LoadAssetsAsync(cRequest.assetName, type);
                    }else{
                        if(loadAll)
                            m_Data = bundle.assetBundle.LoadAllAssets();
                        else
                            m_Data = bundle.assetBundle.LoadAsset(cRequest.assetName, type);

                        if(m_Data == null)
                            error = string.Format("load asset{0} from {1} error", cRequest.assetName, cRequest.key);
                    }

                    return !_IsDone();
                }

            }else{
                return true;
            }
        }

        bool _IsDone(){
            if(error != null){
                Debug.LogWarning(error);
                return true;
            }

            //no async load asset
            if(m_Data != null){
                SetRequestData(cRequest, m_Data);
                return true;
            }

            if(m_Requst != null && m_Requst.isDone){
                bool loadAll = CacheManager.Typeof_ABAllAssets.Equals(cRequest.assetType);

                if(loadAll)
                    m_Data = m_Requst.allAssets;
                else
                    m_Data = m_Requst.asset;

                if(m_Data == null)
                    error = string.Format("load asset({0}) from {1} error", cRequest.assetName, cRequest.key);

                SetRequestData(cRequest, m_Data);

                return true;
            }else{
                return false;
            }
        }

        public override void Reset(){
            base.Reset();
            m_Requst = null;
        }

        public override void ReleaseToPool(){
            if(pool)
                Release(this);
        }


        // 缓存池
        static ObjectPool<AssetBundleLoadAssetOperationFull> webOperationPool = new ObjectPool<AssetBundleLoadAssetOperationFull>(m_ActionOnGet, m_ActionOnRelease);
        private static void m_ActionOnGet(AssetBundleLoadAssetOperationFull op){ op.pool = true; }
        private static void m_ActionOnRelease(AssetBundleLoadAssetOperationFull op){ op.Reset(); }
        public static AssetBundleLoadAssetOperationFull Get(){ return webOperationPool.Get(); }
        public static void Release(AssetBundleLoadAssetOperationFull toRelease){ webOperationPool.Release(toRelease); }
    }


}