using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, PlayerInterface.IEntity
{
    public int index;// 玩家序号，需在编辑器中设置

    PlayerInterface.IControl controller = null;// 被加载的控制器对象

    // Use this for initialization
    void Start()
    {
        if (ControlLoader.controllers != null)
        {
            controller = ControlLoader.controllers[index];// 获取已加载的控制器
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null)
        {
            controller.Update(this);// 调用控制器，并向其提供服务接口
        }
    }

    // TODO: 目前所有接口暂未实现

    // 提供操作的部分
    public void Move(int direct) { }// 移动
    public void Shoot()// 射击
    {
        GetComponent<PlayerShoot>().Shoot();
    }
    public bool PlaceBomb() { return false; }// 放置炸弹
    public bool Upgrade(int index) { return false; }// 兑换属性

    // 提供信息的部分
    public int[][] MapData() { return null; }// 地图信息
    public PlayerInterface.PlayerData[] PlayerData() { return null; }// 玩家信息
    public float Time() { return 0f; }// 剩余时间
    public int Index()// 玩家的序号
    {
        return index;
    }
}
