using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour,TListener {

    public int Damage = 15;// 可调节伤害
    public float TimeBetweenBullets = 0.5f;  //射击间隔
    public float range = 100f; //射击距离
    public float EffectsDisplayTime = 0.2f;  //设计特效持续时间
    public float BuffTime = 5f; //加强时间
    public int BuffValue = 25;//增加伤害值

    public float FixDistance = 0.5f;//用于防止计算时命中自己，与模型的尺寸有关
    public float ShootHeight = 0;//射击起点相对于模型中心的高度 //模型的碰撞箱略有问题，暂时请设为0
    public GameObject GunRef;//绑定枪
    public Vector3 MuzzleDis;//枪口的位移

    //Ray ShootRay;
    //RaycastHit ShootHit;
    int ShootableMask;
    LineRenderer GunLine;

    bool ShootAvaliable;
    float ShootTiming;
    bool isBuffing;
    float BuffTiming;

    //调试用效果
    public GameObject prefab;// 射击模型预制
    public AudioClip shootAudio;// 音频源
    public Material shootLineMaterial;// 射击线材质
    public Material particleMaterial;// 粒子材质
    GameObject prefabInstantiate;// 射击模型实例
    int tick = -1;// 射击动画的时序
    const int ALL_TICKS = 5;// 射击动画总tick数
    Light m_light;// Sopt Lgiht的Light组件
    Transform m_line;// 射击线
    ParticleSystem m_ps;// 粒子系统，用于火焰效果
    AudioSource m_audio;// 音频效果

    public float? getBuffing()
    {
        if (isBuffing)
        {
            return BuffTime - BuffTiming;
        }
        else
        {
            return null;
        }
    }

    // Use this for initialization
    void Start () {
        ShootableMask = LayerMask.GetMask("Attackable");
        GunLine = GetComponent<LineRenderer>();
        ShootAvaliable = true;
        ShootTiming = 0;
        isBuffing = false;
        BuffTiming = 0;
        EventManager.Instance.AddListener(EVENT_TYPE.TURING_FIRE, this);
        EventManager.Instance.AddListener(EVENT_TYPE.SHOOT_BUFF, this);

        //调试阶段使用效果
        prefabInstantiate = Instantiate(prefab, GunRef.transform);
        prefabInstantiate.transform.localPosition += MuzzleDis;//修正位置
        m_light = prefabInstantiate.transform.Find("Spot Light").GetComponent<Light>();
        m_line = prefabInstantiate.transform.Find("line");
        m_ps = prefabInstantiate.transform.Find("Particle System").GetComponent<ParticleSystem>();
        m_audio = prefabInstantiate.GetComponent<AudioSource>();
        m_audio.clip = shootAudio;// 设置音频源
        m_line.Find("Cylinder").GetComponent<Renderer>().material = shootLineMaterial;
        m_ps.GetComponent<Renderer>().material = particleMaterial;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Timing();

        //调试用效果
        if (tick == ALL_TICKS)// 脱离动画状态，关灯
        {
            m_light.enabled = false;
            m_line.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            m_ps.Stop();// 关闭粒子动画
            tick = -1;
        }
        else if (tick >= 0)// 进入动画状态，灯打开
        {
            m_light.enabled = true;
            m_line.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            if (!m_ps.isPlaying)// 启动粒子动画
            {
                m_ps.Play();
            }
            m_audio.Play();// 播放音频
            tick++;
        }
    }

    private void Shoot()
    {
        ShootAvaliable = false;
        Ray ray = new Ray(transform.position + transform.forward * FixDistance + transform.up * ShootHeight, transform.forward);
        RaycastHit hit;
        float length = 100;
        if (Physics.Raycast(ray, out hit, range))
        {
            length = (hit.transform.position - transform.position).magnitude;
            if (hit.transform.GetComponent<PlayerHealth>())//随意获取一定会在玩家上的脚本，用来确定命中的是玩家
            {
                Dictionary<string, object> args = new Dictionary<string, object>();
                args.Add("AttackerID", (int)GetComponent<PlayerScoreManager>().playerID);
                args.Add("ShootPower", Damage);
                EventManager.Instance.PostNotification(EVENT_TYPE.SHOOT_HIT_PLAYER, this, hit.transform.gameObject, args);
                args.Clear();
            }
        }

        //调试用效果
        tick = 0;// 启动射击动画
        m_line.localScale = new Vector3(0.01f, 0.01f, length);// 调整射击线长度
    }

    private void Timing()
    {
        if (!ShootAvaliable)//技能冷却时计时
        {
            ShootTiming += Time.deltaTime;
        }
        if (ShootTiming >= TimeBetweenBullets)//冷却时间到时 设置射击可用 并重置计时器
        {
            ShootAvaliable = true;
            ShootTiming = 0;
        }

        if (isBuffing)//如果存在加强 则进行计时
        {
            BuffTiming += Time.deltaTime;
        }
        if (BuffTiming >= BuffTime) //加强时间到 取消加强
        {
            isBuffing = false;
            Damage -= BuffValue;
            BuffTiming = 0;
        }
    }

    public void IncreaseBulltDamage()
    {
        isBuffing = true;//设置处于加强状态
        Damage += BuffValue;
    }

    public bool CanShoot()
    {
        return ShootAvaliable && !GetComponent<PlayerMovement>().IsMoving();
    }

    public bool OnEvent(EVENT_TYPE Event_Type, Component Sender, Object param = null, Dictionary<string, object> value = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.TURING_FIRE: //选手操作：射击
                if (ShootAvaliable && !GetComponent<PlayerMovement>().IsMoving())//如果当前可射击
                {
                    GetComponent<Animator>().SetTrigger("Shoot");
                    Shoot();
                    return true;
                }
                else
                {
                    return false;
                }
            case EVENT_TYPE.SHOOT_BUFF: //选手操作：加强射击
                IncreaseBulltDamage();
                return true;
            default: return false;
        }
    }

    public Object getGameObject()
    {
        return gameObject;
    }
}
