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

    // ��ȭ ����
    public int Cash = 0; // ĳ�� ��ȭ
    public int Gold = 0; // �ΰ��� ��ȭ
    public int Money_Forest = 0; // �� �׸� ��ȭ
    public int Desert_Forest = 0; // �� �׸� ��ȭ
    public int Arctic_Forest = 0; // �� �׸� ��ȭ
    // �׸� ����
    public Themes Theme = Themes.Forest;
    // ĳ���� ���� ����
    public Dictionary<Characters, bool> Buy_Character = new Dictionary<Characters, bool>()
    {
        {Characters.One, true},
        {Characters.Two, false},
        {Characters.Three, false}
    };

    public Characters Now_Character = Characters.One;

}
