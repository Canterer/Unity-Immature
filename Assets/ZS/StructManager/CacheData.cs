/*
name:CacheData
desc: 缓存管理
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using ZS.Utils;

namespace ZS.Loader
{
	// 缓存资源
	public class CacheData : IDisposable
    {
    	public CacheData(){
            isUnloaded = false;
            assets = new Dictionary<string, object>();
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
    	   if(assetBundle)
                assetBundle.Unload(true);

            assets.Clear();
            assetBundle = null;
            dependencies = null;
            isUnloaded = false;
            //isAssetLoaded = false;
            isError = false;
            isDone = false;

            assetBundleKey = string.Empty;
            assetHashCode = 0;
            count = 0;
    	}

        public void Unload()
        {
            if(assetBundle)
                assetBundle.Unload(false);
            isUnloaded = true;
        }

        public void SetAsset(string name, object asset){ assets[name] = asset; }
        public object GetAsset(string name)
        {
            object asset = null;
            assets.TryGetValue(name, out asset);
            return asset;
        }

        public void SetCacheData(AssetBundle assetBundle, string assetBundleName, int bundleNameHashCode=0)
        {
            this.assetBundle = assetBundle;
            this.assetBundleKey = assetBundleName;
            this.isUnloaded = false;
            if( bundleNameHashCode == 0 && !string.IsNullOrEmpty(assetBundleName))
            {
                int hash = LuaHelper.StringToHash(assetBundleKey);
                this.assetHashCode = hash;
            }else
                this.assetHashCode = bundleNameHashCode;
        }

        static ObjectPool<CacheData> pool = new ObjectPool<CacheData>(null, m_ActionOnRelease);
        private static void m_ActionOnGet(CacheData cd){ }//cd.Dispose();
        private static void m_ActionOnRelease(CacheData cd){ cd.Dispose(); }
        public static CacheData Get(){ return pool.Get(); }
        public static void Release(CacheData toRelease){ pool.Release(toRelease); }
    }
}