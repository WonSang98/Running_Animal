using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character
{
    public enum CHARACTER_CODE
    {
        Rabbit = 0,
        Cat,
        Monky,
        Pig
    }

    public short LV;
    public short STAT_POINT;
    public Ability ability;
    public Active.ACTIVE_CODE ACTIVE;

    public Character(short _LV, short _STAT_POINT, Ability _ability, Active.ACTIVE_CODE _ACTIVE)
    {
        LV = _LV;
        STAT_POINT = _STAT_POINT;
        ability = _ability;
        ACTIVE = _ACTIVE;
    }

    Character() { }

    public Character(Character c)
    {
        LV = c.LV;
        STAT_POINT = c.STAT_POINT;
        ability = new Ability(c.ability);
        ACTIVE = c.ACTIVE;
    }

    // 캐릭터 기본 능력치
    public static Character[] Natural =
    {
        new Character(0, 0,new Ability(
            new Status<float>(2, 0, 100),
            new Status<float>(2, 0, 100),
            new Status<float>(2, 0, 8),
            new Status<int>(2, 0, 2),
            new Status<float>(2, 0, 10),
            new Status<float>(2, 0, 20),
            new Status<float>(2, 0, 0),
            new Status<short>(2, 0, 5),
            new Status<float>(2, 0, 1))
            , Active.ACTIVE_CODE.None),
        new Character(0, 0,new Ability( //고양이
            new Status<float>(0, 0, 70),
            new Status<float>(0, 0, 70),
            new Status<float>(2, 0, 8),
            new Status<int>(2, 0, 2),
            new Status<float>(2, 0, 10),
            new Status<float>(2, 0, 20),
            new Status<float>(2, 0, 0),
            new Status<short>(4, 0, 5),
            new Status<float>(2, 0, 1))
            , Active.ACTIVE_CODE.None),
        new Character(0, 0,new Ability( //원숭이
            new Status<float>(1, 0, 90),
            new Status<float>(1, 0, 90),
            new Status<float>(2, 0, 8),
            new Status<int>(3, 0, 3),
            new Status<float>(1, 0, 9.5f),
            new Status<float>(2, 0, 20),
            new Status<float>(1, 0, -0.01f),
            new Status<short>(1, 0, 4),
            new Status<float>(1, 0, 0.95f))
            , Active.ACTIVE_CODE.None),
        new Character(0, 0,new Ability( //돼지
            new Status<float>(4, 0, 130),
            new Status<float>(2, 0, 100),
            new Status<float>(2, 0, 8),
            new Status<int>(2, 0, 2),
            new Status<float>(2, 0, 10),
            new Status<float>(2, 0, 20),
            new Status<float>(2, 0, 0),
            new Status<short>(0, 0, 2),
            new Status<float>(2, 0, 1))
            , Active.ACTIVE_CODE.None),
    };

    public static int[] COST = { 7500, 7500, 7500, 7500 }; // 캐릭터 구매 비용


}
