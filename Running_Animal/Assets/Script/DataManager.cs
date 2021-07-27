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
        One = 0,
        Two,
        Three,
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
    }

    public enum Passive_Skil// 패시브 스킬 목록
    {
        None = 0,
        LUK_UP, // 행운 증가
        Active_Twice, // 액티브 두 번 사용
        DEF_UP, // 방어력 증가
        HP_UP, // 최대 체력 증가
        MOV_UP, // 이동속도 증가
        MOV_DOWN, // 이동속도 감소
        JMP_UP, // 점프력 증가
        JMP_DOWN, // 점프력 감소
        DWN_UP, // 낙하속도 증가
        DWN_DOWN, // 낙하속도 감소
        Magenet, // 자석버그
        Combo_UP, // 콤보 획득량 증가
        Resurrection, // 부활 부활 철자 엄청기네 ㅋㅋㅋ
        Coin_UP, // 코인 획득량 증가
        Auto_Jump, // 자동 점프
        Random_God, // 낮은 확률로 랜덤 무적
        Hit_God_UP, // 피격시 무적시간 증가
        Max_Jump_Plus, // 점프 횟수 증가


    }


    // 재화 관리
    public int Cash = 100; // 캐쉬 재화
    public int Gold = 10000; // 인게임 재화
    public int Money_Forest = 10; // 숲 테마 재화
    public int Money_Desert = 10; // 숲 테마 재화
    public int Money_Arctic = 10; // 숲 테마 재화
    // 테마 관리
    public Themes Theme = Themes.Forest;
    // 캐릭터 구매 관리
    public bool[] Buy_Character = { true, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public Characters Now_Character = Characters.One;

    //재능 관련
    public float Talent_HP = 0;
    public float Talent_DEF = 0;
    public int Talent_LUK = 0;
    public float Talent_Restore = 0;

    public short[] Talent_LV = { 1, 1, 1, 1 }; // 재능 레벨

    // 게임 플레이 관리
    public bool playing = false;

    public Active_Skil active = Active_Skil.Defense;
    public Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] EXP = { 20, 30, 40, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 }; //레벨
    public bool lvup;
    public int lv = 0;
    public int now_Exp = 0;
    public short stage = 0; // 스테이지 
    public float play_gold = 0; // 게임 중 얻은 골드
    public int multi_coin = 0;
    public float max_hp = 100.0f;
    public float hp = 100.0f;
    public float speed = 8.0f;
    public float jump = 10.0f;
    public float down = 20.0f;
    public float defense = 0.0f;
    public float damage = 20.0f;
    public int combo = 0;
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


}
