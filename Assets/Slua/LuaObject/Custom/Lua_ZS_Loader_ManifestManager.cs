using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Loader_ManifestManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetVariantName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Loader.ManifestManager.GetVariantName(a1);
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
	static public int get_fileManifest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.ManifestManager.fileManifest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_fileManifest(IntPtr l) {
		try {
			ZS.Loader.FileManifest v;
			checkType(l,2,out v);
			ZS.Loader.ManifestManager.fileManifest=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_updateFileManifest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.ManifestManager.updateFileManifest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_updateFileManifest(IntPtr l) {
		try {
			ZS.Loader.FileManifest v;
			checkType(l,2,out v);
			ZS.Loader.ManifestManager.updateFileManifest=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_bundlesWithVariant(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.ManifestManager.bundlesWithVariant);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_bundlesWithVariant(IntPtr l) {
		try {
			System.String[] v;
			checkArray(l,2,out v);
			ZS.Loader.ManifestManager.bundlesWithVariant=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ActiveVariants(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,ZS.Loader.ManifestManager.ActiveVariants);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ActiveVariants(IntPtr l) {
		try {
			System.String[] v;
			checkArray(l,2,out v);
			ZS.Loader.ManifestManager.ActiveVariants=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZS.Loader.ManifestManager");
		addMember(l,GetVariantName_s);
		addMember(l,"fileManifest",get_fileManifest,set_fileManifest,false);
		addMember(l,"updateFileManifest",get_updateFileManifest,set_updateFileManifest,false);
		addMember(l,"bundlesWithVariant",get_bundlesWithVariant,set_bundlesWithVariant,false);
		addMember(l,"ActiveVariants",get_ActiveVariants,set_ActiveVariants,false);
		createTypeMetatable(l,null, typeof(ZS.Loader.ManifestManager));
	}
}
