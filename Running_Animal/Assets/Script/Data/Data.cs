using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class Data
{
    // ��ȭ ����
    public Money Money = new Money(0, 0, new int[] { 0, 0, 0 });

    //���� ����
    public Purchase Purchase = new Purchase(new bool[] { true, false, false, false },
                                     new bool[] { true, false, false });
    // ĳ���� ���� (ĳ���� ��ȭ ������ ����)
    public Character[] Character_STAT =
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
        new Character(0, 0,new Ability( //�����
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
        new Character(0, 0,new Ability( //������
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
        new Character(0, 0,new Ability( //����
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

    //��� ����
    public Talent Talent = new Talent(new Status<float>(0, 0, 0), new Status<float>(0, 0, 0), new Status<short>(0, 0, 0), new Status<float>(0, 0, 0));

    // ���� �� ���� ������ ������
    public PreItem PreItem = new PreItem(new ItemSet(false, 0),
                                        new ItemSet(false, 0),
                                        new ItemSet(false, 0),
                                        new ItemSet(false, 0),
                                        Active.ACTIVE_CODE.None,
                                        PreItem.Random_Item.None);

    // ������ ������� ������
    public Preset Preset = new Preset(Character.CHARACTER_CODE.Rabbit, Theme.THEME_CODE.Forest, 0);

    public TutoData TutoData = new TutoData(false, false, false, false, false);

    // ��Ͽ� ������
    public List<Record> Recent_Data;
    public List<Record> Best_Data;

}
