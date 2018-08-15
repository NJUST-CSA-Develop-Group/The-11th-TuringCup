using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    public GameObject prefab;//MapOption选项
    public Texture2D[] Images;//地图缩略列表
    public string[] OptionValue;//选项附加信息

    private Transform[] options;

    // Use this for initialization
    void Start()
    {
        float center = Images.Length / 2f;
        options = new Transform[Images.Length];
        for(int i = 0; i < Images.Length; i++)
        {
            int index = i;
            GameObject gameObject = GameObject.Instantiate(prefab, transform);
            options[i] = gameObject.transform;
            options[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, options[i].GetComponent<RectTransform>().rect.height * (center - i));//确定位置
            options[i].GetComponent<RawImage>().texture = Images[i];//加载缩略图
            options[i].GetComponent<Button>().onClick.AddListener(delegate { Click(OptionValue[index]); });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Click(string value)
    {
        Debug.Log(value);
        GameObject.Find("Main Camera").GetComponent<CameraEffect>().enabled = false;
        gameObject.SetActive(false);
        transform.parent.Find("Status").gameObject.SetActive(true);// 显示状态UI
    }
}
