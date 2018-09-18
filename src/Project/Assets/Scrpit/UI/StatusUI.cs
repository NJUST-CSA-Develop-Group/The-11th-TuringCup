using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour, TListener//玩家状态UI管理类
{
    public int PlayerID;//1-4
    public GameObject Player;//对玩家的引用
    public GameObject Prefab;
    public Texture2D Avatar;//头像贴图
    public string TeamName;//队名
    public int OriginHealth;//初始血量
    public Texture2D[] Images;//按需存放技能贴图
    public Shader GreyShader;//灰度效果
    public Shader PartColorShader;//部分彩色效果
    public int TreatEffectTicks = 60;

    private GameObject _instantiate;//实例化的prefab
    private RawImage _avatar;//头像的RawImage组件
    private Material _avatarShader;//头像的材质
    private Text _teamName;//队名的Text组件
    private Text _hp;
    private Text _score;
    private Icon[] _skill;//skill管理结构
    private int _treatTick = -1;//治疗显示计时
    // Use this for initialization
    void Start()
    {
        transform.localPosition += new Vector3((PlayerID - 1) * Screen.width / 4, 0, 0);//自动调整位置
        _instantiate = Instantiate(Prefab, transform);//初始化UI
        _instantiate.transform.localScale = new Vector3(Screen.width / 1920f, Screen.width / 1920f, 1f);//按照屏幕尺寸进行缩放
        _avatar = _instantiate.transform.Find("Avatar").GetComponent<RawImage>();
        _avatarShader = new Material(GreyShader);
        _teamName = _instantiate.transform.Find("TeamName").GetComponent<Text>();
        _hp = _instantiate.transform.Find("Health").GetComponent<Text>();
        _score = _instantiate.transform.Find("Score").GetComponent<Text>();
        _skill = new Icon[Images.Length];
        _skill[0] = new Icon(_instantiate.transform.Find("skill/skill0"), Images[0], GreyShader);
        _skill[1] = new Icon(_instantiate.transform.Find("skill/skill1"), Images[1], GreyShader);
        _skill[2] = new Icon(_instantiate.transform.Find("skill/skill2"), Images[2], GreyShader);
        _skill[3] = new Icon(_instantiate.transform.Find("skill/skill3"), Images[3], GreyShader, PartColorShader);
        TeamName = Player.GetComponent<TuringOperate>().AIScript.GetTeamName();
        SetupInfo();//设置玩家信息

        EventManager.Instance.AddListener(EVENT_TYPE.GAME_OVER, this);
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_DEAD, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (Player == null)
        {
            return;
        }
        //更新玩家状态显示
        SetHealth(Player.GetComponent<PlayerHealth>().GetHP().ToString());
        SetScore(Player.GetComponent<PlayerScoreManager>().GetScore());
        SetSkillCD(0, Player.GetComponent<PlayerMovement>().getBuffing());
        SetSkillCD(1, Player.GetComponent<PlayerShoot>().getBuffing());
        SetSkillCD(2, Player.GetComponent<PlayerBomb>().getBuffing());
        if (Player.GetComponent<PlayerHealth>().HadTreat())
        {
            _treatTick = 0;
        }
        if (_treatTick >= 0)
        {
            _treatTick++;
            _skill[_skill.Length - 1].SetMaterialAttr("_Part", 1.0f - _treatTick / (float)TreatEffectTicks);//渐渐变灰
            _skill[_skill.Length - 1].SetGrey(false);
        }
        else
        {
            _skill[_skill.Length - 1].SetGrey(true);
        }
        if (_treatTick >= TreatEffectTicks)
        {
            _treatTick = -1;
        }
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.GAME_OVER:
                transform.parent.gameObject.SetActive(false);// 游戏结束时，隐藏Status的list
                return true;
            case EVENT_TYPE.PLAYER_DEAD:
                if (Sender.gameObject == Player)
                {
                    Die();//对应玩家死亡
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    public void SetupInfo()
    {
        _avatar.texture = Avatar;
        _teamName.text = TeamName;
        SetHealth(OriginHealth.ToString());
        SetScore(0);
        for (int i = 0; i < _skill.Length; i++)
        {
            _skill[i].SetGrey(true);
        }
    }

    public void Die(bool die = true)
    {
        _avatar.material = die ? _avatarShader : null;
    }

    public void SetHealth(string health)
    {
        _hp.text = "HP: " + health;
    }

    public void SetScore(int score)
    {
        _score.text = "得分: " + score;
    }

    public void SetSkillCD(int index, float? cd)
    {
        if (index < 0 || index > _skill.Length - 1)
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
        private Material _shader;
        private Material _defaultShader;
        public Icon(Transform transform, Texture2D image, Shader shader, Shader _default = null)
        {
            _image = transform.Find("image");
            _cd = transform.Find("cd");
            _image.GetComponent<RawImage>().texture = image;
            _shader = new Material(shader);
            if (_default != null)
            {
                _defaultShader = new Material(_default);
            }
        }

        public void SetImg(Texture2D img)
        {
            _image.GetComponent<RawImage>().texture = img;
        }

        public void SetCD(float? cd)//设置CD显示
        {
            if (cd == null)
            {
                _cd.GetComponent<Text>().enabled = false;
            }
            else
            {
                _cd.GetComponent<Text>().enabled = true;
                _cd.GetComponent<Text>().text = cd.Value.ToString();
            }
        }

        public void SetGrey(bool grey = true)//设置shader
        {
            _image.GetComponent<RawImage>().material = grey ? _shader : _defaultShader;
        }

        public void SetMaterialAttr(string name, float value)//设置shader参数
        {
            Debug.Log(value);
            _image.GetComponent<RawImage>().material.SetFloat(name, value);
        }
    }
}
