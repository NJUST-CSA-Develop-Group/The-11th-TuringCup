// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIgnore : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
