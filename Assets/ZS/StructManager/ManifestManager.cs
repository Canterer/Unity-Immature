/*
name:ManifestManager
desc:清单管理器
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZS.Loader
{
	[SLua.CustomLuaClass]
	public static class ManifestManager
	{
		public static FileManifest fileManifest;
		public static FileManifest updateFileManifest;
		public static string[] bundlesWithVariant;

		[SLua.DoNotToLuaAttribute]
		public static ABInfo GetABInfo(string abName)
		{
			ABInfo abInfo = null;
			if(fileManifest != null)
				abInfo = fileManifest.GetABInfo(abName);

			return abInfo;
		}


		[SLua.DoNotToLuaAttribute]
		public static bool CheckPersistentCrc(ABInfo abInfo)
		{
			uint crc = 0;
			if(abInfo.state == ABInfoState.None)
			{
				unit len = 0;
				var url = CUtils.PathCombine(CUtils.GetRealPersistentDataPath(), abInfo.abName);
				crc = CrcCheck.GetLocalFileCrc(url, out len);
				if(crc == abInfo.crc32)
					abInfo.state = ABInfoState.Success;
				else
					abInfo.state = ABInfoState.Fail;
			}
			return abInfo.state == ABInfoState.Success;
		}


		// get the best fit variant name
		public static string GetVariantName(string assetBundleName)
		{
			string md5name = CUtils.GetRightFileName(assetBundleName);//CryptographHelper.Md5String(baseName);

			var bundlesVariants = ManifestManager.fileManifest.GetVariants(md5name);
		}
	}
}