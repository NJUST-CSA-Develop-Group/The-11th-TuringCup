using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager
{
    public static MatchManager man = new MatchManager();
    public Type type = Type.Machine;
    public int map_id;
    public string[] AI;

    public MatchManager()
    {
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

    public void Next()
    {
        if (type != Type.Match)
        {
            return;
        }
        // TODO: calc next AI
    }

    public enum Type
    {
        Test,
        Machine,
        Match
    }
}
