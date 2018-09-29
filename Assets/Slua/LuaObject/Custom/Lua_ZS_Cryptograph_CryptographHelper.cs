using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Cryptograph_CryptographHelper : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ZS.Cryptograph.CryptographHelper o;
			o=new ZS.Cryptograph.CryptographHelper();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Md5String_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Cryptograph.CryptographHelper.Md5String(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Md5Bytes_s(IntPtr l) {
		try {
			System.Byte[] a1;
			checkArray(l,1,out a1);
			var ret=ZS.Cryptograph.CryptographHelper.Md5Bytes(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Cryptograph.CryptographHelper");
		addMember(l,Md5String_s);
		addMember(l,Md5Bytes_s);
		createTypeMetatable(l,constructor, typeof(ZS.Cryptograph.CryptographHelper));
	}
}
