using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    //用于显示队伍信息的UI的脚本
    public GameObject prefab;//UI的预制，需使用playerInfo.prefab
    public Texture avatar;//队伍头像
    public string team_name;//队名

    GameObject prefabInstantiate;// 射击模型实例
    UnityEngine.UI.RawImage m_avatar;
    UnityEngine.UI.Text m_team_name;
    // Use this for initialization
    void Start()
    {
        prefabInstantiate = Instantiate(prefab, transform);// 实例化预制
        m_avatar = prefabInstantiate.transform.Find("avatar").GetComponent<UnityEngine.UI.RawImage>();
        m_team_name = prefabInstantiate.transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
        SetInfo();//此句用于调试，请在设置好队伍相关信息后调用SetInfo()
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInfo()//设置队伍相关信息
    {
        m_avatar.texture = avatar;
        m_team_name.text = team_name;
    }
}
