using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, TListener {

    public float GameTime; //（仅供测试）游戏总时间

    private static float RemainingTime; //游戏剩余时间

    private GameObject[] Players;
    private bool isGameRunning;

    //返回游戏剩余时间
    public static float GerRemainTime()
    {
        return RemainingTime;
    }

    void Start() {
        Players = GameObject.FindGameObjectsWithTag("Player");
        RemainingTime = GameTime;
        isGameRunning = false;
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
            Player.GetComponent<TuringOperate>().enabled = true;
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
            Player.GetComponent<TuringOperate>().enabled = false;
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
