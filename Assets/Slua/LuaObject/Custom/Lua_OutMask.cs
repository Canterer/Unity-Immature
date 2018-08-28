using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_OutMask : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetTarget(IntPtr l) {
		try {
			OutMask self=(OutMask)checkSelf(l);
			UnityEngine.RectTransform a1;
			checkType(l,2,out a1);
			self.SetTarget(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetOutline(IntPtr l) {
		try {
			OutMask self=(OutMask)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.SetOutline(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"OutMask");
		addMember(l,SetTarget);
		addMember(l,SetOutline);
		createTypeMetatable(l,null, typeof(OutMask),typeof(UnityEngine.UI.MaskableGraphic));
	}
}
