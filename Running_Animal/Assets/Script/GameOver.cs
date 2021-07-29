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
                    $"���� ȹ�� ������ : { GameManager.Data.multi_coin}\n" +
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

        GameManager.Data.lvup = false; // ������ ����, true�� �� ���� ��ֹ��� �������ϴ� ��ҷ�.
        GameManager.Data.lv = 0; // ���� ���� �ִ� 0~12������
        GameManager.Data.now_Exp = 0; // ���� ����ġ 
        GameManager.Data.stage = 0; // ��������
        GameManager.Data.multi_coin = 0; // ���� ȹ�淮 ������
        GameManager.Data.max_hp = 100.0f; // �ִ� ü��
        GameManager.Data.hp = 100.0f;
        GameManager.Data.speed = 8.0f; // ���� �ӵ�
        GameManager.Data.jump = 10.0f; // ���� ������
        GameManager.Data.down = 20.0f; // ���� �ϰ� �ӵ�
        GameManager.Data.defense = 0.0f; // ���� ����
        GameManager.Data.damage = 20.0f; // ���� �ǰ� ������
        GameManager.Data.combo = 0; // ���� ���� �� �޺� 
        GameManager.Data.max_combo = 0;
        GameManager.Data.multi_combo = 1; // �޺� ����
        GameManager.Data.max_jump = 2; // �ִ� ���� ���� Ƚ��
        GameManager.Data.luck = 0; // ��� (ȸ�ǿ�, �޺� ũ��Ƽ�ÿ� �����Ѵ�)
        GameManager.Data.max_active = 1; // ��Ƽ�� ��ų �ִ� ��밡�� Ƚ��
        GameManager.Data.use_active = 0; // ��Ƽ�� ��ų ���� ��� Ƚ��
        GameManager.Data.dodge_time = 12; // �ǰݽ� ���� �ð� ����. default 12
        GameManager.Data.restore_eff = 1.0f;

        GameManager.Data.magnet = false; // �нú� �ڼ����� ����
        GameManager.Data.buwhal = 0; // �нú� ��Ȱ ����
        GameManager.Data.auto_jump = false; //�нú� �������� ����
        GameManager.Data.random_god = false; // �нú� ���� Ȯ���� ���� ����
        GameManager.Data.auto_restore = false;
        GameManager.Data.change_chance = 0;
        GameManager.Data.passive_active = false; // �нú� ��Ƽ�� ���Ƚ�� + 1
        GameManager.Data.passive_buwhal = false;

        GameManager.Data.pattern = new List<int>();
        GameManager.Data.Gold += (int)GameManager.Data.play_gold;
        GameManager.Data.play_gold = 0;
    }

    void Update()
    {
        
    }


}
