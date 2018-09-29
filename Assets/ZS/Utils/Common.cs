/*
name:Common
desc:一些公用常量
author:ZS
*/

using System;
using UnityEngine;
using System.IO;

namespace ZS.Utils
{
	[SLua.CustomLuaClass]
	public class Common
	{
		public const string ASSETBUNDLE_SUFFIX = "u3d";
		public const string HTTP_STRING = "http://";
		public const string HTTPS_STRING = "https://";
	}
}