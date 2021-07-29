using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Text info;
    void Start()
    {
        info = GameObject.Find("UI/Text_END").GetComponent<Text>();
        info.text = $"LV : {GameManager.Data.lv}\n" +
                    $"코인 획득 증가량 : { GameManager.Data.multi_coin}\n" +
                    $"MAX_HP : { GameManager.Data.max_hp}\n" +
                    $"Speed : { GameManager.Data.speed}\n" +
                    $"Jump: { GameManager.Data.jump}\n" +
                    $"Down : { GameManager.Data.down}\n" +
                    $"Defense : { GameManager.Data.defense}\n" +
                    $"max_combo : { GameManager.Data.max_combo}\n" +
                    $"multiple_combo : { GameManager.Data.multi_combo}\n" +
                    $"max_jump : { GameManager.Data.max_jump}\n" +
                    $"Luck : { GameManager.Data.luck}\n" +
                    $"dodge_time : { GameManager.Data.dodge_time}\n" +
                    $"max_active : { GameManager.Data.max_active}\n" +
                    $"magnet : { GameManager.Data.magnet}\n" +
                    $"auto_jump : { GameManager.Data.auto_jump}\n" +
                    $"random_god : { GameManager.Data.random_god}\n" +
                    $"max_active : { GameManager.Data.max_active}\n" +
                    $"auto_restore : { GameManager.Data.auto_restore}\n" +
                    $"change_chance : { GameManager.Data.change_chance}";
        GameManager.Data.active = DataManager.Active_Skil.None;
        GameManager.Data.playing = false;
        Time.timeScale = 1;

        GameManager.Data.lvup = false; // 레벨업 여부, true일 시 다음 장애물은 레벨업하는 장소로.
        GameManager.Data.lv = 0; // 현재 레벨 최대 0~12렙까지
        GameManager.Data.now_Exp = 0; // 현재 경험치 
        GameManager.Data.stage = 0; // 스테이지
        GameManager.Data.multi_coin = 0; // 코인 획득량 증가율
        GameManager.Data.max_hp = 100.0f; // 최대 체력
        GameManager.Data.hp = 100.0f;
        GameManager.Data.speed = 8.0f; // 현재 속도
        GameManager.Data.jump = 10.0f; // 현재 점프력
        GameManager.Data.down = 20.0f; // 현재 하강 속도
        GameManager.Data.defense = 0.0f; // 현재 방어력
        GameManager.Data.damage = 20.0f; // 현재 피격 데미지
        GameManager.Data.combo = 0; // 게임 진행 중 콤보 
        GameManager.Data.max_combo = 0;
        GameManager.Data.multi_combo = 1; // 콤보 배율
        GameManager.Data.max_jump = 2; // 최대 점프 가능 횟수
        GameManager.Data.luck = 0; // 행운 (회피와, 콤보 크리티컬에 기인한다)
        GameManager.Data.max_active = 1; // 액티브 스킬 최대 사용가능 횟수
        GameManager.Data.use_active = 0; // 액티브 스킬 현대 사용 횟수
        GameManager.Data.dodge_time = 12; // 피격시 무적 시간 길이. default 12
        GameManager.Data.restore_eff = 1.0f;

        GameManager.Data.magnet = false; // 패시브 자석버그 유무
        GameManager.Data.buwhal = 0; // 패시브 부활 유무
        GameManager.Data.auto_jump = false; //패시브 오토점프 유무
        GameManager.Data.random_god = false; // 패시브 작은 확률로 무적 유무
        GameManager.Data.auto_restore = false;
        GameManager.Data.change_chance = 0;
        GameManager.Data.passive_active = false; // 패시브 액티브 사용횟수 + 1
        GameManager.Data.passive_buwhal = false;

        GameManager.Data.pattern = new List<int>();
        GameManager.Data.Gold += (int)GameManager.Data.play_gold;
        GameManager.Data.play_gold = 0;
    }

    void Update()
    {
        
    }


}
