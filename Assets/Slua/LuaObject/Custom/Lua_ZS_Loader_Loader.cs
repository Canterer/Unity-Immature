using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Loader_Loader : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadLuaTable_s(IntPtr l) {
		try {
			SLua.LuaTable a1;
			checkType(l,1,out a1);
			System.Action<System.Boolean> a2;
			checkDelegate(l,2,out a2);
			System.Action<ZS.Loader.LoadingEventArg> a3;
			checkDelegate(l,3,out a3);
			System.Int32 a4;
			checkType(l,4,out a4);
			ZS.Loader.Loader.LoadLuaTable(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadGroupAsset_s(IntPtr l) {
		try {
			ZS.Loader.BundleGroundQueue a1;
			checkType(l,1,out a1);
			ZS.Loader.Loader.LoadGroupAsset(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadAssetCoroutine_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			System.Type a3;
			checkType(l,3,out a3);
			System.Int32 a4;
			checkType(l,4,out a4);
			var ret=ZS.Loader.Loader.LoadAssetCoroutine(a1,a2,a3,a4);
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
	static public int LoadAsset_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				ZS.Loader.CRequest a1;
				checkType(l,1,out a1);
				System.Boolean a2;
				checkType(l,2,out a2);
				var ret=ZS.Loader.Loader.LoadAsset(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==6){
				System.String a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				System.Type a3;
				checkType(l,3,out a3);
				System.Action<ZS.Loader.CRequest> a4;
				checkDelegate(l,4,out a4);
				System.Action<ZS.Loader.CRequest> a5;
				checkDelegate(l,5,out a5);
				System.Int32 a6;
				checkType(l,6,out a6);
				ZS.Loader.Loader.LoadAsset(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function LoadAsset to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HttpRequestCoroutine_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Object a2;
			checkType(l,2,out a2);
			System.Type a3;
			checkType(l,3,out a3);
			var ret=ZS.Loader.Loader.HttpRequestCoroutine(a1,a2,a3);
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
	static public int HttpRequest_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				ZS.Loader.CRequest a1;
				checkType(l,1,out a1);
				System.Boolean a2;
				checkType(l,2,out a2);
				var ret=ZS.Loader.Loader.HttpRequest(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==6){
				System.String a1;
				checkType(l,1,out a1);
				System.Object a2;
				checkType(l,2,out a2);
				System.Type a3;
				checkType(l,3,out a3);
				System.Action<ZS.Loader.CRequest> a4;
				checkDelegate(l,4,out a4);
				System.Action<ZS.Loader.CRequest> a5;
				checkDelegate(l,5,out a5);
				ZS.Loader.UriGroup a6;
				checkType(l,6,out a6);
				ZS.Loader.Loader.HttpRequest(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function HttpRequest to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WWWRequestCoroutine_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Object a2;
			checkType(l,2,out a2);
			System.Type a3;
			checkType(l,3,out a3);
			var ret=ZS.Loader.Loader.WWWRequestCoroutine(a1,a2,a3);
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
	static public int WWWRequest_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				ZS.Loader.CRequest a1;
				checkType(l,1,out a1);
				System.Boolean a2;
				checkType(l,2,out a2);
				var ret=ZS.Loader.Loader.WWWRequest(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==6){
				System.String a1;
				checkType(l,1,out a1);
				System.Object a2;
				checkType(l,2,out a2);
				System.Type a3;
				checkType(l,3,out a3);
				System.Action<ZS.Loader.CRequest> a4;
				checkDelegate(l,4,out a4);
				System.Action<ZS.Loader.CRequest> a5;
				checkDelegate(l,5,out a5);
				ZS.Loader.UriGroup a6;
				checkType(l,6,out a6);
				ZS.Loader.Loader.WWWRequest(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function WWWRequest to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_asyncSize(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.Loader.asyncSize);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_asyncSize(IntPtr l) {
		try {
			System.UInt32 v;
			checkType(l,2,out v);
			ZS.Loader.Loader.asyncSize=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_maxLoading(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.Loader.maxLoading);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_maxLoading(IntPtr l) {
		try {
			System.Int32 v;
			checkType(l,2,out v);
			ZS.Loader.Loader.maxLoading=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_bundleMax(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.Loader.bundleMax);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_bundleMax(IntPtr l) {
		try {
			System.Int32 v;
			checkType(l,2,out v);
			ZS.Loader.Loader.bundleMax=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BundleLoadBreakMilliSeconds(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.Loader.BundleLoadBreakMilliSeconds);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_BundleLoadBreakMilliSeconds(IntPtr l) {
		try {
			System.Single v;
			checkType(l,2,out v);
			ZS.Loader.Loader.BundleLoadBreakMilliSeconds=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Loader.Loader");
		addMember(l,LoadLuaTable_s);
		addMember(l,LoadGroupAsset_s);
		addMember(l,LoadAssetCoroutine_s);
		addMember(l,LoadAsset_s);
		addMember(l,HttpRequestCoroutine_s);
		addMember(l,HttpRequest_s);
		addMember(l,WWWRequestCoroutine_s);
		addMember(l,WWWRequest_s);
		addMember(l,"asyncSize",get_asyncSize,set_asyncSize,false);
		addMember(l,"maxLoading",get_maxLoading,set_maxLoading,false);
		addMember(l,"bundleMax",get_bundleMax,set_bundleMax,false);
		addMember(l,"BundleLoadBreakMilliSeconds",get_BundleLoadBreakMilliSeconds,set_BundleLoadBreakMilliSeconds,false);
		createTypeMetatable(l,null, typeof(ZS.Loader.Loader),typeof(UnityEngine.MonoBehaviour));
	}
}
