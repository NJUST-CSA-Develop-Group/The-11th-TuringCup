using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour, TListener {

    public GameObject bomb;//将要放置的炸弹Prefab

    public float bombCD; //炸弹放置冷却时间
    public int playerID; //玩家ID
    public float currentBombArea; //当前炸弹爆炸范围
    public float buffTime; //加强时间
    public float buffValue; //加强数值（此处为爆炸范围）

    private bool bombAvaliable = false; //当前是否允许放置炸弹
    private float setBombTiming; //放置炸弹计时器

    private bool isBuffing = false; //当前是否加强
    private float buffTiming; //加强计时器

    public float? getBuffing()
    {
        if (isBuffing)
        {
            return buffTime - buffTiming;
        }
        else
        {
            return null;
        }
    }
    void Start () {
        bombAvaliable = true;
        isBuffing = false;
        setBombTiming = 0;
        buffTiming = 0f;

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
        GameObject newBomb = Instantiate(bomb, new Vector3((transform.position.x), 0, (transform.position.z)), gameObject.transform.rotation);
        bombAvaliable = false;
        //发送炸弹设置事件
        //发送地图更新事件
        Dictionary<string, object> TempDic = new Dictionary<string, object>();
        TempDic.Add("AttackerID", playerID);
        TempDic.Add("BombArea", currentBombArea);
        TempDic.Add("MapCol", (int)(transform.position.x + 0.5));
        TempDic.Add("MapRow", (int)(transform.position.z + 0.5));
        TempDic.Add("MapType", 3);
        EventManager.Instance.PostNotification(EVENT_TYPE.BOMB_SET_INFO, this, newBomb, TempDic);
        EventManager.Instance.PostNotification(EVENT_TYPE.MAP_UPDATE_INFO, this, null, TempDic);
        TempDic.Clear(); //及时清理TempDic以释放内存
    }

    //计时器 包括炸弹放置和技能计时
    private void Timing()
    {
        if (!bombAvaliable)//技能冷却时计时
        {
            setBombTiming += Time.deltaTime;
        }
        if(setBombTiming >= bombCD)//冷却时间到时 设置炸弹可用 并重置计时器
        {
            bombAvaliable = true;
            setBombTiming = 0;
        }

        if (isBuffing)//如果存在加强炸弹 则进行计时
        {
            buffTiming += Time.deltaTime;
        }
        if(buffTiming >= buffTime) //加强时间到 取消加强
        {
            isBuffing = false;
            currentBombArea -= buffValue;
            buffTiming = 0;
        }
    }

    //加强技能 在OnEvent内调用
    public void IncreaseBombArea()
    {
        isBuffing = true;//设置处于加强状态
        currentBombArea += buffValue;
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.TURING_SET_BOMB: //选手操作：放置炸弹
                if (bombAvaliable && !GetComponent<PlayerMovement>().IsMoving())//如果当前可放置炸弹
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
