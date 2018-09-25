/*
name:ABDelayUnloadManager
desc: 
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;

namespace ZS.Loader
{
	[SLua.CustomLuaClass]
	public static class ABDelayUnloadManager
	{
		// delay unload time
		public static float delaySecondTime = 0;//0.1f
		private static Dictionary<int, float> removeDic = new Dictionary<int, float>();
		private static List<int> deleteList = new List<int>();


		internal static void Add(int keyHashCode)
		{
			if(removeDic.ContainsKey(keyHashCode))
				removeDic.Remove(keyHashCode);

			removeDic.Add(keyHashCode, Time.unscaledTime + delaySecondTime);
		}

		internal static void AddDependenciesReferCount(CacheData cache)
		{
			if(cache != null)
			{
				int keyHash = 0;
				int[] allDep = cache.dependencies;

				if(allDep != null)
				{
					CacheData pCache = null;
					for(int i = 0; i < allDep.Length; ++i)
					{
						keyHash = allDep[i];
						pCache = CacheManager.TryGetCache(keyHash);
						if(pCache != null)
						{
							pCache.count++;
							// 如果在回收列表
							if(removeDic.ContainsKey(keyHash))
							{
								removeDic.Remove(keyHash);
								AddDependenciesReferCount(pCache);
							}
						}
					}
				}
			}
		}

		internal static bool CheckRemove(int keyHashCode)
		{
			if(removeDic.ContainsKey(keyHashCode))
			{
				removeDic.Remove(keyHashCode);
				CacheData cache = CacheManager.TryGetCache(keyHashCode);
				AddDependenciesReferCount(cache);
				return true;
			}
			else
				return false;
		}

		internal static void Update()
		{
			deleteList.Clear();

			var items = removeDic.GetEnumerator();
			while(items.MoveNext())
			{
				var kv = items.Current;
				if(Time.unscaledTime >= kv.Value)
				{
					deleteList.Add(kv.Key);
					CacheManager.Unload(kv.Key);
				}
			}

			for(int i = 0; i < deleteList.Count; ++i)
				removeDic.Remove(deleteList[i]);
		}
	}
}