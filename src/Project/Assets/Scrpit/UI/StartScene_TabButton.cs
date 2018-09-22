using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene_TabButton : MonoBehaviour
{
    public string TriggerName;

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        if (MatchManager.man != null)
        {
            if ((MatchManager.man.type == MatchManager.Type.Test && TriggerName == "Test") ||
                (MatchManager.man.type == MatchManager.Type.Machine && TriggerName == "Machine") ||
                (MatchManager.man.type == MatchManager.Type.Match && TriggerName == "Match"))
            {
                Invoke("Click", 1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    string Test, Machine, Match;

    void Click()
    {
        MatchManager.man.SetType(TriggerName);
        transform.parent.GetComponent<Animator>().SetTrigger(TriggerName);
        transform.parent.parent.parent.Find("background").GetComponent<Animator>().SetTrigger(TriggerName);
        switch (TriggerName)
        {
            case "Test":
                Test = "Middle";
                Machine = "Right";
                Match = "Right";
                break;
            case "Machine":
                Test = "Left";
                Machine = "Middle";
                Match = "Right";
                break;
            case "Match":
                Test = "Left";
                Machine = "Left";
                Match = "Middle";
                break;
        }
        transform.parent.parent.Find("TestTab").GetComponent<Animator>().SetTrigger(Test);
        transform.parent.parent.Find("MachineTab").GetComponent<Animator>().SetTrigger(Machine);
        transform.parent.parent.Find("MatchTab").GetComponent<Animator>().SetTrigger(Match);
        Invoke("ResetTrigger", 0.02f);
    }

    void ResetTrigger()
    {
        transform.parent.GetComponent<Animator>().ResetTrigger(TriggerName);
        transform.parent.parent.parent.Find("background").GetComponent<Animator>().ResetTrigger(TriggerName);
        transform.parent.parent.Find("TestTab").GetComponent<Animator>().ResetTrigger(Test);
        transform.parent.parent.Find("MachineTab").GetComponent<Animator>().ResetTrigger(Machine);
        transform.parent.parent.Find("MatchTab").GetComponent<Animator>().ResetTrigger(Match);
    }
}
