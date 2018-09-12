using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZS.Collections
{
	// 线程安全的dictionary
    public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>{
    	private readonly object syncRoot = new object();
    	private readonly Dictionary<TKey, TValue> d = new Dictionary<TKey, TValue>();

    	// 定义继承的方法
    	public void Add(TKey key, TValue value)
    	{
    		lock(syncRoot)
    		{
    			d.Add(key,value);
    		}
    	}

    	public bool Remove(TKey key)
    	{
    		lock(syncRoot)
    		{
    			return d.Remove(key);
    		}
    	}

    	public bool TryGetValue(TKey key, out TValue value)
    	{
    		lock(syncRoot)
    		{
    			return d.TryGetValue(key, out value);
    		}
    	}

    	public bool ContainsKey(TKey key){ return d.ContainsKey(key); }

    	public ICollection<TKey> Keys {
    		get{
    			lock(syncRoot)
    			{
    				return d.Keys;
    			}
    		}
    	}

    	public ICollection<TValue> Values {
    		get{
    			lock(syncRoot)
    			{
    				return d.Values;
    			}
    		}
    	}

    	public TValue this [TKey key]
    	{
    		get { return d[key]; }
    		set {
    			lock(syncRoot)
    			{
    				d[key] = value;
    			}
    		}
    	}

    	public void Add(KeyValuePair<TKey, TValue> item)
    	{
    		lock(syncRoot)
    		{
    			((ICollection<KeyValuePair<TKey, TValue>>)d).Add(item);
    		}
    	}

    	public void Clear()
    	{
    		lock(syncRoot)
    		{
    			d.Clear();
    		}
    	}

    	public bool Contains(KeyValuePair<TKey, TValue> item){
    		return ((ICollection<KeyValuePair<TKey, TValue>>)d).Contains(item);
    	}

    	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    	{
    		lock(syncRoot)
    		{
    			((ICollection<KeyValuePair<TKey, TValue>>)d).CopyTo(array, arrayIndex);
    		}
    	}

    	public int Count{ get{ return d.Count; } }
    	public bool IsReadOnly { get{ return false; } }

    	public bool Remove(KeyValuePair<TKey, TValue> item)
    	{
    		lock(syncRoot)
    		{
    			return ((ICollection<KeyValuePair<TKey, TValue>>)d).Remove(item);
    		}
    	}

    	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    	{
    		return ((ICollection<KeyValuePair<TKey, TValue>>)d).GetEnumerator();
    	}

    	IEnumerator IEnumerable.GetEnumerator()
    	{
    		return ((IEnumerable)d).GetEnumerator();
    	}
    }
}
