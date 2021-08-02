using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // ��ȭ ����
    public int Cash = 100; // ĳ�� ��ȭ
    public int Gold = 100000000; // �ΰ��� ��ȭ
    public int Money_Forest = 10; // �� �׸� ��ȭ
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
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 0, 1, DataManager.Active_Skil.None)
    };

    //��� ����
    public float Talent_HP = 0;
    public float Talent_DEF = 0;
    public int Talent_LUK = 0;
    public float Talent_Restore = 1.0f;

    public short[] Talent_LV = { 0, 0, 0, 0 }; // ��� ����

    // ���� �� ������ ����
    public bool Pre_Shield = false;
    public bool Pre_100 = false;
    public bool Pre_300 = false;

    // ���� �� ������ ����
    public int Exp_run = 0; // 100���� 300���� ���� ��, ���� �� ���� �� �ı� �� ��ֹ� ����ġ �� ���� ����.


    // ���� �÷��� ����
    public bool playing = false; // �÷��� ���̴� ������ �־����� Ȯ��.

    public DataManager.Active_Skil active = DataManager.Active_Skil.None; // ���� ���� ��Ƽ�� �ɷ�
    public DataManager.Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ���� ���� �нú� �ɷ�
    public int[] EXP = { 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999999 }; //������ �� �ʿ� ����ġ
    public bool lvup; // 1.������ ����, true�� �� ���� ��ֹ��� �������ϴ� ��ҷ�.
    public int lv = 0; // 2.���� ���� �ִ� 0~12������
    public int now_Exp = 0; // 3.���� ����ġ 
    public short stage = 0; // 4.����� �������� 
    public float play_gold = 0; // 5.���� �� ���� ���
    public int multi_coin = 0; // 6.���� ȹ�淮 ������
    public float max_hp = 100.0f; // 7.�ִ� ü��
    public float hp = 100.0f; // 8.���� ü��
    public float speed = 9.0f; // 9.���� �ӵ�
    public float jump = 10.0f; // 10.���� ������
    public float down = 20.0f; // 11.���� �ϰ� �ӵ�
    public float defense = 0.0f; // 12.���� ����
    public float damage = 20.0f; // 13.���� �ǰ� ������
    public int combo = 0; // 14.���� ���� �� �޺� 
    public int max_combo = 0; // 15.���� ���� �� �ִ� �޺�
    public int multi_combo = 1; // 16.�޺� ����
    public int max_jump = 2; // 17.�ִ� ���� ���� Ƚ��
    public int luck = 0; // 18.��� (ȸ�ǿ�, �޺� ũ��Ƽ�ÿ� �����Ѵ�)
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

}
