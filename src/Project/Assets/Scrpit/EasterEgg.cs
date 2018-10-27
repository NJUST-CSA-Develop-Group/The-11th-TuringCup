using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{

    private int stage = 0;
    private KeyCode[] Konami = new KeyCode[] {
        KeyCode.UpArrow, KeyCode.UpArrow ,KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow,KeyCode.RightArrow,KeyCode.LeftArrow,KeyCode.RightArrow,
        KeyCode.LeftControl,KeyCode.Space
    };
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stage < 0) return;
        if (Input.GetKeyDown(Konami[stage]))
        {
            stage++;
        }
        if (stage == 10)
        {
            //GameObject.Find("Player 1").transform.position = new Vector3(6.5f, 2, 6.5f);
            GameObject.Find("Player 1").GetComponent<PlayerShoot>().TimeBetweenBullets = 0;
            GameObject.Find("Player 1").GetComponent<PlayerBomb>().bombCD = 0;
            GameObject.Find("Player 1").GetComponent<PlayerScoreManager>().skillScore = 0;
            stage = -1;
            return;
        }
    }
}
