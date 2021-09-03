using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record
{
    // 기록 보관용
    // 필요한 것.
    /*
     * 1. 플레이 날짜 record
     * 2. 총점수 record 
     * 3. 획득 재화 record cd
     * 4. 난이도 preset
     * 5. 콤보 record dc
     * 6. 통과함정 record dc 
     * 7. 패시브 스킬 dc
     * 8. 능력치 ability
     * 9. 캐릭터 preset
     * 10. 스킬 dc
     */
    public string Date;
    public int Score;
    public int Special; // 특수재화
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
