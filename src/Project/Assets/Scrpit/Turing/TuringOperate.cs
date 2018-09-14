using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInterface;

public class TuringOperate : MonoBehaviour, IEntity, TListener
{
    public IControl AIScript;
    public bool active = false;// 默认禁用控制器

    private MapManager map;
    private GameObject[] players;
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Global").GetComponent<MapManager>();
        players = GameObject.FindGameObjectsWithTag("Player");
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_DEAD, this);
    }

    void FixedUpdate()
    {
        if (active)
        {
            AIScript.Update(this);
        }
    }

    void Update()
    {

    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        if (Event_Type == EVENT_TYPE.PLAYER_DEAD && Sender.gameObject == gameObject)//对应玩家死亡
        {
            active = false;
            return true;
        }
        return false;
    }

    public Object getGameObject()
    {
        return gameObject;
    }

    public bool MoveNorth()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_MOVE_NORTH, this, gameObject);
    }

    public bool MoveSouth()
    {

        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_MOVE_SOUTH, this, gameObject);
    }

    public bool MoveWest()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_MOVE_WEST, this, gameObject);
    }

    public bool MoveEast()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_MOVE_EAST, this, gameObject);
    }

    public bool TurnNorth()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_TURN_NORTH, this, gameObject);
    }

    public bool TurnSouth()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_TURN_SOUTH, this, gameObject);
    }

    public bool TurnEast()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_TURN_EAST, this, gameObject);
    }

    public bool TurnWest()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_TURN_WEST, this, gameObject);
    }

    public bool Shoot()
    {
        //发送射击（指定了执行对象）
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_FIRE, this, gameObject);
    }

    public bool SetBomb()
    {
        //发送放置炸弹（指定了执行对象）
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_SET_BOMB, this, gameObject);
    }

    public bool BuffSpeed()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_BUFF_SPEED, this, gameObject);
    }

    public bool BuffShoot()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_BUFF_SHOOT, this, gameObject);
    }

    public bool BuffBomb()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_BUFF_BOMB, this, gameObject);
    }

    public bool BuffHP()
    {
        return EventManager.Instance.PostNotification(EVENT_TYPE.TURING_BUFF_HP, this, gameObject);
    }

    public float GetRemainingTime()
    {
        return GameManager.GetRemainTime();
    }

    public int? GetMapType(int row, int col)
    {
        if(0 <= row && 13 >= row && 0 <= col && 13 >= col) //检查请求是否合法
        {
            return map.GetBoxType(row, col);
        }
        else
        {
            return null;
        }
    }

    public int GetHP()
    {
        return GetComponent<PlayerHealth>().GetHP();
    }

    public int[] GetPosition()
    {
        int[] position = { (int)(transform.position.x + 0.5), (int)(transform.position.z + 0.5) };
        return position;
    }

    public int GetIndex()
    {
        return GetComponent<PlayerScoreManager>().playerID;
        //TODO 将来playerID转换成私有变量时 此处进行修改
    }

    public int GetScore()
    {
        return GetComponent<PlayerScoreManager>().GetScore();
    }

    public bool CanShoot()
    {
        return GetComponent<PlayerShoot>().CanShoot();
    }

    public bool CanBomb()
    {
        return GetComponent<PlayerBomb>().CanBomb();
    }

    public int[] PlayerPosition(int PlayerIndex)
    {
        foreach(GameObject player in players)
        {
            if (PlayerIndex == player.GetComponent<PlayerScoreManager>().playerID)
            {
                int[] position = { (int)(player.transform.position.x + 0.5), (int)(player.transform.position.z + 0.5) }; //四舍五入处理
                return position;
            }
        }
        return null;
    }

    public int? PlayerHealth(int PlayerIndex)
    {
        foreach (GameObject player in players)
        {
            if (PlayerIndex == player.GetComponent<PlayerScoreManager>().playerID)
            {
                return player.GetComponent<PlayerHealth>().GetHP();
            }
        }
        return null;
    }

    public int? PlayerScore(int PlayerIndex)
    {
        foreach (GameObject player in players)
        {
            if (PlayerIndex == player.GetComponent<PlayerScoreManager>().playerID)
            {
                return player.GetComponent<PlayerScoreManager>().GetScore();
            }
        }
        return null;
    }
}
