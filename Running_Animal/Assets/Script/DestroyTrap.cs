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

    // 舌蕉弘戚 願精 井酔

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            Debug.Log("坪昔降塾たたたたたたた");
            GameObject coin = Instantiate(coin_bronze, new Vector3(other.transform.position.x + 0.5f, other.transform.position.y + 4.0f, other.transform.position.z), Quaternion.identity);

            Destroy(other.gameObject); // 益 舌蕉弘聖 督! 吋!



            int per = Random.Range(0, 100);
            if (per < GameManager.Data.luck) // 楳錘 呪帖拭 魚虞 滴軒銅鎮 旋遂.
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
