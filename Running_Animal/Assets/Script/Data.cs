using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // ��ȭ ����
    public int Cash = 100; // ĳ�� ��ȭ
    public int Gold = 10000; // �ΰ��� ��ȭ
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
    public int[] EXP = { 20000000, 30000000, 40000000, 50000000, 60000000, 70000000, 80000000, 90000000, 100000000, 110000000, 120000000, 130000000, 140000000, 999999990 }; //������ �� �ʿ� ����ġ
    public bool lvup; // ������ ����, true�� �� ���� ��ֹ��� �������ϴ� ��ҷ�.
    public int lv = 0; // ���� ���� �ִ� 0~12������
    public int now_Exp = 0; // ���� ����ġ 
    public int play_gold = 0; // ���� �� ���� ���
    public float max_hp = 100.0f; // �ִ� ü��
    public float hp = 100.0f; // ���� ü��
    public float speed = 8.0f; // ���� �ӵ�
    public float jump = 10.0f; // ���� ������
    public float down = 20.0f; // ���� �ϰ� �ӵ�
    public float damage = 20.0f; // ���� �ǰ� ������ 
}
