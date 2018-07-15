using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int Damage { get; set; }// 可调节伤害
    public GameObject prefab;// 射击模型预制
    public AudioClip shootAudio;// 音频源
    public Material shootLineMaterial;// 射击线材质
    public Material particleMaterial;// 粒子材质
    public float fixDistance = 0f;// 修正距离，防止因为模型问题命中自己

    GameObject prefabInstantiate;// 射击模型实例
    int tick = -1;// 射击动画的时序
    const int ALL_TICKS = 5;// 射击动画总tick数
    Light m_light;// Sopt Lgiht的Light组件
    Transform m_line;// 射击线
    ParticleSystem m_ps;// 粒子系统，用于火焰效果
    AudioSource m_audio;// 音频效果
    float time;// 上次触发时间

    // Use this for initialization
    void Start()
    {
        prefabInstantiate = Instantiate(prefab, transform);// 实例化预制
        Damage = 25;// 初始化伤害
        m_light = prefabInstantiate.transform.Find("Spot Light").GetComponent<Light>();
        m_line = prefabInstantiate.transform.Find("line");
        m_ps = prefabInstantiate.transform.Find("Particle System").GetComponent<ParticleSystem>();
        m_audio = prefabInstantiate.GetComponent<AudioSource>();
        m_audio.clip = shootAudio;// 设置音频源
        m_line.Find("Cylinder").GetComponent<Renderer>().material = shootLineMaterial;
        m_ps.GetComponent<Renderer>().material = particleMaterial;
        time = Time.time - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

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

    public void Shoot()// 可被调用的射击函数接口
    {
        if (Time.time - time < 0.5f)// 小于0.5s间隔
        {
            return;
        }
        time = Time.time;
        tick = 0;// 启动射击动画
        Ray ray = new Ray(prefabInstantiate.transform.position + transform.forward * fixDistance, transform.forward);// 初始化命中判定
        RaycastHit hit;// ray相交判定参数
        float length = 15;// 射击线长度，默认100
        if (Physics.Raycast(ray, out hit, 100))
        {
            length = Vector3.Distance(prefabInstantiate.transform.position, hit.point);// 计算射击线长度
            if (length <= 15)// 处在射击范围
            {
                PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();// 尝试获取PlayerHealth组件
                if (player)// 判定击中是否为玩家
                {
                    player.TakeDamage(Damage);// 产生伤害
                }
            }
            else// 射击范围之外
            {
                length = 15;
            }

        }
        m_line.localScale = new Vector3(0.01f, 0.01f, length);// 调整射击线长度
    }
}
