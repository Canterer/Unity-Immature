-- -----------------------------------------
-- name:XX-XX-界面
-- desc:
-- author:胡帅
-- -----------------------------------------
XXPanel = XXPanel or BaseClass(BasePanel)

function XXPanel:__init(model)
	self.model = model
	-- self.parent = parent
	-- self.effectPath = UtilsBase.GetEffectPath(20166)
	self.resList = {
		{path = AssetConfig.unitstate, type = AssetType.Prefab},
		-- {path = string.format(AssetConfig.big_bg, "CompetitionGameBg1"), type = AssetType.Prefab},
		-- {path = self.effectPath, type = AssetType.Prefab},
	}

	self.name = "XXPanel"
    self.tweenType = ViewTweenType.PanelMove
	self.OnOpenEvent:Add(function() self:OnShow() end)
    self.OnHideEvent:Add(function() self:OnHide() end)

    self:InitMemberData()
    self:InitHandler()
end

function XXPanel:__delete()
	-- EventMgr.Instance:RemoveListener(event_name.active_time_change, self.totalDataChange)
	-- EventMgr.Instance:RemoveListener(event_name.active_time_change, self.singleDataChange)

	-- UtilsBase.FieldDeleteMe(self, "name")
	-- UtilsBase.TableDeleteMe(self, "name")
	-- UtilsBase.TimerDelete(self, "name")
	-- UtilsBase.TweenDelete(self, "name")
	-- UtilsBase.TweenIdListDelete(self, "tweenIDList")
end

function XXPanel:OnInitCompleted()
    self.OnOpenEvent:Fire()
end

function XXPanel:InitMemberData()
	-- self.dataList = {}
end

function XXPanel:InitHandler()
	-- self.totalDataChange = function()
 --    	self:RefreshTotalData()
	-- end
end

function XXPanel:InitPanel()
	self.gameObject = self:GetGameObject(AssetConfig.unitstate)
    self.gameObject.name = self.name
    self.transform = self.gameObject.transform
    UtilsUI.AddUIChild(ctx.CanvasContainer, self.gameObject)
    -- UtilsUI.SetParent(self.transform,self.parent.panelContainer)
    -- SetAPosition(self.transform, 0, 0, 0)
    -- SetLocalPosition(self.transform, 0, 0, 0)
    
    --local bigBg = self:GetGameObject(string.format(AssetConfig.big_bg, "CompetitionGameBg1"))
	-- UtilsUI.SetParent(bigBg.transform, UtilsUI.GetTransform(self.transform, "BigBg"))
	-- bigBg.transform.anchoredPosition3D = Vector3(259, -80, 0)  

    -- self.container = UtilsUI.GetTransform(self.transform, "Main/Mask/Container")
    -- self.tabButtonPrefab = UtilsUI.GetGameObject(self.transform,"Main/TabButtonPrefab")
    -- UtilsUI.AddButtonListener(self.transform, "Panel", function() self:TweenHide() end)
    -- UtilsUI.AddButtonListener(self.transform, "Main/PanelBg/closeBtn", function() self:TweenHide() end)
end

function XXPanel:Close()
	--self.model:CloseXXXPanel()
end

function XXPanel:OnHide()
	-- EventMgr.Instance:RemoveListener(event_name.active_time_change, self.totalDataChange)
end

function XXPanel:OnShow(arg)
	-- self:RefreshShow()
end