using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_MyJoystickPointerEvent : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			MyJoystickPointerEvent o;
			o=new MyJoystickPointerEvent();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		LuaUnityEvent_UnityEngine_EventSystems_PointerEventData.reg(l);
		getTypeTable(l,"MyJoystickPointerEvent");
		createTypeMetatable(l,constructor, typeof(MyJoystickPointerEvent),typeof(LuaUnityEvent_UnityEngine_EventSystems_PointerEventData));
	}
}
