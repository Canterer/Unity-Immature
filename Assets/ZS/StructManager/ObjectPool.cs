/*
name:
desc:Object 对象池
author:ZS
*/

using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZS.Pool
{
	// 定义底层虚拟接口
	public interface IReleaseToPool
    {
        void ReleaseToPool();
    }


    //Object池
	public class ObjectPool<T> where T : new()
    {
    	private readonly Stack<T> m_Stack = new Stack<T>();// 使用栈 管理
        private readonly System.Action<T> m_ActionOnGet;// 获取回调动作
        private readonly System.Action<T> m_ActionOnRelease;// 释放回调动作

        public int countAll { get; private set; }
        public int countActive { get{ return countAll - countInactive; } }
        public int countInactive{ get { return m_Stack.Count; } }

        // 构造函数
        public ObjectPool(Action<T> actionOnGet, Action<T> actionOnRelease)
        {
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }

        //	获取函数
        public T Get()
        {
        	T element;//创建对象指针
            if (m_Stack.Count == 0)
            {
                element = new T();//实例化
                countAll++;
            }
            else
                element = m_Stack.Pop();

            if (m_ActionOnGet != null)
                m_ActionOnGet(element);
            return element;
        }

        //	释放函数
        public void Release(T element)
        {
        	// 防止释放函数被多次调用时，重复释放同一对象
        	if (m_Stack.Count > 0 && ReferenceEquals(m_Stack.Peek(), element))
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
            if (m_ActionOnRelease != null)
                m_ActionOnRelease(element);
            m_Stack.Push(element);
        }
    }

    public static class ListPool<T>
    {
        // object pool to avoid allocations
        private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, l => l.Clear());
        public static List<T> Get(){ return s_ListPool.Get(); }
        public static void Release(List<T> toRelease){ s_ListPool.Release(toRelease); }
    }
}