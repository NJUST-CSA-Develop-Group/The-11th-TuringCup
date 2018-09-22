using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene_LogoButton : MonoBehaviour
{
    public static bool first = true;

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

    // Update is called once per frame
    void Update()
    {

    }

    void Click()
    {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.Find("ModeUI/Start").gameObject.SetActive(true);
        transform.parent.parent.Find("ModeUI/ModeTab").gameObject.SetActive(true);
        transform.parent.parent.Find("ModeUI/TestTab").GetComponent<StartScene_Tab>().SetVisible();
        transform.parent.parent.Find("ModeUI/MachineTab").GetComponent<StartScene_Tab>().SetVisible();
        transform.parent.parent.Find("ModeUI/MatchTab").GetComponent<StartScene_Tab>().SetVisible();
    }
}
