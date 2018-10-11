using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Click);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Click()
    {
        SceneManager.LoadScene("StartScene");
    }
}
