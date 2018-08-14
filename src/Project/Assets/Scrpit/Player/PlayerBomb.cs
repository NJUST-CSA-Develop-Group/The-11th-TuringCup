using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour, TListener {

    public GameObject Bomb;//将要放置的炸弹Prefab

    public float BombCD; //炸弹放置冷却时间
    public int PlayerID; //玩家ID
    public float CurrentBombArea; //当前炸弹爆炸范围
    public float BuffTime; //加强时间
    public float BuffValue; //加强数值（此处为爆炸范围）


    private bool BombAvaliable = false; //当前是否允许放置炸弹
    private float SetBombTiming; //放置炸弹计时器

    private bool IsBuffing = false; //当前是否加强
    private float BuffTiming; //加强计时器

	void Start () {
        BombAvaliable = true;
        IsBuffing = false;
        SetBombTiming = 0;
        BuffTiming = 0f;

        EventManager.Instance.AddListener(EVENT_TYPE.TURING_SET_BOMB, this); //注册监听器 监听放置炸弹
        EventManager.Instance.AddListener(EVENT_TYPE.BOMB_BUFF, this); //注册监听器 监听加强炸弹
    }

    private void FixedUpdate()
    {
        Timing();//计时器
    }

    //如果玩家选择放置炸弹并且技能不在CD
    private void SetBomb()
    {
        //创建炸弹
        GameObject newBomb = Instantiate(Bomb, new Vector3((transform.position.x), -0.15f, (transform.position.z)), gameObject.transform.rotation);
        BombAvaliable = false;
        //发送炸弹设置事件
        Dictionary<string, object> TempDic = new Dictionary<string, object>();
        TempDic.Add("PlayerID", PlayerID);
        TempDic.Add("BombArea", CurrentBombArea);
        EventManager.Instance.PostNotification(EVENT_TYPE.BOMB_SET_INFO, this, newBomb, TempDic);
        TempDic.Clear(); //及时清理TempDic以释放内存
    }

    //计时器 包括炸弹放置和技能计时
    private void Timing()
    {
        if (!BombAvaliable)//技能冷却时计时
        {
            SetBombTiming += Time.deltaTime;
        }
        if(SetBombTiming >= BombCD)//冷却时间到时 设置炸弹可用 并重置计时器
        {
            BombAvaliable = true;
            SetBombTiming = 0;
        }

        if (IsBuffing)//如果存在加强炸弹 则进行计时
        {
            BuffTiming += Time.deltaTime;
        }
        if(BuffTiming >= BuffTime) //加强时间到 取消加强
        {
            IsBuffing = false;
            CurrentBombArea -= BuffValue;
            BuffTiming = 0;
        }
    }

    //加强技能 在OnEvent内调用
    public void IncreaseBombArea()
    {
        IsBuffing = true;//设置处于加强状态
        CurrentBombArea += BuffValue;
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.TURING_SET_BOMB: //选手操作：放置炸弹
                if (BombAvaliable && !GetComponent<PlayerMovement>().IsMoving())//如果当前可放置炸弹
                {
                    SetBomb();
                    GetComponent<Animator>().SetTrigger("SetBomb");
                    return true;
                }
                else
                {
                    return false;
                }
            case EVENT_TYPE.BOMB_BUFF: //选手操作：加强炸弹
                IncreaseBombArea();
                return true;
            default: return false;
        }
    }

    public Object getGameObject()
    {
        return gameObject;
    }

}
