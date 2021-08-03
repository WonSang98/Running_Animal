using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    GameObject prfPlayer;
    GameObject Player;

    void Start()
    {
        // ���� �÷��� �� �÷��̾� ����
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Player.transform.Translate(-6.495f, -1.5f, 0);
        Player.transform.name = "Player";


        if (GameManager.Data.playing == false) // ���� ������ ù �κ� 
        {
            // ���� �� ������ �������� ����
            if (GameManager.Data.Pre_Shield)
            {
                // 1ȸ�� ���� ���� ��
                Player.transform.Find("1").gameObject.SetActive(true);
                Player.tag = "Shield";
                GameManager.Data.Pre_Shield = false;
            }

            if (GameManager.Data.Pre_100)
            {
                //100���� �޸��� - 5�� �޸���
                StartCoroutine(OnRun(5.0f));
                GameManager.Data.Pre_100 = false;

            }

            if (GameManager.Data.Pre_300)
            {
                //300���� �޸��� - 10�� �޸���
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
