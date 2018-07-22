using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIBind : MonoBehaviour
{
    public GameObject playerStatus;// 对应的playerSatusUI
    public GameObject playerStatusCurrent;// 对应的playerStatusCurrentUI
    public int index;// 玩家序号，需在编辑器中设置，需和controller中保持一致

    PlayerStatus m_status;
    PlayerStatus m_statusCurrent;
    bool isInited = false;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInited)
        {
            isInited = true;
            m_status = playerStatus.GetComponent<PlayerStatus>();
            m_statusCurrent = playerStatusCurrent.GetComponent<PlayerStatus>();
            if (ControlLoader.controllers[index] != null)// 对玩家信息加载
            {
                m_status.team_name = ControlLoader.controllers[index].GetTeamName();
                m_statusCurrent.team_name = ControlLoader.controllers[index].GetTeamName();
            }
            m_status.SetInfo();
            m_statusCurrent.SetInfo();
        }
    }

    public void SetHealth(int health)
    {
        m_status.SetHealth(health);
        m_statusCurrent.SetHealth(health);
    }

    public void Die(bool dead = true)
    {
        m_status.Die(dead);
        m_statusCurrent.Die(dead);
    }

    public void SetSkill(int index, bool type)
    {
        m_status.SetSkill(index, type);
        m_statusCurrent.SetSkill(index, type);
    }
}
