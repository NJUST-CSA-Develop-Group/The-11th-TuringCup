using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour {
    public bool next = false;
    public bool record = false;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Click);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Click()
    {
        if (record)
        {
            Record();
        }
        if (next)
        {
            MatchManager.man.Next();
        }
        SceneManager.LoadScene("StartScene");
    }

    void Record()
    {
        // RankInfo.info.prize
        //
        // TODO: record
    }
}
