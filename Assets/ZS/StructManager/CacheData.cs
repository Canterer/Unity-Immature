/*
name:CacheData
desc: 缓存管理
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZS.Loader
{
	// 缓存资源
	public class CacheData : IDisposable
    {
    	public CacheData(){

    	}

    	// assetBundle Name
    	public string assetBundleKey { get; internal set; }
    	// hashCode
    	public int assetHashCode { get; internal set; }
    	// assetBundle 对象
    	public AssetBundle assetBundle;
    	// 引用计数
    	public int count;
        // 所有依赖项目
        public int[] dependencies { get; internal set; }
        public Dictionary<string, object> assets;
        public bool isDone { get; internal set; }
        public bool isError { get; internal set; }
        public bool isUnloaded { get; internal set; }
        public bool canUse { get {return !isUnloaded && assetBundle != null;} }

    	public void Dispose()
    	{
    		
    	}

        static ObjectPool<CacheData> pool = new ObjectPool<CacheData>(null, m_ActionOnRelease);
        private static void m_ActionOnGet(CacheData cd){ }//cd.Dispose();
        private static void m_ActionOnRelease(CacheData cd){ cd.Dispose(); }
        public static CacheData Get(){ return pool.Get(); }
        public static void Release(CacheData toRelease){ pool.Release(toRelease); }
    }
}