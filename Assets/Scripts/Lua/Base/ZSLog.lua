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

--         Log.Debug(string.format("%s\n%s", table.concat(new_args, " "), str))
--     end
-- end

print("print(\"ZSLog\")")
Debug.Log("Debug.Log(\"ZSLog\")")

function ZSInit()
	ZS_String = ""
	ZS_Repeart_String = {}

	ZS_Log_Count = 0
	ZS_SendProto_Count = 0
	ZS_RecvProto_Count = 0

	ZS_ZS_Count = 1 --插针
	ZS_Log_Color = "ff00ff"


	-- 协议控制
	ZS_Black_Flag = true	--控制是否开启 黑名单（控制不输出范围）
	ZS_White_Flag = true	--控制是否开启 白名单（控制输出范围）
	-- 当白名单开启时，仅输出 不在黑名单且在白名单
	-- 当白名单不开启时，仅输出 不在黑名单
	ZS_White_List = {
		-- { protoHead = 98 }, -- 监听该协议头
		-- { protoHead = 98 }, -- 监听该协议头
		{ min = 9802, max = 9804 }, --"活"字模块
		{ min = 14600, max = 14605 }, --监听该区域协议
		-- { min = 9802, max = 9803 }, --监听该区域协议
	}
	ZS_Black_List = {
		-- { protoHead = 110 },
		-- { min = 1, max = 2},
		-- { min = 2},--
	}
end
ZSInit()

function ZS()
	ZS_ZS_Count = ZS_ZS_Count == 9 and 0 or (ZS_ZS_Count+1)
	local track_info = debug.getinfo(2, "Sln")
	ZS_String = string.format("From %s:%d ## %s", track_info.short_src, track_info.currentline, ZSRepeart( ZS_ZS_Count,15))
	Log.Debug( ZSRepeart("<color=#ffff00>")..ZS_String..ZSRepeart("</color>") )
end

function ZSLog(...)
	ZS_Log_Count = ZS_Log_Count + 1
	ZS_String = ZSRepeart( "#" , 10 )
	ZS_String = ZS_String.." "..ZS_Log_Count.." ##\n"


	local num_args = select("#", ...)
	local classSelf,varName,value
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
		ZS_String = string.format("%s = %s",varName,tostring(value))
		-- Log.Debug( string.format("%s = %s",varName,tostring(getVarValue(varName))) )
	elseif num_args == 2 then
		classSelf = select(1, ...)
		varName = select(2, ...)
		value = classSelf[varName]
		ZS_String = string.format("self.%s = %s",varName,tostring(value))
		-- Log.Debug( string.format("self.%s = %s",varName ,classSelf[varName]) )
	end

	Log.Debug( string.format(ZSRepeart("<color=#%s>"),ZS_Log_Color)..ZS_String..ZSRepeart("</color>") )
end

-- function ZSLog(string, color)
	
-- function ZSLog(...)
	-- Log.Debug(debug.getlocal (1, 1))
	-- Log.Debug(debug.getlocal (1, 2))
	-- Log.Debug(debug.getlocal (1, 3))
	-- ZS_Log_Count = ZS_Log_Count + 1

	-- -- printf("test")
	-- ZS_String = ZSRepeart( ZS_Log_Count , 10 ).." "
	-- local num_args = select("#", ...)
 --    for i = 1, num_args do
 --        local arg = select(i, ...)
 --        local track_info = debug.getlocal (1, arg)
 --        Log.Debug(track_info)
 --        -- for k,v in pairs(track_info) do
 --        -- 	Log.Debug(string.format("key:%s value:%s",k,v))
 --        -- end
 --        Log.Debug(ZS_String..string.format("%s:%s",i,arg))
 --    end
-- end

function ZSProto(flag, cmd, data)
	if ZSProtoFilter( cmd ) == false then return end
	if flag == 1 then
		ZS_String = ZSRepeart("<color=#88ffff>")..ZSRepeart(">>",10)
	else
		ZS_String = ZSRepeart("<color=#00ffff>")..ZSRepeart("<<<<",10)
	end
	ZS_String = ZS_String..ZSRepeart("proto ## ")..cmd..ZSRepeart(" ##")..ZSRepeart("</color>")
	Log.Debug( ZS_String )

	ZS_String = UtilsBase.serialize(data,nil,true)
	-- ZS_String = ZS_String..ZSSerialize(data, nil, nil, 1, nil)

	if flag == 1 then
		Log.Debug( ZSRepeart("  ",22)..ZSRepeart("proto ## ")..cmd..ZSRepeart(" ##\n\n")..ZSRepeart("<color=#ffffff>")..ZS_String..ZSRepeart("</color>") )
	else
		Log.Debug( ZSRepeart("    ",22)..ZSRepeart("proto ## ")..cmd..ZSRepeart(" ##\n\n")..ZSRepeart("<color=#ffffff>")..ZS_String..ZSRepeart("</color>") )
	end
end

function ZSTable(table,str)
	-- UtilsBase.dump(table,str)
end

function ZSRepeart( repeartStr , n )
	if n == nil then n = 1 end
	if ZS_Repeart_String[ repeartStr.."_ZS_"..n ] == nil then
		ZS_Repeart_String[ repeartStr.."_ZS_"..n ] = string.rep(repeartStr, n)
	end
	return ZS_Repeart_String[ repeartStr.."_ZS_"..n ]
end

-- 序列化
-- 将传入的Obj 字符串化
function ZSSerialize(obj, name, newline, depth, keyTab)
	
	local tempStr = ""
	newline = newline == nil and true or newline
	local space = newline and "		" or ""

	if name then
		tempStr = tempStr .. "name"
		if type(name) == "number" then
		end
	end


	if type(obj) == "number" then
		tempStr = tempStr .. " = " .. tostring(obj)
	elseif type(obj) == "string" then
		tempStr = tempStr .. " = " .. obj
	elseif type(obj) == "boolean" then
		tempStr = tempStr .. " = " .. (obj and "true" or "false")
	elseif type(obj) == "function" then
		tempStr = tempStr .. "【function】"
	elseif type(obj) == "userdata" then
		tempStr = tempStr .. "【userdata】"
	elseif type(obj) == "table" then
		tempStr = tempStr .. " = {"
		for k,v in pairs(obj) do
			tempStr = tempStr .. ZSSerialize(v,k,newline,depth+1,keyTab)
		end
		tempStr = tempStr .. "}"
	end

	return tempStr
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