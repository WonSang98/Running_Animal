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

    public DataContinue DeepCopy()
    {
        DataContinue dc = new DataContinue();
        dc.expNeed = new float[] { 0, 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999 };
        dc.lv = 1;
        dc.lvup = false;
        dc.expNow = 0;
        dc.expMulti = 1;
        dc.goldNow = 0;
        dc.goldMulti = 1;
        dc.combo = 0;
        dc.comboMax = 0;
        dc.comboMulti = 1;

        dc.damage = 1.0f;

        dc.patternList = new List<int>();
        dc.patternCnt = 0;

        dc.activeMax = 1;

        dc.dodge = 12;

        dc.revive = 0;

        dc.passiveGet = new List<Passive.PASSIVE_CODE>();

        return dc;
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
    public float timer; // ��ֹ� ���� �ð� ���� Ÿ�̸�, Trap_�Լ��� ����.

    public DataShot()
    {
        jumpNow = 0;
        activeUse = 0;
        nohit = true;
        time_change = 1;
        expRun = 0;
        timer = 20.0f;
    }

    public DataShot DeepCopy()
    {
        DataShot ds = new DataShot();
        ds.jumpNow = 0;
        ds.activeUse = 0;
        ds.nohit = true;
        ds.time_change = 1;
        ds.expRun = 0;
        ds.timer = 20.0f;

        return ds;
    }
}
