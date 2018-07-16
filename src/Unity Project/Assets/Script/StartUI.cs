using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Find("ButtonContainer").Find("Button").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(StartGame);
    }
	
	// Update is called once per frame
	void Update () {

	}

    void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/GameScene");
    }
}
