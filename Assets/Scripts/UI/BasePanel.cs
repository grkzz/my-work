using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板基类 所有面板 都会继承 方便使用 节约代码量
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BasePanel<T> : MonoBehaviour where T: class
{
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = this as T;    
    }

    void Start()
    {
        //父类当中会去强行调用 初始化方法
        //该初始化方法 又是一个抽象函数 子类必须去实现
        Init();
    }

    //主要用于 初始化 控件的事件监听 等等的逻辑
    public abstract void Init();
    
    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideMe()
    {
        gameObject.SetActive(false);
    }
}
