using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent
{
    //재능 관련 데이터 
    public Status<float> HP;
    public Status<float> DEF;
    public Status<short> LUK;
    public Status<float> RESTORE;

    // 재능 최대 레벨
    public static int MAX_LEVEL = 10;
    // 재능 업그레이드 비용
    public static int[] COST = {500, 1000, 1500, 2000, 2500, 3000, 4000, 5000, 6000, 7000 };
    // 재능 업그레이드 시 능력치
    public static float[,] UPGRADE_POWER =
    {
        {20, 40, 60, 80, 100, 120, 140, 160, 180, 200}, // HP
        {0.01f, 0.02f, 0.03f, 0.04f, 0.05f, 0.06f, 0.07f, 0.08f, 0.09f, 0.10f}, // DEF
        {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, // LUK
        {1.1f, 1,15f, 1.2f, 1.25f, 1.3f, 1.35f, 1.4f, 1.45f, 1.5f} // RESTORE
    };

    public Talent(
        Status<float> _HP,
        Status<float> _DEF,
        Status<short> _LUK,
        Status<float> _RESTORE)
    {
        HP = _HP;
        DEF = _DEF;
        LUK = _LUK;
        RESTORE = _RESTORE;
    }

    Talent()
    {

    }
}
