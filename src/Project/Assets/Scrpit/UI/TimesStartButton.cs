using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Click);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Click()
    {
        transform.parent.GetComponent<Animator>().SetBool("fade", true);//退出场次信息UI
        Invoke("OutFinish", 1);//动画之后启动游戏
    }

    void OutFinish()
    {
        // 启动状态UI，上帝视角
        Transform status = transform.parent.parent.Find("Status");
        status.Find("GameTimes").GetComponent<Animator>().SetBool("start", true);
        status.Find("GodMode").GetComponent<Animator>().SetBool("visible", true);
        EventManager.Instance.PostNotification(EVENT_TYPE.GAME_START, this);
    }
}
