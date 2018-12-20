// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualKeyManager
{
    private static Dictionary<string, VirtualKey> keyMap = new Dictionary<string, VirtualKey>();
    public static VirtualKey Register(string name)
    {
        VirtualKey vkey = new VirtualKey(name);
        keyMap[name] = vkey;
        return vkey;
    }

    public static bool IsKeyDown(string name)
    {
        if (!keyMap.ContainsKey(name))
        {
            return false;
        }
        return keyMap[name].GetKeyDown();
    }

    public static bool IsKeyUp(string name)
    {
        if (!keyMap.ContainsKey(name))
        {
            return false;
        }
        return !keyMap[name].GetKeyDown();
    }

    public class VirtualKey
    {
        private string _name;

        private bool keyStatus = false;

        public VirtualKey(string name)
        {
            _name = name;
        }

        public bool GetKeyDown()
        {
            return keyStatus;
        }

        public void Press()
        {
            keyStatus = true;
        }

        public void Release()
        {
            keyStatus = false;
        }

        public void Remove()
        {
            keyMap.Remove(_name);
        }
    }
}
