using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public bool next = false;
    public bool record = false;

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Click()
    {
        int[] rec = null;
        if (record)
        {
            rec = new int[4];
            for(int i = 0; i < 4; i++)
            {
                rec[i] = RankInfo.curRank.IndexOf(i + 1);
            }
            //Record();
        }
        if (next)
        {
            MatchManager.man.Next(StartCoroutine, () =>
             {

                 SceneManager.LoadScene("StartScene");
             }, rec);
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    /*void Record()
    {
        // RankInfo.info.prize
        //
        // TODO: record
    }*/
}
