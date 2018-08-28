/*
name:
desc:LoadOperation 下载选项
author:ZS
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZS.Loader
{
	// 定义抽象类 IEnumerator 表明该类是一个协程
    public abstract class LoadOperation : IEnumerator, IReleaseToPool{
        internal LoadOperation next;// 使用链表结构连接对象
        private System.Func<bool> m_OnUpdate;//定义两个委托来回调
        private System.Func<bool> m_IsDone;
        //每一个LoadOperation都包含一个CRequest 用于下载
        internal CRequest cRequest { get; private set;}
        protected bool pool { get; set; }// 用于表示是否已创建对象池，


        //定义继承的虚函数
        public virtual void ReleaseToPool(){}
        //定义保护注册委托函数  给委托赋值
        protected void RegisterEvent(System.Func<bool> onUpdate, System.Func<bool> isDone)
        { this.m_OnUpdate = onUpdate; this.m_IsDone = isDone; }
        // 该方法本应用虚函数来实现  virtual function is bad for il2cpp so we use Action
        public bool Update(){ return m_OnUpdate(); }
        public bool IsDone(){ return m_IsDone(); }

        // CRequest 管理函数
        public void SetRequest(CRequest req){ this.cRequest = req;}
        public static void SetRequestData(CRequest req, object data){
            if( !req.isDisposed)
                req.data = data;
        }

        // 链表管理函数
        public bool MoveNext(){ return !IsDone(); }


        // 重置函数 直接将变量置空  
        // 是因为CRequest、LoadOperation都继承ObjectPool，不需要显示释放内存
        public virtual void Reset(){
            pool = false;
            cRequest = null;
            next = null;
        }
    }
}