
ValueChange = ValueChange or BaseClass()

function ValueChange:__init()
	if ValueChange.Instance == nil then
		ValueChange.Instance = self
	end
end

function ValueChange:__delete()
	
end

function ValueChange:GetStart()
	UtilsBase.TimerDelete(self, "timer")
	UtilsBase.TimerDelete(self, "timer")
	self.deltaTime = 100
	self.deltaNum = nil--控制是否开始减速
	--每次跳多少格
	self.timer = TimerManager.Add(nil, self.deltaTime, function() self:Loop() end)
	self.timer1 = TimerManager.Add(5000, function() self:GetResult() end)
end

--times代表需要走的步数
--time代表时间
function ValueChange:MoveStop(times, time)
	self.lastVal = times
	self.deltaNum = 0
	self.tweenId_1 = Tween.Instance:ValueChange(times, 0, time, 
		function()
			self.timer3 = TimerManager.Add(3000, function() self:Close() end)
			UtilsBase.TimerDelete(self, "timer1")
			if self.protoFlag == false then
				self.protoFlag = true
				TreasureBoxManager.Instance:Send14902()
				self:ShowNotice()
			end
		end, LeanTweenType.easeOutQuad,
		function(val)
			local delta = math.floor(self.lastVal - val)
			if self.deltaNum == 0 and delta > 0 then
				self.deltaNum = delta
				self.lastVal = self.lastVal - delta
			end
		end
	).id
end

function ValueChange:GetStop()
	if self.timer then
		UtilsBase.TimerDelete(self, "timer")
	end
	if self.timer1 then
		UtilsBase.TimerDelete(self, "timer1")
	end
end

function ValueChange:GetResult()
	UtilsUI.SetActive(self.status0, false)
	UtilsUI.SetActive(self.status1, true)
	if self.deltaNum == nil then
		local times = 20	-- 代表步数
		local time = 3 		-- 代表时长 time应保持总大于 times*self.deltaTime/1000
		self.endIndex = TreasureBoxManager.Instance:GetFinalIndex(self.battle_id,self.id)
		if self.endIndex > self.index then
			times = self.endIndex - self.index
		elseif self.endIndex < self.index then
			times = self.endIndex + 15 - self.index
		elseif self.endIndex == self.index then
			times = 15--格子总数
		end
		if times < 5 then times = times + 15 end--保证时间间隔长度
		self:MoveStop(times,time)
	end
end



function ValueChange:Loop()
	if self.protoFlag and self.deltaNum == 0 then
		UtilsBase.TimerDelete(self, "timer")
	end 

	self.boxItemList[self.index]:Select(false)
	if self.deltaNum then
		self.index = self.index + self.deltaNum
		self.deltaNum = 0
	else
		self.index = self.index + 1
	end
	if self.index > 15 then self.index = 1 end
	self.boxItemList[self.index]:Select(true)
end