using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //用于显示player状态UI的脚本
    public GameObject prefab;//UI的预制，使用playerStatus或playerStatusCurrent
    public Material deadMaterial;//死亡时黑白材质，需使用deadMaterial
    public string team_name;//队伍名称
    public Texture avatar;//队伍头像
    public Texture skill1Texture;//v
    public Texture skill2Texture;//3个技能的图标
    public Texture skill3Texture;//^

    GameObject prefabInstantiate;//prefab的实例
    UnityEngine.UI.RawImage m_avatar;
    UnityEngine.UI.RawImage m_skill1;
    UnityEngine.UI.RawImage m_skill2;
    UnityEngine.UI.RawImage m_skill3;
    Transform blood;
    // Use this for initialization
    void Start()
    {
        prefabInstantiate = Instantiate(prefab, transform);
        prefabInstantiate.transform.localScale = new Vector3(Screen.width / 1920f, Screen.width / 1920f, 1f);//按照屏幕尺寸进行缩放
        m_avatar = prefabInstantiate.transform.Find("avatar").GetComponent<UnityEngine.UI.RawImage>();
        Transform skills = prefabInstantiate.transform.Find("skill");
        m_skill1 = skills.Find("skill1").GetComponent<UnityEngine.UI.RawImage>();
        m_skill2 = skills.Find("skill2").GetComponent<UnityEngine.UI.RawImage>();
        m_skill3 = skills.Find("skill3").GetComponent<UnityEngine.UI.RawImage>();
        m_skill1.texture = skill1Texture;
        m_skill2.texture = skill2Texture;
        m_skill3.texture = skill3Texture;
        m_avatar.material = null;
        blood = prefabInstantiate.transform.Find("bloodBar").Find("blood");
        m_skill1.enabled = false;
        m_skill2.enabled = false;
        m_skill3.enabled = false;
        //SetInfo();//此句用于调试，请在设置好队伍相关信息后调用SetInfo()
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInfo()//设置队伍相关信息
    {
        m_avatar.texture = avatar;
        prefabInstantiate.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = team_name;
    }

    public void SetHealth(int health)//设置血量显示的生命
    {
        blood.localScale = new Vector3(health / 100f, 1f, 1f);
    }

    public void Die(bool dead = true)//设置死亡状态显示
    {
        if (dead)
        {
            m_avatar.material = new Material(deadMaterial);
        }
        else
        {
            m_avatar.material = null;
        }
    }

    public void SetSkill(int index, bool type)//设置技能图标状态
    {
        switch (index)
        {
            case 1:
                m_skill1.enabled = type;
                break;
            case 2:
                m_skill2.enabled = type;
                break;
            case 3:
                m_skill3.enabled = type;
                break;
        }
    }
}
