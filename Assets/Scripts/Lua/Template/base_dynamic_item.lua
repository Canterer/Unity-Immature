-- -----------------------------------------
-- name:XX-动态-子项
-- desc:预制体采用动态生成 配合LuaGridLayout使用 SetShow(bool)控制预制体实例的显示
-- author:胡帅
-- -----------------------------------------
XXDynamicItemVO = XXDynamicItemVO or {}
function XXDynamicItemVO.New( data )
	local result = {}
	result.name = data.name-- name
	result.status = data.status --状态
	return result
end

XXDynamicItem = XXDynamicItem or BaseClass()
-- XXXItem = XXXItem or BaseClass()

-- XX
function XXDynamicItem:__init(transform, parent)
	self.parent = parent
	self.transform = transform	
	self.gameObject = transform.gameObject

	self:InitPanel()
end

function XXDynamicItem:__delete()
end

function XXDynamicItem:InitPanel()
	-- self.text = UtilsUI.GetText(self.transform, "text")
	-- self.image = UtilsUI.GetImage(self.transform, "image")
	-- self.button = UtilsUI.GetButton(self.transform, "button")
	-- self.buttonGO = UtilsUI.GetGameObject(self.transform,"Button")
	
	-- self.select = UtilsUI.GetGameObject(self.transform, "select")
	-- UtilsUI.InActive(self.select)

	-- UtilsUI.AddButtonListener(self.transform, "Button", function() self:ClickButton() end)
	UtilsUI.AddButtonListener(self.transform, "", function() self:ClickSelf() end)
end

function XXDynamicItem:Select(bool)
	UtilsUI.SetActive(self.select,bool)
end

function XXDynamicItem:SetShow(bool)
	UtilsUI.SetActive(self.gameObject,bool)
end

function XXDynamicItem:SetData(data)
	self.data = data

	-- if self.slot.VO == nil then
 --        self.slot.VO = ItemVO.New({base_id=data.base_id})
 --        self.slot.VO:HideDrop()
 --    end
 --    self.slot:SetData(self.slot.VO)
    
	-- statu 1 未开始 2 进行中 3 已结束
	local statu = 4
	if statu == 1 then
		-- UtilsUI.InActive(self.button)
	elseif statu == 2 then
		-- UtilsUI.Active(self.button)
	elseif statu == 3 then
		-- UtilsUI.InActive(self.button)
	end
end

function XXDynamicItem:ClickSelf()
	self.parent:SelectItem(self)
end

function XXDynamicItem:ClickButton()
	-- self.parent:ClickButton(self.data)
end

--XXX