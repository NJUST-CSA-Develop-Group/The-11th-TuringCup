using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject prefab;//MapOption选项
    public Texture2D[] Images;//地图缩略列表
    public int[] Indexes;//地图编号
    public Texture2D BackImage;//背景纹理
    public Texture2D Selector;//selector的纹理
    public string TabType;

    private Transform[] options;
    private static int _curindex = -1;

    bool auto = false;

    void Start()
    {
        options = new Transform[Images.Length];
        for (int i = 0; i < Images.Length; i++)
        {
            int index = i;
            GameObject gameObject = GameObject.Instantiate(prefab, transform);
            options[i] = gameObject.transform;
            options[i].Find("image").GetComponent<RawImage>().texture = Images[i];//加载缩略图
            options[i].Find("back").GetComponent<RawImage>().texture = BackImage;
            options[i].Find("selector").GetComponent<RawImage>().texture = Selector;//加载selector
            //options[i].Find("Text").GetComponent<Text>().text= 添加地图介绍
            options[i].GetComponent<Button>().onClick.AddListener(delegate { Click(index); });
        }
        if (MatchManager.man.type == MatchManager.Type.Match && TabType == "Match")
        {
            auto = true;
            Click(_curindex);
        }
    }

    private void Click(int index)
    {
        _curindex = index;
        MatchManager.man.map_id = Indexes[index];
        if (!auto)//手动点开始时，要加载一次AI信息
        {
            MatchManager.man.Next();
        }
        SceneManager.LoadScene("DeployScene_2");
    }
}
