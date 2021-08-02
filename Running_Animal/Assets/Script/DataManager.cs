using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataManager 
{
    public enum Themes // 게임 플레이 테마
    {
        Forest = 0,
        Desert,
        Arctic
    }

    public enum Characters // 캐릭터, enum값을 통해 prefeb instance화 할 예정.
    {
        Pig = 0,
        Cat,
        Monky,
        Rabbit
    }

    public enum Active_Skil // 액티브 스킬 목록
    {
        None = 0,
        Defense , // 피격 1회 무시, Player Tag를 Sheild로 변경, 1회 피격시 일정시간 이후 원래 태그 변경
        Flash, // 일정 거리 앞으로 점멸 백그라운드 포함 기믹까지 플레이어 앞으로 땡겨오면 됨. (좌표수정)
        Ghost, // 일정 시간 유체화 
        Heal, // 체력 회복
        Item_Change, // 아이템 체인지
        Change_Coin, // 장애물 코인화
        The_World, // 느-려-져
        Multiple_Combo, // 콤보3배
        Fly, // 나는 날 수 잇서요
        Run, // 질주 우당탕 다 부수기~
    }

    public enum Passive_Skil// 패시브 스킬 목록
    {
        None = 0,
        LUK_UP, // 행운 증가
        Active_Twice, // 액티브 두 번 사용 2
        DEF_UP, // 방어력 증가
        HP_UP, // 최대 체력 증가
        MOV_UP, // 이동속도 증가
        MOV_DOWN, // 이동속도 감소
        JMP_UP, // 점프력 증가
        JMP_DOWN, // 점프력 감소
        DWN_UP, // 낙하속도 증가
        DWN_DOWN, // 낙하속도 감소
        Magenet, // 자석버그 11
        Combo_UP, // 콤보 획득량 증가
        Resurrection, // 부활 부활 철자 엄청기네 ㅋㅋㅋ  13
        Coin_UP, // 코인 획득량 증가
        Auto_Jump, // 자동 점프 15
        Random_God, // 낮은 확률로 랜덤 무적 16
        Hit_God_UP, // 피격시 무적시간 증가
        Max_Jump_Plus, // 점프 횟수 증가
        Auto_Restore, // 자동 체력 재생 19
        Heal_Eff// 회복 효율 증가
    }

    // 재화 관리
    public int Cash = 100; // 캐쉬 재화
    public int Gold = 100000000; // 인게임 재화
    public int Money_Forest = 10; // 숲 테마 재화
    public int Money_Desert = 10; // 숲 테마 재화
    public int Money_Arctic = 10; // 숲 테마 재화
    // 테마 관리
    public Themes Theme = Themes.Forest;
    // 캐릭터 구매 관리
    public bool[] Buy_Character = { true, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public Characters Now_Character = Characters.Rabbit;

    // 캐릭터 스탯
    /*
     * { LV , STAT_POINT, MAX_HP, SPEED, JUMP, DOWN, JUMP_CNT, DEF, LUK, ACTIVE }
     */
    public Character[] Character_STAT =
    {
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None)
    };


    //재능 관련
    public float Talent_HP = 0;
    public float Talent_DEF = 0;
    public int Talent_LUK = 0;
    public float Talent_Restore = 1.0f;

    public short[] Talent_LV = { 1, 1, 1, 1 }; // 재능 레벨

    // 시작 전 아이템 구매
    public bool Pre_Shield = false;
    public bool Pre_100 = false;
    public bool Pre_300 = false;

    // 시작 전 아이템 관련
    public int Exp_run = 0; // 100미터 300미터 질주 시, 질주 가 끝난 후 파괴 된 장애물 경험치 한 번에 적용.

    // 게임 플레이 관리
    public bool playing = false;

    public Active_Skil active = Active_Skil.None;
    public Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] EXP = { 0, 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999999 }; //레벨
    public bool lvup;
    public int lv = 1;
    public int now_Exp = 0;
    public short stage = 0; // 통과한 패턴
    public float play_gold = 0; // 게임 중 얻은 골드
    public int multi_coin = 0;
    public float max_hp = 100.0f;
    public float hp = 100.0f;
    public float speed = 9.0f;
    public float jump = 10.0f;
    public float down = 20.0f;
    public float defense = 0.0f;
    public float damage = 20.0f;
    public int combo = 0;
    public int max_combo = 0; // 게임 진행 중 최대 콤보
    public int multi_combo = 1;
    public int max_jump = 2;
    public int luck = 0;
    public int max_active = 1;
    public int use_active = 0;
    public int dodge_time = 12;
    public float restore_eff = 1.0f; // 회복 효율성

    public bool magnet = false;
    public short buwhal = 0;
    public bool auto_jump = false;
    public bool random_god = false;
    public bool auto_restore = false;
    public bool passive_active = false; // 패시브 액티브 사용횟수 + 1
    public bool passive_buwhal = false; // 패시브 부활 유무

    public List<int> pattern = new List<int>();

    public short change_chance = 0; // 스킬 선택에서 바꿀 수 있는 기회.
}
