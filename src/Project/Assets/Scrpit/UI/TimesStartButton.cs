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
        transform.parent.GetComponent<Animator>().SetBool("fade", true);
        Invoke("OutFinish", 1);
    }

    void OutFinish()
    {
        // 获取状态UI
        Transform status = transform.parent.parent.Find("Status");
        status.Find("GameTimes").GetComponent<Animator>().SetBool("start", true);
        status.Find("GodMode").GetComponent<Animator>().SetBool("visible", true);
        EventManager.Instance.PostNotification(EVENT_TYPE.GAME_START, this);
    }
}
