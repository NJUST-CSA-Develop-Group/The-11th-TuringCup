using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffer : MonoBehaviour {
    public float RequireTime = 10f; //技能请求间隔

    private bool RequireBuffState;//技能请求状态

    private bool RequireAvaliable;//当前是否可以请求升级
    private float RequireTiming;//技能请求计时器

    private void Start()
    {
        RequireBuffState = false;
        RequireAvaliable = true;
        RequireTiming = 0;
    }
    private void FixedUpdate()
    {
        if (RequireAvaliable)
        {
            //GetBuff(); //如果技能可用 检测玩家请求
        }
        else
        {
            Timing(); //如果技能不可用 计时
        }
    }
    /*
    private bool GetBuff()
    {
        //玩家选择发动技能1：增加炸弹爆炸范围
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if ((RequireBuffState = gameObject.GetComponent<PlayerScoreManager>().Upgrade())) //积分扣除成功时才开启加强
            {
                gameObject.SendMessage("IncreaseBombArea");
                RequireAvaliable = false; //开启计时器
            }
            return RequireBuffState; //返回技能请求状态

        }

        //玩家选择发动技能2：增加血量
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if((RequireBuffState = gameObject.GetComponent<PlayerHealth>().IncreaseHP()))
            {
                RequireAvaliable = false;
            }
            return RequireBuffState;
        }


        return false;
    }
    */

    //技能计时器
    private void Timing()
    {
        if (!RequireAvaliable)
        {
            RequireTiming += Time.deltaTime;
        }
        if (RequireTiming >= RequireTime)
        {
            RequireAvaliable = true;
            RequireTiming = 0;
        }
    }
}
