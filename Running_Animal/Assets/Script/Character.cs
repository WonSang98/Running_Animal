using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    // Ä³¸¯ÅÍ ½ºÅÈ
    /*
     * { LV , STAT_POINT, MAX_HP, SPEED, JUMP, DOWN, JUMP_CNT, DEF, LUK, ACTIVE }
     */

    public int LV;
    public int STAT_POINT;
    public float MAX_HP;
    public int LV_MAX_HP;
    public float SPEED;
    public int LV_SPEED;
    public float JUMP_POWER;
    public int LV_JUMP_POWER;
    public float DOWN_POWER;
    public int LV_DOWN_POWER;
    public float JUMP_COUNT;
    public int LV_JUMP_COUNT;
    public float DEF;
    public int LV_DEF;
    public int LUK;
    public int LV_LUK;
    public DataManager.Active_Skil ACTIVE;

    public Character(int _LV, int _STAT_POINT,
                     float _MAX_HP, int _LV_MAX_HP,
                     float _SPEED, int _LV_SPEED,
                     float _JUMP_POWER, int _LV_JUMP_POWER,
                     float _DOWN_POWER, int _LV_DOWN_POWER,
                     float _JUMP_COUNT, int _LV_JUMP_COUNT,
                     float _DEF, int _LV_DEF,
                     int _LUK, int _LV_LUK, DataManager.Active_Skil _ACTIVE)
    {
        LV = _LV;
        STAT_POINT = _STAT_POINT;
        MAX_HP = _MAX_HP;
        LV_MAX_HP = _LV_MAX_HP;
        SPEED = _SPEED;
        LV_SPEED = _LV_SPEED;
        JUMP_POWER = _JUMP_POWER;
        LV_JUMP_POWER = _LV_JUMP_POWER;
        DOWN_POWER = _DOWN_POWER;
        LV_DOWN_POWER = _LV_DOWN_POWER;
        JUMP_COUNT = _JUMP_COUNT;
        LV_JUMP_COUNT = _LV_JUMP_COUNT;
        DEF = _DEF;
        LV_DEF = _LV_DEF;
        LUK = _LUK;
        LV_LUK = _LV_LUK;
        ACTIVE = _ACTIVE;
    }

    Character() { }




}
