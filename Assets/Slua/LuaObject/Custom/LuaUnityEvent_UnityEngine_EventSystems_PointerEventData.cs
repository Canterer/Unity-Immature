
using System;
using System.Collections.Generic;

namespace SLua
{
    public class LuaUnityEvent_UnityEngine_EventSystems_PointerEventData : LuaObject
    {

        [SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int AddListener(IntPtr l)
        {
            try
            {
                UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData> self = checkSelf<UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>>(l);
                UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData> a1;
                checkType(l, 2, out a1);
                self.AddListener(a1);
				pushValue(l,true);
                return 1;
            }
            catch (Exception e)
            {
				return error(l,e);
            }
        }
        [SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int RemoveListener(IntPtr l)
        {
            try
            {
                UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData> self = checkSelf<UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>>(l);
                UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData> a1;
                checkType(l, 2, out a1);
                self.RemoveListener(a1);
				pushValue(l,true);
                return 1;
            }
            catch (Exception e)
            {
                return error(l,e);
            }
        }
        [SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static public int Invoke(IntPtr l)
        {
            try
            {
                UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData> self = checkSelf<UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.PointerEventData>>(l);
				UnityEngine.EventSystems.PointerEventData a0;
				checkType(l,2,out a0);
				self.Invoke(a0);

				pushValue(l,true);
                return 1;
            }
            catch (Exception e)
            {
                return error(l,e);
            }
        }
        static public void reg(IntPtr l)
        {
            getTypeTable(l, typeof(LuaUnityEvent_UnityEngine_EventSystems_PointerEventData).FullName);
            addMember(l, AddListener);
            addMember(l, RemoveListener);
            addMember(l, Invoke);
            createTypeMetatable(l, null, typeof(LuaUnityEvent_UnityEngine_EventSystems_PointerEventData), typeof(UnityEngine.Events.UnityEventBase));
        }

        static bool checkType(IntPtr l,int p,out UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData> ua) {
            LuaDLL.luaL_checktype(l, p, LuaTypes.LUA_TFUNCTION);
            LuaDelegate ld;
            checkType(l, p, out ld);
            if (ld.d != null)
            {
                ua = (UnityEngine.Events.UnityAction<UnityEngine.EventSystems.PointerEventData>)ld.d;
                return true;
            }
			l = LuaState.get(l).L;
            ua = (UnityEngine.EventSystems.PointerEventData v0) =>
            {
                int error = pushTry(l);
                pushValue(l,v0);
                ld.pcall(1, error);
                LuaDLL.lua_settop(l,error - 1);
            };
            ld.d = ua;
            return true;
        }
    }
}
