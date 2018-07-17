using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    //用于显示排名UI的脚本
    public GameObject prefab;//预制UI，需使用prize.prefab
    public string team_name;//队名
    public Texture avatar;//队伍头像
    public Texture orderTexture;//排名图像

    GameObject prefabInstantiate;//prefab的实例
    UnityEngine.UI.RawImage m_avatar;
    UnityEngine.UI.RawImage m_order;
    // Use this for initialization
    void Start()
    {
        prefabInstantiate = Instantiate(prefab, transform);
        m_avatar = prefabInstantiate.transform.Find("avatar").GetComponent<UnityEngine.UI.RawImage>();
        m_order = prefabInstantiate.transform.Find("order").GetComponent<UnityEngine.UI.RawImage>();
        m_order.texture = orderTexture;
        SetInfo();//此句用于调试，请在设置好队伍相关信息后调用SetInfo()
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInfo()//设置队伍相关信息
    {
        prefabInstantiate.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = team_name;
        m_avatar.texture = avatar;
    }
}
