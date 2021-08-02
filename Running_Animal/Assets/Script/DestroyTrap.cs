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
        Destroy(other.gameObject); // 그 장애물을 파! 괘!
        int per = Random.Range(0, 100);
        if (Player.CompareTag("Run"))
        {
            GameManager.Instance.MakeCoin(other.transform);
            if (per < GameManager.Data.luck) // 행운 수치에 따라 크리티컬 적용.
            {
                GameManager.Data.combo += GameManager.Data.multi_combo * 2;
            }
            else
            {
                GameManager.Data.combo += GameManager.Data.multi_combo;
            }
            if (GameManager.Data.lv != 13)
            {
                GameManager.Data.Exp_run += 1;
            }
        }
        else
        {
            if (!other.CompareTag("NonTrap"))
            {
                GameManager.Instance.MakeCoin(other.transform);
                if (per < GameManager.Data.luck) // 행운 수치에 따라 크리티컬 적용.
                {
                    GameManager.Data.combo += GameManager.Data.multi_combo * 2;
                }
                else
                {
                    GameManager.Data.combo += GameManager.Data.multi_combo;
                }
                if (GameManager.Data.lv != 13)
                {
                    GameManager.Data.now_Exp += 1;
                    if (GameManager.Data.now_Exp >= GameManager.Data.EXP[GameManager.Data.lv])
                    {
                        GameManager.Data.lvup = true;
                    }
                }
            }

        }     
        
    }
    
}
