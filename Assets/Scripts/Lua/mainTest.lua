-- import "UnityEngine"
require "Common/setupfiles.lua"

function main()
	local Test = function()
		local testPanelGO = GameObject.Find("MainCanvas/TestPanel")
		if testPanelGO then
			ZS()
			testPanelGO.transform.localScale = Vector3(2,1,1)
		end
		ZS()

		local ball = GameObject.CreatePrimitive(PrimitiveType.Cube);
		ball.transform:SetParent( testPanelGO.transform )
		ball.transform.position = Vector3.one
		ball.transform.localScale = Vector3.one
		end
	end
	Test()
end