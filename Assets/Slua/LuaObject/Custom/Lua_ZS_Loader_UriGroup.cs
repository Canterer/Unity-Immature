using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Loader_UriGroup : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ZS.Loader.UriGroup o;
			o=new ZS.Loader.UriGroup();
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
	static public int Add(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				ZS.Loader.UriGroup self=(ZS.Loader.UriGroup)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				var ret=self.Add(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				ZS.Loader.UriGroup self=(ZS.Loader.UriGroup)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				self.Add(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==4){
				ZS.Loader.UriGroup self=(ZS.Loader.UriGroup)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<ZS.Loader.CRequest,System.Array> a2;
				checkDelegate(l,3,out a2);
				System.Func<ZS.Loader.CRequest,System.Boolean> a3;
				checkDelegate(l,4,out a3);
				self.Add(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(System.Action<ZS.Loader.CRequest,System.Array>),typeof(System.Func<ZS.Loader.CRequest,System.Boolean>),typeof(System.Func<ZS.Loader.CRequest,System.String>))){
				ZS.Loader.UriGroup self=(ZS.Loader.UriGroup)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Action<ZS.Loader.CRequest,System.Array> a2;
				checkDelegate(l,3,out a2);
				System.Func<ZS.Loader.CRequest,System.Boolean> a3;
				checkDelegate(l,4,out a3);
				System.Func<ZS.Loader.CRequest,System.String> a4;
				checkDelegate(l,5,out a4);
				self.Add(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(bool),typeof(bool),typeof(bool))){
				ZS.Loader.UriGroup self=(ZS.Loader.UriGroup)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				System.Boolean a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				self.Add(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Add to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CheckUriCrc(IntPtr l) {
		try {
			ZS.Loader.UriGroup self=(ZS.Loader.UriGroup)checkSelf(l);
			ZS.Loader.CRequest a1;
			checkType(l,2,out a1);
			var ret=self.CheckUriCrc(a1);
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
	static public int CheckWWWComplete_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(ZS.Loader.CRequest),typeof(UnityEngine.Networking.UnityWebRequest))){
				ZS.Loader.CRequest a1;
				checkType(l,1,out a1);
				UnityEngine.Networking.UnityWebRequest a2;
				checkType(l,2,out a2);
				ZS.Loader.UriGroup.CheckWWWComplete(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,1,typeof(ZS.Loader.CRequest),typeof(UnityEngine.WWW))){
				ZS.Loader.CRequest a1;
				checkType(l,1,out a1);
				UnityEngine.WWW a2;
				checkType(l,2,out a2);
				ZS.Loader.UriGroup.CheckWWWComplete(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function CheckWWWComplete to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CheckRequestCurrentIndexCrc_s(IntPtr l) {
		try {
			ZS.Loader.CRequest a1;
			checkType(l,1,out a1);
			var ret=ZS.Loader.UriGroup.CheckRequestCurrentIndexCrc(a1);
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
	static public int CheckAndSetNextUriGroup_s(IntPtr l) {
		try {
			ZS.Loader.CRequest a1;
			checkType(l,1,out a1);
			var ret=ZS.Loader.UriGroup.CheckAndSetNextUriGroup(a1);
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
	static public int get_count(IntPtr l) {
		try {
			ZS.Loader.UriGroup self=(ZS.Loader.UriGroup)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.count);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_uriList(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.UriGroup.uriList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_uriList(IntPtr l) {
		try {
			ZS.Loader.UriGroup v;
			checkType(l,2,out v);
			ZS.Loader.UriGroup.uriList=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Loader.UriGroup");
		addMember(l,Add);
		addMember(l,CheckUriCrc);
		addMember(l,CheckWWWComplete_s);
		addMember(l,CheckRequestCurrentIndexCrc_s);
		addMember(l,CheckAndSetNextUriGroup_s);
		addMember(l,"count",get_count,null,true);
		addMember(l,"uriList",get_uriList,set_uriList,false);
		createTypeMetatable(l,constructor, typeof(ZS.Loader.UriGroup));
	}
}
