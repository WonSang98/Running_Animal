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
    public bool[] Buy_Character = { false, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public DataManager.Characters Now_Character = DataManager.Characters.One;

    // ���� �÷��� ����
    public DataManager.Active_Skil active = DataManager.Active_Skil.Defense;
    public float speed = 8.0f;
}
