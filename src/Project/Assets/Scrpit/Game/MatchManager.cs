﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager
{
    public static MatchManager man = new MatchManager();
    public Type type = Type.Machine;
    public int times;
    public int map_id;
    public string[] AI;

    public MatchManager()
    {
        times = 0;
        map_id = 1;
        AI = new string[]
        {
            "AI/AI1.dll",
            "AI/AI2.dll",
            "AI/AI3.dll",
            "AI/AI4.dll"
        };
    }

    public void SetType(string t)
    {
        switch (t)
        {
            case "Test":
                type = Type.Test;
                break;
            case "Machine":
                type = Type.Machine;
                break;
            case "Match":
                type = Type.Match;
                break;
        }
    }

    public bool TypeIs(string t)
    {
        switch (t)
        {
            case "Test":
                return type == Type.Test;
            case "Machine":
                return type == Type.Machine;
            case "Match":
                return type == Type.Match;
            default:
                return false;
        }
    }

    public void Next()
    {
        if (type != Type.Match)
        {
            return;
        }
        times++;
        // TODO: calc next AI
    }

    public enum Type
    {
        Test,
        Machine,
        Match
    }
}