-- -----------------------------------------
-- name:游戏Lua入口
-- desc:
-- author:胡帅
-- -----------------------------------------
 GameLuaStart = GameLuaStart or BaseClass()

 function GameLuaStart:__init()
 end

 function GameLuaStart:__delete()
 end

 function GameLuaStart:Start()
 	if Application.platform == RuntimePlatform.Android then
        Application.targetFrameRate = 60
    elseif Application.platform == RuntimePlatform.IPhonePlayer then
        Application.targetFrameRate = 60
    else
        Application.targetFrameRate = 60
    end
    UnityEngine.Time.fixedDeltaTime = 0.1
    QualitySettings.blendWeights = BlendWeights.OneBone
    -- QualitySettings.antiAliasing = 0
    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable
    if Application.platform ~= RuntimePlatform.Android and Application.platform ~= RuntimePlatform.IPhonePlayer then
        -- QualitySettings.antiAliasing = 2
        -- QualitySettings.SetQualityLevel(1, true);
        QualitySettings.vSyncCount = 0;
        if ctx.IsDebug then
    --         math.randomseed(tostring(os.time()):reverse():sub(1, 7))
    --         local rnum = math.random(1, 3)
    --         if rnum == 1 then
			 --    Screen.SetResolution(1280,591,false,60)
    --         elseif rnum == 2 then
				-- Screen.SetResolution(1020,540,false,60)
    --         else
				-- Screen.SetResolution(1280,720,false,60)
            -- end
        end
    else
        QualitySettings.antiAliasing = 0
    end
    -- ctx.MainCamera.useOcclusionCulling = false
    ctx.UICamera.nearClipPlane = -15
    if ctx.IsDebug then
        IS_DEBUG = true
         -- Profiling.Profiler.maxNumberOfSamplesPerFrame = -1
    else
        Log.SetLev(3) -- Info
        IS_DEBUG = false
    end
    self.moduleManager:Activate()
    -- if IS_DEBUG then
    --     local __g = _G
    --     setmetatable(__g, {
    --         __newindex = function(_, name, value)
    --             local msg = "VARIABLE : '%s' SET TO GLOBAL VARIABLE \n%s"
    --             rawset(__g, name, value)
    --             local trace = debug.traceback()
    --             if name ~= "connection_recv_data" and string.sub(name, 1, 3) ~= "___" then
    --                 Log.Error(string.format(msg, name, trace), 0)
    --             end
    --         end
    --     })
    -- end
    EventMgr.Instance:Fire(event_name.end_mgr_init)
 end