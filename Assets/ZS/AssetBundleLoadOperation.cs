/*
name:
desc:AssetBundleDownloadOperation 资源集下载选项
author:ZS
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using ZS.Utils;

namespace ZS.Loader
{	
	// 资源加载方式 两种：从本地磁盘加载 从网络地址加载
  public abstract class AssetBundleLoadOperation : LoadOperation
	{
       	// 下载选项 持有资源引用
       	public AssetBundle assetBundle { get; protected set; }
       	// 定义私有委托来回调开始下载
       	private System.Action m_BeginDownload;

       	// 相关记录信息
       	protected bool isDone;
       	public string error { get; internal set; }

       	// 开始下载委托 注册函数
       	protected void RegisterBeginDownLoad(System.Action beginDownload){m_BeginDownload=beginDownload;}

       	public void BeginDownload()
       	{
       		// 获取从父类继承来的CRequest 打印相关下载信息
       		var req = cRequest;
       		// 打印 ZS(req.key, req.assetName)
       		if(m_BeginDownload != null)
       			m_BeginDownload();
       	}

       	// 插入加载链表表尾 用于链式加载资源
       	public void AddNext(LoadOperation assetOperation)
       	{	
       		LoadOperation n = this;
       		LoadOperation next = this.next;
       		while( next != null )
       		{
       			n = next;
       			next = n.next;
       		}
       		n.next = assetOperation;
       	}

       	public override void Reset()
       	{
       		base.Reset();
       		assetBundle = null;
       		error = null;
       		isDone = false;
       	}

       	protected bool _IsDone(){ return isDone; }
    }


    //crc check error
    public sealed class AssetBundleLoadErrorOperation : AssetBundleLoadOperation
    {
    	public AssetBundleLoadErrorOperation()
    	{
    		isDone = true;
    		RegisterEvent(_Update, _IsDone);
    	}
    	public override void Reset(){ base.Reset(); }
    	private bool _Update(){ return false; }
    }	

    public sealed class AssetBundleLoadFromWebOperation : AssetBundleLoadOperation
    {
    	private UnityWebRequest m_webRequest;
    	private AsyncOperation m_asyncOperation;

    	public AssetBundleLoadFromWebOperation()
    	{
    		RegisterEvent(_Update, _IsDone);
    		RegisterBeginDownLoad(_BeginDownload);
    	}
    	private bool _Update()
    	{
    		if(!isDone && downloadIsDone)
    		{
    			FinishDownload();
    			isDone = true;
    		}
    		return !isDone;
    	}

    	private void _BeginDownload()
    	{
    		m_webRequest = UnityWebRequest.GetAssetBundle(this.cRequest.url);
			if (m_webRequest != null)
			{
				m_asyncOperation = m_webRequest.Send();
			}
    	}
		private bool downloadIsDone { get { return (m_webRequest == null) || (m_webRequest.isDone && CacheManager.CheckDependenciesComplete(cRequest));}}

		private void FinishDownload()
		{
			if(m_webRequest == null)
			{
				error = string.Format("the webrequest is null CRequest({0},{1})", cRequest.key, cRequest.assetName);
				return;
			}
			error = m_webRequest.error;

			if (!string.IsNullOrEmpty(error))
				Debug.LogError(error);
			else if( (assetBundle = DownloadHandlerAssetBundle.GetContent(m_webRequest)) == null )
				error = string.Format("the asset bundle({0}) is not exist. CRequest({1})", cRequest.key, cRequest.assetName);

			m_webRequest.Dispose();//加载资源后 释放资源
			m_webRequest = null;
			m_asyncOperation = null;
		}

		public override void ReleaseToPool()
		{
			if (pool)
				Release(this);
		}
		public override void Reset() { base.Reset(); }

		// 定义对象缓存池
		static ObjectPool<AssetBundleLoadFromWebOperation> webOperationPool = new ObjectPool<AssetBundleLoadFromWebOperation>(m_AcitonOnGet, m_ActionOnRelease);
		private static void m_AcitonOnGet(AssetBundleLoadFromWebOperation op){ op.pool = true; }
		private static void m_ActionOnRelease(AssetBundleLoadFromWebOperation op){ op.Reset(); }
		public static AssetBundleLoadFromWebOperation Get() { return webOperationPool.Get(); }
		public static void Release(AssetBundleLoadFromWebOperation toRelease){ webOperationPool.Release(toRelease); }
    }

	public sealed class AssetBundleLoadFromDiskOperation : AssetBundleLoadOperation
	{
		AssetBundleCreateRequest m_abRequest;
		public AssetBundleLoadFromDiskOperation()
		{
			RegisterEvent(_Update, _IsDone);
			RegisterBeginDownLoad(_BeginDownload);
		}

		private bool _Update()
		{
			if (!isDone && downloadIsDone )
			{
				FinishDownload();
				isDone = true;
			}
			return !isDone;
		}

		private void _BeginDownload()
		{
			string url = CUtils.GetAndroidABLoadPath(cRequest.url);
			var abInfo = ManifestManager.GetABInfo(cRequest.key);
			if (abInfo != null && abInfo.size < Loader.asyncSize)
			{
				assetBundle = AssetBundle.LoadFromFile(url);
			}else{
				m_abRequest = AssetBundle.LoadFromFileAsync(url);
			}

			if(m_abRequest != null)
				m_abRequest.priority = cRequest.priority;
		}
		private bool downloadIsDone
		{
			get{
				return ((m_abRequest != null && m_abRequest.isDone) || assetBundle != null || (assetBundle == null && m_abRequest == null)) 
					&& CacheManager.CheckDependenciesComplete(cRequest);
			}
		}

		private void FinishDownload()
		{
			if(m_abRequest != null)
				assetBundle = m_abRequest.assetBundle;

			if(assetBundle == null)
				error = string.Format("the asset bundle({0}) is not exist. CRequest({1})", cRequest.key, cRequest.assetName);
			
			m_abRequest = null;
		}

		public override void ReleaseToPool()
		{
			if(pool)
				Release(this);
		}
		public override void Reset(){ base.Reset(); }

		// 定义对象缓存池
		static ObjectPool<AssetBundleLoadFromDiskOperation> webOperationPool = new ObjectPool<AssetBundleLoadFromDiskOperation>(m_AcitonOnGet, m_ActionOnRelease);
		private static void m_AcitonOnGet(AssetBundleLoadFromDiskOperation op){ op.pool = true; }
		private static void m_ActionOnRelease(AssetBundleLoadFromDiskOperation op){ op.Reset(); }
		public static AssetBundleLoadFromDiskOperation Get() { return webOperationPool.Get(); }
		public static void Release(AssetBundleLoadFromDiskOperation toRelease){ webOperationPool.Release(toRelease); } 
	}
}