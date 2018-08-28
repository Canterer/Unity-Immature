-- 组件需求整理

Component = Component or BaseClass()

function Component:__init()
end

function Component:__delete()
end

Component.QuestDescList = {
	"Progress" = {
		"设置进度",
		"进度动画",
		"进度点特效",
		"进度动画完成回调",
		"设置进度条触发事件",
	},
	"QuickBuy" = {
		"数据实时刷新",
		"最大购买数据",
		"初始购买数量",
		"购买途径切换",
		"关闭回调",
		"购买数量变动回调",
		"所购物品数量变动回调",
		"购买状态(成功失败)变动回调",
		"界面设置",
	},
	"ListView" = {
		"应保持纯逻辑",
		"支持动态添加",
		"支持水平,垂直",
		"支持子物体布局",
		"支持默认选中",
		"支持无限滑动(无缝链接)",
		"支持动态设置容器的大小",
		"支持动态改变子物品大小",
		"支持选中跳动",
		"支持创建动画",
		"支持翻页",
		"支持自动翻页动画",
	},
}

Progress = Progress or BaseClass(Component)
Progress.name = "Progress"
Progress.classList = {}
function Progress:New(parentClass)
	Progress.classList[parentClass] = true
end

function Progress:Delete()

end

function Progress:Clear(parentClass)
	parentClass[Progress_name]
end

function Progress:InitMemberData()
end

function Progress:InitHandler()
end

function Progress:InitUIElement()
	-- self.progressSlider = self.transform:Find("Main/Top/Progress"):GetComponent(Slider)
	-- self.progressRateText = UtilsUI.GetText(self.transform, "Main/Top/ProgressVal")
	-- self.progressVal = 0
end

function Progress:SetProgress(val,max)
	self.progressVal = val
	self.progressMax = max
end

--按顺序添加事件
function Progress:AddCallbackEvent( callback )
	table.insert(self.funcList,{callback})
end

function Progress:UpdateCB(val)
	local result = 0
	for i,v in ipairs(self.funcList) do
		local callback = v[1]
		if callback and type(callback) == "function" then
			result = callback(val)
		end
		if result ~= 0 then
			if result == 1 then
				return
			end
		end
	end
end

function Progress:Delete()
	if self.progressTweenId then
		Tween.Instance:Cancel(self.progressTweenId)
		self.progressTweenId = nil
	end
end

function Progress:EndCB()
	self.progressTweenId = nil
end

function Progress:AnimationStart()
	local updateCB = function(val) self:UpdateCB(val) end
	local endCB = function() self:endCB() end
	self.progressTweenId = Tween.Instance:ValueChange(self.progressVal, self.progressVal+delta, time, endCB, LeanTweenType.linear, updateCB).id
end

--val初始值 max初始最大值 grade等级
function Progress:InitAnimation(val,max,lev)
	self.progressLev = lev
	self.progressCount = 0--支持多级跳转
	self:SetProgress(val,max)

	local callback = function(val)
		--整数筛选
		val = math.floor(val)
		if val == self.progressVal then return 1 end

		val = val - self.progressCount
		if val >= self.progressMax then
			self.progressCount = self.progressCount + self.progressMax
			self.progressLev = self.progressLev + 1
			local newMax = self:GetMaxByLev(self.progressLev)
			self:SetProgress(val-self.progressMaxVal,newMax)
		else
			self:SetProgress(val,self.progressMaxVal)			
		end
		return 0
	end
	self:AddCallbackEvent(callback)
end

--根据等级获得最大值
function Progress:GetMaxByLev( lev )
	local breakCfgInfo = SkillConfigHelper.GetPassiveSkillBreakInfo(lev)
	return breakCfgInfo.exp
end









































ListView = ListView or BaseClass(Component)
ListView.name = "ListView"
function ListView:New(parentClass)
end

function ListView:Delete()

end

function ListView:Clear(parentClass)
end

function ListView:Init(setting)
end

function ListView:SetLayout()
	
end

function ListView:ScrollUpdate(value)
	local dirUp = true
	if dirUp then
		deltaSize = (container.y - mask.y)
		MaskRect一定
		viewRect = 将value转化成Container坐标系下的Rect
		local breakIndex = 1
		local show = false
		for i = getMidRect(self.readyList) , 1, -1 then
			show = true
			if show then
				setPositionShow(i)
			elseif notInHideList(i) then
				setPositionHide(i)
			else
				breakIndex = i
				break
			end
		end
		for i = getMidRect(self.readyList) , breakIndex, +1 then
			show = true
			if show then
				setPositionShow(i)
			elseif notInHideList(i) then
				setPositionHide(i)
			else
				break
			end
		end

		--重构self.readyList()
		for i = minIndex - cacheLev,minIndex then

		end
		for i = maxIndex, maxIndex + cacheLev then

		end
		setReadyList(minIndex-cacheLev,maxIndex+cacheLev)
	end
end

function ListView:GetRealPosition( ... )
	-- body
end

--设置滑动变化
function ListView:SetScrollCallBack()
	-- body
end

--动态改变Mask区域
function ListView:SetMaskRect()
	-- body
end

--缓存水平
function ListView:SetCacheLev()
	-- body
end

function ListView:SetItemRect( index )
	-- body
end

function ListView:GetItemPosition( index )
	self.containerY + (index-1)*(padding+cellSizeY) + top + pivotSize 
end





"ListView" = {
		"应保持纯逻辑",
		"支持动态添加",--子物体未采用链表，动态插入耗时
		"支持水平,垂直",
		"支持子物体布局",
		"支持默认选中",
		"支持无限滑动(无缝链接)",--通过将物体与数据分离 实现
		"支持动态设置容器的大小",
		"支持动态改变子物品大小",
		"支持多级子物体",
		"支持选中跳动",
		"支持创建动画",
		"支持翻页",
		"支持自动翻页动画",
		"支持特殊预制体 跟随滑动(即支持加子物体偏移)",
		"支持多页刷新(即滑动的底部，刷新其他数据)",
		"支持子物体位置偏移"，

-- 列表应保持简单  接口开放  可组合
-- 确定方案：
--		采用预制体循环使用(半动态即支持预制体事先创建,降低实例化消耗)
--		支持预制体布局控制(增加每行自定义布局，用于非常规的上2下3等布局)
--		滑动采用ScrollRect控制parent移动来同步所有预制体移动(弹性滑动,需借助ScrollRect完成)
--		


ListViewItemInternal = "_"
ListViewItem = ListViewItem or {}
function ListViewItem:__init(itemIndex, parent)
	self.parent 				= parent 		--父节点
	self.itemIndex 				= itemIndex 	--逻辑序号(自身唯一)

	self.dataIndex 				= dataIndex 	--数据序号(动态变化)
	self.itemRect 				= {0,0,0,0} 	--自身所占矩形区域
	-- self.logicPos 				= {0,0}  		--逻辑位置(相对父节点)
	-- self.realLocalPos 			= {0,0}			--实际位置(相对顶级父节点)

	------------- 以下针对含有子物体的属性
	-- 子物体本地位置 = 子物体逻辑位置 + 子物体相对位置偏移
	self.childItemList 			= {}			--子物体类列表
	self.childItemDataList 		= {}			--子物体数据列表
	self.childOffSet 			= 0 			--子物体相对位置偏移
	self.childLoginPosList 		= {} 			--子物体逻辑位置

	self.defaultSelectIndex 	= nil 			--默认选中序号
	self.currentSelectIndex 	= nil			--当前选中序号
	self.autoSelectFunc 		= nil 			--自动选中函数

	self.childMaskRect 			= nil 			--mask遮罩矩形区域
	self.childLayoutSetting		= nil

	self.childExpendAnimation  	= nil 			--子物体展开
	self.childCollapseAnimation	= nil 			--子物体收缩
end

function ListViewItem:__delete()	
end

-- 获取Rect
function ListViewItem:GetRect()
end

-- 获取逻辑位置(相对父节点)
function ListViewItem:GetLogicPos()	
end

-- 获取本地位置(相对父节点)
function ListViewItem:GetLocalPos()
end

-- 获取实际位置(相对顶级父节点)
function ListViewItem:GetRealLocalPos()
end

-- 获取子物体By 序号
function ListViewItem:GetChildItemByIndex(index)
	-- todo 将index转换成itemIndex
	local itemIndex = 1
	return self.childItemList[itemIndex]
end

-- 获取自身数据
function ListViewItem:GetData()
	if self.parent == nil then
		print("<color=#ff0000>GetData() but parent is nil ! RealItemIndex:%s</color>", self:GetRealItemIndex())
		return
	end
	if self.dataIndex == nil then
		print("<color=#ff0000>GetData() but dataIndex is nil !</color>")
		return
	end
	local dataList = self.parent.dataList
	if dataList == nil or type(dataList) ~= "table" then
		print("<color=#ff0000>GetData() but parent.dataList is nil or not a table !</color>")
		return
	end
	return dataList[self.dataIndex]
end

-- 获取自身实际序号(相对顶级父节点)
function ListViewItem:GetRealItemIndex()
	return self.itemIndex
end

-- 设置自动选中子物体函数
function ListViewItem:SetAutoSelectFunc()
end

-- 选中并跳转(跳转位置)
function ListViewItem:SelectAndDump(index)
end

-- 数据刷新
function ListViewItem:UpdateData(data)
end

ListView = ListView or BaseClass()
function ListView:__init(scrollRect, container, maskRect)
	self.parent 				= parent 		--父节点

	self.direction				= 0				--0为垂直方向 1为水平方向
	------------- 以下针对含有子物体的属性
	-- 子物体本地位置 = 子物体逻辑位置 + 子物体相对位置偏移
	self.childItemList 			= {}			--子物体类列表
	self.childItemDataList 		= {}			--子物体数据列表
	self.childOffSet 			= 0 			--子物体相对位置偏移
	self.childLoginPosList 		= {} 			--子物体逻辑位置

	self.defaultSelectIndex 	= nil 			--默认选中序号
	self.currentSelectIndex 	= nil			--当前选中序号
	self.autoSelectFunc 		= nil 			--自动选中函数
	self.childDataRecyclingFlag = false 		--子物体数据循环滑动

	self.childMaskRect 			= nil 			--mask遮罩矩形区域
	self.childLayoutSetting		= nil

	self.childExpendAnimation  	= nil 			--子物体展开
	self.childCollapseAnimation	= nil 			--子物体收缩

	self.childUpdateDataFunc 	= nil 			--子物体数据刷新函数

	self.itemDefalutNum 		= 5 			--初始创建预制体
	self.itemAutoMaxNum 		= true 			--自动判断最大预制体数量
	self.itemMaxNum 			= 20 			--最大预制体数量


	-- 开放事件接口
	-- 子物体新建事件
	-- 子物体隐藏事件
	-- 子物体数据刷新事件
	-- 子物体位置刷新事件(自身位置或父节点位置变化事件)

	-- 父节点位置刷新事件
	-- 子物体部分显示事件
	-- 子物体整体显示事件
	-- 子物体整体不显示事件

	-- 子物体所占区域
end

function ListView:ItemPool()
end

function ListView:GetItemFromPool()
end

function ListView:OnValueChanged()
	local cur_pox
	if self.direction == 0 then--垂直的
		cur_pox = self.parent.anchoredPosition.y
	elseif self.direction == 1 then--水平的
		cur_pox = self.parent.anchoredPosition.y
	end

	
end














-- ScrollRect   滑动处理
protected virtual void LateUpdate()
{
    if (!m_Content)
        return;

    EnsureLayoutHasRebuilt();
    UpdateScrollbarVisibility();
    UpdateBounds();
    float deltaTime = Time.unscaledDeltaTime;
    Vector2 offset = CalculateOffset(Vector2.zero);
    if (!m_Dragging && (offset != Vector2.zero || m_Velocity != Vector2.zero))
    {
        Vector2 position = m_Content.anchoredPosition;
        for (int axis = 0; axis < 2; axis++)
        {
            -- Apply spring physics if movement is elastic and content has an offset from the view.
            if (m_MovementType == MovementType.Elastic && offset[axis] != 0)
            {
                float speed = m_Velocity[axis];
                position[axis] = Mathf.SmoothDamp(m_Content.anchoredPosition[axis], m_Content.anchoredPosition[axis] + offset[axis], ref speed, m_Elasticity, Mathf.Infinity, deltaTime);
                if (Mathf.Abs(speed) < 1)
                    speed = 0;
                m_Velocity[axis] = speed;
            }
            -- Else move content according to velocity with deceleration applied.
            else if (m_Inertia)
            {
                m_Velocity[axis] *= Mathf.Pow(m_DecelerationRate, deltaTime);
                if (Mathf.Abs(m_Velocity[axis]) < 1)
                    m_Velocity[axis] = 0;
                position[axis] += m_Velocity[axis] * deltaTime;
            }
            -- If we have neither elaticity or friction, there shouldn't be any velocity.
            else
            {
                m_Velocity[axis] = 0;
            }
        }

        if (m_Velocity != Vector2.zero)
        {
            if (m_MovementType == MovementType.Clamped)
            {
                offset = CalculateOffset(position - m_Content.anchoredPosition);
                position += offset;
            }

            SetContentAnchoredPosition(position);
        }
    }

    if (m_Dragging && m_Inertia)
    {
        Vector3 newVelocity = (m_Content.anchoredPosition - m_PrevPosition) / deltaTime;
        m_Velocity = Vector3.Lerp(m_Velocity, newVelocity, deltaTime * 10);
    }

    if (m_ViewBounds != m_PrevViewBounds || m_ContentBounds != m_PrevContentBounds || m_Content.anchoredPosition != m_PrevPosition)
    {
        m_OnValueChanged.Invoke(normalizedPosition);
        UpdatePrevData();
    }
}

private void UpdatePrevData()
{
    if (m_Content == null)
        m_PrevPosition = Vector2.zero;
    else
        m_PrevPosition = m_Content.anchoredPosition;
    m_PrevViewBounds = m_ViewBounds;
    m_PrevContentBounds = m_ContentBounds;
}