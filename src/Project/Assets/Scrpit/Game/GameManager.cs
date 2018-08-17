using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, TListener {

    public float GameTime; //游戏总时间

    private static float RemainingTime; //游戏剩余时间
    private static bool isGameRunning; //目前游戏是否在进行

    private GameObject[] Players; //获取角色引用 以对角色的脚本进行操作

    //游戏剩余时间只读接口
    public static float GetRemainTime()
    {
        return RemainingTime;
    }

    void Start() {
        Players = GameObject.FindGameObjectsWithTag("Player");
        RemainingTime = GameTime;
        isGameRunning = false;
        //注册监听器
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_START, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_OVER, this);

    }

    private void FixedUpdate()
    {
        //如果游戏正在进行
        if (isGameRunning)
        {
            Timing(); 
            //如果游戏时间到
            if(RemainingTime <= 0)
            {
                //发送GAME_OVER事件 结束游戏进行
                EventManager.Instance.PostNotification(EVENT_TYPE.GAME_OVER, this);
                isGameRunning = false;
                RemainingTime = 0;
            }
        }
    }

    //游戏时间计时器
    private void Timing()
    {
        RemainingTime -= Time.deltaTime;
    }

    //开启所有角色操控脚本
    private void SetPlayerFunctionEnabled()
    {

        foreach (GameObject Player in Players)
        {
            Player.GetComponent<PlayerBomb>().enabled = true;
            Player.GetComponent<PlayerMovement>().enabled = true;
            Player.GetComponent<PlayerShoot>().enabled = true;
            if (Player.GetComponent<TuringOperateOne>())
            {
                Player.GetComponent<TuringOperateOne>().enabled = true;
            }
            if (Player.GetComponent<TuringOperateTwo>())
            {
                Player.GetComponent<TuringOperateTwo>().enabled = true;
            }
            if (Player.GetComponent<TuringOperateThree>())
            {
                Player.GetComponent<TuringOperateThree>().enabled = true;
            }
            if (Player.GetComponent<TuringOperateFour>())
            {
                Player.GetComponent<TuringOperateFour>().enabled = true;
            }
        }

    }

    //关闭所有角色操控脚本
    private void SetPlayerFunctionDisabled()
    {
        foreach (GameObject Player in Players)
        {
            Player.GetComponent<PlayerBomb>().enabled = false;
            Player.GetComponent<PlayerMovement>().enabled = false;
            Player.GetComponent<PlayerShoot>().enabled = false;
            if (Player.GetComponent<TuringOperateOne>())
            {
                Player.GetComponent<TuringOperateOne>().enabled = false;
            }
            if (Player.GetComponent<TuringOperateTwo>())
            {
                Player.GetComponent<TuringOperateTwo>().enabled = false;
            }
            if (Player.GetComponent<TuringOperateThree>())
            {
                Player.GetComponent<TuringOperateThree>().enabled = false;
            }
            if (Player.GetComponent<TuringOperateFour>())
            {
                Player.GetComponent<TuringOperateFour>().enabled = false;
            }
        }
    }
    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            //游戏开始时 开启脚本 开启游戏
            case EVENT_TYPE.GAME_START:
                SetPlayerFunctionEnabled();
                isGameRunning = true;
                return true;
            //游戏结束时 关闭脚本 结束游戏
            case EVENT_TYPE.GAME_OVER:
                SetPlayerFunctionDisabled();
                isGameRunning = false;
                return true;
            default:return false;
        }

    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
