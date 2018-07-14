-- import "UnityEngine"
require "Common/setupfiles.lua"

local Time=UnityEngine.Time
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


		local listView = GameObject.Find("MainCanvas/TestPanel/Main/ListView")
		-- listView.transform:Find("ListView"):GetComponent(ScrollRect)
		local scrollRect = listView.transform:GetComponent(ScrollRect)
		scrollRect.onValueChanged:AddListener(function()
			ZS()
			ZS(Time.time)-- 应用启动多久时间
			ZS(os.time())-- 当前时间戳
		end)
	end
	Test()
end