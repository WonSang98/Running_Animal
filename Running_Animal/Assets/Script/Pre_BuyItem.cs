using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pre_BuyItem : MonoBehaviour
{
    int normal_price = 500;
    int random_price = 1000;

    enum Random_Item
    {
        HP15 = 0, // 체력 15% 증가
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

    private void Start()
    {
        GameManager.Data.max_hp += GameManager.Data.Talent_HP;
        GameManager.Data.hp = GameManager.Data.max_hp;
        GameManager.Data.defense += GameManager.Data.Talent_DEF;
        GameManager.Data.luck += GameManager.Data.Talent_LUK;
        GameManager.Data.restore_eff += (GameManager.Data.Talent_Restore - 1);

        // 맵이 Forest일때
        for (int i = 0; i < 255; i++)
        {
            GameManager.Data.pattern.Add(i);
        }

        GameManager.Data.pattern = GameManager.Instance.ShuffleList(GameManager.Data.pattern);
    }

    public void Buy_HP() // 체력 10% 증가.
    {
        if(GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.max_hp *= 1.1f;
            GameManager.Data.hp = GameManager.Data.max_hp;
        }
    }

    public void Buy_Shield() // 시작 시 1회 쉴드
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_Shield = true;
        }
    }

    public void Buy_100() //100미터 달리기 이용권
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_100 = true;
        }
    }

    public void Buy_300() //300미터 달리기 이용권
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_300 = true;
        }
    }

    public void Buy_Skil() //랜덤 액티브 스킬 구매권
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            int idx = Random.Range(1, Enum.GetNames(typeof(DataManager.Active_Skil)).Length);
            GameManager.Data.active = (DataManager.Active_Skil)idx;
        }
    }

    public void Buy_Random() // 두근 두근 랜덤 시작전 아이템 갓챠~
    {
        if (GameManager.Data.Gold > random_price)
        {
            GameManager.Data.Gold -= random_price;
            int idx = Random.Range(0, Enum.GetNames(typeof(Random_Item)).Length);
            GameManager.Data.active = (DataManager.Active_Skil)idx;
        }
    }
}
