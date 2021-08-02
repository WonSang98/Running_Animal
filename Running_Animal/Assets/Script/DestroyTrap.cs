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
        Destroy(other.gameObject); // �� ��ֹ��� ��! ��!
        int per = Random.Range(0, 100);
        if (Player.CompareTag("Run"))
        {
            GameManager.Instance.MakeCoin(other.transform);
            if (per < GameManager.Data.luck) // ��� ��ġ�� ���� ũ��Ƽ�� ����.
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
                if (per < GameManager.Data.luck) // ��� ��ġ�� ���� ũ��Ƽ�� ����.
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
