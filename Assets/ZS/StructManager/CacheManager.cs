/*
name:CacheManager
desc: 缓存管理
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using ZS.Utils;

// #if UNITY_5_0 || UNITY_5_1 || UNITY_5_2

// #else
using UnityEngine.SceneManagement;
// #endif


namespace ZS.Loader
{
	[SLua.CustomLuaClass]
	public static class CacheManager
	{
		// 下载完成的资源
		[SLua.DoNotToLua]
		public static Dictionary<int, CacheData> caches = new Dictionary<int, CacheData>();

		// check type
		public static readonly Type Typeof_String = typeof(System.String);
		public static readonly Type Typeof_Bytes = typeof(System.Byte[]);
		public static readonly Type Typeof_AssetBundle = typeof(AssetBundle);
		public static readonly Type Typeof_ABScene = typeof(AssetBundleScene);
		public static readonly Type Typeof_ABAllAssets = typeof(UnityEngine.Object[]);
		public static readonly Type Typeof_AudioClip = typeof(AudioClip);
		public static readonly Type Typeof_Texture2D = typeof(Texture2D);
		public static readonly Type Typeof_Object = typeof(UnityEngine.Object);

		// 清理缓存释放资源
		public static void ClearDelay(string assetBundleName)
		{
			assetBundleName = ManifestManager.RemapVariantName(assetBundleName);
			int hash = LuaHelper.StringToHash(assetBundleName);
			ClearDelay(hash);
		}
		public static void ClearDelay(int assetHashCode)
        {
        	CacheData cache = TryGetCache(assetHashCode);
        	if(cache != null)
        	{
        		ABDelayUnloadManager.Add(assetHashCode);
        	}else
        		Debug.LogWarningFormat("ClearCache {0} fail", assetHashCode);
        }

		// unload assetbundle false
		public static bool UnloadCacheFalse(string assetBundleName)
		{
			assetBundleName = ManifestManager.RemapVariantName(assetBundleName);
			int hash = LuaHelper.StringToHash(assetBundleName);
			if(!UnloadCacheFalse(hash))
				return false;

			return true;
		}

		// unload assetbundle false
		public static bool UnloadCacheFalse(int assetHashCode)
		{
			CacheData cache = TryGetCache(assetHashCode);
			if(cache != null)
			{
				cache.Unload();
				return true;
			}else
				return false;
		}

		// unload assetbundle and dependencies false
		public static bool UnloadDependenciesCacheFalse(string assetBundleName)
		{
			assetBundleName = ManifestManager.RemapVariantName(assetBundleName);
			int hash = LuaHelper.StringToHash(assetBundleName);
			if(!UnloadDependenciesCacheFalse(hash))
				return false;

			return true;
		}

		public static bool UnloadDependenciesCacheFalse(int assetHashCode)
		{
			CacheData cache = TryGetCache(assetHashCode);
			if(cache != null)
			{
				int[] allDep = cache.dependencies;
				CacheData cacheTemp = null;
				cache.Unload();
				if( allDep != null)
				{
					for(int i = 0; i < allDep.Length; ++i)
					{
						cacheTemp = TryGetCache(allDep[i]);
						if(cacheTemp != null)
							cacheTemp.Unload();
					}
				}
				return true;
			}else
				return false;
		}

		// 获取可以使用的缓存
		[SLua.DoNotToLua]
		public static CacheData GetCache(string assetBundleName)
		{
			int hash = LuaHelper.StringToHash(assetBundleName);
			return GetCache(hash);
		}

		internal static CacheData GetCache(int assetHashCode)
		{
			CacheData cache = null;
			caches.TryGetValue(assetHashCode, out cache);
			if(cache != null && !cache.isError && cache.canUse)
				return cache;
			else
				return null;
		}		

		internal static CacheData TryGetCache(int assetHashCode)
		{
			CacheData cache = null;
			caches.TryGetValue(assetHashCode, out cache);
			return cache;
		}

		// 获取或创建一个缓存
		internal static bool CreateOrGetCache(int keyHashCode, out CacheData cache)
		{
			if(caches.TryGetValue(keyHashCode, out cache))
			{
			}else
			{
				cache = CacheData.Get();
				cache.assetHashCode = keyHashCode;
				caches.Add(keyHashCode, cache);
			}
			return cache.canUse;
		}
		internal static bool CreateOrGetCache(string key, out CacheData cache)
		{
			int keyHashCode = LuaHelper.StringToHash(key);
			if(caches.TryGetValue(keyHashCode, out cache))
			{
			}else
			{
				cache = CacheData.Get();
				cache.SetCacheData(null, key, keyHashCode);
				caches.Add(cache.assetHashCode, cache);
			}
			return cache.canUse;
		}

		// 判断所有依赖项目是否加载完成
		internal static bool CheckDependenciesComplete(CRequest req)
		{
			if(req.dependencies == null || req.dependencies.Length == 0) return true;

			int[] denps = req.dependencies;
			CacheData cache = null;
			int hash = 0;
			for(int i = 0; i < denps.Length; ++i)
			{
				hash = denps[i];
				if(hash == 0)
					continue;
				else if(caches.TryGetValue(hash, out cache))
				{
					if(!cache.isDone)
						return false;
				}else
					return false;
			} 
			return true;
		}

		// 立即卸载资源
		public static bool Unload(string key)
		{
			int keyHash = LuaHelper.StringToHash(key);
			return Unload(keyHash);
		}
		
		// 立即卸载资源
		public static bool Unload(int hashCode)
		{
			CacheData cache = TryGetCache(hashCode);
			if( cache != null && cache.count == 0 )
			{
				caches.Remove(cache.assetHashCode);//删除
				CacheData.Release(cache);
				return true;
			}else if(cache != null)
				Debug.LogFormat("<color=#cccccc> can't unload cache assetBundle={0},keyhashcode({1},count={2})   </color>", cache.assetBundleKey, cache.assetHashCode, cache.count);

			return false;
		}

		// 是否下载过资源
		public static bool Contains(int keyHash)
		{
			CacheData cache = GetCache(keyHash);
			if(cache != null)
				return true;
			else
				return false;
		}
		public static bool Contains(string key)
		{
			int keyHash = LuaHelper.StringToHash(key);
			return Contains(keyHash);
		}

		// 清理所有资源
		public static void ClearAll()
		{
			var items = caches.GetEnumerator();
			while(items.MoveNext())
				items.Current.Value.Dispose();

			caches.Clear();
		}
	}
}