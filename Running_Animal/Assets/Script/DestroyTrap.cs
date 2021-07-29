using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrap : MonoBehaviour
{
    GameObject coin_bronze;
    GameObject coin_silver;
    GameObject coin_gold;

    void Start()
    {
        coin_bronze = Resources.Load<GameObject>("Item/Coin_Bronze");
        coin_silver = Resources.Load<GameObject>("Item/Coin_Silver");
        coin_gold = Resources.Load<GameObject>("Item/Coin_Gold");
    }

    // ��ֹ��� ���� ���

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            Debug.Log("���ι߽Τ�������������");
            GameObject coin = Instantiate(coin_bronze, new Vector3(other.transform.position.x + 0.5f, other.transform.position.y + 4.0f, other.transform.position.z), Quaternion.identity);

            Destroy(other.gameObject); // �� ��ֹ��� ��! ��!



            int per = Random.Range(0, 100);
            if (per < GameManager.Data.luck) // ��� ��ġ�� ���� ũ��Ƽ�� ����.
            {
                GameManager.Data.combo += GameManager.Data.multi_combo * 2;
            }
            else
            {
                GameManager.Data.combo += GameManager.Data.multi_combo;
            }
            if (GameManager.Data.lv != 12)
            {
                GameManager.Data.now_Exp += 1;
                if (GameManager.Data.now_Exp >= GameManager.Data.EXP[GameManager.Data.lv])
                {
                    GameManager.Data.lvup = true;
                }
            }
        
    }
    
}
