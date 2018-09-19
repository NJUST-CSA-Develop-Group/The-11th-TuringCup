using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(Click);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Click()
    {
        SceneManager.LoadScene("DeployScene_2");
    }
}
