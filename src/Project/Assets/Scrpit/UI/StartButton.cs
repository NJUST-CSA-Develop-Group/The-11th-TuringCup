using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    static bool first = true;

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        if (!first)
        {
            Click();
        }
        first = false;
    }

    void Click()
    {
        GameObject.Find("Main Camera").GetComponent<CameraEffect>().enabled = false;
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.Find("TimesUI").gameObject.SetActive(true);//激活场次信息UI
    }
}
