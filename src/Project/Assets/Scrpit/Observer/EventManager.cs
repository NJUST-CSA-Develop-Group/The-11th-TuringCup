// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;//访问附加的Mono类 同时还包括Dictionary类
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // 事件管理器的实例（在这里主要是实现单例访问）
    private static EventManager instance = null;
    private float ClearTiming; //监听清理器计时器
    /*
     * 事件-监听器链表 键值对
     * 用来存储并管理所有的监听器
     */
    private Dictionary<EVENT_TYPE, List<TListener>> Listeners = new Dictionary<EVENT_TYPE, List<TListener>>();

    // 事件管理器的只读接口
    public static EventManager Instance
    {
        get { return instance; }
        set { }
    }

    // 在程序运行时调用 实现单例访问
    private void Awake()
    {
        ClearTiming = 0;
        //如果不存在事件管理器 则创建
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        //如果存在 则销毁自身
        else
            DestroyImmediate(this);

    }

    private void FixedUpdate()
    {
        ClearTiming += Time.deltaTime;
        //事件管理器每5秒执行一次无效监听器清理
        if(ClearTiming >= 5)
        {
            ClearTiming = 0;
            RemoveRedundancies();
        }
    }

    /// <summary>
    /// 注册监听器 在监听器类的Start函数调用
    /// </summary>
    /// <param name="Event_Type">监听的事件</param>
    /// <param name="Listener">事件的监听器</param>
    public void AddListener(EVENT_TYPE Event_Type, TListener Listener)
    {
        List<TListener> ListenList = null;

        //如果当前事件的监听器不为空 则直接添加监听器至链表
        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            ListenList.Add(Listener);
        }
        //如果当前事件的监听器为空 则新建链表 添加键值对
        else
        {
            ListenList = new List<TListener>();
            ListenList.Add(Listener);
            Listeners.Add(Event_Type, ListenList);
        }
    }

    /// <summary>
    /// 事件传递函数 ===传递事件的关键函数===
    /// </summary>
    /// <param name="Event_Type">将要被处理的事件</param>
    /// <param name="Sender">发送事件的组件</param>
    /// <param name="Param">可选参数 指定响应本事件的对象</param>
    /// <param name="Value">可选参数 可以传递参数</param>
    public bool PostNotification(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        List<TListener> ListenList = null;

        //如果事件对应的监听器为空 直接返回
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
        {
            return false;
        }
        //遍历事件的所有监听器的事件处理函数
        for (int i = 0; i < ListenList.Count; i++)
        {
            //如果param不为空 则寻找特定的对象
            if (param)
            {
                if(!ListenList[i].Equals(null) && param == ListenList[i].getGameObject())
                {
                    //寻找到特定对象后执行操作并返回操作是否成功
                    return ListenList[i].OnEvent(Event_Type, Sender, param, value);
                }
                

            }
            
            //如果没有特定对象（广播事件）则遍历事件所有的监听器
            else if (!ListenList[i].Equals(null))
            {
                ListenList[i].OnEvent(Event_Type, Sender, param, value);//调用函数 传参
            }
                
        }
        return true;
    }

    //注销事件
    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        Listeners.Remove(Event_Type);
    }

    //删除无效的监听器
    private void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<TListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<TListener>>();

        //遍历所有事件
        foreach (KeyValuePair<EVENT_TYPE, List<TListener>> Item in Listeners)
        {
            //遍历事件的所有监听器
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                //如果监听器的引用为null 则删除监听器
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }

            //重构监听器链表
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }

        Listeners = TmpListeners;
    }
}
