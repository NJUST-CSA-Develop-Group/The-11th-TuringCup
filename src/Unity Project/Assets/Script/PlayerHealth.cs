using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsAlive { get; private set; }
    public GameObject prefab;// 射击模型预制
    public AudioClip hurtAudio;// 音频源
    public int health { get; private set; }// 血量

    GameObject prefabInstantiate;// 射击模型实例
    Light m_hurt;// 伤害效果
    AudioSource m_audio;// 音频效果
    //int health;// 血量
    int tick = -1;// 受伤动画的时序
    const int ALL_TICKS = 20;// 动画总tick数
    // Use this for initialization
    void Start()
    {
        prefabInstantiate = Instantiate(prefab, transform);// 实例化预制
        health = 100;// 初始化血量
        IsAlive = true;
        m_hurt = prefabInstantiate.GetComponent<Light>();
        m_audio = prefabInstantiate.GetComponent<AudioSource>();
        m_audio.clip = hurtAudio;// 设置音频源
    }

    // Update is called once per frame
    void Update()
    {
        if (tick == ALL_TICKS)// 脱离动画状态，关灯
        {
            m_hurt.enabled = false;
            tick = -1;
        }
        else if (tick >= 0)// 进入动画状态，灯打开
        {
            m_hurt.enabled = true;
            m_audio.Play();// 播放音频
            tick++;
        }
    }

    private void LateUpdate()
    {
        if (health > 100)// 100HP上限
        {
            health = 100;
        }
        if (health <= 0)// 0HP下限，满足死亡条件
        {
            health = 0;
            Die();
        }
        SetUIHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        tick = 0;
    }

    public void Treat(int treat = 50)
    {
        if (!IsAlive)
        {
            return;
        }
        health += treat;
    }

    private void Die()// 设置死亡
    {
        IsAlive = false;
        // 在此处对UI死亡状态设置（如果需要）
        GetComponent<PlayerUIBind>().Die();
    }

    private void SetUIHealth()// 设置UI对应血条
    {
        // 在此处对UI血量进行设置
        GetComponent<PlayerUIBind>().SetHealth(health);
    }
}
