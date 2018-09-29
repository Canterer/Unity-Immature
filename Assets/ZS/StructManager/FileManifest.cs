/*
name:FileManifest
desc:文件清单
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace ZS.Loader
{
	[SLua.CustomLuaClass]
	public static class FileManifestOptions
	{
		// 本地
		public const int StreamingAssetsPriority = 0;
		// 首包下载的资源
		public const int FirstLoadPriority = 256;//2^8
		// 自动后台下载级别
		public const int AutoHotPriority = 262144;//2^18
		// 用户使用频率优先级别
		public const int UserPriority = 524288;//2^19
		// 手动下载目录
		public const int ManualPriority = 1048576;//2^20
	}

	public class VariantsInfo
	{
		public string variants;
		public string baseName;
		public string fullName = string.Empty;

		public VariantsInfo(string bundleVariant)
		{
			int index = bundleVariant.LastIndexOf('.');
			this.baseName = bundleVariant.Substring(0, index);
			this.variants = bundleVariant.Substring(index+1, bundleVariant.Length - index - 1);
			this.fullName = bundleVariant;
		}

		public override int GetHashCode(){ return fullName.GetHashCode(); }

		public override bool Equals(object obj)
		{
			if(obj == null)
				return false;
			else if(obj is string)
				return fullName == obj.ToString();
			else if(obj is VariantsInfo)
				return this.fullName == ((VariantsInfo)obj).fullName;
			else
				return false;
		}

		public static bool operator ==(VariantsInfo left, VariantsInfo right)
		{
			if(left == null || right == null)
				return false;
			return left.fullName == right.fullName;
		}

		public static bool operator !=(VariantsInfo left, VariantsInfo right)
		{
			if(left == null || right == null)
				return true;
			return left.fullName != right.fullName;
		}
	}

	[SLua.CustomLuaClass]
	public class FileManifest : ScriptableObject, ISerializationCallbackReceiver
	{
		private static readonly string[] EmptyStringArray = new string[0];
		private static readonly int[] EmptyIntArray = new int[0];
		internal Dictionary<string, ABInfo> abInfoDict = new Dictionary<string, ABInfo>(System.StringComparer.OrdinalIgnoreCase);

		[SLua.DoNotToLuaAttribute]
		public List<ABInfo> allAbInfo = new List<ABInfo>();

		[SLua.DoNotToLuaAttribute]
		public string[] allAssetBundlesWithVariant;

		internal Dictionary<string, List<VariantsInfo>> variantDict = new Dictionary<string, List<VariantsInfo>>(System.StringComparer.OrdinalIgnoreCase);
		public int appNumVersion;
		public uint crc32 = 0;
		public bool hasFirstLoad = false;

		public int Count{ get{ return allAbInfo.Count; }}

		[SLua.DoNotToLua]
		public void OnBeforeSerialize(){}

		[SLua.DoNotToLua]
		public void OnAfterDeserialize()
		{
			if(abInfoDict == null)
				abInfoDict = new Dictionary<string, ABInfo>(allAbInfo.Count + 32);

			ABInfo abInfo;
			for(int i = 0; i < allAbInfo.Count; ++i)
			{
				abInfo = allAbInfo[i];
				abInfoDict[abInfo.abName] = abInfo;
			}

			for(int i = 0; i < allAssetBundlesWithVariant.Length; ++i)
				AddVariant(allAssetBundlesWithVariant[i]);
		}

		public string[] GetDirectDependencies(string assetBundleName)
		{
			string[] re = EmptyStringArray;
			var abInfo = GetABInfo(assetBundleName);
			if(abInfo != null)
				re = abInfo.dependencies;
			return re;
		}

		public List<VariantsInfo> GetVariants(string baseName)
		{
			List<VariantsInfo> re = null;
			variantDict.TryGetValue(baseName, out re);
			return re;
		}

		[SLua.DoNotToLuaAttribute]
		public ABInfo GetABInfo(string abName)
		{
			ABInfo abInfo = null;
			abInfoDict.TryGetValue(abName, out abInfo);
			return abInfo;
		}

		[SLua.DoNotToLuaAttribute]
		public void Add(ABInfo ab)
		{
			var abInfo = GetABInfo(ab.abName);
			int i = -1;
			if(abInfo != null)
			{
				i = allAbInfo.IndexOf(abInfo);
				if(i >= 0)
					allAbInfo.RemoveAt(i);
			}
			abInfoDict[ab.abName] = ab;
			if(i >= 0)
				allAbInfo.Insert(i, ab);
			else
				allAbInfo.Add(ab);
		}

		public void AddVariant(string assetBundlesWithVariant)
		{
			var varInfo = new VariantsInfo(assetBundlesWithVariant);
			if(variantDict.ContainsKey(varInfo.baseName))
			{
				var variantList = variantDict[varInfo.baseName];
				if(!variantList.Contains(varInfo))
					variantList.Add(varInfo);
			}else
			{
				List<VariantsInfo> variantList = new List<VariantsInfo>();
				variantList.Add(varInfo);
				variantDict[varInfo.baseName] = variantList;
			}
		}


	}
}