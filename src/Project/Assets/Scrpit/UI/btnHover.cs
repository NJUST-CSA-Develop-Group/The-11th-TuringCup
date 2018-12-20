// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class btnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData data)
    {
        transform.Find("hover").GetComponent<RawImage>().enabled = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        transform.Find("hover").GetComponent<RawImage>().enabled = false;
    }
}
