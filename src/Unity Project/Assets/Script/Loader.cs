using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // 此脚本用于在StartScene阶段进行加载工作，目前加载控制器
    public string dll_path;// dll文件在运行期的目录

    // Use this for initialization
    void Start()
    {
        string[] names = GetControllersName();
        ControlLoader.controllers = new PlayerInterface.IControl[4];
        for (int i = 0; i < 4; i++)// 加载4个控制器
        {
            try
            {
                ControlLoader.controllers[i] = ControlLoader.Load(dll_path, names[i]);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);// TODO: 应采取其他措施显示错误
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    string[] GetControllersName()// 提供Dll文件的文件名
    {
        return new string[4];// TODO: 此处未做实现
    }
}
