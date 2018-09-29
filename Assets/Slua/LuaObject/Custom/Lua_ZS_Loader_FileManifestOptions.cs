using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Loader_FileManifestOptions : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StreamingAssetsPriority(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.FileManifestOptions.StreamingAssetsPriority);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FirstLoadPriority(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.FileManifestOptions.FirstLoadPriority);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoHotPriority(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.FileManifestOptions.AutoHotPriority);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UserPriority(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.FileManifestOptions.UserPriority);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ManualPriority(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.FileManifestOptions.ManualPriority);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Loader.FileManifestOptions");
		addMember(l,"StreamingAssetsPriority",get_StreamingAssetsPriority,null,false);
		addMember(l,"FirstLoadPriority",get_FirstLoadPriority,null,false);
		addMember(l,"AutoHotPriority",get_AutoHotPriority,null,false);
		addMember(l,"UserPriority",get_UserPriority,null,false);
		addMember(l,"ManualPriority",get_ManualPriority,null,false);
		createTypeMetatable(l,null, typeof(ZS.Loader.FileManifestOptions));
	}
}
