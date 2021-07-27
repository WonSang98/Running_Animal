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
    public DataManager.Active_Skil active = DataManager.Active_Skil.Defense; // ���� ���� ��Ƽ�� �ɷ�
    public DataManager.Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ���� ���� �нú� �ɷ�
    public int[] EXP = { 20, 30, 40, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 }; //������ �� �ʿ� ����ġ
    public bool lvup; // ������ ����, true�� �� ���� ��ֹ��� �������ϴ� ��ҷ�.
    public int lv = 0; // ���� ���� �ִ� 0~12������
    public int now_Exp = 0; // ���� ����ġ 
    public short stage = 0; // ��������
    public float play_gold = 0; // ���� �� ���� ���
    public int multi_coin = 0; // ���� ȹ�淮 ������
    public float max_hp = 100.0f; // �ִ� ü��
    public float hp = 100.0f; // ���� ü��
    public float speed = 8.0f; // ���� �ӵ�
    public float jump = 10.0f; // ���� ������
    public float down = 20.0f; // ���� �ϰ� �ӵ�
    public float defense = 0.0f; // ���� ����
    public float damage = 20.0f; // ���� �ǰ� ������
    public int combo = 0; // ���� ���� �� �޺� 
    public int multi_combo = 1; // �޺� ����
    public int max_jump = 2; // �ִ� ���� ���� Ƚ��
    public int luck = 0; // ��� (ȸ�ǿ�, �޺� ũ��Ƽ�ÿ� �����Ѵ�)
    public int max_active = 1; // ��Ƽ�� ��ų �ִ� ��밡�� Ƚ��
    public int use_active = 0; // ��Ƽ�� ��ų ���� ��� Ƚ��
    public int dodge_time = 12; // �ǰݽ� ���� �ð� ����. default 12

    public bool magnet = false; // �нú� �ڼ����� ����
    public short buwhal = 0; // �нú� ��Ȱ ����
    public bool auto_jump = false; //�нú� �������� ����
    public bool random_god = false; // �нú� ���� Ȯ���� ���� ����

}
