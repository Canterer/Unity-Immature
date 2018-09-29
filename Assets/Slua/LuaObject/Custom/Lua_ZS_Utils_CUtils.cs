using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Utils_CUtils : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetRealPersistentDataPath_s(IntPtr l) {
		try {
			var ret=ZS.Utils.CUtils.GetRealPersistentDataPath();
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
	static public int GetRealStreamingAssetsPath_s(IntPtr l) {
		try {
			var ret=ZS.Utils.CUtils.GetRealStreamingAssetsPath();
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
	static public int DebugCastTime_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.DebugCastTime(a1);
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
	static public int GetAssetName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.GetAssetName(a1);
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
	static public int GetAssetBundleName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.GetAssetBundleName(a1);
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
	static public int GetBaseName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.GetBaseName(a1);
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
	static public int CheckWWWUrl_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.CheckWWWUrl(a1);
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
	static public int PathCombine_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=ZS.Utils.CUtils.PathCombine(a1,a2);
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
	static public int GetAndroidABLoadPath_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.GetAndroidABLoadPath(a1);
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
	static public int GetUDKey_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.GetUDKey(a1);
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
	static public int GetRightFileName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.CUtils.GetRightFileName(a1);
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
	static public int get_isRelease(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.CUtils.isRelease);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_printLog(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.CUtils.printLog);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_platform(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.CUtils.platform);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_currPersistentExist(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.CUtils.currPersistentExist);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_currPersistentExist(IntPtr l) {
		try {
			System.Boolean v;
			checkType(l,2,out v);
			ZS.Utils.CUtils.currPersistentExist=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_platformFloder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.CUtils.platformFloder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_realPersistentDataPath(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.CUtils.realPersistentDataPath);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_realPersistentDataPath(IntPtr l) {
		try {
			string v;
			checkType(l,2,out v);
			ZS.Utils.CUtils.realPersistentDataPath=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_realStreamingAssetsPath(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Utils.CUtils.realStreamingAssetsPath);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_realStreamingAssetsPath(IntPtr l) {
		try {
			string v;
			checkType(l,2,out v);
			ZS.Utils.CUtils.realStreamingAssetsPath=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Utils.CUtils");
		addMember(l,GetRealPersistentDataPath_s);
		addMember(l,GetRealStreamingAssetsPath_s);
		addMember(l,DebugCastTime_s);
		addMember(l,GetAssetName_s);
		addMember(l,GetAssetBundleName_s);
		addMember(l,GetBaseName_s);
		addMember(l,CheckWWWUrl_s);
		addMember(l,PathCombine_s);
		addMember(l,GetAndroidABLoadPath_s);
		addMember(l,GetUDKey_s);
		addMember(l,GetRightFileName_s);
		addMember(l,"isRelease",get_isRelease,null,false);
		addMember(l,"printLog",get_printLog,null,false);
		addMember(l,"platform",get_platform,null,false);
		addMember(l,"currPersistentExist",get_currPersistentExist,set_currPersistentExist,false);
		addMember(l,"platformFloder",get_platformFloder,null,false);
		addMember(l,"realPersistentDataPath",get_realPersistentDataPath,set_realPersistentDataPath,false);
		addMember(l,"realStreamingAssetsPath",get_realStreamingAssetsPath,set_realStreamingAssetsPath,false);
		createTypeMetatable(l,null, typeof(ZS.Utils.CUtils));
	}
}
