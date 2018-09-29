using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Utils_Common : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ZS.Utils.Common o;
			o=new ZS.Utils.Common();
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
	static public int get_ASSETBUNDLE_SUFFIX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.Common.ASSETBUNDLE_SUFFIX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HTTP_STRING(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.Common.HTTP_STRING);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HTTPS_STRING(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.Common.HTTPS_STRING);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Utils.Common");
		addMember(l,"ASSETBUNDLE_SUFFIX",get_ASSETBUNDLE_SUFFIX,null,false);
		addMember(l,"HTTP_STRING",get_HTTP_STRING,null,false);
		addMember(l,"HTTPS_STRING",get_HTTPS_STRING,null,false);
		createTypeMetatable(l,constructor, typeof(ZS.Utils.Common));
	}
}
