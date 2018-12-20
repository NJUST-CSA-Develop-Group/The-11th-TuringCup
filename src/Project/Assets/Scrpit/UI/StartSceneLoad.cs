// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartSceneLoad : MonoBehaviour
{
    private string[] _childs =
    {
        "logo",
        "buttonGroup0",
        "buttonGroup1",
        "Return"
    };
    private string[] _mapchilds =
    {
        "maps/Test",
        "maps/Machine",
        "maps/Match"
    };

#if UNITY_ANDROID
    private int count = 0;

    private string[] url = new string[]
    {
        "https://turingcup-1257109822.cos.ap-shanghai.myqcloud.com/TuringCup-apkRec/AIs/AI1.dll",
        "https://turingcup-1257109822.cos.ap-shanghai.myqcloud.com/TuringCup-apkRec/AIs/AI2.dll",
        "https://turingcup-1257109822.cos.ap-shanghai.myqcloud.com/TuringCup-apkRec/AIs/AI3.dll",
        "https://turingcup-1257109822.cos.ap-shanghai.myqcloud.com/TuringCup-apkRec/AIs/AI4.dll",
    };

    private string[] savePath = new string[]
    {
        "/AI/AI1.dll",
        "/AI/AI2.dll",
        "/AI/AI3.dll",
        "/AI/AI4.dll",
    };
#endif

    // Use this for initialization
    void Start()
    {
        float scale;
        foreach (string name in _childs)
        {
            transform.Find(name).localScale = new Vector3(Screen.height / 1080.0f, Screen.height / 1080.0f, 1);
        }
        foreach (string name in _mapchilds)
        {
            scale = Mathf.Min(Screen.height / 1080.0f, Screen.width / 1920.0f);
            transform.Find(name).localScale = new Vector3(scale, scale, 1);
        }
        scale =Mathf.Max(Screen.height / 1080.0f, Screen.width / 1920.0f);
        transform.Find("background").localScale = new Vector3(scale, scale, 1);
        transform.Find("over").localScale = new Vector3(scale, scale, 1);

#if UNITY_ANDROID
        transform.Find("AndroidLoading").gameObject.SetActive(true);
        count = 4;
        if (!Directory.Exists(Application.persistentDataPath + @"/AI"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + @"/AI");
        }
        for(int i = 0; i < url.Length; i++)
        {
            LoadFile(url[i], Application.persistentDataPath + savePath[i]);
        }
#else
        transform.Find("buttonGroup0").GetComponent<Animator>().SetInteger("pos", 1);
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (count == 0)
        {
            transform.Find("AndroidLoading").gameObject.SetActive(false);
            count = -1;
            transform.Find("buttonGroup0").GetComponent<Animator>().SetInteger("pos", 1);
        }
#endif
    }

#if UNITY_ANDROID
    private void LoadFile(string url,string aim)
    {
        if (System.IO.File.Exists(aim))
        {
            count--;
            return;
        }
        StartCoroutine(WWWFileLoad.LoadFile(url, aim, SaveFile));
    }

    public void SaveFile(bool result,string err)
    {
        if (!result)
        {
            Debug.Log(err);
        }
        count--;
    }
#endif
}
