using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectSkil : MonoBehaviour
{
    /*��Ƽ�� ��ų ����*/
    String[,] Active_Explane =
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
    /*�нú� ��ų ����*/
    String[,] Passive_Explane =
    {
        { "None", "�������" },
        { "��� ���� [�нú�]", "����� �����մϴ�."},
        { "�� �� �� [�нú�]", "��Ƽ�� ��ų�� �� �� �� ����� �� �ֽ��ϴ�."},
        { "���� ���� [�нú�]", "������ �����մϴ�."  },
        { "�Ϸ� ���ϵ� �� �� �־� [�нú�]", $"�ִ� ü���� ����մϴ�."},
        { "�ӵ� ���� [�нú�]", "�ӵ��� �����մϴ�."},
        { "�ӵ� ���� [�нú�]", "�ӵ��� �����մϴ�."},
        { "������ ���� [�нú�]", "�������� �����մϴ�."},
        { "������ ���� [�нú�]", "�������� �����մϴ�."},
        { "���� ���� [�нú�]", "'DONW'��ư�� ���� �������� �ӵ��� �����մϴ�."},
        { "���� ���� [�нú�]", "'DOWN'��ư�� ���� �������� �ӵ��� �����մϴ�."},
        { "�� ���� ����! [�нú�]", "��带 ĳ���������� ������ϴ�."},
        { "�ϼ����� [�нú�]", "�޺� ȹ�淮�� 2�谡 �˴ϴ�."},
        { "�������� [�нú�]", "ü���� 0�� ���� ��, �ִ� ü���� ������ ���� �ٽ� �Ͼ�ϴ�." },
        { "��ȭ��â [�нú�]", "��� ȹ�淮�� ����մϴ�." },
        { "���� ���� �Ź� [�нú�]", "���� Ȯ���� �����մϴ�.\n��, ���� ������ �𸨴ϴ�. �� ���� �ְ� �� �� ���� �ֽ��ϴ�.\n�̷� ���� ������ ���� Ƚ���� �ݿ����� �ʽ��ϴ�." },
        { "���ڵ����� ����� [�нú�]", "���� Ȯ���� ��ð� ������ �˴ϴ�.\n��, ���� ������ �𸨴ϴ�. �� ���� �ְ� �� �� ���� �ֽ��ϴ�." },
        { "�ǰݽ� �����ð� ����[�нú�]", "�ǰݽÿ� ������ �Ǵ� �ð��� �þ�ϴ�." },
        { "����亸 [�нú�]", "�ִ� ���� ������ Ƚ���� 1ȸ �����մϴ�."},
        { "�ڰ�ġ�� [�нú�]", $"10�� ���� 4��ŭ�� ü���� ȸ���մϴ�."},
        { "ü�� ���� [�нú�]", "ü�� ȸ�� �迭 ������ �� ��ų�� ȿ���� �����մϴ�."}
    };



    GameObject[] Skil_Box = new GameObject[3];
    
    GameObject[] Skil_Panel = new GameObject[3];
    Text[] Skil_Title = new Text[3];
    Text[] Skil_Info = new Text[3];

    RectTransform[] Skil_Pos = new RectTransform[3];

    int[] Skil_Num = new int[3];
    short[] Skil_Kinds = new short[3];

    GameObject prfPlayer;
    GameObject Player;

    Sprite[] active_sprite; //Active Skil Image;
    Sprite[] passive_sprite; //Passive Skil Image;


    void Start()
    {
        // ITEM CHAGNE ��ų ����� ���� �÷��̾� ����, �� �� �÷��̾�� ȭ�� �ۿ� �����Ͽ� ������ �ʰ� ó����.
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        Player.transform.Translate(-60.495f, -10.5f, 0);
        Player.transform.name = "Player";


        GameManager.Instance.Load();
        Skil_Box[0] = GameObject.Find("UI/Button_Item0");
        Skil_Box[1] = GameObject.Find("UI/Button_Item1");
        Skil_Box[2] = GameObject.Find("UI/Button_Item2");

        for(int i=0; i<3; i++)
        {
            Skil_Pos[i] = Skil_Box[i].GetComponent<RectTransform>();
            Skil_Panel[i] = GameObject.Find("UI").transform.Find("Panel_Item"+i.ToString()).gameObject;
            Skil_Title[i] = Skil_Panel[i].transform.Find("Text_Name").GetComponent<Text>();
            Skil_Info[i] = Skil_Panel[i].transform.Find("Text_Info").GetComponent<Text>();
            Skil_Panel[i].SetActive(false);
        }

        StartCoroutine(Swing());
        active_sprite = Resources.LoadAll<Sprite>("Active_Buttons/");
        passive_sprite = Resources.LoadAll<Sprite>("Passive_Buttons/");

        //�г� ��Ȱ��ȭ.

        GameManager.Data.use_active = 0;
        GameManager.Data.change_chance = 999;


        change_all();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ��ų �̹��� ��鸮�� ȿ��
    IEnumerator Swing() // �¿� ��鸲
    {
        int x = 0;
        int y = 0;
        int z = 0;
        int left = 1; // left 1�Ͻ� ����, , -1�Ͻ� ������,  �̵�
        int up = 1;
        int big = 1;
        Vector2[] Pre_pos = new Vector2[3];
        Vector3[] Pre_scale = new Vector3[3];
        while (true)
        {
            if (x == 25) 
            { 
                x = -25;
                left *= -1;
            }
            if(y == 100)
            {
                y = -100;
                up *= -1;
            }
            if(z == 50)
            {
                z = -50;
                big *= -1;
            }
            for(int j = 0; j < 3; j++)
            {
                Pre_pos[j] = Skil_Pos[j].anchoredPosition; // ��ų �ڽ��� ���� ��ġ
                Pre_scale[j] = Skil_Pos[j].localScale; // ��ų �ڽ��� ���� scale

                Skil_Pos[j].anchoredPosition = new Vector2(Pre_pos[j].x + (-0.35f * left), Pre_pos[j].y + (-0.125f * up));
                Skil_Pos[j].localScale = new Vector3(Pre_scale[j].x - (-0.0006f * big), Pre_scale[j].y - (-0.0006f * big), 1);

            }
            x++;
            y++;
            z++;
            yield return new WaitForSeconds(0.01f);
        }
    }

   

    void change_all()
    {
        Boolean[] bool_active = new Boolean[Enum.GetNames(typeof(DataManager.Active_Skil)).Length];
        Boolean[] bool_passive = new Boolean[Enum.GetNames(typeof(DataManager.Passive_Skil)).Length];

        //�̹� ���õ� �нú� ���ϱ�.
        //�̰Ŵ� ���߿� �����͸� ���� �����ϰ� �����ϸ鼭 �����ؾ��ҵ�.
        //�нú� �κи� ���� ���� �迭ȭ�ؼ�... �̰� ���߿� �ڵ� �����ϸ鼭 �սô�... �켱�� �ϵ��ڵ�����
        bool_passive[2] = GameManager.Data.passive_active;
        bool_passive[11] = GameManager.Data.magnet;
        bool_passive[13] = GameManager.Data.passive_buwhal;
        bool_passive[15] = GameManager.Data.auto_jump;
        bool_passive[16] = GameManager.Data.random_god;
        bool_passive[19] = GameManager.Data.auto_restore;
        


        Skil_Num[0] = 0;
        Skil_Num[1] = 0;
        Skil_Num[2] = 0;
        for (int i = 0; i < 3; i++)
        {
            int per_active = Random.Range(0, 100);
            int temp;
            if (per_active < 25) // active
            {
                Skil_Kinds[i] = 0;
                do
                {
                    temp = Random.Range(1, Enum.GetNames(typeof(DataManager.Active_Skil)).Length);
                } while (bool_active[temp] == true);
                bool_active[temp] = true;
                Skil_Box[i].GetComponent<Image>().sprite = active_sprite[temp];
                Skil_Title[i].text = Active_Explane[temp, 0];
                Skil_Info[i].text = Active_Explane[temp, 1];
            }
            else //Passive
            {


                Skil_Kinds[i] = 1;
                do
                {
                    temp = Random.Range(1, Enum.GetNames(typeof(DataManager.Passive_Skil)).Length);
                } while (bool_passive[temp] == true);
                bool_passive[temp] = true;
                Skil_Box[i].GetComponent<Image>().sprite = passive_sprite[temp];
                Skil_Title[i].text = Passive_Explane[temp, 0];
                Skil_Info[i].text = Passive_Explane[temp, 1];

            }
            
            Skil_Num[i] = temp;
        }
    }

    public void button0()
    {
        if (Skil_Kinds[0] == 0)
        {
            set_active(Skil_Num[0]);
        }
        else
        {
            set_passive(Skil_Num[0]);
        }
        GameManager.Instance.Save();
        SceneManager.LoadScene("Play");
    }

    public void button1()
    {
        if (Skil_Kinds[1] == 0)
        {
            set_active(Skil_Num[1]);
        }
        else
        {
            set_passive(Skil_Num[1]);
        }
        GameManager.Instance.Save();
        SceneManager.LoadScene("Play");
    }

    public void button2()
    {
        if (Skil_Kinds[2] == 0)
        {
            set_active(Skil_Num[2]);
        }
        else
        {
            set_passive(Skil_Num[2]);
        }
        GameManager.Instance.Save();
        SceneManager.LoadScene("Play");
    }

    void set_active(int num)
    {
        GameManager.Data.active = (DataManager.Active_Skil)num;
    }

    void save_passive(int num)
    {
        for(int i=0; i<12; i++)
        {
            if(GameManager.Data.passive[i] == 0)
            {
                GameManager.Data.passive[i] = (DataManager.Passive_Skil)num;
                break;
            }
        }
    }
    void set_passive(int num)
    {   
        switch (num)
            {
            case 0:
                // None
                break;
            case 1:
                // ��� ����
                GameManager.Data.luck += 3;
                save_passive(1);
                break;
            case 2:
                // ��Ƽ�� ��ų �� �� ���
                GameManager.Data.max_active += 1;
                GameManager.Data.passive_active = true;
                save_passive(2);
                break;
            case 3:
                // ���� ����
                GameManager.Data.defense += 1;
                save_passive(3);
                break;
            case 4:
                // �ִ� ü�� ����
                GameManager.Data.max_hp += 20.0f;
                GameManager.Data.hp += 20.0f;
                save_passive(4);
                break;
            case 5:
                // �̵��ӵ� ����
                GameManager.Data.speed += (GameManager.Data.speed / 5);
                save_passive(5); 
                break;
            case 6:
                // �̵��ӵ� ����
                GameManager.Data.speed -= (GameManager.Data.speed / 5);
                save_passive(6);
                break;
            case 7:
                // ������ ����
                GameManager.Data.jump += (GameManager.Data.jump / 5);
                save_passive(7);
                break;
            case 8:
                // ������ ����
                GameManager.Data.jump -= (GameManager.Data.jump / 5);
                save_passive(8);
                break;
            case 9:
                // ���ϼӵ� ����
                GameManager.Data.down += (GameManager.Data.down / 5);
                save_passive(9);
                break;
            case 10:
                // ���ϼӵ� ����
                GameManager.Data.down -= (GameManager.Data.down / 5);
                save_passive(10);
                break;
            case 11:
                // �ڼ�����
                GameManager.Data.magnet = true;
                save_passive(11);
                break;
            case 12:
                // �޺� ȹ�淮 ����
                GameManager.Data.multi_combo *= 2;
                save_passive(12);
                break;
            case 13:
                // ��Ȱ
                GameManager.Data.buwhal += 1;
                GameManager.Data.passive_buwhal = true;
                save_passive(13);
                break;
            case 14:
                //���� ȹ�淮 ����
                GameManager.Data.multi_coin += 1;
                save_passive(14);
                break;
            case 15:
                // �ڵ� ����
                GameManager.Data.auto_jump = true;
                save_passive(15);
                break;
            case 16:
                // ���� Ȯ���� ���� ����
                GameManager.Data.random_god = true;
                save_passive(16);
                break;
            case 17:
                // �ǰݽ� �����ð� ����
                GameManager.Data.dodge_time += (GameManager.Data.dodge_time / 2);
                save_passive(17);
                break;
            case 18:
                // ���� Ƚ�� ����
                GameManager.Data.max_jump += 1;
                save_passive(18);
                break;
            case 19:
                // �ڵ� ü�� ���
                GameManager.Data.auto_restore = true;
                save_passive(19);
                break;
            case 20:
                // ü�� ȸ���� ����.
                GameManager.Data.restore_eff += 0.1f;
                save_passive(20);
                break;
        }
    }

    public void ReLoad()
    {
        if (GameManager.Data.change_chance > 0)
        {
            GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = true;
            GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = true;
            GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = true;
            change_all();

            GameManager.Data.change_chance -= 1;
        }

        if(GameManager.Data.change_chance == 0)
        {
            GameObject.Find("UI/Button_Reload").GetComponent<Button>().interactable = false;
        }
    }
    public void Skil0_Explain() // ��ų ����~
    {
        if (Skil_Panel[0].activeSelf == false)
        {
            Skil_Panel[0].SetActive(true);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
        else
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
    }

    public void Skil1_Explain() // ��ų ����~
    {
        if (Skil_Panel[1].activeSelf == false)
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(true);
            Skil_Panel[2].SetActive(false);
        }
        else
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
    }

    public void Skil2_Explain() // ��ų ����~
    {
        if (Skil_Panel[2].activeSelf == false)
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(true);
        }
        else
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
    }
}
