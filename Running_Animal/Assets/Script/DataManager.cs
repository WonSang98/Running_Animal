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
        Defense , // 피격 1회 무시, Player Tag를 Sheild로 변경, 1회 피격시 일정시간 이후 원래 태그 변경 (Invoke 사용)
        Flash, // 일정 거리 앞으로 점멸 백그라운드 포함 기믹까지 플레이어 앞으로 땡겨오면 됨. (좌표수정)
        Ghost, // 일정 시간 유체화 (Player colider를 끄고 일정시간 이후 다시 키기 Invoke  사용)
    }

    public enum Skil_Prologue // 패시브 스킬 서장
    {
        Active_Cool_Down = 0, //액티브 아이템 시간 단축
        Jump_Up, // 점프 높이 증가
        Jump_Down, // 점프 높이 감소
        Down_Up, // 떨어지는 속도 증가
        Down_Down, // 떨어지는 속도 감소
        Speed_Up, // 속도증가
        Speed_Down, // 속도감소
        Damage_Down, // 피격 데미지 감소
        Luck_Up, // 행운 상승
        Score_Up, // 점수 획득량 추가
        Money_UP, // 골드 획득량 추가

    }
    public enum Skil_Middle // 패시브 스킬 중장
    {
        Active_Cool_Down = 0, //액티브 아이템 시간 단축
        Jump_Up, // 점프 높이 증가
        Jump_Down, // 점프 높이 감소
        Down_Up, // 떨어지는 속도 증가
        Down_Down, // 떨어지는 속도 감소
        Speed_Up, // 속도증가
        Speed_Down, // 속도감소
        Damage_Down, // 피격 데미지 감소
        Luck_Up, // 행운 상승
        Score_Up, // 점수 획득량 추가
        Money_UP, // 골드 획득량 추가
    }
    public enum Skil_Final // 패시브 스킬 종장
    {
        Active_Cool_Down = 0, //액티브 아이템 시간 단축
        Jump_Up, // 점프 높이 증가
        Jump_Down, // 점프 높이 감소
        Down_Up, // 떨어지는 속도 증가
        Down_Down, // 떨어지는 속도 감소
        Speed_Up, // 속도증가
        Speed_Down, // 속도감소
        Damage_Down, // 피격 데미지 감소
        Luck_Up, // 행운 상승
        Score_Up, // 점수 획득량 추가
        Money_UP, // 골드 획득량 추가
    }

    public enum Skil_Book // 패시브 스킬 종장
    {
        Active_Cool_Down = 0, //액티브 아이템 시간 단축
        Jump_Up, // 점프 높이 증가
        Jump_Down, // 점프 높이 감소
        Down_Up, // 떨어지는 속도 증가
        Down_Down, // 떨어지는 속도 감소
        Speed_Up, // 속도증가
        Speed_Down, // 속도감소
        Damage_Down, // 피격 데미지 감소
        Luck_Up, // 행운 상승
        Score_Up, // 점수 획득량 추가
        Money_UP, // 골드 획득량 추가
    }

    public enum Skil_Scroll //패시브 스킬 두루마리
    {
        Revive = 0, // 사망 시 1회 부활
        Auto_Jump, // 자동 점프 
        Auto_Active, // 액티브 자동 사용
        Shield_Active, // 액티브 사용 시 보호막 획득
        God_Active, // 액티브 사용 시 일정시간 무적
        Magnet // 자석
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

    // 게임 플레이 관리
    public Active_Skil active = Active_Skil.Defense;
    public float hp = 100.0f;
    public float speed = 8.0f;
    public float jump = 10.0f;
    public float down = 20.0f;


}
