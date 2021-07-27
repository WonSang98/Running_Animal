using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataManager 
{
    public enum Themes // ���� �÷��� �׸�
    {
        Forest = 0,
        Desert,
        Arctic
    }

    public enum Characters // ĳ����, enum���� ���� prefeb instanceȭ �� ����.
    {
        One = 0,
        Two,
        Three,
        Rabbit
    }

    public enum Active_Skil // ��Ƽ�� ��ų ���
    {
        None = 0,
        Defense , // �ǰ� 1ȸ ����, Player Tag�� Sheild�� ����, 1ȸ �ǰݽ� �����ð� ���� ���� �±� ����
        Flash, // ���� �Ÿ� ������ ���� ��׶��� ���� ��ͱ��� �÷��̾� ������ ���ܿ��� ��. (��ǥ����)
        Ghost, // ���� �ð� ��üȭ 
        Heal, // ü�� ȸ��
        Item_Change, // ������ ü����
        Change_Coin, // ��ֹ� ����ȭ
        The_World, // ��-��-��
        Multiple_Combo, // �޺�3��
        Fly, // ���� �� �� �ռ���
    }

    public enum Passive_Skil// �нú� ��ų ���
    {
        None = 0,
        LUK_UP, // ��� ����
        Active_Twice, // ��Ƽ�� �� �� ���
        DEF_UP, // ���� ����
        HP_UP, // �ִ� ü�� ����
        MOV_UP, // �̵��ӵ� ����
        MOV_DOWN, // �̵��ӵ� ����
        JMP_UP, // ������ ����
        JMP_DOWN, // ������ ����
        DWN_UP, // ���ϼӵ� ����
        DWN_DOWN, // ���ϼӵ� ����
        Magenet, // �ڼ�����
        Combo_UP, // �޺� ȹ�淮 ����
        Resurrection, // ��Ȱ ��Ȱ ö�� ��û��� ������
        Coin_UP, // ���� ȹ�淮 ����
        Auto_Jump, // �ڵ� ����
        Random_God, // ���� Ȯ���� ���� ����
        Hit_God_UP, // �ǰݽ� �����ð� ����
        Max_Jump_Plus, // ���� Ƚ�� ����


    }


    // ��ȭ ����
    public int Cash = 100; // ĳ�� ��ȭ
    public int Gold = 10000; // �ΰ��� ��ȭ
    public int Money_Forest = 10; // �� �׸� ��ȭ
    public int Money_Desert = 10; // �� �׸� ��ȭ
    public int Money_Arctic = 10; // �� �׸� ��ȭ
    // �׸� ����
    public Themes Theme = Themes.Forest;
    // ĳ���� ���� ����
    public bool[] Buy_Character = { true, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public Characters Now_Character = Characters.One;

    //��� ����
    public float Talent_HP = 0;
    public float Talent_DEF = 0;
    public int Talent_LUK = 0;
    public float Talent_Restore = 0;

    public short[] Talent_LV = { 1, 1, 1, 1 }; // ��� ����

    // ���� �÷��� ����
    public bool playing = false;

    public Active_Skil active = Active_Skil.Defense;
    public Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] EXP = { 20, 30, 40, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 }; //����
    public bool lvup;
    public int lv = 0;
    public int now_Exp = 0;
    public short stage = 0; // �������� 
    public float play_gold = 0; // ���� �� ���� ���
    public int multi_coin = 0;
    public float max_hp = 100.0f;
    public float hp = 100.0f;
    public float speed = 8.0f;
    public float jump = 10.0f;
    public float down = 20.0f;
    public float defense = 0.0f;
    public float damage = 20.0f;
    public int combo = 0;
    public int multi_combo = 1;
    public int max_jump = 2;
    public int luck = 0;
    public int max_active = 1;
    public int use_active = 0;
    public int dodge_time = 12;
    public float restore_eff = 1.0f; // ȸ�� ȿ����

    public bool magnet = false;
    public short buwhal = 0;
    public bool auto_jump = false;
    public bool random_god = false;


}
