using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // ��ȭ ����
    public int Cash = 100; // ĳ�� ��ȭ
    public int Gold = 100; // �ΰ��� ��ȭ
    public int Money_Forest = 10; // �� �׸� ��ȭ
    public int Money_Desert = 10; // �� �׸� ��ȭ
    public int Money_Arctic = 10; // �� �׸� ��ȭ
    // �׸� ����
    public DataManager.Themes Theme = DataManager.Themes.Forest;
    // ĳ���� ���� ����
    public bool[] Buy_Character = { false, false, false };
    public DataManager.Characters Now_Character = DataManager.Characters.One;
}
