using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContinue
{
    //스테이지가 다음으로 넘어가도 초기화 되지 않는 데이터들.
    //게임을 성공 실패하기 전에는 누적되는 데이터들 모임 
    public float[] expNeed; //다음 스테이지에 넘어가는데 필요한 경험치.
    public int lv;// 현재 스테이지.

    public float expMulti; // 경험치 배율

    public float goldNow; //게임 중 얻은 골드
    public float goldMulti;//골드 배율

    public int combo; // 현재 콤보
    public int comboMulti; // 콤보 배율
    public int comboMax; // 최대 콤보

    public float damage; // 함정 피격 데미지.

    public short stage; // 클리어한 스테이지 수.
    public short noHitStage; // 아무 피해 없이 스테이지 클리어.
    public short lastHit; // 마지막에 맞은 장애물CODE.
    public int passTrap; // 통과한 트랩의 갯수;

    public List<int> patternList; // 게임 진행하는 패턴. 함정의 패턴...
    public short patternCnt; //통과한 패턴의 수.

    public int activeMax; //액티브 스킬 최대 사용 횟수.


    public int dodge; //피격시 무적 시간.

    public short revive; // 부활 가능 횟수.

    public float pre_speed; // 액티브 스킬 대비해서 스킬 사용 전의 속도를 저장하는 변수이다.

    public List<Passive.PASSIVE_CODE> passiveGet; // 습득한 패시브 스킬 저장


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
        dc.expNeed = this.expNeed;
        dc.lv = this.lv;
        dc.expMulti = this.expMulti;
        dc.goldNow = this.goldNow;
        dc.goldMulti = this.goldMulti;
        dc.combo = this.combo;
        dc.comboMax = this.comboMax;
        dc.comboMulti = this.comboMulti;

        dc.damage = this.damage;

        dc.stage = this.stage;
        dc.noHitStage = this.noHitStage;
        dc.lastHit = this.lastHit;
        dc.passTrap = this.passTrap;

        int cnt_pl = this.patternList.Count;
        for (int i=0; i<cnt_pl; i++)
        {
            dc.patternList.Add(this.patternList[i]);
        }

        dc.patternCnt = this.patternCnt;

        dc.activeMax = this.activeMax;

        dc.dodge = this.dodge;

        dc.revive = this.revive;

        int cnt_pg = this.passiveGet.Count;
        for(int j=0; j<cnt_pg; j++)
        {
            dc.passiveGet.Add(this.passiveGet[j]);
        }

        dc.pre_speed = this.pre_speed;

        return dc;
    }

}

public class DataShot
{
    // 스테이지 넘어 갈 때마다 초기화 되는 것들.
    public int jumpNow; // 현재 점프한 횟수.
    public int activeUse; //액티브 스킬 현재 사용 횟수.
    public bool nohit; // 스테이지에서 피격했는지 안했는지.
    public int time_change; // 스킬 선택창에서 REROLL 횟수.
    public float expRun; // RUN 달리기 할 때 한번에 경험치 적용하려고 한 건데 이거 쓸 지 말지 좀  다시 봐야함.
    public float timer; // 장애물 생성 시간 측정 타이머, Trap_함수와 연관.
    public bool lvup; // 다음 스테이지로 넘어 갈 수 있는지
    public float expNow; // 현재 경험치
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
