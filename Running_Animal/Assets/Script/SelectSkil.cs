using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectSkil : MonoBehaviour
{
    GameObject ui;
    GameObject[] Skil_Box = new GameObject[3];

    int[] Skil_Num = new int[3];
    short[] Skil_Kinds = new short[3];



    Sprite[] active_sprite; //Active Skil Image;
    Sprite[] passive_sprite; //Passive Skil Image;

    GameObject prfPlayer;
    GameObject Player;

    void Start()
    {
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.GetComponent<Rigidbody2D>().gravityScale = 0;
        Player.transform.name = "Player";

        GameManager.Instance.Load();
        ui = GameObject.Find("UI");
        Skil_Box[0] = GameObject.Find("UI/Button_Item0");
        Skil_Box[1] = GameObject.Find("UI/Button_Item1");
        Skil_Box[2] = GameObject.Find("UI/Button_Item2");

        active_sprite = Resources.LoadAll<Sprite>("Active_Buttons/");
        passive_sprite = Resources.LoadAll<Sprite>("Passive_Buttons/");

        GameManager.Data.use_active = 0;
        GameManager.Data.change_chance = 999;

        GameObject.Find("UI/Button_GO").GetComponent<Button>().interactable = false;
        change_all();

        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = false;
        //GameObject.Find("UI/Button_Reload").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_GO").GetComponent<Button>().interactable = true;
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
        GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = false;
        //GameObject.Find("UI/Button_Reload").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_GO").GetComponent<Button>().interactable = true;
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
        GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = false;
        //GameObject.Find("UI/Button_Reload").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_GO").GetComponent<Button>().interactable = true;
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

}
