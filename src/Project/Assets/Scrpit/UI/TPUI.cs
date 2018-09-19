using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPUI : MonoBehaviour
{
    RawImage _avatar;
    Text _name;
    GameObject _player;
    // Use this for initialization
    void Start()
    {
        _avatar = transform.Find("TeamInfo/Avatar").GetComponent<RawImage>();
        _name = transform.Find("TeamInfo/Name").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
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
        // TODO: 显示单独的状态信息
    }
}
