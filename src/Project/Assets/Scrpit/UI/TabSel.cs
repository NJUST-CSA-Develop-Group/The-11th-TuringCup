using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSel : MonoBehaviour
{
    public string TabType;
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        if (!StartButton.first && MatchManager.man.type == MatchManager.Type.Match && MatchManager.man.TypeIs(TabType))
        {
            Click();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Click()
    {
        transform.parent.GetComponent<Animator>().SetInteger("pos", 2);
        transform.parent.parent.Find("maps/" + TabType).GetComponent<Animator>().SetBool("show", true);
        MatchManager.man.SetType(TabType);
    }
}
