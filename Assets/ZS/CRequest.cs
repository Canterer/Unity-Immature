/*
name:
desc:Object 对象池
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using ZS.Utils;

namespace ZS.Loader
{
    // 非托管资源  继承IDisposable 管理内存释放
    [SLua.CustomLuaClass]
    public class CRequest : IDisposable{
        // 静态变量及函数  对象池
        static ObjectPool<CRequest> objectPool = new ObjectPool<CRequest> (m_ActionOnGet, m_ActionOnRelease);
        private static void m_ActionOnGet(CRequest req){
            req.pool = true;
            req.isDisposed = false;
        }
        private static void m_ActionOnRelease(CRequest req){
            req.Dispose();
        }
        // 获取及释放接口
        public static CRequest Get(){ return objectPool.Get(); }
        public static void Release(CRequest toRelease){ objectPool.Release(toRelease); }
        public static CRequest Create(string relativeUrl, string assetName, Type assetType, System.Action<CRequest> onComp, System.Action<CRequest> onEnd,
            object head, bool async){
            var req = CRequest.Get();
            req.relativeUrl = relativeUrl;
            req.assetName = assetName;
            req.assetType = assetType;

            req.OnComplete = onComp;
            req.OnEnd = onEnd;
            req.head = head;
            req.async = async;
            return req;
        }
        public void ReleaseToPool(){
            if(pool)
                Release(this);
        }

        [SLua.DoNotToLua]
        public System.DateTime beginQueueTime;
        [SLua.DoNotToLua]
        public System.DateTime beginLoadTime;

        public int priority = 0;//优先级 值越大优先级越高
        public string error { get; internal set; }

        private Type _assetType;
        public Type assetType{
            get{
                if(_assetType == null)
                    _assetType = CacheManager.Typeof_Object;
                return _assetType;
            }
            set{ _assetType = value; }
        }

        private string _assetName = string.Empty;
        public string assetName{
            get{
                if (string.IsNullOrEmpty (_assetName)) 
                    _assetName = CUtils.GetAssetName (relativeUrl); //Get// key;
                return _assetName;
            }
            set { _assetName = value; }
        }

        public string assetBundleName { get { return key; } }
        
        internal LoadOperation assetOperation;//协同加载ab
        internal GroupQueue<CRequest> group;


        //定义回调
        public System.Action<CRequest> OnEnd;
        public System.Action<CRequest> OnComplete;
        public void DispatchComplete(){
            // Profiler.BeginSample(string.Format("CRequest({0},{1}).DispatchComplete()", this.assetName, this.key));
            if(OnComplete != null)
                OnComplete(this);
            // Profiler.EndSample();
        }
        public void DispatchEnd(){
            if(OnEnd != null)
                OnEnd(this);
        }

        internal bool pool = false;
        internal bool isDisposed = false;
        internal bool needUriGroup = false;
        internal int[] dependencies;
        public int index = 0;//加载uri索引
        public bool isAdditive = false;//场景加载追加模式
        public bool async = false;//是否异步加载

        // 构造函数
        public CRequest(){}


        // 定义继承函数
        public virtual void Dispose(){

        }

        ////////////// 数据结构体
        // 设置加载的数据
        public object head;
        // 加载的头信息
        public Dictionary<string, string> headers;
        // http响应头部信息
        public Dictionary<string, string> responseHeaders;
        // 加载的数据
        public object data;
        // the user data
        public object userData;

        // 私有变量
        private Uri _uri;
        private string _relativeUrl;
        private string _key,_udKey, _udAssetKey;
        private int _KeyHashCode = 0;
        private string _url;
        private UriGroup _uriGroup;

        // 公有变量
        // Get the relative URL
        public string relativeUrl {
            set {
                _uri = null;
                _url = null;
                _udKey = null;
                _key = null;
                _KeyHashCode = 0;
                _relativeUrl = value;
                needUriGroup = CheckNeedUriGroup(value);
            }
            get { return _relativeUrl; }
        }

        public Uri uri {
            get { 
                if( _uri == null )
                    _uri = new Uri(url);
                return _uri;
            }
        }
        // 缓存用的关键字 如果没有设定 则为默认key生成规则
        public string key {
            get {
                if(string.IsNullOrEmpty(_key))
                    key = string.Empty;
                return _key;
            }
            internal set {
                if(value == null)
                    _key = null;
                else
                    _key = CUtils.GetAssetBundleName(relativeUrl);
            }
        }
        // the url unique key
        public string udKey {
            get {
                if(string.IsNullOrEmpty(_udKey))
                    udKey = string.Empty;
                return _udKey;
            }
            set {
                if(value == null)
                    _udKey = null;
                else
                    _udKey = CUtils.GetUDKey(url);
            }
        }
        // the url and assets unique key
        public string udAssetKey {
            get {
                if(string.IsNullOrEmpty(_udAssetKey))
                    udAssetKey = string.Empty;
                return _udAssetKey;
            }
            set {
                if(value == null)
                    _udAssetKey = null;
                else
                    _udAssetKey = string.Format("{0}+{1}", key, assetName);
            }
        }
        // assetBundle key的hashcode 用于计算缓存的资源key
        public int keyHashCode {
            get {
                if(_KeyHashCode == 0)
                    keyHashCode = 1;
                return _KeyHashCode;
            }
            internal set {
                if( value == 0 )
                    _KeyHashCode = value;
                else
                    _KeyHashCode = LuaHelper.StringToHash(key);
            }
        }
        // 请求地址 网址，绝对路径
        public string url {
            get{
                if(!string.IsNullOrEmpty(overrideUrl))
                    return overrideUrl;
                if(string.IsNullOrEmpty(_url))
                    url = string.Empty;
                return _url;
            }
            internal set{
                if(value == null){
                    _url = null;
                }else{
                    if(uris == null){
                        _url = relativeUrl;
                    }else{
                        _url = GetURL(this);
                    }
                }
                ClearOverride();
            }
        }
        // uri 组策略
        public UriGroup uris {
            get {
                if(_uriGroup == null && needUriGroup)
                    _uriGroup = UriGroup.uriList;
                return _uriGroup;
            }
            set {
                _uriGroup = value;
            }
        }

        // 其他数据变量接口
        public bool isShared{ get; internal set; }
        public string overrideUrl { get; internal set; }
        public string overrideHost { get; internal set; }
        
        // 其他函数接口
        // 获取当前 URL
        private static string GetURL(CRequest req){
            string url = string.Empty;
            var uris = req.uris;
            int index = req.index;
            if(uris != null && uris.count > index && index >= 0)
                url = CUtils.PathCombine(uris[index], req.relativeUrl);
            return url;
        }
        // 获取key URL
        public static string GetUDKeyURL(CRequest req){
            string url = string.Empty;
            var uris = req.uris;
            int index = 0;
            if(uris != null && uris.count > index && index >= 0)
                url = CUtils.PathCombine(uris[index], req.relativeUrl);
            else
                url = req.relativeUrl;
            return url;
        }
        // check need set uri group
        public static bool CheckNeedUriGroup(string url){
            if(string.IsNullOrEmpty(url))
                return false;

            if(url.StartsWith("http")
                || url.IndexOf("://") != -1
                || url.StartsWith(CUtils.realPersistentDataPath)
                || url.StartsWith(CUtils.realStreamingAssetsPath)
                )
                return false;
            else
                return true;
        }

        private void ClearOverride(){
            _uri = null;
            overrideUrl = null;
            overrideHost = null;
        }
    }
	// 定义底层虚拟接口
	public interface IReleaseToPool
    {
        void ReleaseToPool();
    }


    //Object池
	public class ObjectPool<T> where T : new()
    {
    	private readonly Stack<T> m_Stack = new Stack<T>();// 使用栈 管理
        private readonly System.Action<T> m_ActionOnGet;// 获取回调动作
        private readonly System.Action<T> m_ActionOnRelease;// 释放回调动作

        public int countAll { get; private set; }
        public int countActive { get{ return countAll - countInActive; } }
        public int countInActive { get{ return m_Stack.Count; } }

        // 构造函数
        public ObjectPool(Action<T> actionOnGet, Action<T> actionOnRelease)
        {
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }

        //	获取函数
        public T Get()
        {
        	T element;//创建对象指针
            if (m_Stack.Count == 0)
            {
                element = new T();//实例化
                countAll++;
            }
            else
                element = m_Stack.Pop();

            if (m_ActionOnGet != null)
                m_ActionOnGet(element);
            return element;
        }

        //	释放函数
        public void Release(T element)
        {
        	// 防止释放函数被多次调用时，重复释放同一对象
        	if (m_Stack.Count > 0 && ReferenceEquals(m_Stack.Peek(), element))
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
            if (m_ActionOnRelease != null)
                m_ActionOnRelease(element);
            m_Stack.Push(element);
        }
    }
}