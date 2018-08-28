/*
name:
desc:Object 对象池
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;

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

        [SLua.DoNotToLua]
        public System.DateTime beginQueueTime;
        [SLua.DoNotToLua]
        public System.DateTime beginLoadTime;

        public object data;
        public int priority = 0;//优先级
        public string error { get; internal set; }

        private Type _assetType;
        public Type assetType{
            get{
                if(_assetType == null)
                    _assetType = CacheManager.Typeof_Object;
                return _assetType;
            }
            set{ _assetType = value; }}

        private string _assetName;
        public string assetName{
            get{
                if (string.IsNullOrEmpty (_assetName)) 
                    _assetName = CUtils.GetAssetName (relativeUrl); //Get// key;
                return _assetName;
            }
            set { _assetName = value; }}
        
        internal LoadOperation assetOperation;
        internal GroupQueue<CRequest> group;


        //定义回调
        public System.Action<CRequest> OnEnd;
        public System.Action<CRequest> OnComplete;

        internal bool pool = false;
        internal bool isDisposed = false;
        internal int[] dependencies;


        public CRequest(){}

        // 定义继承函数
        public virtual void Dispose(){

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