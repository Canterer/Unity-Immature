/*
name:UriGroup
desc:uri 组策略
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZS.Loader
{
    [SLua.CustomLuaClass]
    public class UriGroup {
    	private List<string> uris;
        private Dictionary<int, Action<CRequest, Array>> onWWWCompletes;
        private Dictionary<int, Func<CRequest, bool>> onCrcChecks;
        private Dictionary<int, Func<CRequest, string>> onOverrideUrls;

        public int count { get { return uris.Count; } }
    }
}