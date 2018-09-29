/*
name:FileHelper
desc:
author:ZS
*/

using System;
using UnityEngine;
using System.IO;

namespace ZS.Utils
{
	// 文件读取等操作
	[SLua.CustomLuaClass]
	public class FileHelper
	{
		// saves the file
		public static void SavePersistentFile(Array context, string fileName)
		{
			string path = CUtils.PathCombine(CUtils.GetRealPersistentDataPath(), fileName);
			FileInfo fInfo = new FileInfo(path);
			if( !fInfo.Directory.Exists)
				fInfo.Directory.Create();

			using (StreamWriter sw = new StreamWriter(path, false))
			{
				sw.BaseStream.Write((byte[])context, 0, context.Length);
				sw.Flush();
			}
			Debug.LogFormat("save file path={0}, len={1}", path, context.Length);
		}
		public static void SavePersistentFile(string context, string fileName)
		{
			byte[] cont = LuaHelper.GetBytes(context);
			SavePersistentFile(cont, fileName);
		}

		// reads the persistent file
		public static byte[] ReadPersistentFile(string fileName)
		{
			string path = CUtils.PathCombine(CUtils.GetRealPersistentDataPath(), fileName);
			if(File.Exists(path))
				return File.ReadAllBytes(path);
			else
				return null;
		}

		// changes the name of the persistent file.
		public static bool ChangePersistentFileName(string oldName, string newName)
		{
			string path = CUtils.GetRealPersistentDataPath();
			string oldPath = CUtils.PathCombine(path, oldName);
			string newPath = CUtils.PathCombine(path, newName);
			if(File.Exists(oldPath))
			{
				FileInfo newFile = new FileInfo(newPath);//如果新的存在需要删除
				if(newFile.Exists)
					newFile.Delete();
				FileInfo fInfo = new FileInfo(oldPath);
				fInfo.MoveTo(newPath);
				return true;
			}
			return false;
		}

		// deletes the persistent file.
		public static void DeletePersistentFile(string fileName)
		{
			string path = CUtils.PathCombine( CUtils.GetRealPersistentDataPath(), fileName);
			if(File.Exists(path))
				File.Delete(path);
		}

		// delete the persistent Directory
		public static void DeletePersistentDirectoryFiles(string relative = null)
		{
			string path = CUtils.GetRealPersistentDataPath();
			if(!string.IsNullOrEmpty(relative))
				path = CUtils.PathCombine(path, relative);
			DirectoryInfo dInfo = new DirectoryInfo(path);
			if(dInfo.Exists)
			{
				var allFiles = dInfo.GetFiles("*", SearchOption.AllDirectories);
				FileInfo fInfo;
				for(int i = 0; i < allFiles.Length; ++i)
				{
					fInfo = allFiles[i];
					fInfo.Delete();
				}
			}
		}

		// the persistents file is Exists
		public static bool PersistentFileExists(string abPath)
		{
			string path = CUtils.PathCombine(CUtils.GetRealPersistentDataPath(), abPath);
			return File.Exists(path);
		}
	}
}