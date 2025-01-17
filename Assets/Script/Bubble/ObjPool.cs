using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjPool<TQ, T> : MonoBehaviour where TQ : class//<对象池,对象>
{
    private static TQ _instance;
    public static TQ Instance => _instance;
    protected Queue<T> Pool = new Queue<T>();

    protected virtual void Awake()
    {
        _instance = this as TQ;
    }
    //实现子类的单例模式
    private void Start()
    {
        Init();
    }
    public abstract void Init();
    //提供的初始化方法 子类不要start（）
    
    public T GetObj()
    {
        if (Pool.Count != 0)
            return IniObj(Pool.Dequeue());
        else
            return IniObj(CreateObj());
    }
    public void PutObj(T obj)
    {
        StartCoroutine(RecyleObj(obj));
    }
    
    //核心方法 GetObj()从池中取物  PutObj()还物体于池   
    
    protected abstract T IniObj(T obj );
    //取物时的初始化方法
    protected abstract T CreateObj();
    //新建对象的方法
    protected abstract IEnumerator RecyleObj(T obj);
    //归对象时的协程
    protected abstract void DestroyObj(T obj);
    //销毁对象的方法
    protected abstract void IniPool(int size);
    //对象池初始化
    public void SizeReset(int size)
    {
        while (Pool.Count > size)
        {
            T obj = Pool.Dequeue();
            DestroyObj(obj);
        }
    }
    //对象池重设大小（当生成了过多多余对象
}