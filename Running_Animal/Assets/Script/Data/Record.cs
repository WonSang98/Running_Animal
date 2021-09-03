using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record
{
    // ��� ������
    // �ʿ��� ��.
    /*
     * 1. �÷��� ��¥ record
     * 2. ������ record 
     * 3. ȹ�� ��ȭ record cd
     * 4. ���̵� preset
     * 5. �޺� record dc
     * 6. ������� record dc 
     * 7. �нú� ��ų dc
     * 8. �ɷ�ġ ability
     * 9. ĳ���� preset
     * 10. ��ų dc
     */
    public string Date;
    public int Score;
    public int Special; // Ư����ȭ
    public string Clear;
    public DataContinue REC_dc;
    public Character REC_ab;
    public Preset REC_pre;

    public Record(string _Date, int _Score, int _Special, string _Clear, DataContinue _REC_dc, Character _REC_ab, Preset _REC_pre)
    {
        Date = _Date;
        Score = _Score;
        Special = _Special;
        Clear = _Clear;
        REC_dc = _REC_dc.DeepCopy();
        REC_ab = _REC_ab.DeepCopy();
        REC_pre = _REC_pre.DeepCopy();
    }

    Record()
    {

    }
}
