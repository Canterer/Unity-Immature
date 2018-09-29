/*
name:CryptographHelper
desc:
author:ZS
*/

using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace ZS.Cryptograph
{
	[SLua.CustomLuaClass]
	public class CryptographHelper
	{	

		private static MD5 md5;
		private static MD5 Md5Instance
		{
			get{
				if(md5 == null)
					md5 = new MD5CryptoServiceProvider();
				return md5;
			}
		}
		// string md5加密
		public static string Md5String(string source)
		{
			return Md5Bytes(Encoding.UTF8.GetBytes(source));
		}

		// byte[] md5加密
		public static string Md5Bytes(byte[] inputs)
		{
			byte[] result = Md5Instance.ComputeHash(inputs);
			string md5Str = string.Empty;
			md5Str = System.BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
			return md5Str;
		}
		

	}
}