using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_ZS_Utils_FileHelper : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			ZS.Utils.FileHelper o;
			o=new ZS.Utils.FileHelper();
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
	static public int SavePersistentFile_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(string),typeof(string))){
				System.String a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				ZS.Utils.FileHelper.SavePersistentFile(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,1,typeof(System.Array),typeof(string))){
				System.Array a1;
				checkType(l,1,out a1);
				System.String a2;
				checkType(l,2,out a2);
				ZS.Utils.FileHelper.SavePersistentFile(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SavePersistentFile to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReadPersistentFile_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.FileHelper.ReadPersistentFile(a1);
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
	static public int ChangePersistentFileName_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			var ret=ZS.Utils.FileHelper.ChangePersistentFileName(a1,a2);
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
	static public int DeletePersistentFile_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			ZS.Utils.FileHelper.DeletePersistentFile(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DeletePersistentDirectoryFiles_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			ZS.Utils.FileHelper.DeletePersistentDirectoryFiles(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PersistentFileExists_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=ZS.Utils.FileHelper.PersistentFileExists(a1);
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
		getTypeTable(l,"ZS.Utils.FileHelper");
		addMember(l,SavePersistentFile_s);
		addMember(l,ReadPersistentFile_s);
		addMember(l,ChangePersistentFileName_s);
		addMember(l,DeletePersistentFile_s);
		addMember(l,DeletePersistentDirectoryFiles_s);
		addMember(l,PersistentFileExists_s);
		createTypeMetatable(l,constructor, typeof(ZS.Utils.FileHelper));
	}
}
