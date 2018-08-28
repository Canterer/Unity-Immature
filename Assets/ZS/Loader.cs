/*
name:Loader
desc:下载器  注意该类 继承MonoBehaviour using SLua
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using SLua;

namespace ZS.Loader
{
	[SLua.CustomLuaClassAttribute]
	public class Loader : MonoBehaviour
	{
		static public uint asyncSize = 0*1024;//大于此size的ab使用异步加载
		static public int maxLoading = 3;//最大同时加载assetbundle asset数量
		static public int bundleMax = 5;//最大同时加载assetbundle数量
		static public float BundleLoadBreakMilliSeconds = 25;//加载bundle耗时跳出判断时间

		static private int totalCount = 0;
		static private int currLoaded = 0;
		static private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

		// static protected LoadingEventArg loadingEvent;
		static protected Queue<AssetBundleLoadOperation> bundleQueue = new Queue<AssetBundleLoadOperation>();
		static protected LinkedList<BundleGroundQueue> bundleGroundQueue = new LinkedList<BundleGroundQueue>();
		static protected List<CRequest> loadingTasks = new List<CRequest>();

		// load asset or www operations
		static List<ResourcesLoadOperation> inProgressOperations = new List<ResourcesLoadOperation>();
		// load assetbundle operations
		static List<AssetBundleLoadOperation> inProgressBundleOperations = new List<AssetBundleLoadOperation>();

		//asset callback list
		static Dictionary<string, List<CRequest>> assetCallBackList = new Dictionary<string, List<CRequest>>();
		//downloading assetbundle
		static Dictionary<string, AssetBundleLoadOperation> downloadingBundles = new Dictionary<string, AssetBundleLoadOperation>(); 



		//公共加载方法
		static public void LoadLuaTable(LuaTable reqs, System.Action<bool> groupCompleteFn, System.Action<LoadingEventArg> groupProgressFn, int priority = 0 ){
			var groupQueue = BundleGroundQueue.Get();
			LoadGroupAsset(groupQueue);
		}
		static public void LoadGroupAsset(BundleGroundQueue bGroup){
			LoadingQueue();
		}
		static public AssetBundleLoadOperation LoadAssetCoroutine(string abName, string assetName, System.Type type, int priority = 0){
			var req = CRequest.Get();
			return LoadAsset(req, true);
		}

		static public void LoadAsset(string abName, string assetName, System.Type type, System.Action<CRequest> onComplete, System.Action<CRequest> onEnd, int priority = 0){
			var req = CRequest.Get();
			LoadAsset(req);
		}
		static public AssetBundleLoadAssetOperation LoadAsset(CRequest req, bool coroutine = false){
			AssetBundleLoadAssetOperation op = null;
			var groupQueue = BundleGroundQueue.Get();
			groupQueue.Enqueue(req);
			if (coroutine)
			{
				op = new AssetBundleLoadAssetOperationFull();
				op.SetRequest(req);
				req.assetOperation = op;
			}
			LoadGroupAsset(groupQueue);
			return op;
		}


		// 以 webreuest 方式加载网络非ab资源
		static public HttpLoadOperation HttpRequestCoroutine(string url, object head, System.Type type){
			var req = CRequest.Get();
			return HttpRequest(req, true);
		}
		static public void HttpRequest(string url, object head, System.Type type, System.Action<CRequest> onComplete, System.Action<CRequest> onEnd, UriGroup urisS){
			var req = CRequest.Get();
			HttpRequest(req);
		}
		static public HttpLoadOperation HttpRequest(CRequest req, bool coroutine = false){
			WebRequestOperation op = null;
			if(coroutine)
			{
				op = new WebRequestOperation();
				req.assetOperation = op;
			}else
				op = WebRequestOperation.Get();

			op.SetRequest(req);
			inProgressOperations.Add(op);//静态变量
			op.BeginDownload();
			return op;
		}


		// 以 WWW 方式加载网络非ab资源
		static public HttpLoadOperation WWWRequestCoroutine(string url, object head, System.Type type){
			var req = CRequest.Get();
			return WWWRequest(req, true);
		}
		static public void WWWRequest(string url, object head, System.Type type, System.Action<CRequest> onComplete, System.Action<CRequest> onEnd, UriGroup uris){
			var req = CRequest.Get();
			WWWRequest(req);
		}
		static public HttpLoadOperation WWWRequest(CRequest req, bool coroutine = false){
			WWWRequestOperation op = null;
			if(coroutine)
			{
				op = new WWWRequestOperation();
				req.assetOperation = op;
			}else
				op = WWWRequestOperation.Get();

			op.SetRequest(req);
			inProgressOperations.Add(op);
			op.BeginDownload();
			return op;
		}

		// load logic
		// check the queue and load assetbundle and asset
		static protected bool LoadingQueue(){
			if( inProgressOperations.Count > 0) return false;//wait bundle load
			
			// 获取静态变量 bundleGroundQueue maxLoading loadingTasks
			LinkedListNode<BundleGroundQueue> fristNode = bundleGroundQueue.First;
			while(fristNode != null && maxLoading - loadingTasks.Count > 0)
			{
				BundleGroundQueue value = fristNode.Value;
				if( value.Count > 0 ){
					var req = value.Dequeue();
					LoadAssetBundle(req);
				}else{
					fristNode = fristNode.Next;
					bundleGroundQueue.Remove(value);
				}

				// var ts = System.DateTime.Now - frameBegin;
				if(watch.ElapsedMilliseconds > BundleLoadBreakMilliSeconds) return true;
			}

			return false;
		}

		static protected void LoadAssetBundle(CRequest req){
			string relativeUrl = ManifestManager.RemapVariantName(req.relativeUrl);
			req.relativeUrl = relativeUrl;

			// remove delay unload assetbundle
			ABDelayUnloadManager.CheckRemove(req.keyHashCode);
			if(LoadAssetFromCache(req))
			{
				DispathReqAssetOperation(req, false);
				return;
			}

			totalCount++;

			AssetBundleLoadOperation abDownloadOperation = null;
			// check is loading
			if( downloadingBundles.TryGetVale(req.key, out abDownloadOperation)){

			}else if( CheckAssetBundleCanLoad(req)){// need load
				//load dependencies and refrenece count
				if( ManifestManager.fileManifest != null )
					req.dependencies = LoadDependencies(req, null);//load dependencies assetbundle

				//load assetbundle
				abDownloadOperation = LoadAssetBundleInternal(req);
			}

			//load asset
			ResourcesLoadOperation operation;
			var tp = req.assetType;
			if(req.assetOperation != null){
				operation = req.assetOperation;
			}else if ( CacheManager.Typeof_ABScene.Equals(tp) ){
				operation = new AssetBundleLoadLevelOperation();
				operation.SetRequest(req);
			}else{
				operation = AssetBundleLoadAssetOperationFull.Get();
				operation.SetRequest(req);
			}

			bool isLoading = false;
			//the same asset be one Operation
			if( operation is AssetBundleLoadAssetOperation)
				isLoading = AddAssetBundleLoadAssetOperationToCallBackList((AssetBundleLoadAssetOperation)operation);

			if(!isLoading){
				if(abDownloadOperation != null)
					abDownloadOperation.AddNext(operation);//wait for assetbundle complete
				else
					inProgressOperations.Add(operation);//the assetbundle is done

				loadingTasks.Add(req);
			}

		}



		static internal bool LoadAssetFromCache(CRequest req){
			var cache = CacheManager.TryGetCache(req.keyHashCode);
			if(cache != null){
				var asset = cache.GetAsset(req.udAssetKey);
				if(asset != null){
					req.data = asset;
					return true;
				}

			}
			return false;
		}


		static protected int[] LoadDependencies(CRequest req, CRequest parent)
		{
			string[] deps = ManifestManager.fileManifest.GetDirectDependencies(req.assetBundleName);
			if( deps.length == 0 ) return nil;
			string abName = string.Empth;
			if (parent != null)
				abName = CUtils.GetBaseName(parent.assetBundleName);

			for(int i = 0; i < deps.length; ++i)
			{
				
			}
		}
		
	}
}