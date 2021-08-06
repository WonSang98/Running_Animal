using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    GameObject prfPlayer;
    GameObject Player;

    void Start()
    {
        // 게임 플레이 시 플레이어 생성
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Player.transform.Translate(-6.495f, -1.5f, 0);
        Player.transform.name = "Player";


        if (GameManager.Data.playing == false) // 게임 시작의 첫 부분 
        {
            Set_Random();
            GameManager.Data.active = GameManager.Data.Pre_Active;
            GameManager.Data.Pre_Active = DataManager.Active_Skil.None;
            // 시작 전 구매한 아이템의 적용
            if (GameManager.Data.Pre_HP.USE)
            {
                GameManager.Data.max_hp *= 1.1f;
                GameManager.Data.hp = GameManager.Data.max_hp;
                GameManager.Data.Pre_HP.USE = false;
            }
            if (GameManager.Data.Pre_Shield.USE)
            {
                // 1회용 쉴드 구매 시
                Player.transform.Find("1").gameObject.SetActive(true);
                Player.tag = "Shield";
                GameManager.Data.Pre_Shield.USE = false;
            }

            if (GameManager.Data.Pre_100.USE)
            {
                //100미터 달리기 - 5초 달리기
                StartCoroutine(OnRun(5.0f));
                GameManager.Data.Pre_100.USE = false;

            }

            if (GameManager.Data.Pre_300.USE)
            {
                //300미터 달리기 - 10초 달리기
                StartCoroutine(OnRun(10.0f));
                GameManager.Data.Pre_300.USE = false;
            }
            GameManager.Data.playing = true;
        }
    }
    void Set_Random()
    {
        switch (GameManager.Data.Pre_Random)
        {
            case DataManager.Random_Item.HP15:
                GameManager.Data.max_hp *= 1.15f;
                GameManager.Data.hp = GameManager.Data.max_hp;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.HP30:
                GameManager.Data.max_hp *= 1.30f;
                GameManager.Data.hp = GameManager.Data.max_hp;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.LUK5: 
                GameManager.Data.luck += 5;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.LUK10:
                GameManager.Data.luck += 10;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.SPEED15:
                GameManager.Data.speed *= 1.15f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.SPEED30:
                GameManager.Data.speed *= 1.30f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.JUMP20:
                GameManager.Data.jump *= 1.20f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.JUMP40:
                GameManager.Data.jump *= 1.40f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.GOLD25:
                GameManager.Data.multi_coin += 0.25f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.GOLD50:
                GameManager.Data.multi_coin += 0.5f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.COMBO2:
                GameManager.Data.multi_combo *= 2;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.COMBO3:
                GameManager.Data.multi_combo *= 3;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.JUMP_PLUS:
                GameManager.Data.max_jump += 1;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.DEF10:
                GameManager.Data.defense -= 0.1f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.DEF15:
                GameManager.Data.defense -= 0.15f;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;
            case DataManager.Random_Item.EXP2:
                GameManager.Data.multi_exp *= 2;
                GameManager.Data.Pre_Random = DataManager.Random_Item.None;
                break;

        }
    }
    IEnumerator OnRun(float t)
    {
        float pre_speed = 0;
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                pre_speed = GameManager.Data.speed;
                Player.tag = "Run";
                GameManager.Data.speed = 30.0f;
            }
            if (i == 1)
            {
                if (Player.transform.Find("1").gameObject.activeSelf)
                {
                    Player.tag = "Shield";
                }
                else
                {
                    Player.tag = "Player";
                }
                GameManager.Data.speed = pre_speed;
                GameManager.Data.now_Exp += GameManager.Data.Exp_run;
                GameManager.Data.Exp_run = 0;
                GameManager.Instance.BAR_EXP();
            }
            yield return new WaitForSeconds(t);
        }
    }


}
