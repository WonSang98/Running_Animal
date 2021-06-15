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
        Three
    }

    // 재화 관리
    public int Cash = 100; // 캐쉬 재화
    public int Gold = 100; // 인게임 재화
    public int Money_Forest = 10; // 숲 테마 재화
    public int Money_Desert = 10; // 숲 테마 재화
    public int Money_Arctic = 10; // 숲 테마 재화
    // 테마 관리
    public Themes Theme = Themes.Forest;
    // 캐릭터 구매 관리
    public bool[] Buy_Character = { true, false, false };
    public Characters Now_Character = Characters.One;
}
