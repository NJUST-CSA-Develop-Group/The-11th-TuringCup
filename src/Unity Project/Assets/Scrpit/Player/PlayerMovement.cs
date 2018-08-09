using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,TListener {
    public float moveSpeed;//角色移动的速度

    private Rigidbody playerRigid;//角色刚体
    private Vector3 Position;//玩家现在所处位置
    

    private void Start()
    {
        playerRigid = GetComponent<Rigidbody>(); //获取玩家刚体以操控角色
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_MOVE, this); //注册监听器 监听 选手操作：移动 事件
    }

    //角色移动,h控制东西，v控制南北
    private void Move(float h, float v)
    {
        Vector3 movement = new Vector3();
        movement.Set(h, 0f, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime; //一般化移动
        playerRigid.MovePosition(transform.position + movement); //移动角色位置
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.TURING_MOVE://检测到玩家移动事件
                Move((float)value["Horizontal"], (float)value["Vertical"]); //将移动参数传递至移动函数
                return true;
            default: return false;
        }
    }


    public Object getGameObject()
    {
        return gameObject;
    }
}
