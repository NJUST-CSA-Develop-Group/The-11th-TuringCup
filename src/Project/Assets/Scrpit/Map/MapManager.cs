using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapManager : MonoBehaviour {

    /*
     * 本脚本去掉了之前地图坐标对Position的换算
     * 换为将游戏的原点坐标直接改为在左上角
     * 
     * 目前Map作为string数组存储在List内
     * 后期可以根据需要转换为int数组
     */

    public GameObject BoomCubePrefab;     // 可炸方块
    public GameObject UnboomCubePrefab;   // 不可炸方块
    public float ReduceGameAreaBeginTime; //缩圈开始时的游戏剩余时间
    public float ReduceGameAreaTime; //缩圈时间 仅供测试

    private List<string[]> map; //地图信息作为string数组存储在List内（变相二维数组）
    private float ReduceTiming; //缩圈计时器

    public List<string[]> Map //对Map的只读访问
    {
        get
        {
            return map;
        }

    }

    //先于Start执行 以便后面其他类在Start初始化时获取地图信息
    private void Awake()
    {

        map = new List<string[]>();
        //TODO 多地图加载
        LoadFile(Application.dataPath + "/Maps", "01.csv");
        Debug.Log("MapFile loaded");

        LoadMap(map);
        Debug.Log("Map loaded");

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
                    GameObject.Instantiate(BoomCubePrefab, new Vector3(i, 0, j), gameObject.transform.rotation);
                }
                else if (BoxType == 2)        // 2表示不可炸块
                {
                    GameObject.Instantiate(UnboomCubePrefab, new Vector3(i, 0, j), gameObject.transform.rotation);
                }
            }
        }
    }

    //读取地图文件
    private void LoadFile(string path, string fileName)
    {
        Map.Clear();
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
            Map.Add(line.Split(','));
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
    private void MapUpgrade(int row, int col, int type)
    {
        map[row][col] = type.ToString();
    }

    private void ReduceGameArea(int circle)
    {
        //TODO 实现缩圈
    }



}
