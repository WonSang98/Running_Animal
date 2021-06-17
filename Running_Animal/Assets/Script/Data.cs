using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // 재화 관리
    public int Cash = 100; // 캐쉬 재화
    public int Gold = 100; // 인게임 재화
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
    public float speed = 8.0f;
}
