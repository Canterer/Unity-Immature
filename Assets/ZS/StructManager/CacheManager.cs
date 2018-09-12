/*
name:CacheManager
desc: 缓存管理
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;


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

		}
		public static void ClearDelay(int assetHashCode)
        {

        }

		// unload assetbundle false
		public static bool UnloadCacheFalse(string assetBundleName)
		{

		}

		// unload assetbundle false
		public static bool UnloadCacheFalse(int assetHashCode)
		{

		}

		// unload assetbundle and dependencies false
		public static bool UnloadDependenciesCacheFalse(int assetHashCode)
		{

		}

		// 获取可以使用的缓存
		[SLua.DoNotToLua]
		public static CacheData GetCache(string assetBundleName)
		{

		}

		internal static CacheData GetCache(int assetHashCode)
		{

		}		

		internal static CacheData TryGetCache(int assetHashCode)
		{

		}


		// 判断所有依赖项目是否加载完成
		internal static bool CheckDependenciesComplete(CRequest req)
		{
			return true;
		}
	}
}