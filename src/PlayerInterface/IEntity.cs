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
        void Move(int direct);// 移动
        void Shoot();// 射击
        bool PlaceBomb();// 放置炸弹
        bool Upgrade(int index);// 兑换属性

        // 提供信息的部分
        int[][] MapData();// 地图信息
        PlayerData[] PlayerData();// 玩家信息
        float Time();// 剩余时间
        int Index();// 玩家的序号
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
