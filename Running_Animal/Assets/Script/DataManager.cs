using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    public enum Themes
    {
        Forest = 0,
        Desert,
        Arctic
    }

    public enum Characters
    {
        One = 0,
        Two,
        Three
    }

    // 재화 관리
    public int Cash = 0; // 캐쉬 재화
    public int Gold = 0; // 인게임 재화
    public int Money_Forest = 0; // 숲 테마 재화
    public int Desert_Forest = 0; // 숲 테마 재화
    public int Arctic_Forest = 0; // 숲 테마 재화
    // 테마 관리
    public Themes Theme = Themes.Forest;
    // 캐릭터 구매 관리
    public Dictionary<Characters, bool> Buy_Character = new Dictionary<Characters, bool>()
    {
        {Characters.One, true},
        {Characters.Two, false},
        {Characters.Three, false}
    };

    public Characters Now_Character = Characters.One;

}
