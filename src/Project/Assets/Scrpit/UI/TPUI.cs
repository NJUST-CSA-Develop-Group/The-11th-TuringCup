// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPUI : MonoBehaviour
{
    public Texture2D[] Images;//按需存放技能贴图
    public Shader GreyShader;//灰度效果
    public Shader PartColorShader;//部分彩色效果
    public int TreatEffectTicks = 60;

    private RawImage _avatar;
    private Text _name;
    private GameObject _player;
    private Text _hp;
    private Text _score;
    private StatusUI.Icon[] _skill;//skill管理结构
    private int _treatTick = -1;//治疗显示计时
    // Use this for initialization
    void Start()
    {
        _avatar = transform.Find("TeamInfo/Avatar").GetComponent<RawImage>();
        _name = transform.Find("TeamInfo/Name").GetComponent<Text>();
        _hp = transform.Find("TeamStatus/HP").GetComponent<Text>();
        _score= transform.Find("TeamStatus/Score").GetComponent<Text>();
        _skill = new StatusUI.Icon[Images.Length];
        _skill[0] = new StatusUI.Icon(transform.Find("TeamStatus/skill/skill0"), Images[0], GreyShader);
        _skill[1] = new StatusUI.Icon(transform.Find("TeamStatus/skill/skill1"), Images[1], GreyShader);
        _skill[2] = new StatusUI.Icon(transform.Find("TeamStatus/skill/skill2"), Images[2], GreyShader);
        _skill[3] = new StatusUI.Icon(transform.Find("TeamStatus/skill/skill3"), Images[3], GreyShader, PartColorShader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        UpdateStatus();
    }

    public void Show()
    {
        int current = GameObject.FindGameObjectWithTag("Global").GetComponent<GameManager>().current;
        if (current == 0)
        {
            _player = null;
            return;
        }
        foreach (StatusUI s in transform.parent.Find("GodMode/StatusList").GetComponentsInChildren<StatusUI>())
        {
            if(s.PlayerID== current)
            {
                _player = s.Player;
                SetInfo(s);
                break;
            }
        }
        GetComponent<Animator>().SetBool("visible", true);
    }

    public void Hide()
    {
        GetComponent<Animator>().SetBool("visible", false);
        Invoke("ClearPlayer", 0.5f);
    }

    public void Toggle()//在不同的player视角间切换
    {
        GetComponent<Animator>().SetBool("visible", false);
        Invoke("Show", 0.5f);
    }

    private void SetInfo(StatusUI player)
    {
        _avatar.texture = player.Avatar;
        _name.text = player.Player.GetComponent<TuringOperate>().AIScript.GetTeamName();
    }

    private void ClearPlayer()
    {
        _player = null;
    }

    private void UpdateStatus()
    {
        if (_player == null)
        {
            return;
        }
        SetHealth(_player.GetComponent<PlayerHealth>().GetHP().ToString());
        SetScore(_player.GetComponent<PlayerScoreManager>().GetScore());
        SetSkillCD(0, _player.GetComponent<PlayerMovement>().getBuffing());
        SetSkillCD(1, _player.GetComponent<PlayerShoot>().getBuffing());
        SetSkillCD(2, _player.GetComponent<PlayerBomb>().getBuffing());
        if (_player.GetComponent<PlayerHealth>().HadTreat())
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
}
