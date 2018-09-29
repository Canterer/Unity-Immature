using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Loader_LoadingEventArg : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			ZS.Loader.LoadingEventArg o;
			if(argc==1){
				o=new ZS.Loader.LoadingEventArg();
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==4){
				System.Int64 a1;
				checkType(l,2,out a1);
				System.Int64 a2;
				checkType(l,3,out a2);
				System.Object a3;
				checkType(l,4,out a3);
				o=new ZS.Loader.LoadingEventArg(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_progress(IntPtr l) {
		try {
			ZS.Loader.LoadingEventArg self=(ZS.Loader.LoadingEventArg)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.progress);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_progress(IntPtr l) {
		try {
			ZS.Loader.LoadingEventArg self=(ZS.Loader.LoadingEventArg)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.progress=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_target(IntPtr l) {
		try {
			ZS.Loader.LoadingEventArg self=(ZS.Loader.LoadingEventArg)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.target);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_total(IntPtr l) {
		try {
			ZS.Loader.LoadingEventArg self=(ZS.Loader.LoadingEventArg)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.total);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_current(IntPtr l) {
		try {
			ZS.Loader.LoadingEventArg self=(ZS.Loader.LoadingEventArg)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.current);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Loader.LoadingEventArg");
		addMember(l,"progress",get_progress,set_progress,true);
		addMember(l,"target",get_target,null,true);
		addMember(l,"total",get_total,null,true);
		addMember(l,"current",get_current,null,true);
		createTypeMetatable(l,constructor, typeof(ZS.Loader.LoadingEventArg),typeof(System.ComponentModel.ProgressChangedEventArgs));
	}
}
