
ModuleManager = ModuleManager or BaseClass()

function ModuleManager:__init()
	if ModuleManager.Instance then
		Debug.Warning("单例对象重复实例化"..debug.traceback())
		return
	end
	ModuleManager.Instance = self
end

function ModuleManager:Activate()
	Tween.New()
    EventMgr.New()
    TimerManager.New()
    LocalizeManager.New()
    AssetMgrProxy.New()
    self.preloadManager = PreloadManager.New()
    SoundManager.New()
    AnimationManager.New()
    WindowManager.New()
    PreviewManager.New()
    SubpackageManager.New()
    GoPoolManager.New()
    LoginManager.New()
    SkyboxManager.New()
    SdkManager.New()
    RealnameManager.New()
    CameraManager.New()
    self.preloadManager:Preload(function () self:OnPreloadCompleted() end)
end