using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Utils_LuaHelper : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StringToHash_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.LuaHelper.StringToHash(a1);
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
	static public int GetBytes_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.LuaHelper.GetBytes(a1);
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
		getTypeTable(l,"ZS.Utils.LuaHelper");
		addMember(l,StringToHash_s);
		addMember(l,GetBytes_s);
		createTypeMetatable(l,null, typeof(ZS.Utils.LuaHelper));
	}
}
