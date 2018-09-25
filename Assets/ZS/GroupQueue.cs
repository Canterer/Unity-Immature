using System.Collections.Generic;
using UnityEngine;
using ZS.Collections;

namespace ZS.Loader {
	public abstract class GroupQueue<T> : IReleaseToPool {
		public System.Action<LoadingEventArg> onProgress;
		public System.Action<bool> onComplete;
		public int priority;

		// private
		protected LoadingEventArg loadingEventArg = new LoadingEventArg();
		protected int loadedCount = 0;
		protected int totalCount;
		protected Queue<T> groupRes = new Queue<T>();
		protected SafeDictionary<T,int> loadingGroupRes = new SafeDictionary<T,int>();
		protected List<string> errRes = new List<string>();
		protected readonly object syncRoot = new object();
		protected System.Action<T,bool> m_OnComplete;
		protected int loadingPer;


		// public
		public int Count {
			get{ return this.groupRes.Count;}
		}
		public int LoadingCount {
			get{ return this.loadingGroupRes.Count;}
		}
		// 为啥要有两个 进度计数？？？？

		public bool IsDown {
			// && loadingGroupRes.Count == 0);
			get{ return(groupRes.Count == 0 && loadedCount >= totalCount);}
		}


		public virtual void Enqueue(T req){
			groupRes.Enqueue(req);
			++totalCount;
		}
		public T Dequeue(){
			if(this.groupRes.Count > 0){
				var req = groupRes.Dequeue();
				loadingGroupRes.Add(req, 0);
				return req;
			}
			return default(T);
		}

		public void Progress (T req, int percent){
			AddProgress(req,percent);
		}

		protected virtual void AddProgress(T req, int percent){
			int oldVal = 0;
			if( loadingGroupRes.TryGetValue(req, out oldVal)){
				loadingGroupRes[req] = percent;
				loadingPer += percent-oldVal;
			}
		}

		// 为啥是-=  应该是初始值 0
		protected virtual void RemoveProgress(T req){ loadingPer -= 100;}

		public bool Complete(T req, bool isError){
			if(loadingGroupRes.ContainsKey(req)){
				loadingGroupRes.Remove(req);
				++loadedCount;
				if(m_OnComplete != null)
					m_OnComplete(req, isError);
				else
					OnComplete(req, isError);

				DispatchOnProgress();
			}
			// Debug.LogFormat("i={0},totalCount={1},loadedCount={2},IsDown={3},req={4},isErr={5}",i,totalCount,loadedCount,IsDown,req,isError);
			if( IsDown )
				DispatchOnComplete();

			return true;
		}

		public GroupQueue(){

		}

		protected void OnComplete(T req, bool isError){
			loadingEventArg.total = totalCount;
			loadingEventArg.current = loadedCount;
			if (isError)
				errRes.Add(string.Empty);
		}



		internal void Reset(){
			this.totalCount = 0;
			this.loadedCount = 0;
			groupRes.Clear();
			loadingGroupRes.Clear();
			errRes.Clear();
		}

		public void DispatchOnProgress(){
			if(onProgress != null){
				loadingEventArg.progress = loadedCount * 100 + loadingPer / totalCount;
				onProgress(loadingEventArg);
			}
		}

		protected void DispatchOnComplete(){
			bool isError = errRes.Count > 0;
			Reset();
			if( onComplete != null )
				onComplete (isError);
			
			onComplete = null;
			onProgress = null;
			ReleaseToPool();
		}

		public virtual void ReleaseToPool(){

		}
	}


	public sealed class BundleGroundQueue : GroupQueue<CRequest>{
		public override void Enqueue(CRequest req){
			if(Loader.LoadAssetFromCache(req)){
				ABDelayUnloadManager.CheckRemove(req.keyHashCode);
				Loader.DispatchReqAssetOperation(req, false);
			}else{
				req.group = this;
				base.Enqueue(req);
			}
		}

		public void Enqueue( IList<CRequest> reqs){
			foreach(var abInfo in reqs){
				Enqueue(abInfo);
			}
		}

		public override void ReleaseToPool(){
			Release(this);
		}


		static ObjectPool<BundleGroundQueue> objectPool = new ObjectPool<BundleGroundQueue>(null, m_ActionOnRelease);
		private static void m_ActionOnRelease(BundleGroundQueue re){
			re.onComplete = null;
			re.onProgress = null;
		}
		public static BundleGroundQueue Get(){
			return objectPool.Get();
		}
		public static void Release(BundleGroundQueue toRelease){
			objectPool.Release(toRelease);
		}
	}
}
