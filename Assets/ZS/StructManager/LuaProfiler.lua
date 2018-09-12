--
-- @author      lfl
-- @file        LuaProfiler.lua
--

-- define module
LuaProfiler = LuaProfiler or {}


--这样访问应该会快一点
LuaProfiler.getinfo = debug.getinfo
LuaProfiler.getupvalue = debug.getupvalue
local stringFormat = string.format
local osClock = os.clock
local localCollectgarbage = collectgarbage

-- load modules
local sourceFilePath = "../LuaProfiler_output"
local sourceFilePath2 = "../LuaProfiler_output_line.txt"
--假如是在安卓系统下，路径改下
if IS_ANDROID then --安卓系统
	sourceFilePath = "/mnt/sdcard/LuaProfiler_output.txt"
	sourceFilePath2 = "/mnt/sdcard/LuaProfiler_output_line.txt"
end

-- end
-- get the function title
function LuaProfiler:_func_title(funcinfo)

    -- check
    assert(funcinfo)

    -- the function name
    local name = funcinfo.name or 'anonymous'

    -- the function line
    local line = stringFormat("%d", funcinfo.linedefined or 0)

    -- the function source
    local source = funcinfo.short_src or 'C_FUNC'

    -- make title
    return stringFormat("%s : %s : %s", source, line, name)
end

-- get the function report
function LuaProfiler:_func_report(funcinfo)

    -- get the function title
    local title = self:_func_title(funcinfo)

    -- get the function report
    local report = self._REPORTS_BY_TITLE[title]
    if not report then
        
        -- init report
        report = 
        {
            title       = title
        ,   callcount   = 0
        ,   totaltime   = 0
	    ,   startMonery = 0
	    ,   sumMonery   = 0
        }

        -- save it
        self._REPORTS_BY_TITLE[title] = report
        table.insert(self._REPORTS, report)
    end

    -- ok?
    return report
end

-- profiling call
function LuaProfiler:_profiling_call(funcinfo)

    -- get the function report
    local report = self:_func_report(funcinfo)
    assert(report)

    -- save the call time
    report.calltime    = osClock()
	report.startMonery = localCollectgarbage("count")
    -- update the call count
    report.callcount   = report.callcount + 1

end


-- profiling return
function LuaProfiler:_profiling_return(funcinfo)

    -- get the stoptime
    local stoptime = osClock()

    -- get the function report
    local report = self:_func_report(funcinfo)
    assert(report)
	--内存情况
	local endMomery = localCollectgarbage("count")
	if report.startMonery > 0 then
		report.sumMonery   = report.sumMonery + (endMomery - report.startMonery)
	end
	report.startMonery = 0

    -- update the total time
    if report.calltime and report.calltime > 0 then
		report.totaltime = report.totaltime + (stoptime - report.calltime)
        report.calltime = 0
	end
end


-- the profiling handler
function LuaProfiler._profiling_handler(hooktype)
    -- the function info
    local funcinfo = LuaProfiler.getinfo(2, 'nS')

	--判断下是否有过滤字段
	if LuaProfiler.LuaProfilerFilter then
		local source = funcinfo.short_src or 'C_FUNC'
		if not string.find(source, LuaProfiler.LuaProfilerFilter) then
			return
		end
	end
	
    -- dispatch it
    if hooktype == "call" then
        LuaProfiler:_profiling_call(funcinfo)
    elseif hooktype == "return" then
        LuaProfiler:_profiling_return(funcinfo)
    end
end


-- start profiling
--[[@param mode 这边是当做一个排序模式
--  mode = 1的时候按照os.clock的时长排序
--  mode = 2的时候按照momery的大小来排序]]
--  mode = 3的时候按照callcount的大小来排序]]
--  mode = 4的时候按照平均时长的大小来排序]]
--  mode = 5的时候按照平均内存的大小来排序]]
-- @param filter 过滤的关键字，一般来说通过source来过滤
function LuaProfiler:start(mode, filter)
    Message("调试开始")
	-- init reports
    if mode ~= nil then
        mode = tonumber(mode)
    else
        mode = 1
    end

	self.LuaProfilerMode = mode or 1
	self.LuaProfilerFilter = filter
	--数据保存
	self._REPORTS = {}
	self._REPORTS_BY_TITLE  = {}
	
	-- save the start time
	self._STARTIME = osClock()
	
	-- start to hook
	debug.sethook(LuaProfiler._profiling_handler, 'cr', 0)
end
--@require "util/LuaProfiler":start(4)
--@require "util/LuaProfiler":stop()
--@require "util/LuaProfiler":startCheckFunc("mod/module_manager", "DoUnloadUnusedAssets")
--@require "util/LuaProfiler":endCheckFunc()

--检测某个路径下面的某个函数的执行情况
function LuaProfiler:startCheckFunc(filterSource, funcName)
	--处理部分
	local function funcHander(hooktype, line)
		--过滤掉不是需求的方法
		local funcinfo = LuaProfiler.getinfo(2, 'fnS')
		local source = funcinfo.short_src or 'C_FUNC'
		local name = funcinfo.name or 'anonymous'
		if not (string.find(source, filterSource) and string.find(name, funcName)) then
			return
		end
		--开始处理下
		if hooktype == "call" then
			LuaProfiler.checkFuncClock = osClock()
--			localCollectgarbage("collect")
			LuaProfiler.checkFuncMomery = localCollectgarbage("count")
			--文件写入部分
			local cFile = assert(io.open(sourceFilePath2, "a+"))
			LuaProfiler.cOutputHandle = cFile
			LuaProfiler.outPrintMode = function(contents)
				if LuaProfiler.cOutputHandle then
					LuaProfiler.cOutputHandle.write(LuaProfiler.cOutputHandle, contents)
				end
			end
			LuaProfiler.outPutString = "\n------------------------------------\n"
			LuaProfiler.outPutString = stringFormat("%sfilePath = %s, funcName = %s\n", LuaProfiler.outPutString, filterSource, funcName)
			LuaProfiler.outPutString = stringFormat("%s当前运行时------>>%s\n", LuaProfiler.outPutString, os.date())
			LuaProfiler.outPutString = stringFormat("%s当前的内存 = %7.2fKB\n", LuaProfiler.outPutString, LuaProfiler.checkFuncMomery)
			--打印下参数部分
			local func = funcinfo.func
			local i = 1
			local ups = {}
			while func do -- check for func as it may be nil for tail calls
				local name, value = LuaProfiler.getupvalue(func, i)
				if not name then break end
				ups[name] = value
				i = i + 1
			end
			if i > 1 then
				LuaProfiler.outPutString = stringFormat("%s当前运行函数参数args\n", LuaProfiler.outPutString)
				for key, value in pairs(ups) do
					LuaProfiler.outPutString = stringFormat("%s参数名name = %s；value = %s\n", LuaProfiler.outPutString, key, value)
				end
			end
		elseif hooktype == "return" then
			local endClock = osClock()
--			localCollectgarbage("collect")
			local endMonery = localCollectgarbage("count")
			LuaProfiler.outPutString = stringFormat("%s当前的内存 = %7.2fKB\n", LuaProfiler.outPutString, endMonery)
			LuaProfiler.outPutString = stringFormat("%s运行之后内存增加 = %7.2fKB\n", LuaProfiler.outPutString, endMonery - LuaProfiler.checkFuncMomery)
--			LuaProfiler.checkFuncMomery = nil
			LuaProfiler.outPutString = stringFormat("%s运行消耗总clock = %s\n", LuaProfiler.outPutString, endClock - LuaProfiler.checkFuncClock)
--			LuaProfiler.checkFuncClock = nil
			LuaProfiler.outPrintMode(LuaProfiler.outPutString)
--			LuaProfiler.outPutString = nil
			LuaProfiler.outPrintMode("\n------------------------------------\n")
			--关闭文件
			if LuaProfiler.cOutputHandle then
				io.close(LuaProfiler.cOutputHandle)
				LuaProfiler.cOutputHandle = nil
			end
		elseif hooktype == "line" then
			local lineClock = osClock()
--			localCollectgarbage("collect")
			local lineMonery = localCollectgarbage("count")
			LuaProfiler.outPutString = stringFormat("%s当前的行数line = %s, clock = %s, momery = %7.2fKB\n", LuaProfiler.outPutString, line, lineClock, lineMonery)
		end
	end
	debug.sethook(funcHander, 'crl', 0)
end

--结束检测
function LuaProfiler:endCheckFunc()
	debug.sethook()
end


-- stop profiling
--[[
-- @param mode 打印模式，分成两组
-- mode = 1，表示console打印
-- mode = 2，表示文件打印，为了长时间监控，这个更加靠谱
-- mode = 3，输出csv
-- ]]
function LuaProfiler:stop(mode)
    Message("调试结束")
	-- save the stop time
    if mode ~= nil then
        mode = tonumber(mode)
    else
        mode = 3
    end

	--记录时间
	self._STOPTIME = osClock()
	
	-- stop to hook
	debug.sethook()
	
	-- calculate the total time
	local totaltime = self._STOPTIME - self._STARTIME
	
	-- sort reports
    local type_name = ""
	if self.LuaProfilerMode == 1 then --时间排序
        type_name = "totalTime"
		table.sort(self._REPORTS, function(a, b)
			return a.totaltime > b.totaltime
		end)
	elseif self.LuaProfilerMode == 2 then --内存排序
        type_name = "sumMonery"
		table.sort(self._REPORTS, function(a, b)
			return a.sumMonery > b.sumMonery
		end)
	elseif self.LuaProfilerMode == 3 then --次数排序
        type_name = "callCount"
		table.sort(self._REPORTS, function(a, b)
			return a.callcount > b.callcount
		end)
	elseif self.LuaProfilerMode == 4 then --平均时长
        type_name = "perTime"
		table.sort(self._REPORTS, function(a, b)
			return a.totaltime/a.callcount > b.totaltime/b.callcount
		end)
	elseif self.LuaProfilerMode == 5 then --平均内存
        type_name = "perMonery"
		table.sort(self._REPORTS, function(a, b)
			return a.sumMonery/a.callcount > b.sumMonery/b.callcount
		end)
	end
	
	--输出模式
	local outPrintMode, cOutputHandle
	if mode == 1 then
		outPrintMode = print
	elseif mode == 2 then
        local fn = string.format("%s_%s.txt", sourceFilePath, type_name)
		local cFile = assert(io.open(fn, "w"))
		cOutputHandle = cFile
		outPrintMode = function(contents)
			if cOutputHandle then
				cOutputHandle.write(cOutputHandle, contents)
			end
		end
    elseif mode == 3 then
        local fn = string.format("%s_%s_%s.csv", sourceFilePath, type_name, os.date("%Y%m%d%H%M%S"))
		local cFile = assert(io.open(fn, "w"))
		cOutputHandle = cFile
		outPrintMode = function(contents)
			if cOutputHandle then
				cOutputHandle.write(cOutputHandle, contents)
			end
		end
	end
	outPrintMode("--------------------------------------------------------\n")
	outPrintMode("这是一个监控信息输出部分，我们可以通过他监控游戏情况\n")
	outPrintMode("--------------------------------------------------------\n")
    if mode == 3 then
	    outPrintMode(stringFormat("%s, %s, %s, %s, %s, %s, %s\n", "timesum",  "time/1", "time%", "callTimes", "momery", "sumMonery", "source"))
    else
	    outPrintMode(stringFormat("%-6s \t %-6s \t %-6s \t %-9s \t %-12s \t %-14s \t %s\n", "timesum",  "time1", "time%", "callTimes", "momery", "sumMonery", "source"))
    end

	-- show reports
	for _, report in ipairs(self._REPORTS) do
		
		-- calculate percent
		local percent = (report.totaltime / totaltime) * 100
		--如果是时间排序的，可以这样处理下(这样的话，只输出百分比大于1的)
--		if self.LuaProfilerMode == 1 then
--			if percent < 0.5 then
--				break
--			end
--		else
			-- if _ >= 200 then
			-- 	break
			-- end
--		end
		--every time use monery
		local callCount = report.callcount > 0 and report.callcount or 1
		local monery = report.sumMonery/callCount
		local singleTime = report.totaltime/callCount
		-- trace
        if mode == 3 then
		    outPrintMode(stringFormat("%-0.5f, %-0.5f, %-3.3f%%, %-9d, %-9.2fKB, %-12dKB, %s\n", report.totaltime, singleTime, percent, callCount, monery, report.sumMonery, report.title))
        else
		    outPrintMode(stringFormat("%-0.5f \t %-0.5f \t %-3.3f%% \t %-9d \t %-9.2fKB \t %-12dKB \t %s\n", report.totaltime, singleTime, percent, callCount, monery, report.sumMonery, report.title))
        end
	end
	
	outPrintMode("--------------------------------------------------------\n")
	if (mode == 2) and cOutputHandle then
		io.close(cOutputHandle)
		cOutputHandle = nil
	end
    if cOutputHandle then
        cOutputHandle.close()
        cOutputHandle = nil
    end
end

