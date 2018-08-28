-- -----------------------------------------
-- name:XX-XX-主界面
-- desc:
-- author:胡帅
-- -----------------------------------------
XXWindow = XXWindow or BaseClass(BaseWindow)

function XXWindow:__init(model)
	self.model = model
	self.resList = {
		{path = AssetConfig.treasure_activity_panel, type = AssetType.Prefab},
		-- {path = self.effectPath, type = AssetType.Prefab},
	}

	-- 缓存类型
    self.cacheMode = CacheMode.Destroy--CacheMode.Visible
    self.tweenType = ViewTweenType.PanelMove
    self.winLinkType = WinLinkType.Link
    self.isHideMainUI = false
    self.isHideScene = false
    self.hasSound = true
    self.notopAssets = true--是否不显示顶部通用资产
    self.topAssetsList = nil --顶部资产列表(显示什么资产)  nil则默认资产列表
    self.topAssetsLayout = nil --资产布局   nil则默认布局

	self.name = "XXWindow"
	self.OnOpenEvent:Add(function() self:OnShow() end)
    self.OnHideEvent:Add(function() self:OnHide() end)

    self:InitMemberData()
    self:InitHandler()
end

function XXWindow:__delete()
	-- EventMgr.Instance:RemoveListener(event_name.active_time_change, self.totalDataChange)
	-- EventMgr.Instance:RemoveListener(event_name.active_time_change, self.singleDataChange)

	self:DeleteTabGroup()

	-- UtilsBase.FieldDeleteMe(self, "name")
	-- UtilsBase.TableDeleteMe(self, "name")
	-- UtilsBase.TimerDelete(self, "name")
	-- UtilsBase.TweenDelete(self, "name")
	-- UtilsBase.TweenIdListDelete(self, "tweenIDList")
end

function XXWindow:OnInitCompleted()
    self.OnOpenEvent:Fire()
end

function XXWindow:InitMemberData()
	-- self.dataList = {}
end

function XXWindow:InitHandler()
	-- self.totalDataChange = function()
 --    	self:RefreshTotalData()
	-- end
end

function XXWindows:InitPanel()
	self.gameObject = self:GetGameObject(AssetConfig.treasure_activity_panel)
    self.gameObject.name = self.name
    self.transform = self.gameObject.transform
    UtilsUI.AddUIChild(ctx.CanvasContainer, self.gameObject)
    -- UtilsUI.SetParent(self.transform,self.parent.panelContainer)
    -- SetAPosition(self.transform, 0, 0, 0)
    -- SetLocalPosition(self.transform, 0, 0, 0)

	--local bigBg = self:GetGameObject(string.format(AssetConfig.big_bg, "CompetitionGameBg1"))
	-- UtilsUI.SetParent(bigBg.transform, UtilsUI.GetTransform(self.transform, "BigBg"))
	-- bigBg.transform.anchoredPosition3D = Vector3(259, -80, 0)  

    -- UtilsUI.AddButtonListener(self.transform, "Panel", function() self:TweenClose() end)
    -- UtilsUI.AddButtonListener(self.transform, "Main/PanelBg/closeBtn", function() self:TweenClose() end)

    -- self.tabButtonPrefab = UtilsUI.GetGameObject(self.transform,"Main/TabButtonPrefab")
    -- self.tabGroupGameObject= UtilsUI.GetGameObject(self.transform, "Main/TabButtonGroup")

	-- self.mask = UtilsUI.GetTransform(self.transform, "Main/Mask")
	-- self.scrollrect = UtilsUI.GetTransform(self.transform, "Main/Mask")
	-- self.container = UtilsUI.GetTransform(self.transform, "Main/Mask/Container")
	-- self.containerCache = UtilsUI.GetTransform(self.transform, "Main/Mask/ContainerCache")

	self:InitTabGroup()
end

function XXWindow:Close()
	-- self.model:CloseMain()
end

function XXWindow:OnShow(arg)
	self:RefreshShow()
end

function XXWindow:OnHide()
	-- EventMgr.Instance:RemoveListener(event_name.backpack_item_add, self.addItemData)
end

function XXWindow:InitTabGroup()
	-- local setting = {
	-- 	notAutoSelect = true,
	-- 	reverse = true
	-- }
	-- self.tabgroup = TabGroup.New(UtilsUI.GetGameObject(self.transform, "main/tabgroup"), function(index) self:ChangeTab(index) end, setting)
	-- self.panelContainer = UtilsUI.GetTransform(self.transform, "Main/Right/panelContainer")
	
	-- self.sectionPanel = SectionPanel.New(self)
	-- self.panelList = {
	-- 	[1] = {self.sectionPanel, self.sectionPanel},
	-- 	[2] = {self.sectionPanel},
	-- }

	-- self.tabgroupIndex = 1 --默认开启标签
	-- self.currentPanelList = {} --当前显示的panel列表
	-- self.tabgroup:ChangeTab(self.tabgroupIndex)
end

function XXWindow:ChangeTab(index)
	-- for _,v in ipairs(self.currentPanelList) do
	-- 	v:Hiden()
	-- end

	-- self.currentPanelList = self.panelList[index]
	-- for _,v in ipairs(self.currentPanelList) do
	-- 	v:Show()
	-- end
end

function XXWindow:DeleteTabGroup()
	-- if self.tabgroup ~= nil then
	-- 	self.tabgroup:DeleteMe()
	-- 	self.tabgroup = nil
	-- end

	-- for i,list in ipairs(self.panelList) do
	-- 	for j,v in ipairs(list) do
	-- 		if v["DeleteMe"] ~= nil then
	-- 			v:DeleteMe()
	-- 		end
	-- 		v = nil
	-- 	end
	-- 	list = nil
	-- end
	-- self.panelList = {}
end

function XXWindow:InitRXXXStaticListView()
	self.RXXXMask = UtilsUI.GetTransform(self.transform, "Main/Left/Mask")
	self.RXXXScrollrect = UtilsUI.GetScroll(self.transform, "Main/Left/Mask")
	self.RXXXContainer = UtilsUI.GetTransform(self.transform, "Main/Left/Mask/Container")

	self.RXXXItemList = {}

	for i = 1, 10 do
		local  item = XXStaticItem.New(self.RXXXContainer:GetChild(i -1).transform,self)
		table.insert(self.RXXXItemList,item)
	end
	self.RXXX_setting_data = LuaCycleList.initialize(self.RXXXItemList, self.RXXXContainer, self.RXXXScrollrect, nil)
end

function XXWindow:UpdateRXXXStaticListView()
	local dataList = {}
	local sortList = {}
	local insertCondition = true
	for k,data in pairs(dataList) do
		if insertCondition then
			table.insert(sortList,data)
		end
	end
	local sortfunction = function( a,b )
		return true
	end
	table.sort( sortList, sortfunction )

	self.RXXX_setting_data.data_list = sortList
	LuaCycleList.refresh_circular_list(self.RXXX_setting_data)
end

function XXWindow:DeleteRXXXDynamicListView()
	UtilsBase.TableDeleteMe(self, "RXXXItemList")
end

function XXWindow:InitRXXXDynamicListView()
	self.RXXXItemPrefab = UtilsUI.GetGameObject(self.transform, "Main/Right/Mask/ItemPrefab")
    self.RXXXContainer = UtilsUI.GetTransform(self.transform, "Main/Right/Mask/Container")
    local setting = {
        column = 2
        ,cellSizeX = 254
        ,cellSizeY = 95
        ,bordertop = 10
        ,borderleft = 10
        ,cspacing = 10
        ,rspacing = 10
    }
    self.RXXXContentLayout = LuaGridLayout.New(self.RXXXContainer, setting)
    self.RXXXItemList = {}
end

function XXWindow:UpdateRXXXDynamicListView()
	self.RXXXContentLayout:ReSet()

	for k,item in pairs(self.RXXXItemList) do
		item:SetShow( false )
	end

	local data_list = {}
	local sortList = {}
	local insertCondition = true
	for k,data in pairs(data_list) do
		if insertCondition then
			table.insert(sortList,data)
		end
	end	
	local sortfunction = function(a,b)
		return a.sort < b.sort
	end
	table.sort( sortList, sortfunction )

	for i,data in ipairs(sortList) do
		local item = self:GetRXXXContentItem(i)
		item:SetData(data)
		item:SetShow( true )
		self.RXXXContentLayout:AddCell(item.gameObject)
	end
end

function XXWindow:GetRXXXDynamicContentItem( index )
	local item = self.RXXXItemList[index]
	if item == nil then
		item = XXDynamicItem.New(GameObject.Instantiate(self.RXXXItemPrefab).transform, self)
		item.transform:SetParent(self.RXXXContainer)
		item.transform.localScale = Vector3.one
        item.transform.localPosition = Vector3.zero
		self.RXXXItemList[index] = item
	end
	return item
end

function XXWindow:DeleteRXXXDynamicListView()
	UtilsBase.TableDeleteMe(self, "RXXXItemList")
	UtilsBase.FieldDeleteMe(self, "RXXXContentLayout")
end

function XXWindow:ListTween(itemList,direction, offset, intervalTime, tweenTime, endCallBack )
    UtilsBase.FieldDeleteMe(self, "listViewTimeLine")
    UtilsBase.TweenIdListDelete(self, "listViewTweenIdList")
    self.listViewTweenIdList = {}
    for _, tweenId in pairs(self.listViewTweenIdList) do
        Tween.Instance:Cancel(tweenId)
    end
    self.listViewTimeLine = TimeLine.New()
    local time = 0
    for i = 1, #itemList do
        local item = itemList[i]
        item:SetShow(false)        
        self.listViewTimeLine:AddNode(time, function()
        	if item.SetAlpha then
                item:SetAlpha(0)
            end
        	if direction == "Vertical" then--垂直
        		local y = item.transform.localPosition.y
	            UtilsUI.SetY(item.transform, item.transform.localPosition.y + offset)
	            item:SetShow(true)
	            local tweenId1, tweenId2 = UtilsTween.MoveLocalY(item.transform.gameObject, y, tweenTime)
	            if tweenId1 then table.insert(self.listViewTweenIdList, tweenId1) end
	            if tweenId2 then table.insert(self.listViewTweenIdList, tweenId2) end
        	elseif direction == "Horizontal" then--水平
				local x = item.transform.localPosition.x
	            UtilsUI.SetX(item.transform, item.transform.localPosition.x + offset)
	            item:SetShow(true)
	            local tweenId1, tweenId2 = UtilsTween.MoveLocalX(item.transform.gameObject, x, tweenTime)
	            if tweenId1 then table.insert(self.listViewTweenIdList, tweenId1) end
	            if tweenId2 then table.insert(self.listViewTweenIdList, tweenId2) end
        	end            
        end)
        time = time + intervalTime
    end
    if endCallBack and type(endCallBack) == "function" then
    	self.listViewTimeLine:AddEndCall(endCallBack)
   	end
    self.listViewTimeLine:Start()
end

function XXWindow:DeleteListTween()
    UtilsBase.FieldDeleteMe(self, "listViewTimeLine")
    UtilsBase.TweenIdListDelete(self, "listViewTweenIdList")
end

function XXWindow:InitCustomButton()
	self.customButtonTransform = self.transform
	self.customButtonEffectPos = Vector3.zero
	local customButton = self.customButtonTransform:GetComponent(CustomButton)
	customButton.onHold:AddListener(function () self:CustomButtonOnHold() end)
    customButton.onDown:AddListener(function() self:CustomButtonOnDown() end)
    customButton.onUp:AddListener(function() self:CustomButtonOnUp() end)
end

function XXWindow:CustomButtonOnHold()

end

function XXWindow:CustomButtonOnDown()
	local func = function()
		if self.customButtonEffect == nil then
        	self.customButtonEffect = UIEffect.New(20124, self.customButtonTransform, self.customButtonEffectPos, UIOrder.Window + 1, Vector3.one)
        end
        self:LoadAndPlay()
    end
    self.customButtonTimeId = TimerManager.Add(200, func)
end

function XXWindow:CustomButtonOnUp()
	 if self.customButtonEffect ~= nil then
        self.customButtonEffect:Hide()
    end
    if self.customButtonTimeId ~= nil then
        TimerManager.Delete(self.customButtonTimeId)
        self.customButtonTimeId = nil
    end
end

function XXWindow:CheckTabRed()
	-- self.tabgroup:ShowRed(2, GuildManager.Instance:HasApply())
end