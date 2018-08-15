using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour, TListener
{
    public int PlayerID;//1-4
    public GameObject Prefab;
    public Texture2D Avatar;//头像贴图
    public string TeamName;//队名
    public int OriginHealth;//初始血量
    public Texture2D[] Images;//按需存放技能贴图
    public Shader GreyShader;//灰度效果

    private GameObject _instantiate;
    private RawImage _avatar;
    private Text _teamName;
    private Text _hp;
    private Text _score;
    private Icon _shoot;
    private Icon _bomb;
    private Icon[] _skill;
    // Use this for initialization
    void Start()
    {
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
        GetComponent<RectTransform>().anchoredPosition = new Vector2((PlayerID - 1) * Screen.width / 4, pos.y);//自动调整位置
        _instantiate = Instantiate(Prefab, transform);//初始化UI
        _instantiate.transform.localScale = new Vector3(Screen.width / 1920f, Screen.width / 1920f, 1f);//按照屏幕尺寸进行缩放
        _avatar = _instantiate.transform.Find("Avatar").GetComponent<RawImage>();
        _teamName = _instantiate.transform.Find("TeamName").GetComponent<Text>();
        _hp = _instantiate.transform.Find("Health").GetComponent<Text>();
        _shoot = new Icon(_instantiate.transform.Find("attack/shoot"), GreyShader);
        _bomb = new Icon(_instantiate.transform.Find("attack/bomb"), GreyShader);
        _skill = new Icon[3];
        _skill[0] = new Icon(_instantiate.transform.Find("skill/skill0"), GreyShader);
        _skill[1] = new Icon(_instantiate.transform.Find("skill/skill1"), GreyShader);
        _skill[2] = new Icon(_instantiate.transform.Find("skill/skill2"), GreyShader);

        EventManager.Instance.AddListener(EVENT_TYPE.GAME_OVER, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        if (Event_Type == EVENT_TYPE.GAME_OVER)
        {
            transform.parent.gameObject.SetActive(false);// 游戏结束时，隐藏Status的list
            return true;
        }
        return false;
    }

    public void SetupInfo()
    {
        _avatar.texture = Avatar;
        _teamName.text = TeamName;
        SetHealth(OriginHealth.ToString());
        SetScore(0);
        for (int i = 0; i < 3; i++)
        {
            _skill[i].SetGrey(true);
        }
    }

    public void Die(bool die = true)
    {
        _avatar.material.shader = die ? GreyShader : null;
    }

    public void SetHealth(string health)
    {
        _hp.text = "HP: " + health;
    }

    public void SetScore(int score)
    {
        _score.text = "得分: " + score;
    }

    public void SetShootCD(string cd)
    {
        _shoot.SetGrey(cd != null);
        _shoot.SetCD(cd);
    }

    public void SetBombCD(string cd)
    {
        _bomb.SetGrey(cd != null);
        _bomb.SetCD(cd);
    }

    public void SetSkillCD(int index, string cd)
    {
        if (index < 0 || index > 2)
        {
            Debug.Log("error skill index");
            return;
        }
        _skill[index].SetGrey(cd == null);
        _skill[index].SetCD(cd);
    }

    public Object getGameObject()
    {
        return gameObject;
    }

    private class Icon//对技能图标进行控制，内部类
    {
        private Transform _image;
        private Transform _cd;
        private Shader _shader;
        public Icon(Transform transform, Shader shader)
        {
            _image = transform.Find("image");
            _cd = transform.Find("cd");
            _shader = shader;
        }

        public void SetImg(Texture2D img)
        {
            _image.GetComponent<RawImage>().texture = img;
        }

        public void SetCD(string cd)
        {
            if (cd == null)
            {
                _cd.GetComponent<Text>().enabled = false;
            }
            else
            {
                _cd.GetComponent<Text>().enabled = true;
                _cd.GetComponent<Text>().text = cd;
            }
        }

        public void SetGrey(bool grey = true)
        {
            _image.GetComponent<RawImage>().material.shader = grey ? _shader : null;
        }
    }
}
