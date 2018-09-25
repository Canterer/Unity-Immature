/*
name:LuaHelper
desc:
author:ZS
*/

using System;
using UnityEngine;
using System.IO;

namespace ZS.Utils
{
	[SLua.CustomLuaClass]
	public static class LuaHelper
	{

		// str 转换成 hashCode
		public static int StringToHash(string str)
		{
			int hash = Animator.StringToHash(str);
			return hash;
		}

		public static byte[] GetBytes(string utf8Str)
		{
			return System.Text.Encoding.UTF8.GetBytes(utf8Str);
		}	
	}
}