using UnityEngine;
using System.Collections;
using SLua;
using System.IO;

public class start_game : MonoBehaviour {

	LuaSvr lua_svr;
	LuaState lua_state;

	// Use this for initialization
	void Start () {

#if UNITY_5
		Application.logMessageReceived += this.log;
#else
		Application.RegisterLogCallback(this.log);
#endif

		// lua_state = new LuaState();
		// lua_state.loaderDelegate = ( (string fn) => {
		// 	//获取Lua文件执行目录 
		// 	string file_path = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\Lua\\" + fn;
		// 	Debug.Log(file_path); 
		// 	return File.ReadAllBytes(file_path);
		// });

		lua_svr = new LuaSvr();
		LuaSvr.mainState.loaderDelegate = path => {return File.ReadAllBytes(Directory.GetCurrentDirectory()+"/Assets/Scripts/Lua/"+path);};
		lua_svr.init( tick, complate, LuaSvrFlag.LSF_BASIC|LuaSvrFlag.LSF_EXTLIB);
	}
	int progress = 0;
	void tick( int p )
	{
		progress = p;
		Debug.Log(progress); 
	}

	void complate()
	{
		lua_svr.start("mainTest.lua");//执行TestLua.lua 的main方法

		// lua_state.doString("print(\"Hello Lua\")");
		// lua_state.doFile("mainTest.lua");
	}
	// Update is called once per frame
	void Update () {
	
	}

	void log(string cond, string trace, LogType lt)
	{
		Debug.Log(cond); 
		Debug.Log(trace); 
		Debug.Log(lt); 
	}
}
