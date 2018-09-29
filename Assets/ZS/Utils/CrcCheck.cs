/*
name:
desc:CrcCheck 对象池
author:ZS
*/

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ZS.Loader;

namespace ZS.Utils
{
	[SLua.CustomLuaClass]
	public static class CrcCheck
	{
		// 根据组策略校验request.url的crc值
		public static bool CheckUriCrc(CRequest req)
		{
			bool check = ManifestManager.CheckReqCrc(req);//CheckLocalFileCrc(req.url, out crc);
			if(!check)
			{
				var re = UriGroup.CheckAndSetNextUriGroup(req);//CUtils.SetRequestUri(req, 1);
				return re;
			}
			return true;
		}

		//获取文件crc
		public static uint GetLocalFileCrc(string path, out uint l)
		{
			uint crc = 0;
			l = 0;
			try
			{
				FileInfo fileInfo = new FileInfo(path);
				if(fileInfo.Exists)
				{
					using (FileStream fileStream =fileInfo.OpenRead())
					{
						byte[] array;
						int num = 0;
						long length = fileStream.Length;
						int i = (int)length;
						if(length > 2147483647L)
							length = 2147483647L;

						l = (uint)i;
						array = new byte[i];
						while(i > 0)
						{
							int num2 = fileStream.Read(array, num, i);
							if(num2 == 0)
								break;
							num += num2;
							i -= num2;
						}
						crc = Crc32.Compute(array);
					}
				}
			}
			catch(System.Exception e)
			{
				Debug.LogWarning(e);
			}
			return crc;
		}
	}
}