using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Active : MonoBehaviour
{
    public enum ACTIVE_CODE
    {
        None = 0,
        Defense, // �ǰ� 1ȸ ����, Player Tag�� Sheild�� ����, 1ȸ �ǰݽ� �����ð� ���� ���� �±� ����
        Flash, // ���� �Ÿ� ������ ���� ��׶��� ���� ��ͱ��� �÷��̾� ������ ���ܿ��� ��. (��ǥ����)
        Ghost, // ���� �ð� ��üȭ 
        Heal, // ü�� ȸ��
        Item_Change, // ������ ü����
        Change_Coin, // ��ֹ� ����ȭ
        The_World, // ��-��-��
        Multiple_Combo, // �޺�3��
        Fly, // ���� �� �� �ռ���
    }
    /*��Ƽ�� ��ų ����*/
    public String[,] Active_Explane =
    {
        { "None", "�������" },
        { "���� [��Ƽ��]", "5�� ���� ��� �����̵� �� �� ���� �� �ִ� ���и� �θ��ϴ�."},
        { "���� [��Ƽ��]", "���� �Ÿ� ������ �����մϴ�."},
        { "����ȭ [��Ƽ��]", "3�� ���� ��� �������κ��� ���ظ� ���� �ʽ��ϴ�."  },
        { "ȸ�� [��Ƽ��", "50��ŭ�� ü���� ȸ���մϴ�."},
        { "�ٽ�, �� �ѹ� [��Ƽ��]", "��ų ���� ��, �������� 1ȸ �ٲ� �� �ֽ��ϴ�."},
        { "Ȳ���� �� [��Ƽ��]", "ȭ�鿡 ���̴� ��� ������ ���� �ٲ�����ϴ�."},
        { "������ �ð� [��Ƽ��]", "5�� ���� �ð��� ������ �帨�ϴ�."},
        { "�� ���� ���� �� ����[��Ƽ��]", "���� �ı� �� �޺��� ������ 3�踸ŭ ���Դϴ�."},
        { "���� [��Ƽ��]", "5�� ���� ���� Ƚ���� �Ѱ谡 �������ϴ�. ����, �󸶳� �ö� �� �������?"}
    };
    //��Ƽ�� ��ų �̹���
    public Sprite[] Active_Sprites;
    private void Start()
    {
        Active_Sprites = Resources.LoadAll<Sprite>("Active_Buttons/");
    }

    // Skill ID : 1 Defense
    public void OnDefense()
    {
        GameManager.Play.Player.transform.Find("1").gameObject.SetActive(true);
        GameManager.Play.Player.tag = "Shield";
        Invoke("OffDefense", 5.0f);

    }

    public void OffDefense()
    {
        if (GameManager.Play.Player.CompareTag("Shield"))
        {
            GameManager.Play.Player.tag = "Player";
            GameManager.Play.Player.transform.Find("1").gameObject.SetActive(false);

        }
    }

    // Skill ID : 2 Flash
    public IEnumerator OnFlash()
    {
        Vector3 pos_first = GameManager.Play.Player.transform.position;
        float temp_Speed = GameManager.Play.Status.ability.SPEED.value;
        GameManager.Play.Player.transform.Find("2").gameObject.SetActive(true);
        GameManager.Play.Player.transform.Translate(temp_Speed, 0, 0);
        GameManager.Play.Status.ability.SPEED.value = temp_Speed * 2;
        while (GameManager.Play.Player.transform.position.x <= pos_first.x)
        {
            GameManager.Play.Player.transform.Translate(-1 * temp_Speed * Time.deltaTime, 0, 0);
            yield return null;
        }
        GameManager.Play.Status.ability.SPEED.value = temp_Speed;
        GameManager.Play.Player.transform.Find("2").gameObject.SetActive(false);
    }

    // Skill ID : 3 Ghost
    public IEnumerator OnGhost()
    {
        SpriteRenderer spr = GameManager.Play.Player.GetComponent<SpriteRenderer>();
        for (int i = 0; i < 2; i ++)
        {
            if(i == 0)
            {
                spr = GameManager.Play.Player.GetComponent<SpriteRenderer>();
                Color c = spr.color;
                c.a = 0.5f;
                spr.color = c;
                GameManager.Play.Player.tag = "God";
            }
            else if(i == 1)
            {
                spr = GameManager.Play.Player.GetComponent<SpriteRenderer>();
                Color c = spr.color;
                c.a = 1.0f;
                spr.color = c;

                GameManager.Play.Player.tag = "Player";
            }
            yield return new WaitForSeconds(5);
        }
    }

    // Skill ID : 4 Heal
    public void OnHeal()
    {
        float plus_hp; // ȸ���� ü���� ��
        plus_hp = 50 * GameManager.Play.Status.ability.RESTORE.value; // �⺻ ȸ���� 50, ��� �� �ٸ� ��ҿ� ���� ȸ���� �����ɰ�
        if (GameManager.Play.Status.ability.HP.value + plus_hp >= GameManager.Play.Status.ability.MAX_HP.value)
        {
            GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
        }
        else
        {
            GameManager.Play.Status.ability.HP.value += plus_hp;
        }
        gameObject.GetComponent<UI_Play>().BAR_HP();
    }

    // Skill ID : 5 Item_Change
    // ��ų ����â���� ������ �ٲ� �� ����...
    public void Item_Change()
    {
        if (SceneManager.GetActiveScene().name == "Select_Item")
        {
            GameObject.Find("SceneManager").GetComponent<ControlSelectSkill>().ReLoad();
        }
    }

    // Skill ID : 6 Change_Coin
    // ��ֹ��� �������� �ٲ��ش�.
    public void Change_Coin()
    {
        GameObject[] Trap = GameObject.FindGameObjectsWithTag("Trap");
        GameObject[] Trap_Blood = GameObject.FindGameObjectsWithTag("Trap_Blood");
        GameObject[] Trap_Stun = GameObject.FindGameObjectsWithTag("Trap_Stun");
        GameObject[] Jump = GameObject.FindGameObjectsWithTag("Jump");

        GameObject coin = Resources.Load<GameObject>("Item/Coin");

        foreach (GameObject t in Trap)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

        foreach (GameObject t in Trap_Blood)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

        foreach (GameObject t in Trap_Stun)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

        foreach (GameObject t in Jump)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

    }

    //Skill ID : 7 The_World
    public IEnumerator OnSlow()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0) Time.timeScale = 0.5f;
            else if (i == 1) Time.timeScale = 1.0f;

            yield return new WaitForSeconds(2.5f);
        }
    }

    //Skill ID : 8 Multiple_Combe

    public IEnumerator MultiCombo()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                GameManager.Play.DC.comboMulti *= 3;
            }
            if (i == 1)
            {
                GameManager.Play.DC.comboMulti /= 3;
            }

            yield return new WaitForSeconds(10f);
        }
    }

    //Skill ID : 9 Fly

    public IEnumerator OnFly()
    {
        int temp = GameManager.Play.Status.ability.MAX_JUMP.value;
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                GameManager.Play.Status.ability.MAX_JUMP.value = 999999;
            }
            else
            {
                GameManager.Play.Status.ability.MAX_JUMP.value = temp;
            }
            yield return new WaitForSeconds(5f);
        }
    }

    //Skill ID : 10 Run

    public IEnumerator OnRun(float t)
    {
        float pre_speed = GameManager.Play.Status.ability.SPEED.value;
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                GameManager.Play.Player.tag = "Run";
                GameManager.Play.Status.ability.SPEED.value = 30.0f;
            }
            if (i == 1)
            {
                GameManager.Play.Status.ability.SPEED.value = pre_speed;
                GameManager.Play.Player.tag = "Player";
                GameManager.Play.DC.expNow += GameManager.Play.DS.expRun;
                GameManager.Play.DS.expRun = 0;
            }
            yield return new WaitForSeconds(t);
        }
    }

    public void Stop_Active()
    {
        StopAllCoroutines();
    }
}
