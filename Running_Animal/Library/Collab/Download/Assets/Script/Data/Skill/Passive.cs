using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Passive : MonoBehaviour
{
    public enum PASSIVE_CODE
    {
        None = 0,
        LUK_UP, // ��� ����
        Active_Twice, // ��Ƽ�� �� �� ��� 2
        DEF_UP, // ���� ����
        HP_UP, // �ִ� ü�� ����
        MOV_UP, // �̵��ӵ� ����
        MOV_DOWN, // �̵��ӵ� ����
        JMP_UP, // ������ ����
        JMP_DOWN, // ������ ����
        DWN_UP, // ���ϼӵ� ����
        DWN_DOWN, // ���ϼӵ� ����
        Magenet, // �ڼ����� 11
        Combo_UP, // �޺� ȹ�淮 ����
        Resurrection, // ��Ȱ ��Ȱ ö�� ��û��� ������  13
        Coin_UP, // ���� ȹ�淮 ����
        Auto_Jump, // �ڵ� ���� 15
        Random_God, // ���� Ȯ���� ���� ���� 16
        Hit_God_UP, // �ǰݽ� �����ð� ����
        Max_Jump_Plus, // ���� Ƚ�� ����
        Auto_Restore, // �ڵ� ü�� ��� 19
        Heal_Eff// ȸ�� ȿ�� ����
    }

    /*�нú� ��ų ����*/
    public String[,] Passive_Explane =
    {
        { "None", "�������" },
        { "��� ���� [�нú�]", "����� �����մϴ�."},
        { "�� �� �� [�нú�]", "��Ƽ�� ��ų�� �� �� �� ����� �� �ֽ��ϴ�."},
        { "���� ���� [�нú�]", "������ 15% �����մϴ�."  },
        { "�Ϸ� ���ϵ� �� �� �־� [�нú�]", $"�ִ� ü���� 15% ����մϴ�."},
        { "�ӵ� ���� [�нú�]", "�ӵ��� �����մϴ�."},
        { "�ӵ� ���� [�нú�]", "�ӵ��� �����մϴ�."},
        { "������ ���� [�нú�]", "�������� �����մϴ�."},
        { "������ ���� [�нú�]", "�������� �����մϴ�."},
        { "���� ���� [�нú�]", "'DONW'��ư�� ���� �������� �ӵ��� �����մϴ�."},
        { "���� ���� [�нú�]", "'DOWN'��ư�� ���� �������� �ӵ��� �����մϴ�."},
        { "�� ���� ����! [�нú�]", "��带 ĳ���������� ������ϴ�."},
        { "�ϼ����� [�нú�]", "�޺� ȹ�淮�� 2�谡 �˴ϴ�."},
        { "�������� [�нú�]", "ü���� 0�� ���� ��, �ִ� ü���� ������ ���� �ٽ� �Ͼ�ϴ�." },
        { "��ȭ��â [�нú�]", "��� ȹ�淮�� 10% ����մϴ�." },
        { "���� ���� �Ź� [�нú�]", "���� Ȯ���� �����մϴ�.\n��, ���� ������ �𸨴ϴ�. �� ���� �ְ� �� �� ���� �ֽ��ϴ�.\n�̷� ���� ������ ���� Ƚ���� �ݿ����� �ʽ��ϴ�." },
        { "���ڵ����� ����� [�нú�]", "���� Ȯ���� ��ð� ������ �˴ϴ�.\n��, ���� ������ �𸨴ϴ�. �� ���� �ְ� �� �� ���� �ֽ��ϴ�." },
        { "�ǰݽ� �����ð� ����[�нú�]", "�ǰݽÿ� ������ �Ǵ� �ð��� �þ�ϴ�." },
        { "����亸 [�нú�]", "�ִ� ���� ������ Ƚ���� 1ȸ �����մϴ�."},
        { "�ڰ�ġ�� [�нú�]", $"1�� ���� ��ü ü���� 2% ��ŭ�� ü���� ȸ���մϴ�."},
        { "ü�� ���� [�нú�]", "ü�� ȸ�� �迭 ������ �� ��ų�� ȿ���� 10% �����մϴ�."}
    };

    //�нú� ��ų �̹���
    public Sprite[] Passive_Sprites;
    private void Start()
    {
        Passive_Sprites = Resources.LoadAll<Sprite>("Passive_Buttons/");
    }
    public void set_passive(int num)
    {
        switch (num)
        {
            case 0:
                // None
                break;
            case 1:
                // ��� ����
                GameManager.Play.Status.ability.LUK.value += 3;
                save_passive(1);
                break;
            case 2:
                // ��Ƽ�� ��ų �� �� ���
                GameManager.Play.DC.activeMax += 1;
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Active_Twice] = true;
                save_passive(2);
                break;
            case 3:
                // ���� ����
                GameManager.Play.Status.ability.DEF.value += GameManager.Play.Status.ability.DEF.value * 0.15f;
                save_passive(3);
                break;
            case 4:
                // �ִ� ü�� ����
                GameManager.Play.Status.ability.MAX_HP.value += GameManager.Play.Status.ability.MAX_HP.value * 0.15f;
                GameManager.Play.Status.ability.HP.value += GameManager.Play.Status.ability.HP.value * 0.15f;
                save_passive(4);
                break;
            case 5:
                // �̵��ӵ� ����
                GameManager.Play.Status.ability.SPEED.value *= 1.2f; 
                save_passive(5);
                break;
            case 6:
                // �̵��ӵ� ����
                GameManager.Play.Status.ability.SPEED.value *= 0.8f;
                save_passive(6);
                break;
            case 7:
                // ������ ����
                GameManager.Play.Status.ability.JUMP.value *= 1.2f;
                save_passive(7);
                break;
            case 8:
                // ������ ����
                GameManager.Play.Status.ability.JUMP.value *= 0.8f;
                save_passive(8);
                break;
            case 9:
                // ���ϼӵ� ����
                GameManager.Play.Status.ability.DOWN.value *= 1.2f;
                save_passive(9);
                break;
            case 10:
                // ���ϼӵ� ����
                GameManager.Play.Status.ability.DOWN.value *= 0.8f;
                save_passive(10);
                break;
            case 11:
                // �ڼ�����
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Magenet] = true;
                save_passive(11);
                break;
            case 12:
                // �޺� ȹ�淮 ����
                GameManager.Play.DC.comboMulti *= 2;
                save_passive(12);
                break;
            case 13:
                // ��Ȱ
                GameManager.Play.DC.revive += 1;
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Resurrection] = true;
                save_passive(13);
                break;
            case 14:
                //���� ȹ�淮 ����
                GameManager.Play.DC.goldMulti += 0.1f;
                save_passive(14);
                break;
            case 15:
                // �ڵ� ����
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Auto_Jump] = true;
                save_passive(15);
                break;
            case 16:
                // ���� Ȯ���� ���� ����
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Random_God] = true;
                save_passive(16);
                break;
            case 17:
                // �ǰݽ� �����ð� ����
                GameManager.Play.DC.dodge += (GameManager.Play.DC.dodge / 2);
                save_passive(17);
                break;
            case 18:
                // ���� Ƚ�� ����
                GameManager.Play.Status.ability.MAX_JUMP.value += 1;
                save_passive(18);
                break;
            case 19:
                // �ڵ� ü�� ���
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Auto_Restore] = true;
                save_passive(19);
                break;
            case 20:
                // ü�� ȸ���� ����.
                GameManager.Play.Status.ability.RESTORE.value += 0.1f;
                save_passive(20);
                break;
        }
    }

    public IEnumerator Auto_Jump()
    {
        while (true)
        {
            int per = Random.Range(0, 100);
            if (per < 20 + GameManager.Play.Status.ability.LUK.value)
            {
                GameManager.Play.Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, GameManager.Play.Status.ability.JUMP.value, 0);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    public IEnumerator Resurrection()
    {
        GameManager.Instance.GetComponent<UI_Play>().RemoveButton();
        StartCoroutine(gameObject.GetComponent<InterAction>().OnDodge(60));

        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                GameManager.Play.Status.ability.SPEED.value = 0.0f;
                GameManager.Play.Status.ability.HP.value = (GameManager.Play.Status.ability.MAX_HP.value / 2);
                gameObject.GetComponent<UI_Play>().BAR_HP();
            }
            if (i == 1)
            {
                GameManager.Play.Status.ability.SPEED.value = GameManager.Play.DC.pre_speed;
                GameManager.Instance.GetComponent<UI_Play>().RemakeButton();
                gameObject.GetComponent<InterAction>().Apply_Passive();
                GameManager.Play.Player.tag = "Player";
                
            }
            yield return new WaitForSeconds(3.0f);
        }
    }

    public IEnumerator Random_God()
    {
        while (true)
        {
            int per = Random.Range(0, 100);
            if (per < 10 + GameManager.Play.Status.ability.LUK.value)
            {
                StartCoroutine("Small_God");
            }
            yield return new WaitForSeconds(8f);
        }
    }

    public IEnumerator Small_God()
    {
        Color player_opacity;
        float time = 0;
        player_opacity = GameManager.Play.Player.GetComponent<SpriteRenderer>().color;
        player_opacity.a = 0.5f;
        GameManager.Play.Player.GetComponent<SpriteRenderer>().color = player_opacity;
        GameObject Player = GameManager.Play.Player.gameObject;
        while (time < 2)
        {
            time += Time.deltaTime;
            Player.tag = "God";
            yield return null;
        }
        player_opacity = GameManager.Play.Player.GetComponent<SpriteRenderer>().color;
        player_opacity.a = 1.0f;
        GameManager.Play.Player.GetComponent<SpriteRenderer>().color = player_opacity;
        Player.tag = "Player";
    }

    public IEnumerator Auto_Restore()
    {
        while (true)
        {
            //GameManager.Play.Status.ability.HP.value += 4 * GameManager.Play.Status.ability.RESTORE.value;
            GameManager.Play.Status.ability.HP.value += GameManager.Play.Status.ability.MAX_HP.value * 0.01f * GameManager.Play.Status.ability.RESTORE.value;
            if(GameManager.Play.Status.ability.HP.value > GameManager.Play.Status.ability.MAX_HP.value)
            {
                GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
            }
            gameObject.GetComponent<UI_Play>().BAR_HP();
            yield return new WaitForSeconds(1f);
        }
    }

    public void save_passive(int num)
    {
        GameManager.Play.DC.passiveGet.Add((PASSIVE_CODE)(num));
    }

    public void Stop_Passive()
    {
        StopAllCoroutines();
    }
}
