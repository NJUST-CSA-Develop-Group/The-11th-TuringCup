// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimes : MonoBehaviour, TListener
{

    // Use this for initialization
    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_START, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        if (Event_Type == EVENT_TYPE.GAME_START)
        {
            GetComponentInChildren<UnityEngine.UI.Text>().text = "第" + (MatchManager.man.current + 1).ToString() + "场";
            return true;
        }
        return false;
    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
