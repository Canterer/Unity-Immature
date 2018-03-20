--需求：
--可以设置滑动方向
--可以设置 并排数量(即与滑动垂直的方向，并排数量)
--可以设置默认选中
--提供选中设置
--无视锚点、轴心点
--支持创建时的动画载入
--无需管理对象释放等问题
--灵活的布局设置
--上下左右滑动的事件监听回调
--灵活查看物品的 index 即 多方的map映射
--点击的回调函数
--滑动事件 
-- 滑动速度的 变速调控  需要监听滑动长按时间 距离
--可以动态修改回调函数

--设置隐藏方式
--设置是否滑动
--支持翻页
--支持无限滑动效果

--返回可见的子物体数量 
--返回总的子物体数量
--设置默认生成子物体数量
--支持树形结构


--初始化

--Init(content,itemPrefab,itemClass)
--设置方向 并排数量 默认生成数量
--设置布局 间距(上下、左右)  去掉边距
--滑动  采用偏移
--动态更新数据接口

--数据采用环形数组组织  --链表或哈希表模拟

--重点是刷新
--update()
	

-- -----------------------------------------
-- name:ListView
-- desc:
-- author:胡帅
-- -----------------------------------------
ListView = ListView or BaseClass()

-- content 列表子项的父物体(一般会需要Mask遮罩)
-- itemPrefab 列表子项的预制体
-- itemClass 列表子项的控制类
-- direciton 列表子项排列方向、(若存在滑动,则也代表滑动监听方向)
-- row_col 对应每个排列方向上并列子项数量
function ListView:Init(content,itemPrefab,itemClass,direction,row_col, defalutItemNum)
	self.content = content
	self.itemPrefab = itemPrefab
	self.itemClass = itemClass
	self.direction = direction -- false:"Horizontal" true:"Vertical"
	self.row_col = row_col -- direction 上的item数量
end

function ListView:InitMemberData()
	-- self.itemList = {}--有序表
	-- self.dataList = {}--Key-Value表
	-- self.defalutSelect = nil --默认选中
	-- self.itemCallBack = nil
	-- self.currentSelectItem = {}
	self.itemRectList = {}
	self.itemPositionList = {}
end

function ListView:InitScrollRect( scrollTransform )
	self.scrollRect = scrollTransform or nil -- 传入scrollrect的Transform将启用自动隐藏
	if self.scrollRect then
		self.eventTrigger = self.scrollRect:AddComponent(EventTrigger)
		self:AddTriggerEvent( EventTriggerType.Scroll, function(triggerEvent) self:OnScroll(triggerEvent) end )
	end
end

function ListView:AddTriggerEvent( eventID,callBack )
	local entry = UnityEngine.EventSystems.EventTrigger.Entry()
	entry.eventID = eventID

	local CB = function( triggerEvent)--UnityEngine.EventTrigger.EventTrigger.TriggerEvent
		callBack(triggerEvent)
	end
	entry.callBack.AddListener(CB)
	if self.eventTrigger then
		self.eventTrigger.triggers.Add(entry)
	end
end

-- 1.添加事件监听  滑动(点击不处理,最好有最小滑动距离限制)
-- 2.处理好实例化元素的位置更新  获取(记录)元素的大小  保证记录的实时有效性
-- 3.处理好数据的更新问题
function ListView:OnScroll( triggerEvent )
	-- baseEventData as PointerEventData
	-- ZSLogTable(triggerEvent,"OnScroll triggerEvent")
	-- local value = 1
	-- self.itemOffset = self.itemOffset + value
	-- if self.direction then
	-- 	for k,v in pairs(self.itemPositionList) do
	-- 		v.x = v.x + self.itemOffset
	-- 	end
	-- else

	-- end
end

--从当前选中的index  上下开始计算位置 直至完全超出显示区域
function ListView:UpdatePosition()
end
-- ？ 如何处理长滑动 ？
function ListView:UpdateData( dataList )
	if dataList then
		self.dataList = dataList
	end

	--item 与 data
end










-- function ListView:SetLayout()
-- 	self.cspacing = 1 --间距
-- 	self.rspacing = 2 --间距
-- 	self.border =  --边距
-- end

-- function ListView:GetItemByIndex( index )
-- end

-- -- DataKey 指 数据的地址  一般数据都会申明遍历持有
-- function ListView:GetItemByDataKey( data )
-- end

-- --设置列表选中回调
-- function ListView:SetClickItemCallBack( callBack )
-- 	self.itemCallBack = callBack
-- end

-- function ListView:GetCurrentSelectItem()
-- end

-- -- 设置水平布局
-- function ListView:SetHorizontalLayout( border, spacing )
-- 	if self.direction then

-- 	end
-- end

-- -- 设置垂直布局
-- function ListView:SetVerticalLayout( border, spacing )
-- 	if not self.direction then
		
-- 	end
-- end

-- function ListView:UpdateData( dataList )
-- end
