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
                    $"ƒ⁄¿Œ »πµÊ ¡ı∞°∑Æ : { GameManager.Data.multi_coin}\n" +
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

        GameManager.Instance.Re_Stat();
    }


}
