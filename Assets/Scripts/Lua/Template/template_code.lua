TemplateCode = TemplateCode or {}

--普通进度条
function TemplateCode:InitProgress()
    self.progressSlider = self.transform:Find("Progress"):GetComponent(Slider)
    self.progressRateText = UtilsUI.GetText(self.transform, "Progress/val")
end

function TemplateCode:UpdateProgress( val,max )
    self.progressSlider.value = val/max
    self.progressRateText.text = string.format("%.1f%%",val/max*100)
end

--显示关注度的进度条
function TemplateCode:InitProgress()
	self.progress = UtilsUI.GetImage(self.transform, "progress")
end

function TemplateCode:UpdateProgress( n )
	local offset = 0.07
	local max = 0.93
	if n == 0.1 then n = 0.15 end-- 最初从0%到10%时显示多点 便于观察
	local m = n*(max - offset) + offset
	self.progress.fillAmount =  m
end

--具有跨越最大值动画的进度条
function TemplateCode:InitProgress()
	self.progressSlider = self.transform:Find("Progress"):GetComponent(Slider)
	self.progressRateText = UtilsUI.GetText(self.transform, "ProgressVal")
    self.progressEffect = UIEffect.New(20128, UtilsUI.GetTransform(self.transform, "Progress/Fill Area/fill/effect", Vector3(0, 0, 0), UIOrder.AssetsPanel + 1, Vector3(1, 1, 1))
    self.progressEffect:LoadAndPlay()

	self.progressCount = 0
	self.progressVal = 0
	self.progressMaxVal = 1
	self.progressLev = 0
	self.progressTweenId = nil
end

function TemplateCode:SetProgress( val,max )
	self.progressVal = val
	self.progressMaxVal = max
	self.progressSlider.value = val/max
	self.progressRateText.text = string.format("%s/%s",math.floor(val),max)
end

function TemplateCode:InitProgressAnimation( curr, lev)
	self.progressLev = lev
	local max = self:GetProgressMaxByLev(lev)
	self:SetProgress(curr,max)
end

function TemplateCode:GetProgressMaxByLev(lev)
	local max = lev*100
	return max
end

function TemplateCode:ProgressAnimationStart(delta)
	self:DeleteProgress()
	self.progressCount = 0
	if delta == 0 then return end
	local time = delta > 10 and 1 or 0.2
	self.progressTweenId = Tween.Instance:ValueChange(self.progressVal, self.progressVal+delta, time, 
	function() 
		self.progressTweenId = nil
	end, LeanTweenType.linear, 
	function(val) 
		val = val - self.progressCount
		if math.floor(val) == self.progressVal then return end 
		if math.floor(val) >= self.progressMaxVal then
			self.progressLev = self.progressLev + 1
			self.progressCount = self.progressCount + self.progressMaxVal
			local max = self:GetProgressMaxByLev(self.progressLev)
			self:SetProgress(math.floor(val)-self.progressMaxVal,max)
		else
			self:SetProgress(math.floor(val),self.progressMaxVal)			
		end
	end).id
end

function TemplateCode:DeleteProgress()
	if self.progressTweenId then
		Tween.Instance:Cancel(self.progressTweenId)
		self.progressTweenId = nil
	end
	if self.progressEffect then
		UtilsBase.FieldDeleteMe(self, "progressEffect")
	end
end


function TemplateCode:InitToggle()
	self.toggle = UtilsUI.GetToggle(self.transform, "Toggle")
    self.toggleBg = UtilsUI.GetImage(self.transform, "Toggle/Background")
    self.toggleMark = UtilsUI.GetGameObject(self.transform, "Toggle/Background/Checkmark")
    self.toggleLabel = UtilsUI.GetText(self.transform, "Toggle/Label")
    UtilsUI.AddButtonListener(self.transform, "Toggle", function()
    	self.toggleFlag = self.toggle.isOn
    end)
    self.toggleFlag = self.toggle.isOn
end

--标签切换  单个界面对应单个标签
function TemplateCode:InitTabGroup()
	local setting = {
		notAutoSelect = true,
		reverse = true
	}
	self.tabgroup = TabGroup.New(UtilsUI.GetGameObject(self.transform, "main/tabgroup"), function(index) self:ChangeTab(index) end, setting)
	self.panelContainer = UtilsUI.GetTransform(self.transform, "Main/Right/panelContainer")
	
	self.sectionPanel = SectionPanel.New(self)
	self.SectionPanel = SectionPanel.New(self)
	self.panelList = {
		[1] = self.sectionPanel,
		[2] = self.SectionPanel,
	}

	self.tabgroupIndex = 1 --默认开启标签
	self.currentPanel = nil --当前显示的panel
	self.tabgroup:ChangeTab(self.tabgroupIndex)
end

function TemplateCode:ChangeTab(index)
	if self.currentPanel then
		self.currentPanel:Hiden()
	end

	self.currentPanel = self.panelList[index]
	if self.currentPanel then
		self.currentPanel:Show()
	end 
end

function TemplateCode:DeleteTabGroup()
	if self.tabgroup ~= nil then
		self.tabgroup:DeleteMe()
		self.tabgroup = nil
	end

	for i,panel in pairs(self.panelList) do
		panel:DeleteMe()
	end
	self.currentPanel = nil
	self.panelList = {}
end

--标签切换  多界面同一标签
function TemplateCode:InitTabGroup()
	local setting = {
		notAutoSelect = true,
		reverse = true
	}
	self.tabgroup = TabGroup.New(UtilsUI.GetGameObject(self.transform, "main/tabgroup"), function(index) self:ChangeTab(index) end, setting)
	self.panelContainer = UtilsUI.GetTransform(self.transform, "Main/Right/panelContainer")
	
	self.sectionPanel = SectionPanel.New(self)
	self.SectionPanel = SectionPanel.New(self)
	self.panelList = {
		[1] = {self.sectionPanel, self.SectionPanel},
		[2] = {self.SectionPanel},
	}

	self.tabgroupIndex = 1 --默认开启标签
	self.currentPanelList = {} --当前显示的panel列表
	self.tabgroup:ChangeTab(self.tabgroupIndex)
end

function TemplateCode:ChangeTab(index)
	for _,v in pairs(self.currentPanelList) do
		v:Hiden()
	end

	self.currentPanelList = self.panelList[index]
	for _,v in pairs(self.currentPanelList) do
		v:Show()
	end
end

function TemplateCode:DeleteTabGroup()
	if self.tabgroup ~= nil then
		self.tabgroup:DeleteMe()
		self.tabgroup = nil
	end

	for i,list in pairs(self.panelList) do
		for j,v in pairs(list) do
			v:DeleteMe()
			v = nil
		end
		list = nil
	end
	self.panelList = {}
end

--列表 预制体子项固定 非循环利用
function TemplateCode:InitXXXListView()
	self.YYYContainer = UtilsUI.GetTransform(self.transform, "Main/Left/Mask/Container")
	self.YYYItemList = {}
	local itemCount = self.YYYContainer.childCount
	for i = 1, itemCount do
		local item = XXStaticItem.New(self.YYYContainer:GetChild(i -1),self)
		table.insert(self.YYYItemList,item)
	end
end

function TemplateCode:UpdateXXXListView()
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

	local itemCount = #self.YYYItemList
	if #sortList > itemCount then return end

	local count = 0

	for i,data in ipairs(sortList) do
		count = count + 1
		local item = self.YYYItemList[count]
		item:SetData(data)
		UtilsUI.Active(item.gameObject)
	end
	for i=count+1,itemCount do
		local item = self.YYYItemList[i]
		UtilsUI.InActive(item.gameObject)
	end
end

function TemplateCode:DeleteXXXListView()
	UtilsBase.TableDeleteMe(self, "YYYItemList")
end

--列表 预制体子项固定 循环利用
function TemplateCode:InitRXXXStaticListView()
	self.RXXXMask = UtilsUI.GetTransform(self.transform, "Main/Left/Mask")
	self.RXXXScrollrect = UtilsUI.GetScroll(self.transform, "Main/Left/Mask")
	self.RXXXContainer = UtilsUI.GetTransform(self.transform, "Main/Left/Mask/Container")

	self.RXXXItemList = {}
	local itemCount = self.RXXXContainer.childCount
	for i = 1, itemCount do
		local  item = XXStaticItem.New(self.RXXXContainer:GetChild(i -1).transform,self)
		table.insert(self.RXXXItemList,item)
	end
	self.RXXX_setting_data = LuaCycleList.initialize(self.RXXXItemList, self.RXXXContainer, self.RXXXScrollrect, nil)
end

function TemplateCode:UpdateRXXXStaticListView()
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

function TemplateCode:DeleteRXXXDynamicListView()
	UtilsBase.TableDeleteMe(self, "RXXXItemList")
end

--列表 动态复制预制体子项
function TemplateCode:InitXXXListView()
	self.YYYItemPrefab = UtilsUI.GetGameObject(self.transform, "Main/Right/Mask/ItemPrefab")
    self.YYYContainer = UtilsUI.GetTransform(self.transform, "Main/Right/Mask/Container")
    local setting = {
        column = 2
        ,cellSizeX = 254
        ,cellSizeY = 95
        ,bordertop = 10
        ,borderleft = 10
        ,cspacing = 10
        ,rspacing = 10
    }
    self.YYYContentLayout = LuaGridLayout.New(self.YYYContainer, setting)
    self.YYYItemList = {}
end

function TemplateCode:UpdateXXXListView()
	self.YYYContentLayout:ReSet()

	local dataList = {}
	local sortList = {}
	local insertCondition = true
	for k,data in pairs(dataList) do
		if insertCondition then
			table.insert(sortList,data)
		end
	end	
	local sortfunction = function(a,b)
		return a.sort < b.sort
	end
	table.sort( sortList, sortfunction )


	for i,data in ipairs(sortList) do
		local item = self:GetXXXListViewItem(i)
		item:SetData(data)
		item:SetShow(true)
		self.YYYContentLayout:AddCell(item.gameObject)
	end
	local count = #sortList
	local itemCount = #self.YYYItemList
	for i=count+1,itemCount do
		local item = self:GetXXXListViewItem(i)
		item:SetShow(false)
	end
end

function TemplateCode:GetXXXListViewItem( index )
	local item = self.YYYItemList[index]
	if item == nil then
		if index <= self.YYYContainer.childCount then
			item = XXDynamicItem.New(self.YYYContainer:GetChild(index-1), self)
		else
			item = XXDynamicItem.New(GameObject.Instantiate(self.YYYItemPrefab).transform, self)
			item.transform:SetParent(self.YYYContainer)
			item.transform.localScale = Vector3.one
	        item.transform.localPosition = Vector3.zero
	    end
		self.YYYItemList[index] = item
	end
	return item
end

function TemplateCode:DeleteXXXListView()
	UtilsBase.TableDeleteMe(self, "YYYItemList")
	UtilsBase.FieldDeleteMe(self, "YYYContentLayout")
end

--list 子项动画
function NewDragonBattlePanel:ListTween(itemList,direction, offset, intervalTime, tweenTime, endCallBack )
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

function NewDragonBattlePanel:DeleteListTween()
    UtilsBase.FieldDeleteMe(self, "listViewTimeLine")
    UtilsBase.TweenIdListDelete(self, "listViewTweenIdList")
end

--长按回调
function NewDragonBattlePanel:InitCustomButton()
	self.customButtonTransform = self.transform
	self.customButtonEffectPos = Vector3.zero
	local customButton = self.customButtonTransform:GetComponent(CustomButton)
	customButton.onHold:AddListener(function () self:CustomButtonOnHold() end)
    customButton.onDown:AddListener(function() self:CustomButtonOnDown() end)
    customButton.onUp:AddListener(function() self:CustomButtonOnUp() end)
end

function NewDragonBattlePanel:CustomButtonOnHold()
end

function NewDragonBattlePanel:CustomButtonOnDown()
	local func = function()
		if self.customButtonEffect == nil then
        	self.customButtonEffect = UIEffect.New(20124, self.customButtonTransform, self.customButtonEffectPos, UIOrder.Window + 1, Vector3.one)
        end
        self:LoadAndPlay()
    end
    self.customButtonTimeId = TimerManager.Add(200, func)
end

function NewDragonBattlePanel:CustomButtonOnUp()
	if self.customButtonEffect ~= nil then
        self.customButtonEffect:Hide()
    end
    if self.customButtonTimeId ~= nil then
        TimerManager.Delete(self.customButtonTimeId)
        self.customButtonTimeId = nil
    end
end

function TemplateCode:InitBigBg()
	local bigBg = self:GetGameObject(string.format(AssetConfig.big_bg, "DragonSkillPreviewBg"))
	local bgTransform = bigBg.transform
	UtilsUI.SetParent(bgTransform, UtilsUI.GetTransform(self.transform, "Main/Left/Nothing/Bg2"))
    SetAPosition(bgTransform, 0, 0, 0)
    SetLocalRotationAngelZero(bgTransform)
    SetLocalScaleOne(bgTransform)
end

-- 数据本地持久化
function TemplateCode:GetLocalPersistentData(key)
	if self.timeKey == nil then
        self.timeKey = "forecast_show_time_"..UtilsBase.SelfKey()
    end
    if self.listKey == nil then
        self.listKey = "forecast_show_times_list_"..UtilsBase.SelfKey()
    end
    if PlayerPrefs.HasKey(key) then
    	PlayerPrefs.SetInt(self.timeKey,UtilsBase.ServerTime())
    end
end

function TemplateCode:SetLocalPersistentData(key, value)
    if PlayerPrefs.HasKey(key) then
    	PlayerPrefs.SetInt(self.timeKey,UtilsBase.ServerTime())
    end
end

function TemplateCode:InitTimeLine()
	if self.timeline == nil then
        self.timeline = TimeLine.New()
    end
    self.timeline:Clear()
    self.timeline:AddNode(0, function() 
    end)
    self.timeline:AddNode(1, function()
    end)
	self.timeline:AddEndCall(function()  end)
    self.timeline:Start()
end
