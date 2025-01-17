using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T _instance;

    public static T Instance => _instance;

    protected virtual void Awake()
    {
        _instance = this as T;
    }

    void Start()
    {
        Init();
    }


    public abstract void Init();

    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}

//使用方法 UI的面板继承这个基类， 并且写为单例，  Init() 是 初始化函数，实现时要去除自己本身的start（），ShowMe()，HideMe() 是这个单例的可见性函数，可以在子类重写