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
        Defense , // �ǰ� 1ȸ ����, Player Tag�� Sheild�� ����, 1ȸ �ǰݽ� �����ð� ���� ���� �±� ���� (Invoke ���)
        Flash, // ���� �Ÿ� ������ ���� ��׶��� ���� ��ͱ��� �÷��̾� ������ ���ܿ��� ��. (��ǥ����)
        Ghost, // ���� �ð� ��üȭ (Player colider�� ���� �����ð� ���� �ٽ� Ű�� Invoke  ���)
    }

    public enum Skil_Prologue // �нú� ��ų ����
    {
        Active_Cool_Down = 0, //��Ƽ�� ������ �ð� ����
        Jump_Up, // ���� ���� ����
        Jump_Down, // ���� ���� ����
        Down_Up, // �������� �ӵ� ����
        Down_Down, // �������� �ӵ� ����
        Speed_Up, // �ӵ�����
        Speed_Down, // �ӵ�����
        Damage_Down, // �ǰ� ������ ����
        Luck_Up, // ��� ���
        Score_Up, // ���� ȹ�淮 �߰�
        Money_UP, // ��� ȹ�淮 �߰�

    }
    public enum Skil_Middle // �нú� ��ų ����
    {
        Active_Cool_Down = 0, //��Ƽ�� ������ �ð� ����
        Jump_Up, // ���� ���� ����
        Jump_Down, // ���� ���� ����
        Down_Up, // �������� �ӵ� ����
        Down_Down, // �������� �ӵ� ����
        Speed_Up, // �ӵ�����
        Speed_Down, // �ӵ�����
        Damage_Down, // �ǰ� ������ ����
        Luck_Up, // ��� ���
        Score_Up, // ���� ȹ�淮 �߰�
        Money_UP, // ��� ȹ�淮 �߰�
    }
    public enum Skil_Final // �нú� ��ų ����
    {
        Active_Cool_Down = 0, //��Ƽ�� ������ �ð� ����
        Jump_Up, // ���� ���� ����
        Jump_Down, // ���� ���� ����
        Down_Up, // �������� �ӵ� ����
        Down_Down, // �������� �ӵ� ����
        Speed_Up, // �ӵ�����
        Speed_Down, // �ӵ�����
        Damage_Down, // �ǰ� ������ ����
        Luck_Up, // ��� ���
        Score_Up, // ���� ȹ�淮 �߰�
        Money_UP, // ��� ȹ�淮 �߰�
    }

    public enum Skil_Book // �нú� ��ų ����
    {
        Active_Cool_Down = 0, //��Ƽ�� ������ �ð� ����
        Jump_Up, // ���� ���� ����
        Jump_Down, // ���� ���� ����
        Down_Up, // �������� �ӵ� ����
        Down_Down, // �������� �ӵ� ����
        Speed_Up, // �ӵ�����
        Speed_Down, // �ӵ�����
        Damage_Down, // �ǰ� ������ ����
        Luck_Up, // ��� ���
        Score_Up, // ���� ȹ�淮 �߰�
        Money_UP, // ��� ȹ�淮 �߰�
    }

    public enum Skil_Scroll //�нú� ��ų �η縶��
    {
        Revive = 0, // ��� �� 1ȸ ��Ȱ
        Auto_Jump, // �ڵ� ���� 
        Auto_Active, // ��Ƽ�� �ڵ� ���
        Shield_Active, // ��Ƽ�� ��� �� ��ȣ�� ȹ��
        God_Active, // ��Ƽ�� ��� �� �����ð� ����
        Magnet // �ڼ�
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

    // ���� �÷��� ����
    public Active_Skil active = Active_Skil.Defense;
    public float hp = 100.0f;
    public float speed = 8.0f;
    public float jump = 10.0f;
    public float down = 20.0f;


}
