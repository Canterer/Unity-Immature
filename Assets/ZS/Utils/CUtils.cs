/*
name:
desc:Object 对象池
author:ZS
*/

using System;
using UnityEngine;
using System.IO;
using ZS.Cryptograph;

namespace ZS.Utils
{
	[SLua.CustomLuaClass]
	public static class CUtils
	{
		public const bool isRelease = false;
		public const bool printLog = true;

		#if UNITY_IOS
		public const string platform = "ios";
		#elif UNITY_ANDROID
		public const string platform = "android";
		#elif UNITY_FACEBOOK || USE_GAEMROOM
		public const string platform = "gameroom";
		#elif UNITY_METRO
		public const string platform = "metroplayer";
		#elif UNITY_OSX || UNITY_STANDALONE_OSX
		public const string platform = "osx";//standaloneosxintel
		#else
		public const string platform = "win";//standalonewindows
		#endif

		private static string _platformFloder;
		public static string platformFloder
		{
			get {
				if(string.IsNullOrEmpty(_platformFloder))
					_platformFloder = platform;//md5加密 //CryptographHelper.Md5String(platform);
				return _platformFloder;
			}
		}

		private static string _realPersistentDataPath;
		public static string realPersistentDataPath {
			get {
				if(string.IsNullOrEmpty(_realPersistentDataPath))
					_realPersistentDataPath = PathCombine(Application.persistentDataPath, platformFloder);
				return _realPersistentDataPath;
			}
			set { _realPersistentDataPath = null; }
		}
		public static string GetRealPersistentDataPath(){ return realPersistentDataPath; }


		private static string _realStreamingAssetsPath;
		public static string realStreamingAssetsPath {
			get {
				if(string.IsNullOrEmpty(_realStreamingAssetsPath))
					_realStreamingAssetsPath = PathCombine(Application.streamingAssetsPath, platformFloder);
				return _realStreamingAssetsPath;
			}
			set { _realStreamingAssetsPath = null; }
		}
		public static string GetRealStreamingAssetsPath(){ return realStreamingAssetsPath; }


		public static bool currPersistentExist = false;

		private static System.Text.StringBuilder _textSB = new System.Text.StringBuilder(2048);
		private static System.DateTime _last_time = System.DateTime.Now;
		private static System.DateTime _begin_time = System.DateTime.Now;
		public static double DebugCastTime(string tips)
		{
			var ds = System.DateTime.Now - _last_time;
			var all_ds = (System.DateTime.Now - _begin_time).TotalMilliseconds;
			double cast = ds.TotalSeconds;
			_last_time = System.DateTime.Now;
			if(!string.IsNullOrEmpty(tips))
				Debug.LogFormat("Cast Time \"{0}\" Cast({1}s) runtime({2}ms), frame = {3}", tips, cast, all_ds, Time.frameCount);

			return all_ds;
		}

		internal static void AnalysePathName(string pathName, out int fileIndex, out int fileLen, out int dotIndex, out int suffixLen)
		{
			int len = pathName.Length;
			fileIndex = 0;// the last / or \ position
			int firstDotIndex = len;
			int lastDotIndex = len;
			int questIndex = len;// the ? position

			int i = len - 1;
			char cha;
			while(i >= 0)
			{
				cha = pathName[i];
				if( cha == '/' || cha == '\\')
				{
					fileIndex = i + 1;
					break;
				}
				if(cha == '?')
					questIndex = i;
				if(cha == '.')
				{
					firstDotIndex = i;
					if(lastDotIndex == len)
						lastDotIndex = i;
				}
				--i;
			}
			if( firstDotIndex > questIndex )
				firstDotIndex = questIndex;
			dotIndex = lastDotIndex;
			suffixLen = questIndex - lastDotIndex;
			fileLen = firstDotIndex - fileIndex;
		}

		// 从URL中获取assetName
		public static string GetAssetName(string url)
		{
			if(string.IsNullOrEmpty(url))
				return string.Empty;

			string fname = string.Empty;
			int lastFileIndex, lastDotIndex, fileLen, suffixLen;
			AnalysePathName(url, out lastFileIndex, out fileLen, out lastDotIndex, out suffixLen);
			if(fileLen == 0)
				return string.Empty;

			fname = url.Substring(lastFileIndex, fileLen);
			return fname;
		}

		public static string GetAssetBundleName(string url)
		{
			if(string.IsNullOrEmpty(url))
				return string.Empty;

			int idxEnd = url.IndexOf('?');
			int idxBegin = url.IndexOf(platformFloder);
			if(idxBegin == -1)
				idxBegin = 0;
			else
				idxBegin = idxBegin + platformFloder.Length + 1;

			if(idxBegin >= url.Length)
				idxBegin = 0;
			if(idxEnd == -1)
				idxEnd = url.Length;

			int len = idxEnd - idxBegin;
			string re = string.Empty;
			if(len == 0) return re;

			re = url.Substring(idxBegin,len);
			return re; 
		}

		public static string GetBaseName(string assetbundleVariants)
		{
			if(string.IsNullOrEmpty(assetbundleVariants))
				return string.Empty;
			string baseName = assetbundleVariants;
			int lastFileIndex,lastDotIndex,fileLen,suffixLen;
			AnalysePathName(assetbundleVariants, out lastFileIndex, out fileLen, out lastDotIndex, out suffixLen);

			if(suffixLen > 1)
			{
				suffixLen = suffixLen - 1;
				string suffix = assetbundleVariants.Substring(lastDotIndex + 1, suffixLen);

				if(suffix.Equals(Common.ASSETBUNDLE_SUFFIX))
					baseName = assetbundleVariants.Substring(0, lastFileIndex + fileLen + 4);
				else
					baseName = assetbundleVariants.Substring(0, lastFileIndex + lastDotIndex - lastFileIndex);
			}
			return baseName;
		}

		public static string CheckWWWUrl(string url)
		{
			if(url.IndexOf("://") == -1)
				url = string.Format("file:///{0}", url);
			return url;
		}

		[System.Obsolete("please use UriGroup")]
		public static string GetFileFullPath(string absolutePath)
		{
			string path = "";
			path = Application.persistentDataPath + "/" + absolutePath;
			currPersistentExist = File.Exists(path);
			if(!currPersistentExist)
				path = Application.streamingAssetsPath + "/" + absolutePath;

			if(path.IndexOf("://") == -1)
				path = "file://" + path;

			return path;
		}

		// path combine
		public static string PathCombine(string path1, string path2)
		{
			if(path2.Length == 0)
				return path1;
			if(path1.Length == 0)
				return path2;

			string path = string.Empty;
			char c2 = path2[0];
			char c = path1[path1.Length - 1];
			if(c2 == '\\' || c2 == '/' || c2 == ':')
				path2 = path2.Substring(1);

			if(c != '\\' && c != '/' && c != ':')
				path = path1 + "/" + path2;
			else
				path = path1 + path2;

			return path;
		}

		// Get Android AssetBundle.LoadFrom Path
		public static string GetAndroidABLoadPath(string path)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
				path = path.Replace(Common.JAR_FILE,"").Replace("!/assets","!assets");
			#endif

			return path;
		}

		// Get the UD key
		public static string GetUDKey(string url)
		{
			string udKey = url;//Md5s base64 string
			return udKey;
		}

		//get MD5 or normal filename
		public static string GetRightFileName(string fileName)
		{
			if(string.IsNullOrEmpty(fileName))
				return string.Empty;

			int lastFileIndex, lastDotIndex, fileLen, suffixLen;
			AnalysePathName(fileName, out lastFileIndex, out fileLen, out lastDotIndex, out suffixLen);

			string fname = fileName.Substring(lastFileIndex, fileLen);
			string md5 = string.Empty;
			md5 = CryptographHelper.Md5String(fname);
			_textSB.Length = 0;
			_textSB.Append(fileName);
			if(fileLen > 0)
				_textSB.Replace(fname, md5, lastFileIndex, fileLen);
			fname = _textSB.ToString();
			return fname;
		}
	}
}