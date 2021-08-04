using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrap : MonoBehaviour
{
    // ��ֹ��� ���� ���
    // �� �κ��� TrapMove�� �־ ���� ������ �ͱ⵵ ��...
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
