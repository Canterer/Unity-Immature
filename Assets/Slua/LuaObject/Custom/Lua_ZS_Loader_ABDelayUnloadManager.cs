using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Loader_ABDelayUnloadManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_delaySecondTime(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.ABDelayUnloadManager.delaySecondTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_delaySecondTime(IntPtr l) {
		try {
			System.Single v;
			checkType(l,2,out v);
			ZS.Loader.ABDelayUnloadManager.delaySecondTime=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Loader.ABDelayUnloadManager");
		addMember(l,"delaySecondTime",get_delaySecondTime,set_delaySecondTime,false);
		createTypeMetatable(l,null, typeof(ZS.Loader.ABDelayUnloadManager));
	}
}
