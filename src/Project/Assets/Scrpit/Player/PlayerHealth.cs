﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,TListener {
    public int increaseHPValue;//仅供测试 技能增加的血量数值
    private bool hadTreat = false;

    private int currentHP;//当前角色血量
    private bool isDead;
    private int _dealthFrom;

    private void Start()
    {
        currentHP = 100;
        //注册监听器 事件分别为 炸弹爆炸 角色回血（具体操作）
        EventManager.Instance.AddListener(EVENT_TYPE.BOMB_EXPLODE, this);
        EventManager.Instance.AddListener(EVENT_TYPE.SHOOT_HIT_PLAYER, this);
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_INCREASE_HP, this);
    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Global").GetComponent<MapManager>().
            GetBoxType((int)(transform.position.x + 0.5), (int)(transform.position.z + 0.5)) == -1 && !isDead
            )
        {
            currentHP = System.Int32.MinValue;//负无穷生命
            //currentHP = 0;//保证0血
            PlayerDeath(-1);
        }
    }
    //血量的只读接口
    public int GetHP()
    {
        return currentHP;
    }

    //生命结算
    public void Settlement()
    {
        if (isDead)
        {
            return;
        }
        currentHP = Mathf.Min(Mathf.Max(currentHP, 0), 100);
        if (currentHP == 0)
        {
            isDead = true;

            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerBomb>().enabled = false;
            GetComponent<PlayerShoot>().enabled = false;

            Dictionary<string, object> TempDic = new Dictionary<string, object>();
            TempDic.Add("AttackerID", _dealthFrom);
            EventManager.Instance.PostNotification(EVENT_TYPE.PLAYER_DEAD, this, null, TempDic);
            TempDic.Clear();
        }
    }

    //伤害接口
    private void TakeDamage(int Damage, int Attacker)
    {
        if (isDead)
        {
            return;
        }
        currentHP -= Damage;
        if (currentHP <= 0 && !isDead)//角色血量小于0时死亡
        {
            //currentHP = 0;
            PlayerDeath(Attacker);
        }
    }

    //血量增加函数
    private bool IncreaseHP()
    {
        currentHP += increaseHPValue;
        //currentHP = Mathf.Min(100, currentHP);
        hadTreat = true;
        return true;
    }

    public bool HadTreat()
    {
        bool _hadtreat = hadTreat;
        hadTreat = false;
        return _hadtreat;
    }

    private void PlayerDeath(int Attacker)
    {
        _dealthFrom = Attacker;
        /*isDead = true;

        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerBomb>().enabled = false;
        GetComponent<PlayerShoot>().enabled = false;

        Dictionary<string, object> TempDic = new Dictionary<string, object>();
        TempDic.Add("AttackerID", Attacker);
        EventManager.Instance.PostNotification(EVENT_TYPE.PLAYER_DEAD, this, null, TempDic);
        TempDic.Clear();*/
        // TODO 角色死亡 设置动画 禁用脚本 传递分数
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.BOMB_EXPLODE: //炸弹爆炸事件
                TakeDamage((int)value["BombPower"], (int)value["AttackerID"]); //造成伤害并传递炸弹威力数值
                return true;
            case EVENT_TYPE.SHOOT_HIT_PLAYER: //子弹命中事件
                TakeDamage((int)value["ShootPower"], (int)value["AttackerID"]); //造成伤害
                return true;
            case EVENT_TYPE.PLAYER_INCREASE_HP: //角色回血（具体操作事件）
                return IncreaseHP();
            default: return false;
        }
    }

    public Object getGameObject()
    {
        return gameObject;
    }
}