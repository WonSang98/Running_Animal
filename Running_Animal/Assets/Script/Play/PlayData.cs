using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContinue
{
    //스테이지가 다음으로 넘어가도 초기화 되지 않는 데이터들.
    //게임을 성공 실패하기 전에는 누적되는 데이터들 모임 

    public float[] expNeed; //다음 스테이지에 넘어가는데 필요한 경험치.
    public int lv;// 현재 스테이지.
    public bool lvup; // 다음 스테이지로 넘어 갈 수 있는지

    public float expNow; // 현재 경험치
    public float expMulti; // 경험치 배율

    public float goldNow; //게임 중 얻은 골드
    public float goldMulti;//골드 배율

    public int combo; // 현재 콤보
    public int comboMulti; // 콤보 배율
    public int comboMax; // 최대 콤보

    public float damage; // 함정 피격 데미지.

    public List<int> patternList; // 게임 진행하는 패턴. 함정의 패턴...
    public short patternCnt; //통과한 패턴의 수.

    public int activeMax; //액티브 스킬 최대 사용 횟수.


    public int dodge; //피격시 무적 시간.

    public short revive; // 부활 가능 횟수.

    public List<Passive.PASSIVE_CODE> passiveGet; // 습득한 패시브 스킬 저장


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
    // 스테이지 넘어 갈 때마다 초기화 되는 것들.
    public int jumpNow; // 현재 점프한 횟수.
    public int activeUse; //액티브 스킬 현재 사용 횟수.
    public bool nohit; // 스테이지에서 피격했는지 안했는지.
    public int time_change; // 스킬 선택창에서 REROLL 횟수.

    public float expRun; // RUN 달리기 할 때 한번에 경험치 적용하려고 한 건데 이거 쓸 지 말지 좀  다시 봐야함.

    public DataShot()
    {
        jumpNow = 0;
        activeUse = 0;
        nohit = true;
        time_change = 1;
        expRun = 0;

    }
}
