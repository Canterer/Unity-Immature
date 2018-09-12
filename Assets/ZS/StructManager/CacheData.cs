/*
name:CacheData
desc: 缓存管理
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZS.Loader
{
	// 缓存资源
	public class CacheData : IDisposable
    {
    	public CacheData(){

    	}

    	// assetBundle Name
    	public string assetBundleKey { get; internal set; }
    	// hashCode
    	public int assetHashCode { get; internal set; }
    	// assetBundle 对象
    	public AssetBundle assetBundle;
    	// 引用计数
    	public int count;


    	public void Dispose()
    	{
    		
    	}
    }
}