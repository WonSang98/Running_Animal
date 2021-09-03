using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContinue
{
    //���������� �������� �Ѿ�� �ʱ�ȭ ���� �ʴ� �����͵�.
    //������ ���� �����ϱ� ������ �����Ǵ� �����͵� ���� 

    public float[] expNeed; //���� ���������� �Ѿ�µ� �ʿ��� ����ġ.
    public int lv;// ���� ��������.

    public float expMulti; // ����ġ ����

    public float goldNow; //���� �� ���� ���
    public float goldMulti;//��� ����

    public int combo; // ���� �޺�
    public int comboMulti; // �޺� ����
    public int comboMax; // �ִ� �޺�

    public float damage; // ���� �ǰ� ������.

    public short stage; // Ŭ������ �������� ��.
    public short noHitStage; // �ƹ� ���� ���� �������� Ŭ����.
    public short lastHit; // �������� ���� ��ֹ�CODE.
    public int passTrap; // ����� Ʈ���� ����;

    public List<int> patternList; // ���� �����ϴ� ����. ������ ����...
    public short patternCnt; //����� ������ ��.

    public int activeMax; //��Ƽ�� ��ų �ִ� ��� Ƚ��.


    public int dodge; //�ǰݽ� ���� �ð�.

    public short revive; // ��Ȱ ���� Ƚ��.

    public float pre_speed; // ��Ƽ�� ��ų ����ؼ� ��ų ��� ���� �ӵ��� �����ϴ� �����̴�.

    public List<Passive.PASSIVE_CODE> passiveGet; // ������ �нú� ��ų ����


    public DataContinue() //DEFAULT
    {
        expNeed = new float[]{ 0, 24, 24, 24, 35, 35, 56, 56, 68, 105, 105, 105, 210, 999999 };
        //expNeed = new float[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 999999 };
        //                     0   1   2   3   4   5   6  7    8   9    10  11   12   
        lv = 1;
        expMulti = 1;
        goldNow = 0;
        goldMulti = 1;
        combo = 0;
        comboMax = 0;
        comboMulti = 1;

        damage = 1.0f;

        stage = 0;
        noHitStage = 0;
        lastHit = 0;
        passTrap = 0;

        patternList = new List<int>();
        patternCnt = 0;

        activeMax = 1;

        dodge = 12;

        revive = 0;

        passiveGet = new List<Passive.PASSIVE_CODE>();

        pre_speed = 8;
    }

    public DataContinue DeepCopy()
    {
        DataContinue dc = new DataContinue();
        dc.expNeed = new float[] { 0, 24, 24, 24, 35, 35, 56, 56, 68, 105, 105, 105, 210, 999999 };
        dc.lv = 1;
        dc.expMulti = 1;
        dc.goldNow = 0;
        dc.goldMulti = 1;
        dc.combo = 0;
        dc.comboMax = 0;
        dc.comboMulti = 1;

        dc.damage = 1.0f;

        dc.stage = 0;
        dc.noHitStage = 0;
        dc.lastHit = 0;
        dc.passTrap = 0;

        dc.patternList = new List<int>();
        dc.patternCnt = 0;

        dc.activeMax = 1;

        dc.dodge = 12;

        dc.revive = 0;

        dc.passiveGet = new List<Passive.PASSIVE_CODE>();

        dc.pre_speed = 8;

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
    public bool lvup; // ���� ���������� �Ѿ� �� �� �ִ���
    public float expNow; // ���� ����ġ
    public int AC_multicombo;

    public DataShot()
    {
        jumpNow = 0;
        activeUse = 0;
        nohit = true;
        time_change = 1;
        expRun = 0;
        timer = 20.0f;
        lvup = false;
        expNow = 0;
        AC_multicombo = 1;
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
        ds.lvup = false;
        ds.expNow = 0;
        ds.AC_multicombo = 1;

        return ds;
    }
}
