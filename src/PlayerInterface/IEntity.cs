using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerInterface
{
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
    }

    public struct PlayerData
    {
        // 玩家信息存储结构
        public int x;
        public int y;
        public int blood;
        public bool[] upgrade;
    }
}
