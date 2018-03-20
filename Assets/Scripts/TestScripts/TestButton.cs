using UnityEngine;
using System.Collections;
using SLua;
using System.IO;
using UnityEngine.UI;

public class TestButton : MonoBehaviour {
	public GameObject buttonPrefab;

	void Start()
	{
		Debug.Log("Test Button");
		//Debug.LogError (buttonPrefab == null ? "==========null" : "==============not null");
		var go = (GameObject)Instantiate (buttonPrefab);
		Transform transform = go.GetComponent<Transform> ();
		transform.SetParent (GameObject.Find ("Canvas").GetComponent<Transform> ());
		transform.position = new Vector3 (100, 100, 0);
		var button = go.GetComponent <Button>();
		Debug.Log ("getcomponent "+button);

		Text text = button.GetComponentInChildren<UnityEngine.UI.Text> ();
		text.text = string.Format ("cs button");
		go.GetComponent<RectTransform> ().sizeDelta = new Vector2 (90,30);
		//text.rectTransform.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, 100, 300);//上边距为100，高为300

		button.onClick.AddListener( () => {
			print("Test Button onClick !");
		});


		print("TestButton LuaSvr");
		LuaSvr ls_svr = new LuaSvr ();
		ls_svr.init (null, () => {
			LuaTable lt = (LuaTable)ls_svr.start("Test/testButton");
			LuaFunction lf = (LuaFunction)lt["func"];
		});

	}

	void Update(){

	}
}
