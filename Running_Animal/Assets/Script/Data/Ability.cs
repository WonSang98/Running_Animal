using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public Status<float> MAX_HP; //최대 체력
    public Status<float> HP; //현재 체력
    public Status<float> SPEED; // 속도
    public Status<int> MAX_JUMP; // 최대 가능한 점프횟수
    public Status<float> JUMP; // 점프력
    public Status<float> DOWN; // 하강력
    public Status<float> DEF; // 방어력
    public Status<short> LUK; // 행운
    public Status<float> RESTORE; // 재생력

    public Ability
        (
        Status<float> _MAXHP,
        Status<float> _HP,
        Status<float> _SPEED,
        Status<int> _MAX_JUMP,
        Status<float> _JUMP,
        Status<float> _DOWN,
        Status<float> _DEF,
        Status<short> _LUK,
        Status<float> _RESTORE
        )
    {
        MAX_HP = _MAXHP;
        HP = _HP;
        SPEED = _SPEED;
        MAX_JUMP = _MAX_JUMP;
        JUMP = _JUMP;
        DOWN = _DOWN;
        DEF = _DEF;
        LUK = _LUK;
        RESTORE = _RESTORE;
    }
    public Ability(Ability a)
    {
        MAX_HP = a.MAX_HP;
        HP = a.HP;
        SPEED = a.SPEED;
        MAX_JUMP = a.MAX_JUMP;
        JUMP = a.JUMP;
        DOWN = a.DOWN;
        DEF = a.DEF;
        LUK = a.LUK;
        RESTORE = a.RESTORE;
    }

    public Ability DeepCopy()
    {
        Ability ab = new Ability();
        ab.MAX_HP = this.MAX_HP.DeepCopy();
        ab.HP = this.HP.DeepCopy();
        ab.SPEED = this.SPEED.DeepCopy();
        ab.MAX_JUMP = this.MAX_JUMP.DeepCopy();
        ab.JUMP = this.JUMP.DeepCopy();
        ab.DOWN = this.DOWN.DeepCopy();
        ab.DEF = this.DEF.DeepCopy();
        ab.LUK = this.LUK.DeepCopy();
        ab.RESTORE = this.RESTORE.DeepCopy();

        return ab;
    }

    Ability()
    {
    }
}