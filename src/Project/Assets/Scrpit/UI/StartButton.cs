﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public static bool first = true;
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        if (!first)
        {
            Debug.Log(first);
            Click();
        }
    }

    // Update is called once per frame
    void Update()
    {
        first = false;
    }

    void Click()
    {
        transform.parent.GetComponent<Animator>().SetInteger("pos", 2);
        transform.parent.parent.Find("buttonGroup1").GetComponent<Animator>().SetInteger("pos", 1);
    }
}
