using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject prefab;//MapOption选项
    public Texture2D[] Images;//地图缩略列表
    public int[] Indexes;//地图编号
    public Texture2D Selector;//selector的纹理

    private Transform[] options;
    public int _curindex { get; private set; }

    void Start()
    {
        _curindex = -1;
        options = new Transform[Images.Length];
        for(int i = 0; i < Images.Length; i++)
        {
            int index = i;
            GameObject gameObject = GameObject.Instantiate(prefab, transform);
            options[i] = gameObject.transform;
            options[i].Find("image").GetComponent<RawImage>().texture = Images[i];//加载缩略图
            options[i].Find("selector").GetComponent<RawImage>().texture = Selector;//加载selector
            //options[i].Find("Text").GetComponent<Text>().text= 添加地图介绍
            options[i].Find("image").GetComponent<Button>().onClick.AddListener(delegate { Click(index); });
        }
    }

    private void Click(int index)
    {
        if (_curindex >= 0)
        {
            options[_curindex].Find("selector").gameObject.SetActive(false);
        }
        options[index].Find("selector").gameObject.SetActive(true);
        _curindex = index;
    }
}
