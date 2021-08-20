using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContinue
{
    //���������� �������� �Ѿ�� �ʱ�ȭ ���� �ʴ� �����͵�.
    //������ ���� �����ϱ� ������ �����Ǵ� �����͵� ���� 

    public float[] expNeed; //���� ���������� �Ѿ�µ� �ʿ��� ����ġ.
    public int lv;// ���� ��������.
    public bool lvup; // ���� ���������� �Ѿ� �� �� �ִ���

    public float expNow; // ���� ����ġ
    public float expMulti; // ����ġ ����

    public float goldNow; //���� �� ���� ���
    public float goldMulti;//��� ����

    public int combo; // ���� �޺�
    public int comboMulti; // �޺� ����
    public int comboMax; // �ִ� �޺�

    public float damage; // ���� �ǰ� ������.

    public List<int> patternList; // ���� �����ϴ� ����. ������ ����...
    public short patternCnt; //����� ������ ��.

    public int activeMax; //��Ƽ�� ��ų �ִ� ��� Ƚ��.


    public int dodge; //�ǰݽ� ���� �ð�.

    public short revive; // ��Ȱ ���� Ƚ��.

    public List<Passive.PASSIVE_CODE> passiveGet; // ������ �нú� ��ų ����


    public DataContinue() //DEFAULT
    {
        expNeed = new float[]{ 0, 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999 };
        lv = 1;
        lvup = false;
        expNow = 0;
        expMulti = 1;
        goldNow = 0;
        goldMulti = 1;
        combo = 0;
        comboMax = 0;
        comboMulti = 1;

        damage = 1.0f;

        patternList = new List<int>();
        patternCnt = 0;

        activeMax = 1;

        dodge = 12;

        revive = 0;

        passiveGet = new List<Passive.PASSIVE_CODE>();
    }

}

public class DataShot
{
    // �������� �Ѿ� �� ������ �ʱ�ȭ �Ǵ� �͵�.
    public int jumpNow; // ���� ������ Ƚ��.
    public int activeUse; //��Ƽ�� ��ų ���� ��� Ƚ��.
    public bool nohit; // ������������ �ǰ��ߴ��� ���ߴ���.
    public int time_change; // ��ų ����â���� REROLL Ƚ��.

    public float expRun; // RUN �޸��� �� �� �ѹ��� ����ġ �����Ϸ��� �� �ǵ� �̰� �� �� ���� ��  �ٽ� ������.

    public DataShot()
    {
        jumpNow = 0;
        activeUse = 0;
        nohit = true;
        time_change = 1;
        expRun = 0;

    }
}
