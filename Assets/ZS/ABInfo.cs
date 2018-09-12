/*
name:ABInfo
desc:
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;

namespace ZS.Loader
{
	public enum ABInfoState
	{
		None = 0,
		Success = 1 << 0,
		Fail = 1 << 1,
	}

	[System.SerializableAttribute]
	public class ABInfo
	{
		public ABInfo(string abName, uint crc32, uint size, int priority)
		{
			this.abName = abName;
			this.crc32 = crc32;
			this.size = size;
			this.priority = priority;
		}

		public uint crc32 = 0;
		public uint size = 0;
		public int priority = 0;
		public string abName = string.Empty;
		public string[] dependencies;

		//运行时记录crc校验结果
		internal ABInfoState state = ABInfoState.None;

		public ABInfo Clone()
		{
			var abInfo = new ABInfo(this.abName, this.crc32, this.size, this.priority);
			abInfo.dependencies = this.dependencies;
			return abInfo;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("(abName={0},", this.abName);
			stringBuilder.AppendFormat("crc32={0},", this.crc32);
			stringBuilder.AppendFormat("size={0},", this.size);
			stringBuilder.AppendFormat("priority={0},", this.priority);
			if(dependencies == null)
				stringBuilder.Append("dependencies:null");
			else
			{
				stringBuilder.Append("dependencies:");
				foreach(var s in dependencies)
					stringBuilder.AppendFormat("{0},", s);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}