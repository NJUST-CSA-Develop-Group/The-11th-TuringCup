using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,TListener {
    public int IncreaseHPValue;//仅供测试 技能增加的血量数值

    private int CurrentHP;//当前角色血量

    private void Start()
    {
        CurrentHP = 100;
        //注册监听器 事件分别为 炸弹爆炸 角色回血（具体操作）
        EventManager.Instance.AddListener(EVENT_TYPE.BOMB_EXPLODE, this);
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_INCREASE_HP, this);
    }

    //血量的只读接口
    public int GetHP()
    {
        return CurrentHP;
    }

    //伤害接口
    private void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;
        if (CurrentHP <= 0)//角色血量小于0时死亡
        {
            CurrentHP = 0;
            PlayerDeath();
        }
    }

    //血量增加函数
    private bool IncreaseHP()
    {
        CurrentHP += IncreaseHPValue;
        return true;
    }

    private void PlayerDeath()
    {
        // TODO 角色死亡 设置动画 禁用脚本 传递分数
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.BOMB_EXPLODE: //炸弹爆炸事件
                TakeDamage((int)value["BombPower"]); //造成伤害并传递炸弹威力数值
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
