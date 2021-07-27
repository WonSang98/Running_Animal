using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // 재화 관리
    public int Cash = 100; // 캐쉬 재화
    public int Gold = 10000; // 인게임 재화
    public int Money_Forest = 10; // 숲 테마 재화
    public int Money_Desert = 10; // 숲 테마 재화
    public int Money_Arctic = 10; // 숲 테마 재화
    // 테마 관리
    public DataManager.Themes Theme = DataManager.Themes.Forest;
    // 캐릭터 구매 관리
    public bool[] Buy_Character = { false, false, false, false };
    public int[] Cost_Character = { 500, 500, 500, 500 };
    public DataManager.Characters Now_Character = DataManager.Characters.One;

    // 게임 플레이 관리
    public DataManager.Active_Skil active = DataManager.Active_Skil.Defense; // 현재 가진 액티브 능력
    public DataManager.Passive_Skil[] passive = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // 현재 가진 패시브 능력
    public int[] EXP = { 20, 30, 40, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 }; //레벨업 시 필요 경험치
    public bool lvup; // 레벨업 여부, true일 시 다음 장애물은 레벨업하는 장소로.
    public int lv = 0; // 현재 레벨 최대 0~12렙까지
    public int now_Exp = 0; // 현재 경험치 
    public short stage = 0; // 스테이지
    public float play_gold = 0; // 게임 중 얻은 골드
    public int multi_coin = 0; // 코인 획득량 증가율
    public float max_hp = 100.0f; // 최대 체력
    public float hp = 100.0f; // 현재 체력
    public float speed = 8.0f; // 현재 속도
    public float jump = 10.0f; // 현재 점프력
    public float down = 20.0f; // 현재 하강 속도
    public float defense = 0.0f; // 현재 방어력
    public float damage = 20.0f; // 현재 피격 데미지
    public int combo = 0; // 게임 진행 중 콤보 
    public int multi_combo = 1; // 콤보 배율
    public int max_jump = 2; // 최대 점프 가능 횟수
    public int luck = 0; // 행운 (회피와, 콤보 크리티컬에 기인한다)
    public int max_active = 1; // 액티브 스킬 최대 사용가능 횟수
    public int use_active = 0; // 액티브 스킬 현대 사용 횟수
    public int dodge_time = 12; // 피격시 무적 시간 길이. default 12

    public bool magnet = false; // 패시브 자석버그 유무
    public short buwhal = 0; // 패시브 부활 유무
    public bool auto_jump = false; //패시브 오토점프 유무
    public bool random_god = false; // 패시브 작은 확률로 무적 유무

}
