using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // 此脚本用于在StartScene阶段进行加载工作，目前加载控制器
    public string dll_path;// dll文件在运行期的目录

    // Use this for initialization
    void Start()
    {
        string[] names = GetControllersName();// 获取dll文件的名称
        ControlLoader.controllers = new PlayerInterface.IControl[4];
        for (int i = 0; i < 4; i++)// 加载4个控制器
        {
            try
            {
                if (names[i] != null)
                {
                    ControlLoader.controllers[i] = ControlLoader.Load(dll_path, names[i]);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);// TODO: 应采取其他措施显示错误
            }
        }

        PlayerInfo[] infos = GameObject.Find("playerList").GetComponentsInChildren<PlayerInfo>();
        for (int i = 0; i < 4; i++)// 对PlayerInfo初始化
        {
            if (ControlLoader.controllers[i] != null)
            {
                infos[i].team_name = ControlLoader.controllers[i].GetTeamName();
            }
            else
            {
                infos[i].team_name = "人类";
            }
            infos[i].SetInfo();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    string[] GetControllersName()// 提供Dll文件的文件名
    {
        string[] ret = new string[4];
        string path = System.Environment.CurrentDirectory + "\\list.txt";// 从当前文件夹下的list.txt文件读取
        try
        {
            StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            for (int i = 0; i < 4; i++)
            {
                ret[i] = sr.ReadLine();// 读取文件
            }
            sr.Close();
        }
        catch (Exception)
        {
            Debug.Log("无法读取list.txt");
        }
        return ret;
    }
}
