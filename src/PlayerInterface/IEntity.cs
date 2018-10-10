using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerInterface
{
    /// <summary>
    /// 本处是提供用户操作服务的接口
    /// </summary>
    public interface IEntity
    {
        // 这个接口用于提供对当前玩家的操作和对环境感知信息的获取

        // 提供操作的部分

        // 移动
        /// <summary>
        /// 向北移动
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool MoveNorth();
        /// <summary>
        /// 向南移动
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool MoveSouth();
        /// <summary>
        /// 详细移动
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool MoveWest();
        /// <summary>
        /// 向东移动
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool MoveEast();

        /// <summary>
        /// 向北转向
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool TurnNorth();

        /// <summary>
        /// 向南转向
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool TurnSouth();

        /// <summary>
        /// 向西转向
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool TurnWest();

        /// <summary>
        /// 向东转向
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool TurnEast();

        // 射击
        /// <summary>
        /// 向角色前方射击
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool Shoot();

        // 放置炸弹
        /// <summary>
        /// 在角色当前的座标位置放置一颗炸弹
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool SetBomb();

        // 兑换属性

        /// <summary>
        /// 扣除一定积分 增加角色移动速度 持续5秒
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool BuffSpeed();
        /// <summary>
        /// 扣除一定积分 增加射击威力 持续5秒
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool BuffShoot();
        /// <summary>
        /// 扣除一定积分 扩大炸弹的爆炸范围 持续5秒
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool BuffBomb();
        /// <summary>
        /// 扣除一定积分 恢复一定血量
        /// </summary>
        /// <returns>操作是否成功</returns>
        bool BuffHP();

        // 提供信息的部分
        //int[][] MapData();// 地图信息
        //PlayerData[] PlayerData();// 玩家信息
        //float Time();// 剩余时间
        //int Index();// 玩家的序号

        //环境感知部分

        /// <summary>
        /// 获取剩余游戏时间
        /// </summary>
        /// <returns>剩余游戏时间</returns>
        float GetRemainingTime();

        /// <summary>
        /// 获取指定座标的属性
        /// </summary>
        /// <param name="row">指定行</param>
        /// <param name="col">指定列</param>
        /// <returns>座标的属性 -1 = 不可用  0 = 可用  1 = 可炸方块  2 = 不可炸方块  3 = 炸弹
        /// 查询非法 返回Null</returns>
        int? GetMapType(int row, int col);

        /// <summary>
        /// 获取当前角色血量
        /// </summary>
        /// <returns>血量</returns>
        int GetHP();

        /// <summary>
        /// 返回当前角色序号
        /// </summary>
        /// <returns>序号 范围1-4</returns>
        int GetIndex();

        /// <summary>
        /// 获取当角色位置
        /// </summary>
        /// <returns>以[0]为行、[1]为列的一位数组座标</returns>
        int[] GetPosition();

        /// <summary>
        /// 获取当前角色积分
        /// </summary>
        /// <returns>积分</returns>
        int GetScore();

        /// <summary>
        /// 获取是否可以射击
        /// </summary>
        /// <returns>是否可以射击</returns>
        bool CanShoot();

        /// <summary>
        /// 获取是否可以放置炸弹
        /// </summary>
        /// <returns>是否可以放置炸弹</returns>
        bool CanBomb();

        /// <summary>
        /// 获取指定角色位置
        /// </summary>
        /// <param name="PlayerIndex">指定玩家序号 范围1-4</param>
        /// <returns>以[0]为行、[1]为列的一位数组座标，若未找到，返回null</returns>
        int[] PlayerPosition(int PlayerIndex);

        /// <summary>
        /// 获取指定角色血量
        /// </summary>
        /// <param name="PlayerIndex">指定玩家序号 范围1-4</param>
        /// <returns>指定角色血量，若未找到，返回null</returns>
        int? PlayerHealth(int PlayerIndex);

        /// <summary>
        /// 获取指定玩家分数
        /// </summary>
        /// <param name="PlayerIndex">指定玩家序号 范围1-4</param>
        /// <returns>指定角色分数，若未找到，返回null</returns>
        int? PlayerScore(int PlayerIndex);

        /// <summary>
        /// 获取外围缩圈的圈数
        /// </summary>
        /// <returns>缩圈的圈数</returns>
        int GetCircle();

    }

    /// <summary>
    /// 用于收集角色信息
    /// </summary>
    public struct PlayerData
    {
        /// <summary>
        /// 角色位置X
        /// </summary>
        public int x;
        /// <summary>
        /// 角色位置Z
        /// </summary>
        public int z;
        /// <summary>
        /// 角色血量
        /// </summary>
        public int HP;
        /// <summary>
        /// 角色升级情况
        /// </summary>
        public bool[] upgrade;
    }
}
