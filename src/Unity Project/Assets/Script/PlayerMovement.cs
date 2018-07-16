using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour {
    public float moveSpeed = 6.0f;//角色移动的速度
    private Rigidbody playerRigid;//角色刚体
    private Animator anim;//角色动画
    private Vector3 movement;//控制每帧移动方向的V3向量
    private Vector3 nowPosition;//玩家现在所处位置
    private Vector3 targetPosition;//玩家想要移动的目标位置
    private int statement;//玩家所处状态（与animator不同）
    private float perDistance=3f;//每一格的距离
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody>();
        nowPosition = transform.position;
        statement = 0;

    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (statement == 0)
        {
            if (h > 0)
            {
                MoveEast();
            }
            else if (h < 0)
            {
                MoveWest();
            }
            else if(v>0)
            {
                MoveNorth();
            }
            else if(v<0)
            {
                MoveSouth();
            }
            else
            {

            }
        }
        else if (statement == 1)
        {
            MoveEast();
        }
        else if (statement == 2)
        {
            MoveSouth();
        }
        else if(statement==3)
        {
            MoveWest();
        }
        else if(statement==4)
        {
            MoveNorth();
        }
        else
        {

        }
    }
    private void MoveEast()//向东方向移动一格
    {
        Vector3 offset = new Vector3(perDistance, 0, 0);
        targetPosition = nowPosition + offset;
        Turn(1);
        AnimatingIdleToWalk();
        if (System.Math.Abs(transform.position.x-targetPosition.x) <= 0.01f)
        {
            nowPosition = transform.position;
            statement = 0;
            AnimatingWalkToIdle();
        }
        else
        {    
            statement = 1;
            Movement(1, 0);
        }
    }
    private void MoveWest()//向西方向移动一格
    {
        float NewDistance = perDistance * -1;
        Vector3 offset = new Vector3(NewDistance, 0, 0);
        targetPosition = nowPosition + offset;
        Turn(3);
        AnimatingIdleToWalk();
        if (System.Math.Abs(transform.position.x - targetPosition.x) <= 0.01f)
        {
            nowPosition = transform.position;
            statement = 0;
            AnimatingWalkToIdle();
        }
        else
        { 
            statement = 3;
            Movement(-1, 0);
        }

    }
    private void MoveNorth()//向北方向移动一格
    {
        float NewDistance = perDistance ;
        Vector3 offset = new Vector3(0, 0, NewDistance);
        targetPosition = nowPosition + offset;
        Turn(4);
        AnimatingIdleToWalk();
        if (System.Math.Abs(transform.position.z - targetPosition.z) <= 0.01f)
        {
            nowPosition = transform.position;
            statement = 0;
            AnimatingWalkToIdle();
        }
        else
        {
            statement = 4;
            Movement(0, 1);
        }
    }
    private void MoveSouth()//向南方向移动一格
    {
        float NewDistance = perDistance * -1;
        Vector3 offset = new Vector3(0, 0, NewDistance);
        targetPosition = nowPosition + offset;
        Turn(2);
        AnimatingIdleToWalk();
        if (System.Math.Abs(transform.position.z - targetPosition.z) <= 0.01f)
        {
            nowPosition = transform.position;
            statement = 0;
            AnimatingWalkToIdle();
        }
        else
        {
            statement = 2;
            Movement(0, -1);
        }
    }
    private void Movement(float h,float v)//玩家移动,h控制东西，v控制南北
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        playerRigid.MovePosition(transform.position + movement);
    }
    private void Turn(int state)//控制角色面对方向，参数state1-4分别代表东南西北
    {
        if (state == 1)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (state == 2)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(state==3)
        {
            transform.rotation= Quaternion.Euler(0, 270, 0);
        }
        else if (state == 4)
        {
            transform.rotation = Quaternion.Euler(0, 360, 0);
        }
        else
        {

        }
    }
    private void AnimatingWalkToIdle()//从移动到站立状态
    {
        anim.SetBool("IsWalking", false);
    }
    private void AnimatingIdleToWalk()//从站立到移动状态
    {
        anim.SetBool("IsWalking", true);
    }

}
