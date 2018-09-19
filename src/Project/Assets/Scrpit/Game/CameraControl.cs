using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject status = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            int current = GameObject.FindGameObjectWithTag("Global").GetComponent<GameManager>().current;
            GameObject.FindGameObjectWithTag("Global").GetComponent<GameManager>().current = 0;
            if (current != 0)
            {
                HideTPUI();
                Invoke("ShowMain", 0.5f);
            }
        }
        else
        {
            int old_current = GameObject.FindGameObjectWithTag("Global").GetComponent<GameManager>().current;
            int current = -1;
            if (Input.GetKeyDown(KeyCode.F9))
            {
                current = 1;
            }
            else if (Input.GetKeyDown(KeyCode.F10))
            {
                current = 2;
            }
            else if (Input.GetKeyDown(KeyCode.F11))
            {
                current = 3;
            }
            else if (Input.GetKeyDown(KeyCode.F12))
            {
                current = 4;
            }
            else
            {
                return;
            }
            GameObject.FindGameObjectWithTag("Global").GetComponent<GameManager>().current = current;
            if (old_current == 0)
            {
                HideMain();
                Invoke("ShowTPUI", 0.5f);
            }
            else if (current != old_current)
            {
                status.transform.Find("ThirdPersonUI").GetComponent<TPUI>().Toggle();
            }
        }
    }

    void ShowMain()
    {
        status.transform.Find("GodMode").GetComponent<Animator>().SetBool("visible", true);
    }

    void HideMain()
    {
        status.transform.Find("GodMode").GetComponent<Animator>().SetBool("visible", false);
    }

    void ShowTPUI()
    {
        status.transform.Find("ThirdPersonUI").GetComponent<TPUI>().Show();
    }

    void HideTPUI()
    {
        status.transform.Find("ThirdPersonUI").GetComponent<TPUI>().Hide();
    }
}
