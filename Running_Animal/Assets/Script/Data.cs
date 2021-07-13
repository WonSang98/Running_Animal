using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // 재화 관리
    public int Cash = 100; // 캐쉬 재화
    public int Gold = 10000; // 인게임 재화
    public int Money_Forest = 10; // 숲 테마 재화
    public int Money_Desert = 10; // 숲 테마 재화
    public int Money_Arctic = 10; // 숲 테마 재화
    // 테마 관리
    public DataManager.Themes Theme = DataManager.Themes.Forest;
    // 캐릭터 구매 관리
    public bool[] Buy_Character = { false, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public DataManager.Characters Now_Character = DataManager.Characters.One;

    // 게임 플레이 관리
    public DataManager.Active_Skil active = DataManager.Active_Skil.Defense;
    public int[] EXP = { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 99999999 }; //레벨업 시 필요 경험치
    public bool lvup; // 레벨업 여부, true일 시 다음 장애물은 레벨업하는 장소로.
    public int lv = 0; // 현재 레벨 최대 0~12렙까지
    public int now_Exp = 0; // 현재 경험치 
    public int play_gold = 0; // 게임 중 얻은 골드
    public float max_hp = 100.0f; // 최대 체력
    public float hp = 100.0f; // 현재 체력
    public float speed = 8.0f; // 현재 속도
    public float jump = 10.0f; // 현재 점프력
    public float down = 20.0f; // 현재 하강 속도
    public float damage = 20.0f; // 현재 피격 데미지 
}
