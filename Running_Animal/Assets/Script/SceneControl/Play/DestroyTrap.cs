using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrap : MonoBehaviour
{
    // 장애물이 끝에 도달해서 파괴하는 경우.
    UI_Play UI_Play;
    private void Start()
    {
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        UI_Play.Trap_Combo(other.transform);

        if (GameManager.Play.Player.CompareTag("Run"))
        {
            if (GameManager.Play.DC.lv != 13)
            {
                GameManager.Play.DS.expRun += GameManager.Play.DC.expMulti;
            }
        }
        else
        {
            if (!other.CompareTag("NonTrap"))
            {
                GameManager.Play.DC.expNow += GameManager.Play.DC.expMulti;

                UI_Play.BAR_EXP();
                if (GameManager.Play.DC.expNow >= GameManager.Play.DC.expNeed[GameManager.Play.DC.lv])
                {
                    GameManager.Play.DC.lvup = true;
                }
            }
        }
    }
}

