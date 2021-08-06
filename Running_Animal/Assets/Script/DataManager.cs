using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pre_Item
{
    public bool USE; // �÷��� �� �� ���� �� ������
    public int CNT; // ���� �ִ� ����.

    public Pre_Item(bool _USE, int _CNT)
    {
        USE = _USE;
        CNT = _CNT;
    }
}

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
        Pig = 0,
        Cat,
        Monky,
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
        Active_Twice, // ��Ƽ�� �� �� ��� 2
        DEF_UP, // ���� ����
        HP_UP, // �ִ� ü�� ����
        MOV_UP, // �̵��ӵ� ����
        MOV_DOWN, // �̵��ӵ� ����
        JMP_UP, // ������ ����
        JMP_DOWN, // ������ ����
        DWN_UP, // ���ϼӵ� ����
        DWN_DOWN, // ���ϼӵ� ����
        Magenet, // �ڼ����� 11
        Combo_UP, // �޺� ȹ�淮 ����
        Resurrection, // ��Ȱ ��Ȱ ö�� ��û��� ������  13
        Coin_UP, // ���� ȹ�淮 ����
        Auto_Jump, // �ڵ� ���� 15
        Random_God, // ���� Ȯ���� ���� ���� 16
        Hit_God_UP, // �ǰݽ� �����ð� ����
        Max_Jump_Plus, // ���� Ƚ�� ����
        Auto_Restore, // �ڵ� ü�� ��� 19
        Heal_Eff// ȸ�� ȿ�� ����
    }

    public enum Random_Item // ���� �� ���� ���� ������ ���
    {
        None = 0,
        HP15, // ü�� 15% ����
        HP30, // ü�� 30% ����
        LUK5, // ��� 5 ����
        LUK10, // ��� 10 ����
        SPEED15, // �ӵ� 15%
        SPEED30, // �ӵ� 30%
        JUMP20, // ���� 20%
        JUMP40, // ���� 40%
        GOLD25, // ��� ȹ�淮 25% ����
        GOLD50, // ��� ȹ�淮 50% ����
        COMBO2, // �޺� ȹ�淮 2��
        COMBO3, // �޺� ȹ�淮 3��
        JUMP_PLUS, // ���� Ƚ�� 1ȸ �߰�
        DEF10, // ���� 10% �氨
        DEF15, // ���� 15% �氨
        EXP2, // ����ġ 2�� (�������� ���� �ӵ� UP)
    }

    // ��ȭ ����
    public int Cash = 100; // ĳ�� ��ȭ
    public int Gold = 1000000; // �ΰ��� ��ȭ
    public int Money_Forest = 1000; // �� �׸� ��ȭ
    public int Money_Desert = 10; // �� �׸� ��ȭ
    public int Money_Arctic = 10; // �� �׸� ��ȭ
    // �׸� ����
    public Themes Theme = Themes.Forest;
    // ĳ���� ���� ����
    public bool[] Buy_Character = { true, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public Characters Now_Character = Characters.Rabbit;

    // ĳ���� ����
    /*
     * { LV , STAT_POINT, MAX_HP, SPEED, JUMP, DOWN, JUMP_CNT, DEF, LUK, ACTIVE }
     */
    public Character[] Character_STAT =
    {
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None)
    };


    //��� ����
    public float Talent_HP = 0;
    public float Talent_DEF = 0;
    public int Talent_LUK = 0;
    public float Talent_Restore = 1.0f;

    public short[] Talent_LV = { 1, 1, 1, 1 }; // ��� ����

    // ���� �� ������ ����
    public Pre_Item Pre_HP = new Pre_Item(false, 0);
    public Pre_Item Pre_Shield = new Pre_Item(false, 0);
    public Pre_Item Pre_100 = new Pre_Item(false, 0);
    public Pre_Item Pre_300 = new Pre_Item(false, 0);
    public Active_Skil Pre_Active = Active_Skil.None;
    public Random_Item Pre_Random = Random_Item.None;

    // ���� �� ������ ����
    public float Exp_run = 0; // 100���� 300���� ���� ��, ���� �� ���� �� �ı� �� ��ֹ� ����ġ �� ���� ����.

    // ���̵� ����
    /*
     * �ǰ� ������ ���
     * ���� ȹ�淮 ����
     * ȸ�� ȿ�� ����
     * ��� ����
     * ���� ����
     * �ӵ� ����
     * ���� �������� �ʿ� ����ġ�� ����
     */
    public Difficulty[] Forest_Diff =
    {
        new Difficulty(20,  0.00f, 0.00f , 0 , 0.00f , 0.00f  , 0), // LEVEL 1
        new Difficulty(25,  0.05f, 0.01f , 0 , 0.01f , 0.25f  , 0), // LEVEL 2
        new Difficulty(30,  0.10f, 0.02f , 0 , 0.02f , 0.50f  , 0), // LEVEL 3
        new Difficulty(40,  0.20f, 0.04f , 5 , 0.04f , 0.33f  , 10), // LEVEL 4
        new Difficulty(50,  0.30f, 0.06f , 6 , 0.06f , 0.66f  , 10), // LEVEL 5
        new Difficulty(60,  0.40f, 0.08f , 7 , 0.08f , 0.99f  , 10), // LEVEL 6
        new Difficulty(75,  0.60f, 0.12f , 10, 0.12f , 1.5f  , 15), // LEVEL 7
        new Difficulty(90,  0.80f, 0.16f , 12, 0.16f , 2.00f , 15), // LEVEL 8
        new Difficulty(105, 1.00f, 0.20f , 14, 0.20f , 2.50f , 20), // LEVEL 9
        new Difficulty(120, 1.20f, 0.25f , 15, 0.25f , 3.00f , 20), // LEVEL 10
    };
    public int Diff_LV = 0; // ���̵� ����

    // ���� �÷��� ����
    public bool playing = false;

    public Active_Skil active = Active_Skil.None;
    public Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public float[] EXP = { 0, 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999 }; //����
    public bool lvup;
    public int lv = 1;
    public float now_Exp = 0;
    public float multi_exp = 1;
    public short stage = 0; // ����� ����
    public float play_gold = 0; // ���� �� ���� ���
    public float multi_coin = 1;
    public float max_hp = 100.0f;
    public float hp = 100.0f;
    public float speed = 9.0f;
    public float jump = 10.0f;
    public float down = 20.0f;
    public float defense = 1.0f;
    public float damage = 1.00f;
    public int combo = 0;
    public int max_combo = 0; // ���� ���� �� �ִ� �޺�
    public int multi_combo = 1;
    public int max_jump = 2;
    public float luck = 0;
    public int max_active = 1;
    public int use_active = 0;
    public int dodge_time = 12;
    public float restore_eff = 1.0f; // ȸ�� ȿ����

    public bool magnet = false;
    public short buwhal = 0;
    public bool auto_jump = false;
    public bool random_god = false;
    public bool auto_restore = false;
    public bool passive_active = false; // �нú� ��Ƽ�� ���Ƚ�� + 1
    public bool passive_buwhal = false; // �нú� ��Ȱ ����

    public List<int> pattern = new List<int>();

    public short change_chance = 0; // ��ų ���ÿ��� �ٲ� �� �ִ� ��ȸ.
}
