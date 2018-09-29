/*
name:ManifestManager
desc:清单管理器
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using ZS.Utils;

namespace ZS.Loader
{
	[SLua.CustomLuaClass]
	public static class ManifestManager
	{
		public static FileManifest fileManifest;
		public static FileManifest updateFileManifest;
		public static string[] bundlesWithVariant;
		static string[] _activeVariants = {};
		public static string[] ActiveVariants
		{
			get { return _activeVariants; }
			set { _activeVariants = value; }
		}

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
				uint len = 0;
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
			if(bundlesVariants == null) return assetBundleName;

			int bestFit = int.MaxValue;
			string bestFitVariant = string.Empty;
			VariantsInfo variInfo;
			for(int i = 0; i < bundlesVariants.Count; ++i)
			{
				variInfo = bundlesVariants[i];

				int found = System.Array.IndexOf(_activeVariants, variInfo.variants);
				if(found == -1)
					found = int.MaxValue - 1;
				if(found < bestFit)
				{
					bestFit = found;
					bestFitVariant = variInfo.variants;
				}
			}

			if( !string.IsNullOrEmpty(bestFitVariant))
				return assetBundleName + "." + bestFitVariant;
			else
				return assetBundleName;
		}

		// remaps the asset bundle name to the best fitting asset bundle variant.
		internal static string RemapVariantName(string assetBundleName)
		{
			string baseName = assetBundleName;

			var bundlesVariants = ManifestManager.fileManifest.GetVariants(baseName);
			if(bundlesVariants == null) return assetBundleName;

			int bestFit = int.MaxValue;
			int bestFitIndex = -1;
			VariantsInfo variInfo;
			for(int i = 0; i < bundlesVariants.Count; ++i)
			{
				variInfo = bundlesVariants[i];
				int found = System.Array.IndexOf(_activeVariants, variInfo.variants);

				//if there is no active variant found. we still want to use the first
				if(found == -1)
					found = int.MaxValue -1;

				if(found < bestFit)
				{
					bestFit = found;
					bestFitIndex = i;
				}
			}

			if(bestFit != -1)
				return bundlesVariants[bestFitIndex].fullName;
			else
				return assetBundleName;

		}

		[SLua.DoNotToLuaAttribute]
		public static bool CheckReqCrc(CRequest req)
		{
			if(req.url.StartsWith(Common.HTTP_STRING))
				return true;
			var abName = req.assetBundleName;
			ABInfo abInfo = null;
			if(updateFileManifest != null
				&& (abInfo = updateFileManifest.GetABInfo(abName)) != null)//update file need crc check
				return CheckPersistentCrc(GetABInfo(abInfo.abName));
			else if( fileManifest != null && (abInfo = fileManifest.GetABInfo(abName)) != null
				&& abInfo.priority > FileManifestOptions.StreamingAssetsPriority) // auto update file need crc check 
				return CheckPersistentCrc(GetABInfo(abInfo.abName));

			return FileHelper.PersistentFileExists(abName);
		}
	}
}