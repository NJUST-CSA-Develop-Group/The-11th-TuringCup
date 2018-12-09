using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour, TListener//显示排名UI的管理类
{
    public GameObject prefab;//prize预制
    public Texture2D[] PrizeImages;//prize图
    public Texture2D RankBack;
    public Texture2D FirstRankBack;

    private Transform[] prizes;//全部的排名UI

    // Use this for initialization
    void Start()
    {
        float center = PrizeImages.Length / 2f;
        prizes = new Transform[PrizeImages.Length];
        for (int i = 0; i < PrizeImages.Length; i++)
        {
            GameObject gameObject = GameObject.Instantiate(prefab, transform);
            prizes[i] = gameObject.transform;
            prizes[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -160 + (center - i - 1) * (prizes[i].GetComponent<RectTransform>().rect.height - 16));//自适应位置
            prizes[i].Find("image").GetComponent<RawImage>().texture = PrizeImages[i];
            prizes[i].Find("back").GetComponent<RawImage>().texture = i == 0 ? FirstRankBack : RankBack;
        }
        prizes[0].localScale = new Vector3(1.25f, 1.25f, 1);//第一名放大
        for (int i = 0; i < 4; i++)//隐藏
        {
            prizes[i].gameObject.SetActive(false);
        }

        EventManager.Instance.AddListener(EVENT_TYPE.GAME_OVER, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetTeamNameScore(string name, int health, int score, int id, int index)
    {
        RankInfo.info.prize[index] = new RankInfo.Prize { id = id, name = name, health = health, score = score };
        prizes[index].Find("TeamName").GetComponent<Text>().text = name;
        prizes[index].Find("health").GetComponent<Text>().text = "血量:" + health.ToString();
        prizes[index].Find("score").GetComponent<Text>().text = "得分:" + score.ToString();
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        if (Event_Type == EVENT_TYPE.GAME_OVER)
        {
#if UNITY_ANDROID
            GameObject.Find("handle").SetActive(false);
#endif
            Cursor.visible = true;
            //在此处调用SetTramNameScore设置队名分数
            List<int> ranklist = RankInfo.info.Sort();
            foreach (var it in GameObject.FindGameObjectsWithTag("Player"))
            {
                it.GetComponent<TuringOperate>().closeFile();
                int index = ranklist.FindIndex(c => c == it.GetComponent<PlayerScoreManager>().playerID);
                SetTeamNameScore(it.GetComponent<TuringOperate>().AIScript.GetTeamName(), it.GetComponent<PlayerHealth>().GetHP(), it.GetComponent<PlayerScoreManager>().GetScore(), it.GetComponent<PlayerScoreManager>().playerID, index);//设置队伍信息
                prizes[index].Find("image").GetComponent<RawImage>().texture = PrizeImages[it.GetComponent<PlayerScoreManager>().playerID - 1];
                if (index == 0)
                {
                    GameObject.Find("Main Camera").GetComponent<CameraControl>().SetAim(it);
                }
            }
            GameObject.Find("Main Camera/Camera").GetComponent<CameraEffect>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<CameraMovement>().AllowMouse = false;
            transform.parent.Find("shade").gameObject.SetActive(true);
            for(int i = 0; i < 4; i++)
            {
                prizes[i].gameObject.SetActive(true);
            }
            {
                Transform status = transform.parent.parent.Find("Status");
                status.Find("GodMode").GetComponent<Animator>().SetBool("visible", false);
                //status.Find("ThirdPersonUI").GetComponent<Animator>().SetBool("visible", false);
            }
            Invoke("Show", 1);
            return true;
        }
        return false;
    }

    void Show()
    {
        transform.parent.GetComponent<Animator>().SetBool("visible", true);
    }


    public Object getGameObject()
    {
        return gameObject;
    }
}
