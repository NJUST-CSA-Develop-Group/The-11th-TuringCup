﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour, TListener {
    public float explosionTime; //仅供测试 炸弹爆炸时间
    public int explosionPower;//仅供测试 炸弹威力

    private bool hadSetInfo = false; //炸弹是否设置了基本信息
    private int bombOwner = 0; //炸弹所有者编号
    private float timing = 0; //炸弹倒计时  

    private float bombRadius; //炸弹爆炸范围 

    private bool playerFirstExit; //

    private ParticleSystem explosionSmoke;

    /*
     * ----------------------注意----------------------
     * 在Unity中 炸弹的Inspector排序中
     * 默认未启用的Sphere Collider必须在默认启用的Sphere Collider的上面
     * 否则在当前代码的条件下无法实现玩家退出炸弹后
     * 不能再次进入炸弹模型的功能
     */
    private SphereCollider spherecollider; //获取炸弹的物理碰撞器

    private void Awake()
    {
        /*
         * 由于创建炸弹实例之后
         * PlayerBomb马上会发送BOMB_SET_INFO事件
         * 故监听器的注册提前到Awake阶段
         */
        EventManager.Instance.AddListener(EVENT_TYPE.BOMB_SET_INFO, this); //注册监听器 监听设置信息事件
    }

    //初始化其他变量
    void Start () {
        timing = 0f;
        spherecollider = GetComponent<SphereCollider>();
        hadSetInfo = false;
        explosionSmoke = GetComponent<ParticleSystem>();
        playerFirstExit = true;
        Dictionary<string, object> TempDic = new Dictionary<string, object>();
        TempDic.Add("MapCol", (int)(transform.position.x + 0.5));
        TempDic.Add("MapRow", (int)(transform.position.z + 0.5));
        TempDic.Add("MapType", 3);
        EventManager.Instance.PostNotification(EVENT_TYPE.MAP_UPDATE_INFO, this, null, TempDic);
        TempDic.Clear();
    }

    private void FixedUpdate()
    {
        if (timing >= explosionTime)
        {
            timing = -1f;//计时器置-1 防止多次发送事件
            Explode();
        }
        else if (timing >= 0)
        {
            BoomTiming(); //时间未到 继续计时
        }

    }
    
    //单次设置炸弹拥有者
    //本函数会在炸弹创建同时调用
    public void SetBombInfo(int PlayerID, float Radius)
    {
        if (!hadSetInfo)//只有在没有设置信息的情况下才允许设置
        {
            bombOwner = PlayerID; //设置角色ID
            bombRadius = Radius; //设置炸弹范围
            hadSetInfo = true; //不允许再次设置
        }
    }

    //炸弹爆炸计时函数
    private void BoomTiming()
    {
            timing += Time.deltaTime;
    }

    //本函数只会被调用一次 当玩家退出炸弹的碰撞体后激活炸弹的碰撞体 玩家不能再次进入
    private void OnTriggerExit(Collider other)
    {
        if (playerFirstExit)
        {
            if (other.CompareTag("Player"))
            {
                spherecollider.enabled = true;//启动物理碰撞器
                playerFirstExit = false;
            }
        }
        
    }

    private void Explode()
    {
        //获取爆炸范围内的所有在Attackable层的物体（包括可炸方块和玩家）
        Collider[] colliders = Physics.OverlapSphere(transform.position, bombRadius,LayerMask.GetMask("Attackable"));
        Dictionary<string, object> TempDic = new Dictionary<string, object>();
        spherecollider.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        explosionSmoke.Play();
        TempDic.Add("AttackerID", bombOwner);
        TempDic.Add("BombPower", explosionPower);
        TempDic.Add("MapCol", (int)(transform.position.x + 0.5));
        TempDic.Add("MapRow", (int)(transform.position.z + 0.5));
        TempDic.Add("MapType", 0);
        EventManager.Instance.PostNotification(EVENT_TYPE.MAP_UPDATE_INFO, this, null, TempDic);
        foreach (Collider hit in colliders)
        {
            //向在Attackable层内的 在爆炸范围内的每个实例发送事件（指定了特定对象）
            EventManager.Instance.PostNotification(EVENT_TYPE.BOMB_EXPLODE, this, hit.gameObject, TempDic);
        }
        TempDic.Clear();
        Destroy(gameObject, 1.5f);//延迟销毁 播放动画
        GetComponent<AudioSource>().Play();
    }

    //仅用于调试（在Scene中画出爆炸范围）
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, bombRadius);
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            case (EVENT_TYPE.BOMB_SET_INFO): //设置信息事件
                SetBombInfo((int)value["AttackerID"],(float)value["BombArea"]); //传参
                return true;
            default:return false;
        }
    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
