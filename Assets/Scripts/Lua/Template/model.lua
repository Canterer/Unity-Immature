-- -----------------------------------------
-- name:XX-XX-Model
-- desc:
-- author:胡帅
-- -----------------------------------------

XXModel = XXModel or BaseClass(BaseModel)

function XXModel:__init()
    self:InitMemberData()
    self:InitHandler()
end

function XXModel:__delete()	
	-- self.totalDataEvent:DeleteMe()
	-- self.singleDataEvent:DeleteMe()
	-- EventMgr.Instance:RemoveListener(event_name.role_lvup, self.role_level_change)
end

function XXModel:InitMemberData()
	-- self.dataList = {}
end

function XXModel:InitHandler()
	-- self.totalDataEvent = EventLib.New()
    -- self.singleDataEvent = EventLib.New()
	-- self.role_level_change = function() self:UpdateDataList() end
    -- EventMgr.Instance:AddListener(event_name.role_lvup, self.role_level_change)
end

function XXModel:OpenMain(args)
	if self.main == nil then
		self.main = XXXWindow.New(self)
	end
	self.main:Open(args)
end

function XXModel:CloseMain()
	if self.main ~= nil then
		WindowManager.Instance:CloseWindow(self.main)
	end
end

function XXModel:OpenXXXPanel(args)
	if self.XXXPanel == nil then
		self.XXXPanel = XXXPanel.New(self)
	end
	self.XXXPanel:Show(args)
end

function XXModel:CloseXXXPanel()
	if self.XXXPanel ~= nil then
		self.XXXPanel:DeleteMe()
		self.XXXPanel = nil
	end
end

------------------ 数据接口 ------------------
function XXModel:DataInterface()

end

------------------ 操作接口 -------------------
function XXModel:OperateInterface()

end