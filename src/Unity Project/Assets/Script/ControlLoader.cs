using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

class ControlLoader
{
    public static PlayerInterface.IControl[] controllers;// 跨scene存储已加载的控制器，应在StartScene对此变量初始化

    public static PlayerInterface.IControl Load(string path, string name)
    {
        string[] file = Directory.GetFiles(path, name, SearchOption.TopDirectoryOnly);
        if (file.Length < 1)
        {
            throw new Exception("没有这个文件: " + path + name);
        }
        Type[] type = Assembly.LoadFrom(file[0]).GetTypes();
        if (type.Length < 1)
        {
            throw new Exception("未对接口实现: " + path + name);
        }
        return (PlayerInterface.IControl)Activator.CreateInstance(type[0]);
    }
}
