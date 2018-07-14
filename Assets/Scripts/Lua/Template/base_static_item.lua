-- -----------------------------------------
-- name:XX-静态-子项
-- desc:预制体个数确定 配合LuaCycleList使用 update_my_self()函数必要
-- author:胡帅
-- -----------------------------------------
XXStaticItemVO = XXStaticItemVO or {}
function XXStaticItemVO.New( data )
	local result = {}
	result.name = data.name-- name
	result.status = data.status --状态
	return result
end

XXStaticItem = XXStaticItem or BaseClass()
-- XXXItem = XXXItem or BaseClass()

-- XX
function XXStaticItem:__init(transform, parent)
	self.parent = parent
	self.transform = transform	
	self.gameObject = transform.gameObject

	self:InitPanel()
end

function XXStaticItem:__delete()
end

function XXStaticItem:InitPanel()
	-- self.text = UtilsUI.GetText(self.transform, "text")
	-- self.image = UtilsUI.GetImage(self.transform, "image")
	-- self.button = UtilsUI.GetButton(self.transform, "button")
	-- self.buttonGO = UtilsUI.GetGameObject(self.transform,"Button")
	-- self.slot = ItemSlot.New(UtilsUI.GetGameObject(self.transform, "Main/ItemSlot"))
	-- self.slot:SetNoTips(true)
	
	-- self.select = UtilsUI.GetGameObject(self.transform, "select")
	-- UtilsUI.InActive(self.select)

	-- UtilsUI.AddButtonListener(self.transform, "Button", function() self:ClickButton() end)
	-- UtilsUI.AddButtonListener(self.transform, "", function() self:ClickSelf() end)
end

function XXStaticItem:Select(bool)
	UtilsUI.SetActive(self.select,bool)
end

function XXStaticItem:update_my_self(data,index)
	self.index = index
	self:SetData(data)
end

function XXStaticItem:SetData(data)
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

function XXStaticItem:ClickSelf()
	self.parent:SelectItem(self)
end

function XXStaticItem:ClickButton()
	-- self.parent:ClickButton(self.data)
end

--XXX