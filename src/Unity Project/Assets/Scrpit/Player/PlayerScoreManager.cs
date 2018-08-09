using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour, TListener {

    public float PlayerID; //当前玩家ID

    public int SkillScore; //一个技能所花费的分数
    public int BoxDestroyScore; //摧毁盒子获得的分数
    public int KillPlayerScore; //击杀敌人获得的分数


    public float RequireTime; //技能请求间隔

    private int CurrentScore;//当前分数


    private bool RequireAvaliable;//当前是否可以请求升级
    private float RequireTiming;//技能请求计时器

    private void Start()
    {
        CurrentScore = 0;
        RequireAvaliable = true;
        RequireTiming = 0;
        /*
         * 注册监听器
         * 事件分别为
         * 摧毁方块 选手操作：加强炸弹 
         * 选手操作：回血 选手操作：加强射击
         */
        EventManager.Instance.AddListener(EVENT_TYPE.BOX_DESTROY, this);
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_BUFF_BOMB, this);
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_BUFF_HP, this);
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_BUFF_SHOOT, this);
        
    }

    //分数只读接口
    public int getScore()
    {
        return CurrentScore;
    }

    private void FixedUpdate()
    {
        Timing(); //计时器
    }


    //增加分数 2018年8月5日截止有两种分数增加形式
    private void GainScore(string Type)
    {
        switch (Type)
        {
            case "BoxDestroy":
                CurrentScore += BoxDestroyScore;
                break;
            case "KillPlayer":
                CurrentScore += KillPlayerScore;
                break;
        }
    }

    //发动技能的扣分接口
    public bool Upgrade()
    {
        //如果当前分数大于所需分数 扣分
        if (CurrentScore >= SkillScore) 
        {
            CurrentScore -= SkillScore;
            return true;
        }
        //否则拒绝操作
        else
        {
            return false;
        }
    }

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

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        bool isSuccess = false;
        switch (Event_Type)
        {
            case EVENT_TYPE.BOX_DESTROY: //监测到方块摧毁事件
                if (PlayerID == (int)value["PlayerID"]) //当摧毁方块的玩家和当前玩家相同时
                {
                    GainScore("BoxDestroy");//加分
                    return true;
                }
                else
                    return false;

            case EVENT_TYPE.TURING_BUFF_SHOOT: //检测到加强射击事件
                //当技能请求可用并且分数足够时
                if (RequireAvaliable && (isSuccess = Upgrade()))
                {
                    //发送加强射击至具体实现类
                    isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.SHOOT_BUFF, this, gameObject);
                    RequireAvaliable = false; //置请求不可用
                }
                return isSuccess;

            //注释见上
            case EVENT_TYPE.TURING_BUFF_HP:
                isSuccess = false;
                if((gameObject.GetComponent<PlayerHealth>().GetHP()) <= 100)
                {
                    if (RequireAvaliable && (isSuccess = Upgrade()))
                    {
                        isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.PLAYER_INCREASE_HP, this, gameObject);
                        RequireAvaliable = false;
                    }
                }
                return isSuccess;

            //注释见上
            case EVENT_TYPE.TURING_BUFF_BOMB:
                isSuccess = false;
                if (RequireAvaliable && (isSuccess = Upgrade()))
                {
                    isSuccess = EventManager.Instance.PostNotification(EVENT_TYPE.BOMB_BUFF, this, gameObject);
                    RequireAvaliable = false;
                }
                return isSuccess;

            default: return false;

        }
    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
