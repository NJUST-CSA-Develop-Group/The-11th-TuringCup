using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, TListener
{

    public float GameTime; //游戏总时间
    public string AIConfFilePath;//AI脚本配置文件的路径

    private static float RemainingTime; //游戏剩余时间
    private static bool isGameRunning; //目前游戏是否在进行

    private GameObject[] Players; //获取角色引用 以对角色的脚本进行操作
    private PlayerInterface.IControl[] AIscripts;//控制脚本实体

    private int DeadPlayer;

    //游戏剩余时间只读接口
    public static float GetRemainTime()
    {
        return RemainingTime;
    }

    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        RemainingTime = GameTime;
        isGameRunning = false;
        DeadPlayer = 0;
        LoadAIs();//加载控制脚本
        //注册监听器
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_START, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_OVER, this);
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_DEAD, this);

    }

    private void FixedUpdate()
    {
        //如果游戏正在进行
        if (isGameRunning)
        {
            Timing();
            //如果游戏时间到
            if (RemainingTime <= 0 || DeadPlayer == 3)
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

    //从文件加载控制脚本，如果没有，则使用默认脚本
    private void LoadAIs()
    {
        AIscripts = new PlayerInterface.IControl[4];
        int index = 0;
        if (System.IO.File.Exists(AIConfFilePath))
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(new System.IO.FileStream(AIConfFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
            while (!sr.EndOfStream)
            {
                string path = sr.ReadLine().Trim();
                if (path == "")
                {
                    continue;
                }
                LoadAI(index, path);
                index++;
            }
            sr.Close();
        }
        for (; index < 4; index++)
        {
            LoadAI(index, "");
        }
    }

    private void LoadAI(int i, string path)
    {
        if (path != "" && System.IO.File.Exists(path))
        {
            System.Type[] type = System.Reflection.Assembly.LoadFrom(path).GetTypes();
            if (type.Length > 1)
            {
                AIscripts[i] = (PlayerInterface.IControl)System.Activator.CreateInstance(type[0]);//加载选手脚本
                return;
            }
        }
        AIscripts[i] = new DefaultOperate(i);//加载默认脚本
        return;
    }

    //对所有角色挂载控制脚本
    private void SetAI()
    {
        foreach (GameObject Player in Players)
        {
            int index = (int)Player.GetComponent<PlayerScoreManager>().playerID - 1;
            Player.GetComponent<TuringOperate>().AIScript = AIscripts[index];
        }
    }

    //开启所有角色操控脚本
    private void SetAIEnabled()
    {
        foreach (GameObject Player in Players)
        {
            Player.GetComponent<TuringOperate>().active = true;
        }
    }

    //关闭所有角色操控脚本
    private void SetAIDisabled()
    {
        foreach (GameObject Player in Players)
        {
            Player.GetComponent<TuringOperate>().active = true;
        }
    }
    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            //游戏开始时 开启脚本 开启游戏
            case EVENT_TYPE.GAME_START:
                SetAI();
                SetAIEnabled();
                isGameRunning = true;
                return true;
            //游戏结束时 关闭脚本 结束游戏
            case EVENT_TYPE.GAME_OVER:
                SetAIDisabled();
                isGameRunning = false;
                return true;
            case EVENT_TYPE.PLAYER_DEAD:
                DeadPlayer++;
                return true;
            default: return false;
        }

    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
