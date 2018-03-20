using UnityEngine;
using System.Collections;
using SLua;
using System.IO;

public class AppDelegate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		LuaState ls_state = new LuaState();
		//设置脚本启动代理   => 代表Lambda表达式
		ls_state.loaderDelegate = ( ( string fn ) =>{
			string file_path = Directory.GetCurrentDirectory() + "/Assets/TestLua/" + fn;
			Debug.Log( file_path );
			return File.ReadAllBytes( file_path );
		});

		ls_state.doFile("testLuaReadPath.lua");
		LuaTable lt = (LuaTable)ls_state.run("main");
		//lt.print.call("str");
		//lt.print.call(2);
		LuaFunction lf = (LuaFunction)lt["func"];
		lf.call();

		print ("AppDelegate LuaSvr");
		LuaSvr ls_svr = new LuaSvr();
		LuaSvr.mainState.loaderDelegate = path => {return File.ReadAllBytes(Directory.GetCurrentDirectory()+"/Assets/TestLua/"+path);};
		
		// void tick(int) doBindProgress 代表doBind 的百分比进度
		// complete doBind完成后的回调函数
		// 第三个参数缺省 LuaSvrFlag flag=LuaSvrFlag.LSF_BASIC|LuaSvrFlag.LSF_EXTLIB
		ls_svr.init( tick,complete );
		ls_svr.start("TestLua.lua");//执行TestLua.lua 的main方法

	}
	//GameObject go = GameObject.Find("Button");
	// Update is called once per frame
	void Update () {
	
	}

	void tick(int doBindProgress)
	{
		//Debug.Log(string.Format("doBindProgress {0}%",doBindProgress));
	}
	void complete()
	{

	}
}
