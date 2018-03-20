-- import "UnityEngine"
require "Common/setupfiles.lua"

function main()
	
	local testPanelGO = GameObject.Find("MainCanvas/TestPanel")
	if testPanelGO then
		testPanelGO.transform.localScale = Vector3(2,1,1)
	end

	local ball = GameObject.CreatePrimitive(PrimitiveType.Cube);
	ball.transform:SetParent( testPanelGO.transform )
	ball.transform.position = Vector3.one
	ball.transform.localScale = Vector3.one
end