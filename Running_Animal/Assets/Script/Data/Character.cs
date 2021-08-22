using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character
{
    public enum CHARACTER_CODE
    {
        Pig = 0,
        Cat,
        Monky,
        Rabbit
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
            new Status<float>(0, 100),
            new Status<float>(0, 100),
            new Status<float>(0, 8),
            new Status<int>(0, 2),
            new Status<float>(0, 10),
            new Status<float>(0, 20),
            new Status<float>(0, 0),
            new Status<short>(0, 5),
            new Status<float>(0, 1))
            , Active.ACTIVE_CODE.None),
        new Character(0, 0,new Ability(
            new Status<float>(0, 100),
            new Status<float>(0, 100),
            new Status<float>(0, 8),
            new Status<int>(0, 2),
            new Status<float>(0, 10),
            new Status<float>(0, 20),
            new Status<float>(0, 0),
            new Status<short>(0, 5),
            new Status<float>(0, 1))
            , Active.ACTIVE_CODE.None),
        new Character(0, 0,new Ability(
            new Status<float>(0, 100),
            new Status<float>(0, 100),
            new Status<float>(0, 8),
            new Status<int>(0, 2),
            new Status<float>(0, 10),
            new Status<float>(0, 20),
            new Status<float>(0, 0),
            new Status<short>(0, 5),
            new Status<float>(0, 1))
            , Active.ACTIVE_CODE.None),
        new Character(0, 0,new Ability(
            new Status<float>(0, 100),
            new Status<float>(0, 100),
            new Status<float>(0, 8),
            new Status<int>(0, 2),
            new Status<float>(0, 10),
            new Status<float>(0, 20),
            new Status<float>(0, 0),
            new Status<short>(0, 5),
            new Status<float>(0, 1))
            , Active.ACTIVE_CODE.None)
    };

    public static int[] COST = { 500, 500, 500, 500 }; // 캐릭터 구매 비용


}
