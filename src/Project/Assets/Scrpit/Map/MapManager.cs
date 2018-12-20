// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapManager : MonoBehaviour, TListener {

    /*
     * 本脚本去掉了之前地图坐标对Position的换算
     * 换为将游戏的原点坐标直接改为在左上角
     * 
     * 目前Map作为string数组存储在List内
     * 后期可以根据需要转换为int数组
     */

    public GameObject boomCubePrefab;     // 可炸方块
    public GameObject unboomCubePrefab;   // 不可炸方块
    public GameObject blockCube; //缩圈阻挡方块
    public float reduceGameAreaBeginTime; //缩圈开始时的游戏剩余时间
    public float reduceGameAreaTime; //缩圈时间 仅供测试

    private List<string[]> map; //地图信息作为string数组存储在List内（变相二维数组）
    private float reduceTiming; //缩圈计时器
    public int circle; //缩圈序号

    Collider[] colliders;
    Vector3 boxPosition;



    //先于Start执行 以便后面其他类在Start初始化时获取地图信息
    private void Awake()
    {
        reduceTiming = 0;
        circle = 0;
        boxPosition = new Vector3(); 
        map = new List<string[]>();

        //TODO 多地图加载
#if UNITY_ANDROID
        LoadFile(Application.persistentDataPath + "/Maps", MatchManager.man.map_id.ToString() + ".csv");
#else
        LoadFile(Application.dataPath + "/Maps", MatchManager.man.map_id.ToString() + ".csv");
#endif
        Debug.Log("MapFile loaded");

        LoadMap(map);
        Debug.Log("Map loaded");

        EventManager.Instance.AddListener(EVENT_TYPE.MAP_UPDATE_INFO, this);

    }

    private void FixedUpdate()
    {
        ReduceGameArea();
    }

    private void LoadMap(List<string[]> map)
    {
        int BoxType;

        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                BoxType = GetBoxType(i, j);
                if (BoxType == 1)      // 1表示可炸块
                {
                    GameObject.Instantiate(boomCubePrefab, new Vector3(i, 0, j), gameObject.transform.rotation);
                }
                else if (BoxType == 2)        // 2表示不可炸块
                {
                    GameObject.Instantiate(unboomCubePrefab, new Vector3(i, 0, j), gameObject.transform.rotation);
                }
            }
        }
    }

    //读取地图文件
    private void LoadFile(string path, string fileName)
    {
        map.Clear();
        Debug.Log("afterClear"); StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + "//" + fileName);
        }
        catch
        {
            Debug.Log("The file could not be read!");
        }
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            map.Add(line.Split(','));
        }
        sr.Close();
        sr.Dispose();
    }

    //返回请求的方块的类型
    public int GetBoxType(int row, int col)
    {
        return int.Parse(map[row][col]);
    }


    //更新地图数组信息（不更新地图 更新地图工作交由事件驱动的实现类）
    private bool MapUpdate(int x, int z, int type)
    {
        if(map[x][z] != null)
        {
            map[x][z] = type.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }

    //缩圈
    private void ReduceGameArea()
    {
        if(GameManager.GetRemainTime() <= reduceGameAreaBeginTime && GameManager.GetRemainTime() > 0f ) //如果进入缩圈时间
        {
            reduceTiming += Time.deltaTime; 
            if (reduceTiming >= reduceGameAreaTime && circle < 5) //如果到达缩圈时刻
            {
                reduceTiming = 0;
                for(int i = circle; i <= 13 - circle; i++) //正方形边框区域进行处理
                {
                    boxPosition.Set(i, 0, circle); //修改当前位置
                    colliders = Physics.OverlapSphere(boxPosition, 0.1f);
                    foreach (Collider collider in colliders)
                    {
                        //不销毁人物模型
                        if (!collider.gameObject.CompareTag("Player"))
                        {
                            Destroy(collider.gameObject); //销毁原有的方块
                        }
                        
                    }
                    Instantiate(blockCube, boxPosition, transform.rotation); //创建阻挡方块
                    MapUpdate(i, circle, -1); //更新地图信息

                    boxPosition.Set(13 - i, 0, 13 - circle);
                    colliders = Physics.OverlapSphere(boxPosition, 0.1f);
                    foreach (Collider collider in colliders)
                    {
                        if (!collider.gameObject.CompareTag("Player"))
                        {
                            Destroy(collider.gameObject); //销毁原有的方块
                        }

                    }
                    Instantiate(blockCube, boxPosition, transform.rotation);
                    MapUpdate(13 - i, 13 - circle, -1);

                    boxPosition.Set(circle, 0, i);
                    colliders = Physics.OverlapSphere(boxPosition, 0.1f);
                    foreach (Collider collider in colliders)
                    {
                        if (!collider.gameObject.CompareTag("Player"))
                        {
                            Destroy(collider.gameObject); //销毁原有的方块
                        }
                    }
                    Instantiate(blockCube, new Vector3(circle, 0, i), transform.rotation);
                    MapUpdate(circle, i, -1);

                    boxPosition.Set(13 - circle, 0, 13 - i);
                    colliders = Physics.OverlapSphere(boxPosition, 0.1f);
                    foreach (Collider collider in colliders)
                    {
                        if (!collider.gameObject.CompareTag("Player"))
                        {
                            Destroy(collider.gameObject); //销毁原有的方块
                        }
                    }
                    Instantiate(blockCube, boxPosition, transform.rotation);
                    MapUpdate(13 - circle, 13 - i, -1);
                }
                circle++;
            }
        }
        //TODO 实现缩圈

    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param, Dictionary<string, object> value)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.MAP_UPDATE_INFO:
                return MapUpdate((int)value["MapCol"], (int)value["MapRow"], (int)value["MapType"]);
            default:return false;
        }
    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
