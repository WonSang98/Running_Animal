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
            // 시작 전 구매한 아이템의 적용
            if (GameManager.Data.Pre_Shield)
            {
                // 1회용 쉴드 구매 시
                Player.transform.Find("1").gameObject.SetActive(true);
                Player.tag = "Shield";
                GameManager.Data.Pre_Shield = false;
            }

            if (GameManager.Data.Pre_100)
            {
                //100미터 달리기 - 5초 달리기
                StartCoroutine(OnRun(5.0f));
                GameManager.Data.Pre_100 = false;

            }

            if (GameManager.Data.Pre_300)
            {
                //300미터 달리기 - 10초 달리기
                StartCoroutine(OnRun(10.0f));
                GameManager.Data.Pre_300 = false;
            }
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
                GameManager.Data.speed = pre_speed;
                Player.tag = "Player";
                GameManager.Data.now_Exp += GameManager.Data.Exp_run;
                GameManager.Data.Exp_run = 0;
                GameManager.Instance.BAR_EXP();
            }
            yield return new WaitForSeconds(t);
        }
    }


}
