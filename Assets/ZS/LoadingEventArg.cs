using System.Collections;
using UnityEngine;

namespace ZS.Loader {

    [SLua.CustomLuaClass]
    public class LoadingEventArg : System.ComponentModel.ProgressChangedEventArgs {
    	// public int number;//current loading number
    	public object target {
    		get;
    		internal set;
    	}
    	public long tatal {
    		get;
    		internal set;
    	}
    	public long current {
    		get;
    		internal set;
    	}

    	// 基类 ProgressChangedEventArgs 为ProgressChanged事件提供数据
    	// 属性: ProgressPercentage 获取异步任务进度百分比、 UserState 获取唯一的用户状态
    	public LoadingEventArg() : base(0, null){
    	}
    	public LoadingEventArg( long bytesReceived, long totalBytesToReceive, object userState) : base((totalBytesToReceive == -1L) ? 0 : ((int)(bytesReceived*100L/totalBytesToReceive)), userState){
    		this.current = bytesReceived;
    		this.total = totalBytesToReceive;
    		this.target = userState;
    	}
    }
}