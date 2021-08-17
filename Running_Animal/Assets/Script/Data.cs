using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // ��ȭ ����
    public int Cash = 100; // ĳ�� ��ȭ
    public int Gold = 1000000; // �ΰ��� ��ȭ
    public int Money_Forest = 1000; // �� �׸� ��ȭ
    public int Money_Desert = 10; // �� �׸� ��ȭ
    public int Money_Arctic = 10; // �� �׸� ��ȭ
    // �׸� ����
    public DataManager.Themes Theme = DataManager.Themes.Forest;
    // ĳ���� ���� ����
    public bool[] Buy_Character = { false, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public DataManager.Characters Now_Character = DataManager.Characters.Rabbit;

    // ĳ���� ����
    /*
     * { LV , STAT_POINT, MAX_HP, SPEED, JUMP, DOWN, JUMP_CNT, DEF, LUK, ACTIVE }
     */

    public Character[] Character_STAT =
    {
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 50, 1, DataManager.Active_Skil.None)
    };

    //��� ����
    public float Talent_HP = 0;
    public float Talent_DEF = 0;
    public int Talent_LUK = 0;
    public float Talent_Restore = 1.0f;

    public short[] Talent_LV = { 0, 0, 0, 0 }; // ��� ����

    // ���� �� ������ ����
    public Pre_Item Pre_HP = new Pre_Item(false, 0);
    public Pre_Item Pre_Shield = new Pre_Item(false, 0);
    public Pre_Item Pre_100 = new Pre_Item(false, 0);
    public Pre_Item Pre_300 = new Pre_Item(false, 0);
    public DataManager.Active_Skil Pre_Active = DataManager.Active_Skil.None;
    public DataManager.Random_Item Pre_Random = DataManager.Random_Item.None;

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
    public int Diff_LV = 1; // ���̵� ����

    // ���� �÷��� ����
    public bool playing = false; // �÷��� ���̴� ������ �־����� Ȯ��.

    public DataManager.Active_Skil active = DataManager.Active_Skil.None; // ���� ���� ��Ƽ�� �ɷ�
    public DataManager.Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ���� ���� �нú� �ɷ�
    public float[] EXP = { 0, 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999 }; //������ �� �ʿ� ����ġ
    public bool lvup; // 1.������ ����, true�� �� ���� ��ֹ��� �������ϴ� ��ҷ�.
    public int lv = 1; // 2.���� ���� �ִ� 0~12������
    public float now_Exp = 0; // 3.���� ����ġ 
    public float multi_exp = 1;
    public short stage = 0; // 4.����� �������� 
    public float play_gold = 0; // 5.���� �� ���� ���
    public float multi_coin = 1; // 6.���� ȹ�淮 ������
    public float max_hp = 100.0f; // 7.�ִ� ü��
    public float hp = 100.0f; // 8.���� ü��
    public float speed = 9.0f; // 9.���� �ӵ�
    public float jump = 10.0f; // 10.���� ������
    public float down = 20.0f; // 11.���� �ϰ� �ӵ�
    public float defense = 1.0f; // 12.���� ����
    public float damage = 20.0f; // 13.���� �ǰ� ������
    public int combo = 0; // 14.���� ���� �� �޺� 
    public int max_combo = 0; // 15.���� ���� �� �ִ� �޺�
    public int multi_combo = 1; // 16.�޺� ����
    public int max_jump = 2; // 17.�ִ� ���� ���� Ƚ��
    public float luck = 0; // 18.��� (ȸ�ǿ�, �޺� ũ��Ƽ�ÿ� �����Ѵ�)
    public int max_active = 1; // 19.��Ƽ�� ��ų �ִ� ��밡�� Ƚ��
    public int use_active = 0; // 20.��Ƽ�� ��ų ���� ��� Ƚ��
    public int dodge_time = 12; // 21.�ǰݽ� ���� �ð� ����. default 12
    public float restore_eff = 1.0f; // 22.ȸ�� ȿ����

    public bool magnet = false; // 23.�нú� �ڼ����� ����
    public short buwhal = 0; // 24.��Ȱ ���� Ƚ��
    public bool auto_jump = false; //25.�нú� �������� ����
    public bool random_god = false; // 26.�нú� ���� Ȯ���� ���� ����
    public bool auto_restore = false; // 27.�нú� �ڵ� ü�� ��� ����
    public bool passive_active = false; // �нú� ��Ƽ�� ���Ƚ�� + 1
    public bool passive_buwhal = false; // �нú� ��Ȱ ����

    public List<int> pattern = new List<int>(); // 28.����� ������ ���.

    public short change_chance = 0; // 29.��ų ���ÿ��� �ٲ� �� �ִ� ��ȸ.

    public bool no_hit = false; // 30. ���������� �¾Ҵ��� �� �¾Ҵ���.
    
}
