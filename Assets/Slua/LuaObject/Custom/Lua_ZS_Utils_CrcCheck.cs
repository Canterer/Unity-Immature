using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Utils_CrcCheck : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CheckUriCrc_s(IntPtr l) {
		try {
			ZS.Loader.CRequest a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CrcCheck.CheckUriCrc(a1);
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
	static public int GetLocalFileCrc_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.UInt32 a2;
			var ret=ZS.Utils.CrcCheck.GetLocalFileCrc(a1,out a2);
			pushValue(l,true);
			pushValue(l,ret);
			pushValue(l,a2);
			return 3;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Utils.CrcCheck");
		addMember(l,CheckUriCrc_s);
		addMember(l,GetLocalFileCrc_s);
		createTypeMetatable(l,null, typeof(ZS.Utils.CrcCheck));
	}
}
