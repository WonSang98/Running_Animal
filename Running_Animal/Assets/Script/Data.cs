using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // 재화 관리
    public int Cash = 100; // 캐쉬 재화
    public int Gold = 1000000; // 인게임 재화
    public int Money_Forest = 1000; // 숲 테마 재화
    public int Money_Desert = 10; // 숲 테마 재화
    public int Money_Arctic = 10; // 숲 테마 재화
    // 테마 관리
    public DataManager.Themes Theme = DataManager.Themes.Forest;
    // 캐릭터 구매 관리
    public bool[] Buy_Character = { false, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public DataManager.Characters Now_Character = DataManager.Characters.Rabbit;

    // 캐릭터 스탯
    /*
     * { LV , STAT_POINT, MAX_HP, SPEED, JUMP, DOWN, JUMP_CNT, DEF, LUK, ACTIVE }
     */

    public Character[] Character_STAT =
    {
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None)
    };

    //재능 관련
    public float Talent_HP = 0;
    public float Talent_DEF = 0;
    public int Talent_LUK = 0;
    public float Talent_Restore = 1.0f;

    public short[] Talent_LV = { 0, 0, 0, 0 }; // 재능 레벨

    // 시작 전 아이템 구매
    public Pre_Item Pre_HP = new Pre_Item(false, 0);
    public Pre_Item Pre_Shield = new Pre_Item(false, 0);
    public Pre_Item Pre_100 = new Pre_Item(false, 0);
    public Pre_Item Pre_300 = new Pre_Item(false, 0);
    public DataManager.Active_Skil Pre_Active = DataManager.Active_Skil.None;
    public DataManager.Random_Item Pre_Random = DataManager.Random_Item.None;

    // 시작 전 아이템 관련
    public float Exp_run = 0; // 100미터 300미터 질주 시, 질주 가 끝난 후 파괴 된 장애물 경험치 한 번에 적용.

    // 난이도 설정
    /*
     * 피격 데미지 상승
     * 코인 획득량 증가
     * 회복 효율 감소
     * 행운 감소
     * 방어력 감소
     * 속도 증가
     * 다음 스테이지 필요 경험치량 증가
     */
    public Difficulty[] Forest_Diff =
    {
        new Difficulty(20,  0.00f, 0.00f , 0 , 0.00f , 0.00f  , 0), // LEVEL 1
        new Difficulty(25,  0.05f, 0.01f , 0 , 0.01f , 0.25f  , 0), // LEVEL 2
        new Difficulty(30,  0.10f, 0.02f , 0 , 0.02f , 0.50f  , 0), // LEVEL 3
        new Difficulty(40,  0.20f, 0.04f , 5 , 0.04f , 0.33f  , 10), // LEVEL 4
        new Difficulty(50,  0.30f, 0.06f , 6 , 0.06f , 0.66f  , 10), // LEVEL 5
        new Difficulty(60,  0.40f, 0.08f , 7 , 0.08f , 0.99f  , 10), // LEVEL 6
        new Difficulty(75,  0.60f, 0.12f , 10, 0.12f , 1.5f  , 15), // LEVEL 7
        new Difficulty(90,  0.80f, 0.16f , 12, 0.16f , 2.00f , 15), // LEVEL 8
        new Difficulty(105, 1.00f, 0.20f , 14, 0.20f , 2.50f , 20), // LEVEL 9
        new Difficulty(120, 1.20f, 0.25f , 15, 0.25f , 3.00f , 20), // LEVEL 10
    };
    public int Diff_LV = 1; // 난이도 레벨

    // 게임 플레이 관리
    public bool playing = false; // 플레이 중이던 게임이 있었는지 확인.

    public DataManager.Active_Skil active = DataManager.Active_Skil.None; // 현재 가진 액티브 능력
    public DataManager.Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // 현재 가진 패시브 능력
    public float[] EXP = { 0, 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999 }; //레벨업 시 필요 경험치
    public bool lvup; // 1.레벨업 여부, true일 시 다음 장애물은 레벨업하는 장소로.
    public int lv = 1; // 2.현재 레벨 최대 0~12렙까지
    public float now_Exp = 0; // 3.현재 경험치 
    public float multi_exp = 1;
    public short stage = 0; // 4.통과한 스테이지 
    public float play_gold = 0; // 5.게임 중 얻은 골드
    public float multi_coin = 1; // 6.코인 획득량 증가율
    public float max_hp = 100.0f; // 7.최대 체력
    public float hp = 100.0f; // 8.현재 체력
    public float speed = 9.0f; // 9.현재 속도
    public float jump = 10.0f; // 10.현재 점프력
    public float down = 20.0f; // 11.현재 하강 속도
    public float defense = 1.0f; // 12.현재 방어력
    public float damage = 20.0f; // 13.현재 피격 데미지
    public int combo = 0; // 14.게임 진행 중 콤보 
    public int max_combo = 0; // 15.게임 진행 중 최대 콤보
    public int multi_combo = 1; // 16.콤보 배율
    public int max_jump = 2; // 17.최대 점프 가능 횟수
    public float luck = 0; // 18.행운 (회피와, 콤보 크리티컬에 기인한다)
    public int max_active = 1; // 19.액티브 스킬 최대 사용가능 횟수
    public int use_active = 0; // 20.액티브 스킬 현대 사용 횟수
    public int dodge_time = 12; // 21.피격시 무적 시간 길이. default 12
    public float restore_eff = 1.0f; // 22.회복 효율성

    public bool magnet = false; // 23.패시브 자석버그 유무
    public short buwhal = 0; // 24.부활 가능 횟수
    public bool auto_jump = false; //25.패시브 오토점프 유무
    public bool random_god = false; // 26.패시브 작은 확률로 무적 유무
    public bool auto_restore = false; // 27.패시브 자동 체력 재생 유무
    public bool passive_active = false; // 패시브 액티브 사용횟수 + 1
    public bool passive_buwhal = false; // 패시브 부활 유무

    public List<int> pattern = new List<int>(); // 28.재생될 패턴의 목록.

    public short change_chance = 0; // 29.스킬 선택에서 바꿀 수 있는 기회.

    public bool no_hit = false; // 30. 스테이지간 맞았는지 안 맞았는지.
    
}
