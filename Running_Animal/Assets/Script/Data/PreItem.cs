using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemSet
// 아이템 관리 데이터 셋
// 해당 아이템을 적용하는지 (Boolean)
// 해당 아이템의 보유 개수  (int)
public struct ItemSet
{
    public bool USE;
    public int CNT;

    public ItemSet(bool _USE, int _CNT)
    {
        USE = _USE;
        CNT = _CNT;
    }
}

public class PreItem
{
    public enum Random_Item // 시작 전 구매 랜덤 아이템 목록
    {
        None = 0,
        HP15, // 체력 15% 증가
        HP30, // 체력 30% 증가
        LUK5, // 행운 5 증가
        LUK10, // 행운 10 증가
        SPEED15, // 속도 15%
        SPEED30, // 속도 30%
        JUMP20, // 점프 20%
        JUMP40, // 점프 40%
        GOLD25, // 골드 획득량 25% 증가
        GOLD50, // 골드 획득량 50% 증가
        COMBO2, // 콤보 획득량 2배
        COMBO3, // 콤보 획득량 3배
        JUMP_PLUS, // 점프 횟수 1회 추가
        DEF10, // 피해 10% 경감
        DEF15, // 피해 15% 경감
        EXP2, // 경험치 2배 (스테이지 도달 속도 UP)
    }

    public ItemSet Pre_HP;
    public ItemSet Pre_Shield;
    public ItemSet Pre_100;
    public ItemSet Pre_300;
    public Active.ACTIVE_CODE Pre_Active;
    public Random_Item Pre_Random;

    public PreItem(
        ItemSet _pre_hp,
        ItemSet _pre_shield,
        ItemSet _pre_100,
        ItemSet _pre_300, 
        Active.ACTIVE_CODE _pre_active,
        Random_Item _pre_random)
    {

        Pre_HP = _pre_hp;
        Pre_Shield = _pre_shield;
        Pre_100 = _pre_100;
        Pre_300 = _pre_300;
        Pre_Active = _pre_active;
        Pre_Random = _pre_random;
    }

    PreItem()
    {

    }
}
