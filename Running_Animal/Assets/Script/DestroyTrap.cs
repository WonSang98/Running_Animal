using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrap : MonoBehaviour
{
    // 장애물이 닿은 경우
    // 이 부분은 TrapMove에 넣어도 되지 않을까 싶기도 함...
    GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.Trap_Combo(other.transform);

        if (Player.CompareTag("Run"))
        {
            if (GameManager.Data.lv != 13)
            {
                GameManager.Data.Exp_run += 1 * GameManager.Data.multi_exp;
            }
        }
        else
        {
            if (!other.CompareTag("NonTrap"))
            {
                GameManager.Data.now_Exp += 1 * GameManager.Data.multi_exp;
                GameManager.Instance.BAR_EXP();
                    if (GameManager.Data.now_Exp >= GameManager.Data.EXP[GameManager.Data.lv])
                    {
                        GameManager.Data.lvup = true;
                    }
            }
        }

    }        
}
