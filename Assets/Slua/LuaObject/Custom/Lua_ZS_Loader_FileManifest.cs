using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Loader_FileManifest : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ZS.Loader.FileManifest o;
			o=new ZS.Loader.FileManifest();
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
	static public int GetDirectDependencies(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetDirectDependencies(a1);
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
	static public int GetVariants(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetVariants(a1);
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
	static public int AddVariant(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.AddVariant(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_appNumVersion(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.appNumVersion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_appNumVersion(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.appNumVersion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_crc32(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.crc32);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_crc32(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			System.UInt32 v;
			checkType(l,2,out v);
			self.crc32=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_hasFirstLoad(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.hasFirstLoad);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_hasFirstLoad(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.hasFirstLoad=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Count(IntPtr l) {
		try {
			ZS.Loader.FileManifest self=(ZS.Loader.FileManifest)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Count);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Loader.FileManifest");
		addMember(l,GetDirectDependencies);
		addMember(l,GetVariants);
		addMember(l,AddVariant);
		addMember(l,"appNumVersion",get_appNumVersion,set_appNumVersion,true);
		addMember(l,"crc32",get_crc32,set_crc32,true);
		addMember(l,"hasFirstLoad",get_hasFirstLoad,set_hasFirstLoad,true);
		addMember(l,"Count",get_Count,null,true);
		createTypeMetatable(l,constructor, typeof(ZS.Loader.FileManifest),typeof(UnityEngine.ScriptableObject));
	}
}
