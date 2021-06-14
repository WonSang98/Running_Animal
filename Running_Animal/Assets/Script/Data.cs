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
    public bool[] Buy_Character = { false, false, false };
    public DataManager.Characters Now_Character = DataManager.Characters.One;
}
