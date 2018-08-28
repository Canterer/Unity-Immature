function ZSInitProto()
	-- 协议控制
	ZS_Black_Flag = true	--控制是否开启 黑名单（控制不输出范围）
	ZS_White_Flag = true	--控制是否开启 白名单（控制输出范围） false代表输出所有不在黑名单的
	-- 当白名单开启时，仅输出 不在黑名单且在白名单
	-- 当白名单不开启时，仅输出 不在黑名单
	ZS_White_List = {
		-- { protoHead = 136 }, --监听该协议头
		-- { protoHead = 103 }, --监听该协议头
		-- { protoHead = 117 }, --监听该协议头
		-- { protoHead = 115 }, --监听该协议头
		-- { protoHead = 146 }, --监听该协议头
		-- { protoHead = 116 }, --监听该协议头		-- { protoHead = 146 }, --监听该协议头


		{ min = 10550}, --监听该协议
		-- { min = 10408}, --监听该协议
		{ min = 10034, max = 10035 }, --监听该区域协议
	}
	ZS_Black_List = {
		-- { protoHead = 110 },
		-- { min = 1, max = 2},
		{ min = 1011},--心跳包
		{min = 10106},
		{min = 10107},
		{min = 10110},
	}
	ZS_Serialize_Limit_Flag = true		--控制打印限制
end

function ZSTest()
	local test1 = function()

		local depthList = {}
		depthList[0] = {}
		ZS(#depthList)

		-- NewTitleManager.Instance.main:UpdateTop(true)
		-- SkyArenaManager.Instance.model:OpenFightPanel()
		-- EventMgr.Instance:Fire(event_name.active_time_change)
		-- TipsManager.Instance.model:ShowTips({vo = ItemVO.New({base_id=20112})})
		-- RemindManager.Instance:LogRemindStatus( 12000 )
		-- local tableA = {tag="A"}
		-- local tableB = {tag="B"}
		-- tableA.key = tableB
		-- tableB.key = tableA
		-- UtilsBase.dump(tableB,"test table")
		-- WingManager.Instance.model:OpenWingQualityTipsPanel()
		-- WingManager.Instance.model:OpenGetNewWingPanel(args)
		-- TipsManager.Instance.model:ShowAttrScore({attrList={{name="1",val=5001},{name="3",val=295},{name="3",val=295},{name="3",val=295},{name="3",val=295},{name="3",val=295},{name="3",val=295},{name="3",val=295}},score="000"})
		-- OpenServerActivityManager.Instance.model:OpenMain(OpenServerActivityTreeTabIndex.Rank_Activity_Level)
		-- WindowManager.Instance:OpenWindowById(WindowConfig.WinID.dragonWindow,{tab1=2,level = 0})
		-- WindowManager.Instance:OpenWindowById(WindowConfig.WinID.open_server_charge)
		-- WindowManager.Instance:OpenWindowById(WindowConfig.WinID.new_title_panel)
		-- WindowManager.Instance:OpenWindowById(WindowConfig.WinID.agendaWindow, {})
        -- WindowManager.Instance:OpenWindowById(WindowConfig.WinID.gemMerge, {equipType = 5 ,holeId = 1 ,gemType = 3,gemId = 12102})

		-- OpenServerChargeManager.Instance.model:OpenMain()
		-- FirstChargeManager.Instance.model:OpenReturnCharge()
		-- StoreManager.Instance.model:OpenVipPanel()
		-- PanelShowQueue.Instance:Show(PanelShowVo.New(WindowConfig.WinID.firstCharge))

		-- GuideManager.Instance:PlayGuide(99999, function() ZS("22") end)
	end
	test1()
    -- QuickShopManager.Instance:OpenAssetOrItem(12800)
    -- NoticeManager.Instance:ShowCollectDesc(15251)
    -- NoticeManager.Instance:ShowCollectDesc(14037)
    -- WindowManager.Instance:OpenWindowById(9802)
    -- TreasureBoxManager.Instance.model:OpenTreasureBoxPanel()
    -- ZS(NoticeConfigHelper.GetParseContent(19006, 20))
    -- SkillManager.Instance.model:OpenSkillPassiveBreakPanel()
    -- local groupIndex,itemIndex = RankMapOfTreeIndexAndRankType(RankType.MountTotalScore)
    -- RankManager.Instance.model:OpenMain({[1]=groupIndex,[2]=itemIndex})
    -- SkillManager.Instance.model:OpenNewPassiveSkill(75001)
    -- ChangeNameManager.Instance.model:OpenChangeNamePanel()
    -- SkillManager.Instance.model:OpenLifeSkillPopGradePanel()
    -- NoticeManager.Instance:ShowParseTopById(5012,2)
    -- ZSLogTable(RoleManager.Instance.roleData,"roleData")

    -- local randomindex1 = math.random(1, Config.DataCreateRole.data_create_role_random_name_length)
    -- local randomindex2 = math.random(1, Config.DataCreateRole.data_create_role_random_name_length)
    -- ZS(randomindex1,randomindex2,Config.DataCreateRole.data_create_role_random_name_length)

    -- local statsdata = StoryManager.Instance.quest_statsList[4]
    -- ZS(statsdata,"ZSLog statsdata")
    -- QuestOfferManager.Instance:IsAutoNext()
    local test2 = function()
    	ChatManager.Instance.model:ShowMain({Channel = ChatEumn.Channel.World})
	    --描述文字中预留%s 用于填充物品描述 举例：%s是难得的珍品 =》 [完美幸运石]是难得的珍品
	    --描述颜色,描述文字,参数1,参数2,参数3,itemId,itemNum,id} 市场物品分享
	    local form = _T("来自市场的%s分享")
	    local formColor = "ff0000"
	    local base_id = 134501
	    local num = 1
	    local id = 1

	    local args = string.format("#%s,%s,%s,%s,%s,%s,%s,%s",formColor,form,2,4,1,base_id,num,id)
	    local matchString,appendString = MarketConfigHelper.GetSharedDesc(form,formColor,base_id,num)
	    args = string.format(args,"%%s")
	    local element = {}
	    element.matchString = matchString
	    element.sendString = string.format("{b,57,%s}", args)
	    element.appendString = appendString
	    ChatManager.Instance.model:InputElementMsg(element, ChatEumn.ExtraContext.chat)
	end
	local test3 = function()
		local setting = {
            type = ComfirmType.Normal,
            str = _T("您当前有一次免费改名机会，是否前往改名？"),
            surecb = function()
                self.model:OpenChangeNamePanel()
            end,
            cancelcb = function()
            end,
            suretext = _T("确认"),
            canceltext = _T("取消"),
        }
        NoticeManager.Instance:ShowComfirm(setting)
	end
end


-- -- ----------------------------------------------------------
-- -- 公共函数库
-- -- ----------------------------------------------------------
-- import('UnityEngine')
-- import('UnityEngine.UI')
-- import('UnityEngine.Events')

-- import('Game.Logic')
-- import('Game.Asset')

-- -- 初始化后由ctx.IsDebug值替换，修改debug模式请在base_setting.txt文件中修改
-- IS_DEBUG = true
-- DEVELOPER_DEBUG = false

-- print = function(...)
--     if IS_DEBUG then
--     	local args = {...}
--     	local new_args = {}
--     	for _, v in ipairs(args) do
--     		table.insert(new_args, tostring(v))
--     	end
--         -- 打印父节点
--         local track_info = debug.getinfo(2, "Sln")
--         local str = string.format("From %s:%d in function `%s`", track_info.short_src,track_info.currentline,track_info.name or "")
--         Debug.Log(string.format("%s\n%s", table.concat(new_args, " "), str))
--     end
-- end

-- print("print(\"ZSLog\")")
-- Debug.Log("Debug.Log(\"ZSLog\")")
-- Debug.LogWarning("Debug.LogWarning(\"ZSLog\")")
-- Debug.LogError("Debug.LogError(\"ZSLog\")")

-- Debug.LogFormat("Debug.Log(\"{0}\")","ZSLog")
-- Debug.LogWarningFormat("Debug.LogWarningFormat(\"{0}\")","ZSLog")
-- Debug.LogErrorFormat("Debug.LogErrorFormat(\"{0}\")","ZSLog")

function ZSInit()
	ZS_String = ""
	ZS_Repeart_String = {}

	ZS_Log_Count = 0
	ZS_SendProto_Count = 0
	ZS_RecvProto_Count = 0

	ZS_ZS_Count = 0 --插针
	ZS_Color_ZS = "#FFCC44"
	ZS_Color_Log = "#7CFF88"
	ZS_Color_Table = "#FF27C9"
	ZS_Color_Proto_Send = "#FF7733"
	ZS_Color_Proto_Recv = "#9CEC42"

	ZS_Profiler_Flag = false--是否开启

	ZSInitProto()
end

ZSInit()

function ZS(...)
	local num_args = select("#", ...)	
	local track_info = debug.getinfo(2, "Sln")
	ZS_String = string.format("From %s:%d ## ", track_info.short_src, track_info.currentline)
	if num_args == 0 then
		ZS_ZS_Count = ZS_ZS_Count == 9 and 0 or (ZS_ZS_Count+1)
		ZS_String = ZS_String..ZSRepeart( ZS_ZS_Count,15)
	elseif type(select(1,...)) == "table" then
		ZSLogTable( select(1,...), select(2,...) )
		return
	else
		for i = 1, num_args do
	        local arg = select(i, ...)
	        ZS_String = ZS_String.." "..tostring(arg)
    	end
	end

	ZS_Debug_Log( ZS_String, ZS_Color_ZS )
	ZS_String = ""
end


-- 一个参数时  字符串 打印变量名为该字符串的本地变量
-- 两个参数时	Table,字符串 打印Table[字符串]
-- 两个参数时	字符串,变量	 打印 字符串 = 变量
function ZSLog(...)
	ZS_Log_Count = ZS_Log_Count + 1
	ZS_String = ZSRepeart( "#" , 10 )
	ZS_String = ZS_String.." "..ZS_Log_Count.." ##\n"

	local num_args = select("#", ...)
	local table,varName,value
	if num_args == 1 then
		local function getVarValue( name )
			local value,found
			--本地变量
			local i = 1
			while true do
				local n,v = debug.getlocal(3,i)
				if not n then break end
				if n == name then
					value = v
					found = true
				end
				i = i + 1
			end
			if found then return value else return nil end
		end

		varName = select(1, ...)
		value = getVarValue(varName)
		if type(value) == "table" then
			ZSLogTable(value,varName)
			return
		else
			ZS_String = ZS_String .. string.format("%s = %s",varName,tostring(value))			
		end
	elseif num_args == 2 then
		if type(select(1, ...)) == "table" then
			table = select(1, ...)
			varName = select(2, ...)
			value = table[varName]
			if type(value) == "table" then
				ZSLogTable(value,string.format("Table.%s",varName))
				return
			elseif tonumber(varName) then
				ZS_String = ZS_String .. string.format("Table[%s] = %s",tonumber(varName),tostring(value))
			else
				ZS_String = ZS_String .. string.format("Table.%s = %s",varName,tostring(value))
			end
		elseif type(select(1, ...)) == "string" then
			varName = select(1, ...)
			value = select(2, ...)
			if type(value) == "table" then
				ZSLogTable(value,varName)
				return
			else
				ZS_String = ZS_String .. string.format("self.%s = %s",varName,tostring(value))
			end
		end
	end
	local track_info = debug.getinfo(2, "Sln")
	ZS_String = ZS_String..string.format("\nFrom %s:%d", track_info.short_src, track_info.currentline)
	ZS_Debug_Log( ZS_String,ZS_Color_Log )
	ZS_String = ""
end

function ZSProto(flag, cmd, data)
	if ZSProtoFilter( cmd ) == false then return end
	ZS_String = ZSRepeart( flag==1 and ">>" or "<<<<",10)
	ZS_String = ZS_String..ZSRepeart("proto ## ")..cmd..ZSRepeart(" ##\n")
	ZS_String = ZS_String..ZSSerializeTable(data, nil, true)
	ZS_Debug_Log( ZS_String,flag==1 and ZS_Color_Proto_Send or ZS_Color_Proto_Recv )
	ZS_String = ""
end

-- 序列化
-- 将传入的Obj 字符串化+
function ZSSerialize(key, value, newline, depth, tableList)
	local tempStr = ""
	local space = "---| "
	depth = depth or 0
	newline = newline == nil and true or newline
	tableList = tableList or {}--记录解析过的table

	tempStr = tempStr .. ZSRepeart(space, depth)
	if key then
		if type(key) == "number" then
			tempStr = tempStr .. "[" .. key .. "] = "
		elseif type(key) == "string" then
			-- if tempKey == "traceinfo" then tempValue = {} end
			if tonumber(key) then
				tempStr = tempStr .."\"".. key .."\" = "
			else
				tempStr = tempStr .. key .. " = "
			end
		else
			tempStr = tempStr .. key
		end
	end

	if value == nil then
		tempStr = tempStr.."nil"
	elseif type(value) == "number" or type(value) == "boolean" then
		tempStr = tempStr..tostring(value)
	elseif type(value) == "string" then
		tempStr = tempStr.."\""..tostring(value).."\""
	elseif type(value) == "function" then
		tempStr = tempStr.."【function】"
	elseif type(value) == "userdata" then
		tempStr = tempStr.."【userdata】"
	elseif type(value) == "table" then
		if tableList[value] == nil then
			if next(value) then
				tempStr = tempStr .."<"..tostring(value).."> {"..( newline and "\n" or " ")
				tableList[value] = key
				for k,v in pairs(value) do
					tempStr = tempStr .. ZSSerialize(k, v, newline, depth+1, tableList) .. ( newline and "\n" or " ")
				end
				tempStr = tempStr..ZSRepeart(space, depth).."}"
			else
				tempStr = tempStr .."<"..tostring(value).."> {}"
			end
		else--重复table(所指地址相同)
			tempStr = tempStr.."【"..tableList[value].."】<"..tostring(value)..">{...}"
		end
	end

	return tempStr
end

-- 反序列化
-- function ZSUnserialize(str)
--     return assert(loadstring("local tmp = " .. str .. " return tmp"))()
-- end

function ZSLogTable(table,str)
	ZS_String = ZS_String..ZSSerializeTable(table,str)
	ZS_Debug_Log( ZS_String,ZS_Color_Table )
	ZS_String = ""
end

function ZSRepeart( repeartStr , n )
	if n == nil then n = 1 end
	if ZS_Repeart_String[ repeartStr.."_ZS_"..n ] == nil then
		ZS_Repeart_String[ repeartStr.."_ZS_"..n ] = string.rep(repeartStr, n)
	end
	return ZS_Repeart_String[ repeartStr.."_ZS_"..n ]
end

function ZSProtoFilter( cmd )
	if ZS_Black_Flag then
		for k,v in pairs(ZS_Black_List) do
			if v.protoHead then
				if v.protoHead == math.floor(cmd/100) then return false end
			else
				v.max = v.max and v.max or v.min
				if cmd >= v.min and cmd <= v.max then return false end
			end
		end
	end

	if ZS_White_Flag then
		for k,v in pairs(ZS_White_List) do
			if v.protoHead then
				if v.protoHead == math.floor(cmd/100) then return true end
			else
				v.max = v.max and v.max or v.min
				if cmd >= v.min and cmd <= v.max then return true end
			end
		end
	end

	return not ZS_White_Flag
end

function ZS_Debug_Log( str,color )
	local ind_s,ind_e = string.find(str, "\n")
	if ind_s then
		ind_s,ind_e = string.find(str, "\n",ind_e+1)
	end
	if ind_s then
		Debug.LogWarning( string.format(ZSRepeart("<color=%s>"),color)..string.sub(str,1,ind_s-1)..ZSRepeart("</color>").."\n"..string.format(ZSRepeart("<color=%s>"),color)..string.sub(str,ind_e+1)..ZSRepeart("</color>") )
	else
		Debug.LogWarning( string.format(ZSRepeart("<color=%s>"),color)..str..ZSRepeart("</color>") )
	end
end

function ZSGetSerializeMaxLengthByDepth(depth)
	local lengthList = {
		[0] = 40000,
		[1] = 10000,
		[2] = 2000,
		[3] = 500,
		[4] = 100,
		[5] = 16,
		[6] = 2,
	}
	return ZS_Serialize_Limit_Flag and lengthList[depth] or 40000
end

function ZSGetSerializeMaxLineNumByDepth(depth)
	local lineNumList = {
		[0] = 400,--最少显示4个【1】或5个【2】或80个【3】
		[1] = 300,--最少显示1个【2】或16个【3】
		[2] = 80,
		[3] = 20,
		[4] = 1,
		[5] = 1,
		[6] = 1,
	}
	return ZS_Serialize_Limit_Flag and lineNumList[depth] or 1000
end

function ZSColorShow()
	local r = 0
	local g = 0
	local b = 0xf
	local color
	for i = 0, 0x3f do
		if i < 0x10 then
			g = i
		elseif i < 0x20 then
			b = 0x1f - i
		elseif i < 0x30 then
			r = i - 0x20
		elseif i <= 0x3f then
			g = 0x3f- i
		end
		color = r*0x110000+g*0x1100+b*0x11
		ZS_String = ZS_String..string.format("\n<color=#%.6x>#%.6x</color>",color,color)
		-- ZS_String = ZS_String..string.format("<color=#%.6x>#</color>",color)
	end
	ZS(ZS_String)

	--打印显示
	local table = { [1] = 1, a = "a",string = "", nilV = nil }
	ZS()
	ZS("ZS(...)")
    ZSLog("table")
    ZSLog(table,"a")
    ZSLogTable(table,"table")
    ZSProto(1,100,{})
    ZSProto(2,111,{})
end
-- ZSColorShow()

function ZS_GM_Update()
	if Input.GetKeyDown(KeyCode.Escape) then
        ZSTest()
      --   local test =  function ()
      --   	local appendString = _T("市场物品分享[1[2[2]]3]测试")
      --   	local matchString = _T("市场物品分享%[.*%]测试")
      --   	local sendString = _T(" bbbb ")
      --   	local sendMsg = string.gsub(appendString, matchString, sendString, 1)
		    -- print(appendString)
		    -- print(matchString)
		    -- print(sendString)
		    -- print(sendMsg)
      --   end
        -- test()
    end
end

import("UnityEngine.Profiling.Profiler")
ProfilerSample = UnityEngine.Profiling.Profiler
function ZS_Profiler(str)
	-- if (str ~= nil) == ZS_Profiler_Flag then
	-- 	if str then
	-- 		ZS("ZS_Profiler key:"..str.." is error")
	-- 	end
	-- 	return
	-- end
	-- ZS_Profiler_Flag = not ZS_Profiler_Flag
	-- if str then
	-- 	ProfilerSample.BeginSample(str)
	-- else
	-- 	ProfilerSample.EndSample()	
	-- end
end



----------------------------------------------------------------
ZSLogNodeList = ZSLogNodeList or {}
ZSLogNodeTotalIndex = 100
ZSLogNodeIndex = 0
ZSLogLastIndex = 0
function InitZSLogNodeList()
	if next(ZSLogNodeList) then
		ZS("try InitZSLogNodeList() again!!!")
		return
	end
	local lastNode = nil
	for i=1,ZSLogNodeTotalIndex do
		local node = { last = lastNode, next = nil, parent = nil, newLineNum = 0, depth = 0, value = nil}
		if lastNode then
			lastNode.next = node
		end
		lastNode = node
		ZSLogNodeList[i] = node
	end
end
InitZSLogNodeList()

function ZSLogNodeListExpend(inc)
	local lastNode = ZSLogNodeList[ZSLogNodeTotalIndex]
	for i=ZSLogNodeTotalIndex+1,ZSLogNodeTotalIndex+inc do
		local node = { last = lastNode, next = nil, parent = nil, newLineNum = 0, depth = 0, value = nil}
		if lastNode then
			lastNode.next = node
		end
		lastNode = node
		ZSLogNodeList[i] = node
	end
	ZSLogNodeTotalIndex = ZSLogNodeTotalIndex + inc
end

function GetZSLogNode(index)
	local logNode,clearFlag
	if index == nil or index > ZSLogNodeTotalIndex then--获取新节点
		ZSLogNodeIndex = ZSLogNodeIndex + 1
		index = ZSLogNodeIndex
		clearFlag = true
	end
	
	if index > ZSLogNodeTotalIndex then
		ZSLogNodeListExpend(100)
		clearFlag = true
	end
	local logNode = ZSLogNodeList[index]
	if clearFlag then
		logNode.parent = nil
		logNode.newLineNum = 0
		logNode.depth = 0
		logNode.value = nil
	end

	if ZSLogLastIndex <= index then
		ZSLogLastIndex = index
	else
		ZS(index,ZSLogLastIndex)
		ZSLogLastIndex = 0
	end
	return logNode
end

function ZSSerializeTable(value, key, newline, depth, tableList)
	local tempStr = ""
	local space = "---| "
	depth = depth or 0
	newline = newline == nil and true or newline
	tableList = tableList or {}--记录解析过的table
	
	-- 重建索引表（遍历表）记录表的结构信息
	local rootNode = ZSReBuild(value,key,depth,nil)
	if rootNode then
		ZS(ZSLogNodeIndex,rootNode.newLineNum)
	end
	local depthList = {}
	for i=1,ZSLogNodeIndex do
		local logNode = GetZSLogNode(i)
		local depth = logNode.depth
		if depthList[depth] == nil then
			depthList[depth] = {}
		end
		table.insert(depthList[depth],logNode)
	end

	if next(depthList) == nil then
		ZSLogNodeIndex = 0
		return ZSSerialize(key,value)
	else
		ZS_String = ZSReLog(depthList,newline)
		ZSLogNodeIndex = 0
		return ZS_String
	end
end

-- 重建索引表（遍历表）记录表的结构信息
function ZSReBuild(value,key,depth,tableList)
	depth = depth or 0
	tableList = tableList or {}
	
	if type(value) == "table" and next(value) then
		-- 从未解析过的table 且table的深度不高
		if tableList[value] == nil then
			tableList[value] = true
			local logNode = GetZSLogNode()
			logNode.depth = depth
			logNode.value = value
			logNode.key = key

			local newLineNum = 2
			for k,v in pairs(value) do
				local subLogNode,subNewLineNum = ZSReBuild(v,k,depth+1,tableList)
				newLineNum = newLineNum + subNewLineNum
				if subLogNode then
					subLogNode.parent = logNode
				end
			end
			logNode.newLineNum = newLineNum
			return logNode,newLineNum
		else
			return nil,1
		end
	else
		return nil,1
	end
end

function ZSReBuildFilter(key,value,depth)
	local rootNode = GetZSLogNode(1)
	if rootNode.newLineNum < 1300 and depth > 8 then 
		return false
	end
	return true
end

-- 反复裁剪打印长度和内容
function ZSReLog(depthList,newline)
	local rootNode = GetZSLogNode(1)
	local totalNum = rootNode.newLineNum-- 总行数
	local depth = rootNode.depth-- 为0
	local tempStr = ""

	local cutFlag = false
	-- if rootNode.newLineNum > 3*ZSGetSerializeMaxLineNumByDepth(0) then
	-- 	cutFlag = true
	-- else
	-- 	local depthIndex = #depthList
	-- 	if depthIndex >= 4 and #depthList[depthIndex] + #depthList[depthIndex-1] > ZSGetSerializeMaxLineNumByDepth(0) then
	-- 		cutFlag = true
	-- 	end
	-- end
	if cutFlag then
		-- 仅裁剪 总行数 从上至下
		local maxLineNum = ZSGetSerializeMaxLineNumByDepth(depth)
		local depthIndex = #depthList--注意表是从0开始的 #depthList求的是最深子节点深度
		while( totalNum > maxLineNum ) do--不用考虑maxDepthIndex小于0的情况
		-- if totalNum > ZSGetSerializeMaxLineNumByDepth(depth) then
			-- 关闭最深的table  使其显示一行
			local nodeList = depthList[depthIndex]
			if nodeList then
				for i=#nodeList,1 do
					local node = nodeList[i]
					local parent = node.parent
					local changeLineNum = node.newLineNum - 1
					node.newLineNum = 1					
					while( parent ) do--更新表结构 parent为nill的是根节点
						local parentLineNum = parent.newLineNum
						parentLineNum = parentLineNum - changeLineNum
						parent = parent.parent
					end
					totalNum = totalNum - changeLineNum
					if totalNum < maxLineNum then
						break
					end
				end
			end
			depthIndex = depthIndex - 1
		end
	else
		-- 裁剪  从下至上
		local depthIndex = #depthList--从最深的节点处理 注意 depthList从0开始 
		while( depthIndex > 0 ) do--根节点 depth为0 根节点不参与裁剪
			local nodeList = depthList[depthIndex]
			local maxLineNum = ZSGetSerializeMaxLineNumByDepth(depthIndex-1)--取其父节点的最大数限制
			-- ZS(depthIndex,#nodeList,maxLineNum)
			if nodeList then
				for i,logNode in ipairs(nodeList) do
					local parentNode = logNode.parent
					if parentNode.newLineNum > maxLineNum then
						local changeLineNum = logNode.newLineNum - 1
						logNode.newLineNum = 1
						while( parentNode ) do--更新表结构 parent为nill的是根节点
							parentNode.newLineNum = parentNode.newLineNum - changeLineNum
							parentNode = parentNode.parent
						end
						ZS(logNode.parent.newLineNum,maxLineNum,depthIndex,rootNode.newLineNum,changeLineNum)
					end
				end
			end
			depthIndex = depthIndex - 1
		end
		ZS(depthIndex,rootNode.newLineNum)
	end
	local tempStr = ZSSerializeLog(nil,nil,newline,depth,nil,rootNode)
	return tempStr
end

-- rootNode 
function ZSSerializeLog(key, value, newline, depth, tableList, logNode)
	if logNode then
		key = logNode.key
		value = logNode.value
	end
	local tempStr = ""
	local space = "---| "
	depth = depth or 0
	newline = newline == nil and true or newline
	tableList = tableList or {}--记录解析过的table

	tempStr = tempStr .. ZSRepeart(space, depth)
	if key then
		if type(key) == "number" then
			tempStr = tempStr .. "[" .. key .. "] = "
		elseif type(key) == "string" then
			-- if tempKey == "traceinfo" then tempValue = {} end
			if tonumber(key) then
				tempStr = tempStr .."\"".. key .."\" = "
			else
				tempStr = tempStr .. key .. " = "
			end
		else
			tempStr = tempStr .. key
		end
	end

	if value == nil then
		tempStr = tempStr.."nil"
	elseif type(value) == "number" or type(value) == "boolean" then
		tempStr = tempStr..tostring(value)
	elseif type(value) == "string" then
		tempStr = tempStr.."\""..tostring(value).."\""
	elseif type(value) == "function" then
		tempStr = tempStr.."【function】"
	elseif type(value) == "userdata" then
		tempStr = tempStr.."【userdata】"
	elseif type(value) == "table" then
		if tableList[value] == nil then
			if next(value) then
				if logNode then
					if logNode.newLineNum == 1 then
						tempStr = tempStr .."<"..tostring(value).."> {...}(too leng)"
					else
						tempStr = tempStr .."<"..tostring(value).."> {"..( newline and "\n" or " ")
						local nextNode = logNode.next
						for k,v in pairs(value) do
							if nextNode and v == nextNode.value then
								ZS(nextNode.depth,nextNode.newLineNum)
								tempStr = tempStr .. ZSSerializeLog(nil, nil, newline, depth+1, tableList, nextNode) .. ( newline and "\n" or " ")
								nextNode = nextNode.next
								-- 跳过已经打印过的子节点
								while(nextNode.depth > logNode.depth+1) do
									nextNode = nextNode.next
								end
							else
								if nextNode then
									ZS(nextNode.depth,logNode.depth,nextNode.key)
								end
								tempStr = tempStr .. ZSSerializeLog(k, v, newline, depth+1, tableList) .. ( newline and "\n" or " ")
							end
						end
						tempStr = tempStr..ZSRepeart(space, depth).."}"
					end
				else
					tempStr = tempStr .."<"..tostring(value).."> {"..( newline and "\n" or " ")
					tableList[value] = key
					for k,v in pairs(value) do
						tempStr = tempStr .. ZSSerializeLog(k, v, newline, depth+1, tableList) .. ( newline and "\n" or " ")
					end
					tempStr = tempStr..ZSRepeart(space, depth).."}"
				end
			else
				tempStr = tempStr .."<"..tostring(value).."> {}"
			end
		else--重复table(所指地址相同)
			tempStr = tempStr.."【"..tableList[value].."】<"..tostring(value)..">{...}"
		end
	end

	return tempStr
end