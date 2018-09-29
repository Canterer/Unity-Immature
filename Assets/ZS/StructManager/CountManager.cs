/*
name:CountManager
desc: 计数器管理
author:ZS
*/
using ZS.Utils;

namespace ZS.Loader
{
	// 计数器
	[SLua.CustomLuaClass]
	public static class CountManager
	{
		public static int Subtract(int hashCode)
		{
			CacheData cached = CacheManager.TryGetCache(hashCode);
			if(cached != null && cached.count >= 1)
			{
				cached.count--;
				if(cached.count == 0)//所有引用被清理
				{
					int[] allDep = cached.dependencies;
					CacheManager.ClearDelay(hashCode);
					int tempDenHash = 0;
					if( allDep != null)
					{
						for(int i = 0; i < allDep.Length; ++i)
						{
							tempDenHash = allDep[i];
							if(tempDenHash != hashCode)
								Subtract(tempDenHash);
						}
					}
				}
				return cached.count;
			}else if(cached != null && cached.count <= 0)
				UnityEngine.Debug.LogWarningFormat("CountManager.Subtract (assetBundle={0},hashcode={1},count={2}) frameCount{3}", cached.assetBundleKey, hashCode, cached.count, UnityEngine.Time.frameCount);

			return -1;
		}

		public static int Subtract(string key)
		{
			int hashCode = LuaHelper.StringToHash(key);
			return Subtract(hashCode);
		}

		// 目标引用加一
		public static int Add(int hashCode)
		{
			CacheData cached = CacheManager.TryGetCache(hashCode);
			if(cached != null)
			{
				cached.count++;
				return cached.count;
			}
			return -1;
		}

		public static int Add(string key)
		{
			int hashCode = LuaHelper.StringToHash(key);
			return Add(hashCode);
		}

		internal static int WillAdd(int hashCode)
		{
			CacheData cached = null;
			CacheManager.CreateOrGetCache(hashCode, out cached);
			cached.count++;
			return cached.count;
		}
	}
}