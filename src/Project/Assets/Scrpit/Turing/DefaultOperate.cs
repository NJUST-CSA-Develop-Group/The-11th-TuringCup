using System;
using System.Collections.Generic;
using UnityEngine;
using PlayerInterface;

class DefaultOperate : IControl
{
    private int _index;
    private static KeyCode[] _up = { KeyCode.W, KeyCode.T, KeyCode.I, KeyCode.UpArrow };
    private static KeyCode[] _dwon = { KeyCode.S, KeyCode.G, KeyCode.K, KeyCode.DownArrow };
    private static KeyCode[] _left = { KeyCode.A, KeyCode.F, KeyCode.J, KeyCode.LeftArrow };
    private static KeyCode[] _right = { KeyCode.D, KeyCode.H, KeyCode.L, KeyCode.RightArrow };

    private static KeyCode[] _shoot = { KeyCode.E, KeyCode.Y, KeyCode.O, KeyCode.End };
    private static KeyCode[] _bomb = { KeyCode.Q, KeyCode.R, KeyCode.U, KeyCode.Home };

    private static KeyCode[] _buffSpeed = { KeyCode.Alpha4, KeyCode.Alpha8, KeyCode.F8, KeyCode.Keypad4 };
    private static KeyCode[] _buffShoot = { KeyCode.Alpha2, KeyCode.Alpha6, KeyCode.F6, KeyCode.Keypad2 };
    private static KeyCode[] _buffBomb = { KeyCode.Alpha1, KeyCode.Alpha5, KeyCode.F5, KeyCode.Keypad1 };
    private static KeyCode[] _buffHP = { KeyCode.Alpha3, KeyCode.Alpha7, KeyCode.F7, KeyCode.Keypad3 };

    internal DefaultOperate(int i)
    {
        _index = (i + 1) % 4;
    }

    public string GetTeamName()
    {
        return "键盘控制器";
    }

    public void Update(IEntity entity)
    {
        //不再允许键盘操作
        if (_index != 1)
        {
            return;
        }
        Move(entity);
        Attack(entity);
        Buff(entity);
        return;
    }

    private void Move(IEntity entity)
    {
        if (Input.GetKey(_up[_index]))
        {
            entity.MoveNorth();
        }
        if (Input.GetKey(_dwon[_index]))
        {
            entity.MoveSouth();
        }
        if (Input.GetKey(_left[_index]))
        {
            entity.MoveWest();
        }
        if (Input.GetKey(_right[_index]))
        {
            entity.MoveEast();
        }
    }

    private void Attack(IEntity entity)
    {
        if (Input.GetKey(_shoot[_index]))
        {
            entity.Shoot();
        }
        if (Input.GetKey(_bomb[_index]))
        {
            entity.SetBomb();
        }
    }

    private void Buff(IEntity entity)
    {
        if (Input.GetKey(_buffSpeed[_index]))
        {
            entity.BuffSpeed();
        }
        if (Input.GetKey(_buffShoot[_index]))
        {
            entity.BuffShoot();
        }
        if (Input.GetKey(_buffBomb[_index]))
        {
            entity.BuffBomb();
        }
        if (Input.GetKey(_buffHP[_index]))
        {
            entity.BuffHP();
        }
    }
}
