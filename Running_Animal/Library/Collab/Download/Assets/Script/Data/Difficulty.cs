using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty
{
    /*
     * 피격 데미지 상승
     * 코인 획득량 증가
     * 회복 효율 감소
     * 행운 감소
     * 방어력 감소
     * 속도 증가
     * 다음 스테이지 필요 경험치량 증가
     */

    public float DMG;
    public float COIN;
    public float RESTORE;
    public short LUK;
    public float DEF;
    public float SPEED;
    public float EXP;

    public static string[] DIFF_CODE = { "입춘", "경칩", "입하", "하지", "대서", "입추", "추분", "입동", "대설", "동지"};

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
        new Difficulty(20,  1.00f, 0.00f , 0 , 0.00f , 0.00f  , 0), // LEVEL 1
        new Difficulty(25,  1.05f, 0.01f , 0 , 0.01f , 0.25f  , 0), // LEVEL 2
        new Difficulty(30,  1.10f, 0.02f , 0 , 0.02f , 0.50f  , 0), // LEVEL 3
        new Difficulty(40,  1.20f, 0.04f , 5 , 0.04f , 0.33f  , 10), // LEVEL 4
        new Difficulty(50,  1.30f, 0.06f , 6 , 0.06f , 0.66f  , 10), // LEVEL 5
        new Difficulty(60,  1.40f, 0.08f , 7 , 0.08f , 0.99f  , 10), // LEVEL 6
        new Difficulty(75,  1.60f, 0.12f , 10, 0.12f , 1.5f  , 15), // LEVEL 7
        new Difficulty(90,  1.80f, 0.16f , 12, 0.16f , 2.00f , 15), // LEVEL 8
        new Difficulty(105, 2.00f, 0.20f , 14, 0.20f , 2.50f , 20), // LEVEL 9
        new Difficulty(120, 2.20f, 0.25f , 15, 0.25f , 3.00f , 20), // LEVEL 10
    };
}
