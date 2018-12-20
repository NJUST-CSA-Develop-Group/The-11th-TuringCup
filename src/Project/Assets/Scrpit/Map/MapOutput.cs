// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MapOutput
{
#if UNITY_ANDROID
    public static void Output()
    {
        if(!Directory.Exists(Application.persistentDataPath + "/Maps"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Maps");
        }
        for(int i = 0; i <maps.Length; i++)
        {
            string path = Application.persistentDataPath + "/Maps/" + (i + 1).ToString() + ".csv";
            if (File.Exists(path))
            {
                continue;
            }
            FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
            fs.Write(Encoding.ASCII.GetBytes(maps[i]), 0, Encoding.ASCII.GetByteCount(maps[i]));
        }
    }

    static string[] maps = new string[]
    {
        @"0,0,1,1,1,1,1,1,1,1,1,1,0,0
0,2,1,1,1,1,1,1,1,1,1,1,2,0
1,2,0,1,2,1,1,1,1,2,1,0,2,1
1,2,1,2,1,1,2,2,1,1,2,1,2,1
1,1,1,1,1,1,1,1,1,1,1,1,1,1
1,1,1,1,2,2,1,1,2,2,1,1,1,1
1,2,0,1,1,1,1,1,1,1,1,0,2,1
1,2,0,1,1,1,1,1,1,1,1,0,2,1
1,1,1,1,2,2,1,1,2,2,1,1,1,1
1,1,1,1,1,1,1,1,1,1,1,1,1,1
1,2,1,2,1,1,2,2,1,1,2,1,2,1
1,2,0,1,2,1,1,1,1,2,1,0,2,1
0,2,1,1,1,1,1,1,1,1,1,1,2,0
0,0,1,1,1,1,1,1,1,1,1,1,0,0
",
        @"0,2,1,1,1,1,1,1,1,1,1,1,1,0
0,0,1,2,1,1,1,1,1,1,1,2,0,0
1,2,1,1,2,1,1,1,2,1,2,2,1,1
1,1,1,2,1,0,1,1,1,1,1,1,1,1
0,2,1,1,2,1,1,1,2,0,2,2,1,1
1,1,1,0,1,1,1,1,0,1,1,1,1,1
1,1,1,2,1,1,2,1,1,1,2,1,1,1
1,1,1,2,1,1,1,1,1,1,1,2,0,1
1,1,2,1,1,1,1,1,2,1,2,0,1,1
1,1,1,2,1,1,1,1,1,1,1,1,1,1
2,1,1,1,0,1,1,1,1,1,1,2,2,1
1,1,1,2,1,1,1,1,2,1,1,1,1,1
0,1,1,0,1,1,1,1,1,1,1,1,1,0
0,0,1,1,1,2,1,1,1,1,2,1,0,0
",
        @"0,0,1,1,1,1,1,1,1,1,1,1,0,0
0,2,1,1,1,1,1,2,1,1,2,2,2,0
1,2,1,1,2,1,1,1,1,1,1,1,1,1
1,2,1,2,1,1,2,1,1,1,2,1,1,1
1,1,1,1,1,1,1,1,2,1,1,2,1,1
1,1,1,1,2,2,1,1,2,1,1,1,1,1
1,2,1,1,1,1,0,0,1,1,2,1,1,1
1,1,1,2,1,1,0,0,1,1,1,1,2,1
1,1,1,1,1,2,1,1,2,2,1,1,1,1
1,1,2,1,1,2,1,1,1,1,1,1,1,1
1,1,1,2,1,1,1,2,1,1,2,1,2,1
1,1,1,1,1,1,1,1,1,2,1,1,2,1
0,2,2,2,1,1,2,1,1,1,1,1,2,0
0,0,1,1,1,1,1,1,1,1,1,1,0,0
",
    };
#endif
}
