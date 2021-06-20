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

    public enum Passive_Skil // �нú� ��ų ���
    {
        None = 0,
        Speed_Up,
        Hp_UP,
        Jump_Up
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
