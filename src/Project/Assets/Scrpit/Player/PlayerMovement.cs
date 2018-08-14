using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, TListener
{
    public float moveSpeed;//角色移动的速度

    private bool isMoving; //角色目前是否在移动
    private Vector3 StartPosition; //角色移动的初始位置
    private Vector3 TargetPosition; //角色移动的目标位置

    private GameObject Global; //全局脚本绑定的GameObject引用
    private MapManager Map; //地图脚本引用

    private Animator Anim;
    //玩家目前移动状态的只读接口
    public bool IsMoving()
    {
        return isMoving;
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        Global = GameObject.FindGameObjectWithTag("Global");//此处Tag后期注意修改
        Map = Global.GetComponent<MapManager>(); //获取MapManager的引用
        isMoving = false;
        StartPosition = new Vector3();
        TargetPosition = new Vector3();
        //注册四个移动方向的监听器
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_MOVE_NORTH, this);
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_MOVE_SOUTH, this);
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_MOVE_WEST, this);
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_MOVE_EAST, this);
    }

    private void FixedUpdate()
    {
        //如果正在移动
        if (isMoving)
        {
            Move();
        }
    }


    private void Move()
    {
        if (transform.position == TargetPosition)
        {
            isMoving = false;
            Anim.SetBool("isMoving", false);
            return;
        }
        if (Map.GetBoxType((int)TargetPosition.x, (int)TargetPosition.z) != 0) //判断目标位置是否可用
        {
            
            if(transform.position != StartPosition)
            {
                TargetPosition = StartPosition;
                //TODO 如果初始位置也被占用的情况
            }
            else
            {
                isMoving = false;
                Anim.SetBool("isMoving", false);
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.TURING_MOVE_NORTH:
                if (!isMoving && (int)(transform.position.z + 0.5) < 14)
                {
                    StartPosition.Set((int)(transform.position.x + 0.5), 0f, (int)(transform.position.z + 0.5));
                    TargetPosition.Set((int)(transform.position.x + 0.5), 0f, (int)(transform.position.z + 0.5) + 1);
                    isMoving = true;
                    Anim.SetBool("isMoving", true);
                    return true;
                }
                else
                {
                    return false;
                }
            case EVENT_TYPE.TURING_MOVE_SOUTH:
                if (!isMoving && (int)(transform.position.z + 0.5) > 0)
                {
                    StartPosition.Set((int)(transform.position.x + 0.5), 0f, (int)(transform.position.z + 0.5));
                    TargetPosition.Set((int)(transform.position.x + 0.5), 0f, (int)(transform.position.z + 0.5) - 1);
                    isMoving = true;
                    Anim.SetBool("isMoving", true);
                    return true;
                }
                else
                {
                    return false;
                }
            case EVENT_TYPE.TURING_MOVE_WEST:
                if (!isMoving && (int)(transform.position.x + 0.5) > 0)
                {
                    StartPosition.Set((int)(transform.position.x + 0.5), 0f, (int)(transform.position.z + 0.5));
                    TargetPosition.Set((int)(transform.position.x + 0.5) - 1, 0f, (int)(transform.position.z + 0.5));
                    isMoving = true;
                    Anim.SetBool("isMoving", true);
                    return true;
                }
                else
                {
                    return false;
                }
            case EVENT_TYPE.TURING_MOVE_EAST:
                if (!isMoving && (int)(transform.position.z + 0.5) < 14)
                {
                    StartPosition.Set((int)(transform.position.x + 0.5), 0f, (int)(transform.position.z + 0.5));
                    TargetPosition.Set((int)(transform.position.x + 0.5) + 1, 0f, (int)(transform.position.z + 0.5));
                    isMoving = true;
                    Anim.SetBool("isMoving", true);
                    return true;
                }
                else
                {
                    return false;
                }
            default: return false;
        }
    }


    public Object getGameObject()
    {
        return gameObject;
    }
}
