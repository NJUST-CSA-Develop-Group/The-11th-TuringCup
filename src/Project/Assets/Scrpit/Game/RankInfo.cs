using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankInfo
{
    public static RankInfo info = new RankInfo();//单例的实现

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
        deadlist.Sort((a, b) => a.index - b.index);//index小优先
        deadlist.Sort((a, b) => b.score - a.score);//score大优先
        deadlist.Sort((a, b) => (int)(a.time - b.time));//后死优先
        alivelist.Sort((a, b) => a.index - b.index);
        alivelist.Sort((a, b) => b.score - a.score);
        alivelist.Sort((a, b) => b.health - a.health);
        List<int> ret = new List<int>();
        foreach (var it in alivelist)
        {
            ret.Add(it.index);
        }
        foreach(var it in deadlist)
        {
            ret.Add(it.index);
        }
        deadlist.Clear();
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
