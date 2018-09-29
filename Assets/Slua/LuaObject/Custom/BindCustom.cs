using System;
using System.Collections.Generic;
namespace SLua {
	[LuaBinder(3)]
	public class BindCustom {
		public static Action<IntPtr>[] GetBindList() {
			Action<IntPtr>[] list= {
				Lua_MyJoystickEvent.reg,
				Lua_MyJoystickPointerEvent.reg,
				Lua_Joystick.reg,
				Lua_Custom.reg,
				Lua_Custom_IFoo.reg,
				Lua_Deleg.reg,
				Lua_foostruct.reg,
				Lua_FloatEvent.reg,
				Lua_ListViewEvent.reg,
				Lua_SLuaTest.reg,
				Lua_System_Collections_Generic_List_1_int.reg,
				Lua_XXList.reg,
				Lua_AbsClass.reg,
				Lua_HelloWorld.reg,
				Lua_NewCoroutine.reg,
				Lua_ZS_Loader_CRequest.reg,
				Lua_ZS_Cryptograph_CryptographHelper.reg,
				Lua_ZS_Loader_Loader.reg,
				Lua_ZS_Loader_LoadingEventArg.reg,
				Lua_ZS_Loader_ABDelayUnloadManager.reg,
				Lua_ZS_Loader_CacheManager.reg,
				Lua_ZS_Loader_CountManager.reg,
				Lua_ZS_Loader_FileManifestOptions.reg,
				Lua_ZS_Loader_FileManifest.reg,
				Lua_ZS_Loader_ManifestManager.reg,
				Lua_ZS_Loader_UriGroup.reg,
				Lua_ZS_Utils_CUtils.reg,
				Lua_ZS_Utils_Common.reg,
				Lua_ZS_Utils_CrcCheck.reg,
				Lua_ZS_Utils_FileHelper.reg,
				Lua_ZS_Utils_LuaHelper.reg,
				Lua_AssetBundleScene.reg,
				Lua_System_Collections_Generic_Dictionary_2_int_string.reg,
				Lua_System_String.reg,
			};
			return list;
		}
	}
}
