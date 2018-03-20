-- import "UnityEngine"
require "setupfiles.lua"
require "baseclass.lua"
require "ListView.lua"

function main()
	print("print")
	Debug.Log("Debug.Log")
	Log.Debug("Log.Debug")
	UnityEngine.Logger.Log("Logger.Log")
	
	local testPanelGO = GameObject.Find("MainCanvas/TestPanel")
	if testPanelGO then
		require "ListView.lua"
		testPanelGO.transform.localScale = Vector3(2,1,1)
	end

	local ball = GameObject.CreatePrimitive(PrimitiveType.Cube);
	ball.transform:SetParent( testPanelGO.transform )
	ball.transform.position = v
	ball.transform.localScale = Vector3.one
end