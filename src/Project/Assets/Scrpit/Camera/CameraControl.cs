using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public int AnimTick = 20;//切换动画帧数
    public Vector3 TPCRelativeVector = new Vector3(0, 2f, -2f);//第三人称相机相对向量
    Vector3 mainPos;
    public GameObject Aim { get; private set; }
    int tick = -1;
    // Use this for initialization
    void Start()
    {
        mainPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (tick >= 0)
        {
            Vector3 aimPos = mainPos;
            if (Aim != null)
            {
                aimPos = Aim.transform.position + TPCRelativeVector;
            }
            if (tick == AnimTick)
            {
                tick = -1;
                transform.position = aimPos;
                return;
            }
            transform.position += (aimPos - transform.position) / (AnimTick - tick);
            tick++;
        }
        else
        {
            if (Aim != null)
            {
                transform.position = Aim.transform.position + TPCRelativeVector;
            }
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (Aim == null) return;
            Aim = null;
            tick = 0;
        }
        else
        {
            int current = -1;
            if (Input.GetKeyDown(KeyCode.F9))
            {
                current = 1;
            }
            else if (Input.GetKeyDown(KeyCode.F10))
            {
                current = 2;
            }
            else if (Input.GetKeyDown(KeyCode.F11))
            {
                current = 3;
            }
            else if (Input.GetKeyDown(KeyCode.F12))
            {
                current = 4;
            }
            else
            {
                return;
            }
            if (Aim == null)
            {
                if (tick == -1)//保证上一个动画完成
                {
                    mainPos = transform.position;
                }
            }
            else if (Aim.GetComponent<PlayerScoreManager>().playerID == current)
            {
                return;
            }
            foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (p.GetComponent<PlayerScoreManager>().playerID == current)
                {
                    Aim = p;
                    break;
                }
            }
            tick = 0;
        }
    }

    public void SetAim(GameObject player)
    {
        Aim = player;
        tick = 0;
        //transform.position = aim.transform.position + TPCRelativeVector;
    }
}
