using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour, TListener
{
    public GameObject prefab;//prize预制
    public Texture2D[] PrizeImages;//prize图

    private Transform[] prizes;

    // Use this for initialization
    void Start()
    {
        float center = PrizeImages.Length / 2f;
        prizes = new Transform[PrizeImages.Length];
        for (int i = 0; i < PrizeImages.Length; i++)
        {
            GameObject gameObject = GameObject.Instantiate(prefab, transform);
            prizes[i] = gameObject.transform;
            prizes[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, (center - i - 1) * (prizes[i].GetComponent<RectTransform>().rect.height + 16));//自适应位置
            prizes[i].Find("image").GetComponent<RawImage>().texture = PrizeImages[i];
        }
        prizes[0].localScale = new Vector3(1.25f, 1.25f, 1);//第一名放大

        EventManager.Instance.AddListener(EVENT_TYPE.GAME_OVER, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetTeamNameScore(string name, int score, int index)
    {
        prizes[index].Find("TeamName").GetComponent<Text>().text = name;
        prizes[index].Find("score").GetComponent<Text>().text = "得分:" + score.ToString();
    }

    private bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        if (Event_Type == EVENT_TYPE.GAME_OVER)
        {
            // TODO: 在此处调用SetTramNameScore设置队名分数
            for(int i = 0; i<= 3; i++)
            {
                SetTeamNameScore("123", 123, i);
            }
            GameObject.Find("Main Camera").GetComponent<CameraEffect>().enabled = true;
            return true;
        }
        return false;
    }

    bool TListener.OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        throw new System.NotImplementedException();
    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
