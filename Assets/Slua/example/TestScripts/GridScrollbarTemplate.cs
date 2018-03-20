
using UnityEngine;
using System.Collections;
using SLua;
using System.IO;
using UnityEngine.UI;

public class GridScrollbarTemplate : MonoBehaviour {

	public GameObject layoutRect;
	public int row,col,count;
	public GameObject itemPrefab;
	void Start()
	{
		LuaSvr ls_svr = new LuaSvr ();
		ls_svr.init (null, () => {
			LuaTable lt = (LuaTable)ls_svr.start("Test/GridScrollbarTemplate");
			//LuaSvr.mainState.getFunction("init").call(layoutRect,row,col,count,itemPrefab);
			LuaFunction lf = (LuaFunction)lt["init"];
			lf.call(lt,layoutRect,row,col,count,itemPrefab);
		});

	}

	void Update(){

	}
}
