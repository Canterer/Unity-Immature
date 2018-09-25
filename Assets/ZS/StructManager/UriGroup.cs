/*
name:UriGroup
desc:uri 组策略
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

namespace ZS.Loader
{
    [SLua.CustomLuaClass]
    public class UriGroup {
    	private List<string> uris;
        private Dictionary<int, Action<CRequest, Array>> onWWWCompletes;
        private Dictionary<int, Func<CRequest, bool>> onCrcChecks;
        private Dictionary<int, Func<CRequest, string>> onOverrideUrls;

        public int count { get { return uris.Count; } }

        public UriGroup(){
        	uris = new List<string> ();
        }

        static UriGroup _uriList;
        public static UriGroup uriList {
        	get{
        		if(_uriList == null){
        			_uriList = new UriGroup();
        			_uriList.Add(CUtils.GetRealPersistentDataPath(), true);
        			_uriList.Add(CUtils.GetRealStreamingAssetsPath());
        		}
        		return _uriList;
        	}
        	set { _uriList = value; }
        }

        internal string this [int index] {
        	get {
        		if(uris.Count > index && index >= 0)
        			return uris[index];
        		else
        			return string.Empty;
        	}
        }

        // 添加uri
        public int Add(string uri){
        	int len = uris.Count;
        	uris.Add(uri);
        	return len;
        }
        public void Add(string uri, bool needCheckCrc){
        	int index = Add(uri);
        	// if(needCheckCrc)
        	// 	AddOnCrcChecks(index, CrcCheck.CheckUriCrc);
        }
        public void Add(string uri, bool needCheckCrc, bool onWWWComp, bool onOverrideUrl){
        	int index = Add(uri);
        	// if(onWWWComp)
        	// 	AddOnWWWCompletes(index, SaveWWWFileToPersistent);
        	// if(needCheckCrc)
        	// 	AddOnCrcChecks(index, CrcCheck.CheckUriCrc);
        	// if(onOverrideUrl)
        	// 	AddOnOverrideUrls(index, OverrideRequestUrlByCrc);
        }

        public void Add(string uri, Action<CRequest, Array> onWWWComplete, Func<CRequest, bool> onCrcCheck){
        	int index = Add(uri);
        	// if(onWWWComplete != null)
        	// 	AddOnWWWCompletes(index, onWWWComplete);
        	// if(onCrcCheck != null)
        	// 	AddOnCrcChecks(index, CrcCheck.CheckUriCrc);
        }

        public void Add(string uri, Action<CRequest, Array> onWWWComplete, Func<CRequest, bool> onCrcCheck, Func<CRequest, string> onOverrideUrl){
        	int index = Add(uri);
        	// if(onWWWComplete != null)
        	// 	AddOnWWWCompletes(index, onWWWComplete);
        	// if(onCrcCheck != null)
        	// 	AddOnCrcChecks(index, CrcCheck.CheckUriCrc);
        	// if(onOverrideUrl != null)
        	// 	AddOnOverrideUrls(index, onOverrideUrl);
        }



        internal void OnWWWComplete(CRequest req, byte[] bytes){
            Action<CRequest, Array> act = null;
            if(onWWWComplete != null && onWWWComplete.TryGetValue(req.index, out act))
                act(req, bytes);
        }        
        internal void OnWWWComplete(CRequest req, UnityWebRequest www){
            Action<CRequest, Array> act = null;
            if(onWWWComplete != null && onWWWComplete.TryGetValue(req.index, out act))
                act(req, www.downloadHandler.data);
        }

        // check WWW Complete event
        public static void CheckWWWComplete(CRequest req, WWW www){
            if(req.uris != null)
                req.uris.OnWWWComplete(req, www.bytes);
        }
        public static void CheckWWWComplete(CRequest req, UnityWebRequest www){
            if(req.uris != null)
                req.uris.onWWWComplete(req, www);
        }

    }
}