using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 枚举出可能出现的事件
 * 提高代码可读性
 * 事件不一定要为枚举类型
 * 也可以为其他基本类型
 */
public enum EVENT_TYPE {
    GAME_START, //游戏开始
    GAME_OVER, //游戏结束
    //--------------------
    TURING_MOVE,//仅供测试
    TURING_MOVE_NORTH, //玩家操作：向北移动
    TURING_MOVE_SOUTH, //玩家操作：向南移动
    TURING_MOVE_EAST, //玩家操作：向东移动
    TURING_MOVE_WEST, //玩家操作：向西移动
    TURING_FIRE, //玩家操作：开火
    TURING_SET_BOMB, //玩家操作：设置炸弹
    TURING_BUFF_BOMB, //玩家操作：增强炸弹
    TURING_BUFF_SHOOT, //玩家操作：增强射击
    TURING_BUFF_HP, //玩家操作：回血
    TURING_BUFF_SPEED, //玩家操作：加速
    //--------------------
    SPEED_BUFF, //角色加速（具体操作）
    //--------------------
    PLAYER_DEAD, //角色死亡
    PLAYER_INCREASE_HP, //角色回血（具体操作）
    //--------------------
    BOMB_SET_INFO, //设置炸弹信息
    BOMB_EXPLODE, //炸弹爆炸
    BOMB_BUFF, //增加炸弹爆炸范围（具体操作）
    //--------------------
    SHOOT_BUFF, //角色射击增强（具体操作）
    SHOOT_HIT_PLAYER, // 射击命中玩家
    //--------------------
    BOX_DESTROY, //方块被摧毁
    //--------------------
    MAP_UPDATE_INFO, //更新地图信息
    MAP_REDUCE_GAME_AREA, //缩小游戏区域
    //--------------------
    UI_UPDATE_REMAIN_TIME, // UI刷新剩余时间
    UI_UPDATE_PLAYER_STATE, // UI更新玩家状态
};

public interface TListener
{
    /// <summary>
    /// 事件处理接口
    /// </summary>
    /// <param name="Event_Type">事件类型</param>
    /// <param name="Sender">发送事件的游戏组件</param>
    /// <param name="param">可选参数 可指定事件的处理对象</param>
    /// <param name="value">可选参数 Dictionary类型 可传递游戏数值</param>
    bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null);

    /// <summary>
    /// 获取当前游戏对象的引用
    /// </summary>
    /// <returns>当前类游戏对象的引用（只读）</returns>
    Object getGameObject();
}
