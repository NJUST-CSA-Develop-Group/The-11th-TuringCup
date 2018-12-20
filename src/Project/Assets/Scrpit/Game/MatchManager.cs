// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

#define MATCH
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager
{
    public static MatchManager man = new MatchManager();
    public static bool first = true;
    public Type type = Type.Machine;
    public int times;
    public int map_id;
    public string[] AI;
    public delegate void NextCallback();
    public delegate Coroutine StartCo(IEnumerator co);
    public int current = 0;
#if MATCH
    private static readonly string host = "http://192.168.0.104:8089";//"http://www.turing-cup.online:8089";
    private int[] curPlayer = new int[4];
    private static readonly string salt = "asdegewgfewgwesghsbsfd";
#endif

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
#if UNITY_ANDROID
        MapOutput.Output();
#endif
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

    public void Next(StartCo startCo, NextCallback callback, int[] rank)
    {
        if (type == Type.Test)
        {
            AI[0] = "AI/AI1.dll";
        }
        else if (type == Type.Machine)
        {
            AI[0] = "";
        }
        if (type != Type.Match)
        {
            callback();
            return;
        }
        times++;
        // TODO: calc next AI
        startCo(DoNext(callback, rank));
    }

    public IEnumerator DoNext(NextCallback callback, int[] rank)
    {
#if MATCH
        map_id = (new System.Random().Next() % 3) + 1;
        UnityWebRequest req;
        bool temp_first = first;
        if (first)
        {
            first = false;
            while (true)
            {
                req = UnityWebRequest.Get(host + "/current");
                yield return req.SendWebRequest();
                if (req.responseCode != 200)
                {
                    continue;
                }
                current = int.Parse(req.downloadHandler.text);
                break;
            }
        }
        else
        {
            //提交
            string sign = salt;
            Result result = new Result();
            result.id = new int[4];
            result.rank = new int[4];
            for (int i = 0; i < 4; i++)
            {
                result.id[i] = curPlayer[i];
                sign += curPlayer[i].ToString();
                if (rank != null)
                {
                    result.rank[i] = rank[i];
                }
            }
            SHA256 sha256 = SHA256.Create();
            byte[] signB = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sign));
            sign = "";
            foreach (byte b in signB)
            {
                sign += b.ToString("X2");
            }
            result.sign = sign;
            if (rank == null)
            {
                result.canceled = true;
            }
            while (true)
            {
                req = UnityWebRequest.Get(host + "/result");
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = "POST";
                req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(result)));
                req.uploadHandler.contentType = "application/json";
                yield return req.SendWebRequest();
                if (req.responseCode == 200)
                {
                    break;
                }
            }
            current++;
        }
        if (rank != null || (rank == null && temp_first == false))
        {
            callback();
        }
        while (true)
        {
            req = UnityWebRequest.Get(host + "/match?id=" + current.ToString());
            yield return req.SendWebRequest();
            if (req.responseCode == 200)
            {
                break;
            }
            if (req.responseCode == 404)
            {
                //System.IO.FileStream ffs = new System.IO.FileStream("log.txt", System.IO.FileMode.Create);
                //ffs.Write(System.Text.Encoding.UTF8.GetBytes("404"), 0, System.Text.Encoding.UTF8.GetByteCount("404"));
                //ffs.Close();
                if (!System.IO.File.Exists(".tc.finish"))
                {
                    System.IO.File.Create(".tc.finish");
                }
                Application.Quit();
                yield return null;
                yield break;
            }
        }
        Match m = (Match)JsonUtility.FromJson(req.downloadHandler.text, typeof(Match));
        curPlayer[0] = m.player1;
        curPlayer[1] = m.player2;
        curPlayer[2] = m.player3;
        curPlayer[3] = m.player4;
        for (int i = 0; i < 4; i++)
        {
            AI[i] = "AI/" + curPlayer[i].ToString() + "/AI" + (i + 1).ToString() + ".dll";
        }
        //System.IO.FileStream fs = new System.IO.FileStream("log.txt", System.IO.FileMode.Create);
        //fs.Write(System.Text.Encoding.UTF8.GetBytes(req.downloadHandler.text), 0, System.Text.Encoding.UTF8.GetByteCount(req.downloadHandler.text));
        //fs.Close();
#else
        yield return null;
#endif
        callback();
    }

    public enum Type
    {
        Test,
        Machine,
        Match
    }

#if MATCH
    [System.Serializable]
    internal class Match
    {
        public int player1;
        public int player2;
        public int player3;
        public int player4;
    }

    [System.Serializable]
    internal class Result
    {
        public string sign;
        public int[] id;
        public int[] rank;
        public bool canceled;
    }
#endif
}
