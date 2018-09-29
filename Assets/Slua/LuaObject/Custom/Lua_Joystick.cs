using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_Joystick : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Start(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			self.Start();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnBeginDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnBeginDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnEndDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnEndDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FireDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			self.FireDrag();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerClick(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerClick(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerDown(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerDown(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerUp(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerUp(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_updateTime(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.updateTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_updateTime(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.updateTime=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_moveLock(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.moveLock);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_moveLock(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.moveLock=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_mRadius(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.mRadius);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_mRadius(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.mRadius=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_content(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.content);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_content(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			UnityEngine.RectTransform v;
			checkType(l,2,out v);
			self.content=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_onBeginDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.onBeginDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onBeginDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			MyJoystickEvent v;
			checkType(l,2,out v);
			self.onBeginDrag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_onDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.onDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			MyJoystickEvent v;
			checkType(l,2,out v);
			self.onDrag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_onEndDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.onEndDrag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onEndDrag(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			MyJoystickEvent v;
			checkType(l,2,out v);
			self.onEndDrag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_onClick(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.onClick);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onClick(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			MyJoystickEvent v;
			checkType(l,2,out v);
			self.onClick=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_onDown(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.onDown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onDown(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			MyJoystickPointerEvent v;
			checkType(l,2,out v);
			self.onDown=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_onUp(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.onUp);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_onUp(IntPtr l) {
		try {
			Joystick self=(Joystick)checkSelf(l);
			MyJoystickPointerEvent v;
			checkType(l,2,out v);
			self.onUp=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"Joystick");
		addMember(l,Start);
		addMember(l,OnBeginDrag);
		addMember(l,OnDrag);
		addMember(l,OnEndDrag);
		addMember(l,FireDrag);
		addMember(l,OnPointerClick);
		addMember(l,OnPointerDown);
		addMember(l,OnPointerUp);
		addMember(l,"updateTime",get_updateTime,set_updateTime,true);
		addMember(l,"moveLock",get_moveLock,set_moveLock,true);
		addMember(l,"mRadius",get_mRadius,set_mRadius,true);
		addMember(l,"content",get_content,set_content,true);
		addMember(l,"onBeginDrag",get_onBeginDrag,set_onBeginDrag,true);
		addMember(l,"onDrag",get_onDrag,set_onDrag,true);
		addMember(l,"onEndDrag",get_onEndDrag,set_onEndDrag,true);
		addMember(l,"onClick",get_onClick,set_onClick,true);
		addMember(l,"onDown",get_onDown,set_onDown,true);
		addMember(l,"onUp",get_onUp,set_onUp,true);
		createTypeMetatable(l,null, typeof(Joystick),typeof(UnityEngine.MonoBehaviour));
	}
}
