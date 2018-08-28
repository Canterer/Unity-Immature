-- -----------------------------------------
-- name:XX-XX-管理器
-- desc:
-- author:胡帅
-- -----------------------------------------
XXManager = XXManager or BaseClass(BaseManager)

function XXManager:__init()
    if XXManager.Instance then
        return
    end
    XXManager.Instance = self
    self.model = XXModel.New()
    self:InitMemberData()
    self:InitHandler()
end

function XXManager:__delete()
end

function XXManager:InitMemberData()

end

function XXManager:InitHandler()
    -- self:AddNetHandler(12300, self.On12300)
end

function XXManager:ReqInitData()
    -- self:Send12310()
end

------------------ 协议 -------------------
function XXManager:Send12300()
    self:Send(12300,{})
end

function XXManager:On12300(data)
    -- NoticeManager.Instance:MsgDispatch(data)
end

------------------ 数据接口 ------------------
function XXManager:DataInterface()

end

------------------ 操作接口 -------------------
function XXManager:OperateInterface()

end