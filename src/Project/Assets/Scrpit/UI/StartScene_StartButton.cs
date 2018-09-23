using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene_StartButton : MonoBehaviour
{
    bool auto = false;
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        if (MatchManager.man.type == MatchManager.Type.Match)
        {
            auto = true;
            Click();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Click()
    {
        SelectMap sel = null;
        switch (MatchManager.man.type)
        {
            case MatchManager.Type.Test:
                sel = transform.parent.Find("TestTab/Tab").GetComponent<SelectMap>();
                break;
            case MatchManager.Type.Machine:
                sel = transform.parent.Find("MachineTab/Tab").GetComponent<SelectMap>();
                break;
            case MatchManager.Type.Match:
                sel = transform.parent.Find("MatchTab/Tab").GetComponent<SelectMap>();
                break;
            default:break;
        }
        if (sel._curindex == -1)
        {
            MatchManager.man.map_id = 0;
        }
        else
        {
            MatchManager.man.map_id = sel.Indexes[sel._curindex];
        }
        if (!auto)//手动点开始时，要加载一次AI信息
        {
            MatchManager.man.Next();
        }
        SceneManager.LoadScene("DeployScene_2");
    }
}
