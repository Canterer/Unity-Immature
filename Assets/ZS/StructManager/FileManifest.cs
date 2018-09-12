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


	}
}