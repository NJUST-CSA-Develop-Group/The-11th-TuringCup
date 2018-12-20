// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private Text _text;
    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        _text.text = ((int)System.Math.Ceiling(GameManager.GetRemainTime())).ToString("D");
    }
}
