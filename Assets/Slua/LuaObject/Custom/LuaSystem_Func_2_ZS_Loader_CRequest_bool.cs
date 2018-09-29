
using System;
using System.Collections.Generic;
namespace SLua
{
    public partial class LuaDelegation : LuaObject
    {

        static internal bool Lua_System_Func_2_ZS_Loader_CRequest_bool(LuaFunction ld ,ZS.Loader.CRequest a1) {
            IntPtr l = ld.L;
            int error = pushTry(l);

			pushValue(l,a1);
			ld.pcall(1, error);
			bool ret;
			checkType(l,error+1,out ret);
			LuaDLL.lua_settop(l, error-1);
			return ret;
		}
	}
}
