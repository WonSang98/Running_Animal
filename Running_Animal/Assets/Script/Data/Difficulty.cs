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
    public short LUK;
    public float DEF;
    public float SPEED;
    public float EXP;

    public static string[] DIFF_CODE = { "����", "��Ĩ", "����", "����", "�뼭", "����", "�ߺ�", "�Ե�", "�뼳", "����"};

    public Difficulty(float _DMG, float _COIN, float _RESTORE, short _LUK, float _DEF, float _SPEED, float _EXP)
    {
        DMG = _DMG;
        COIN = _COIN;
        RESTORE = _RESTORE;
        LUK = _LUK;
        DEF = _DEF;
        SPEED = _SPEED;
        EXP = _EXP;
    }
    public static Difficulty[] Forest =
    {
        new Difficulty(16,  1.00f, 0.00f , 0 , 0.00f , 0.00f  , 1), // LEVEL 1
        new Difficulty(21,  1.5f, 0.04f , 1 , 0.05f , 0.00f  , 1), // LEVEL 2
        new Difficulty(30,  2.5f, 0.06f , 2 , 0.10f , 1.00f  , 1.132f), // LEVEL 3
        new Difficulty(41,  3.0f, 0.09f , 3 , 0.16f , 1.00f  , 1.13f), // LEVEL 4
        new Difficulty(64,  3.5f, 0.13f , 5 , 0.22f , 1.00f  , 1.13f), // LEVEL 5
        new Difficulty(85,  7.0f, 0.20f , 12 , 0.31f , 2.00f  , 1.23f), // LEVEL 6
        new Difficulty(105,  7.0f, 0.26f , 18, 0.40f , 2.00f  , 1.23f), // LEVEL 7
        new Difficulty(130,  10.0f, 0.33f , 23, 0.50f , 3.00f , 1.36f), // LEVEL 8
        new Difficulty(160, 11.0f, 0.41f , 28, 0.61f , 3.00f , 1.36f), // LEVEL 9
        new Difficulty(230, 14.0f, 0.48f , 34, 0.72f , 3.00f , 1.36f), // LEVEL 10
    };
}
