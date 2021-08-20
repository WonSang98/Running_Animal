using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 재화 관리
    public Money Money = new Money(999999, 9999999, new int[]{ 9999999 , 9999999 , 9999999 });
    
    //구매 관리
    public Purchase Purchase = new Purchase(new bool[] { true, false, false, false },
                                     new bool[] { true, false, false });
    // 캐릭터 스탯 (캐릭터 강화 데이터 저장)
    public Character[] Character_STAT =
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
            new Status<float>(0, 0))
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
            new Status<float>(0, 0))
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
            new Status<float>(0, 0))
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
            new Status<float>(0, 0))
            , Active.ACTIVE_CODE.None)
    };

    //재능 관련
    public Talent Talent = new Talent(new Status<float>(0, 0), new Status<float>(0, 0), new Status<short>(0, 0), new Status<float>(0, 0));

    // 시작 전 구매 아이템 데이터
    public PreItem PreItem = new PreItem(new ItemSet(false, 0),
                                        new ItemSet(false, 0),
                                        new ItemSet(false, 0),
                                        new ItemSet(false, 0),
                                        Active.ACTIVE_CODE.None,
                                        PreItem.Random_Item.None);

    // 프리셋 유저기반 데이터
    public Preset Preset = new Preset(Character.CHARACTER_CODE.Rabbit, Theme.THEME_CODE.Forest, 0);

}
