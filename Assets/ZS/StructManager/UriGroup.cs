/*
name:UriGroup
desc:uri 组策略
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using ZS.Utils;

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
            if(onWWWCompletes != null && onWWWCompletes.TryGetValue(req.index, out act))
                act(req, bytes);
        }        
        internal void OnWWWComplete(CRequest req, UnityWebRequest www){
            Action<CRequest, Array> act = null;
            if(onWWWCompletes != null && onWWWCompletes.TryGetValue(req.index, out act))
                act(req, www.downloadHandler.data);
        }

        // check WWW Complete event
        public static void CheckWWWComplete(CRequest req, WWW www){
            if(req.uris != null)
                req.uris.OnWWWComplete(req, www.bytes);
        }
        public static void CheckWWWComplete(CRequest req, UnityWebRequest www){
            if(req.uris != null)
                req.uris.OnWWWComplete(req, www);
        }


        // 检测CRequest index 处的url crc校验，默认返回true
        public static bool CheckRequestCurrentIndexCrc(CRequest req)
        {
            if(req.uris != null)
                return req.uris.CheckUriCrc(req);
            else
                return true;
        }

        public bool CheckUriCrc(CRequest req)
        {
            Func<CRequest, bool> act = null;
            if(onCrcChecks != null && onCrcChecks.TryGetValue(req.index, out act))
                return act(req);
            return true;
        }

        // 设置CRequest next index处的url
        public static bool CheckAndSetNextUriGroup(CRequest req)
        {
            if(req.uris == null)
                return false;
            int count = req.uris.count;
            int index = req.index + 1;
            if(count > index && index >= 0)
            {
                req.index = index;
                req.url = string.Empty;
                return true;
            }else
                return false;
        }


    }
}