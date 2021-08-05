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
