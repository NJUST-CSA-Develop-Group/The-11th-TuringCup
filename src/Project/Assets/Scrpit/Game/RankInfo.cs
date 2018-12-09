using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankInfo
{
    public static RankInfo info = new RankInfo();//单例的实现

    public static List<int> curRank;

    public List<DeadInfo> deadlist = new List<DeadInfo>();

    public Prize[] prize;

    public List<int> Sort()
    {
        prize = new Prize[4];
        List<AliveInfo> alivelist = new List<AliveInfo>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var it in players)
        {
            int id = it.GetComponent<PlayerScoreManager>().playerID;
            if (!deadlist.Exists(c => c.index == id))
            {
                int health = it.GetComponent<PlayerHealth>().GetHP();
                int score = it.GetComponent<PlayerScoreManager>().GetScore();
                alivelist.Add(new AliveInfo { index = id, health = health, score = score });
            }
        }
        /*
        deadlist.Sort((a, b) => a.index - b.index);//index小优先
        deadlist.Sort((a, b) => b.score - a.score);//score大优先
        deadlist.Sort((a, b) => (int)(a.time - b.time));//后死优先
        alivelist.Sort((a, b) => a.index - b.index);
        alivelist.Sort((a, b) => b.score - a.score);
        alivelist.Sort((a, b) => b.health - a.health);
        */
        deadlist.Sort((a, b) =>
        {
            int retVal = (int)((a.time - b.time) * 1000);
            if (Mathf.Abs(retVal) > 40) return retVal;
            retVal = b.score - a.score;
            if (retVal != 0) return retVal;
            return a.index - b.index;
        });
        alivelist.Sort((a, b) =>
        {
            int retVal = b.health - a.health;
            if (retVal != 0) return retVal;
            retVal = b.score - a.score;
            if (retVal != 0) return retVal;
            return a.index - b.index;
        });
        List<int> ret = new List<int>();
        foreach (var it in alivelist)
        {
            ret.Add(it.index);
        }
        foreach (var it in deadlist)
        {
            ret.Add(it.index);
        }
        deadlist.Clear();
        curRank = ret;
        return ret;
    }

    public class DeadInfo
    {
        public int index;
        public float time;
        public int score;
    }

    private class AliveInfo
    {
        public int index;
        public int health;
        public int score;
    }

    public class Prize
    {
        public int id;
        public string name;
        public int health;
        public int score;
    }
}
