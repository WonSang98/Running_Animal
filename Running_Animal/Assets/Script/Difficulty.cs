using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty
{
    /*
     * �ǰ� ������ ���
     * ���� ȹ�淮 ����
     * ȸ�� ȿ�� ����
     * ��� ����
     * ���� ����
     * �ӵ� ����
     * ���� �������� �ʿ� ����ġ�� ����
     */

    public float DMG;
    public float COIN;
    public float RESTORE;
    public float LUK;
    public float DEF;
    public float SPEED;
    public float EXP;

    public Difficulty(float _DMG, float _COIN, float _RESTORE, float _LUK, float _DEF, float _SPEED, float _EXP)
    {
        DMG = _DMG;
        COIN = _COIN;
        RESTORE = _RESTORE;
        LUK = _LUK;
        DEF = _DEF;
        SPEED = _SPEED;
        EXP = _EXP;
    }

    Difficulty() { }

}
