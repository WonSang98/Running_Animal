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
    bool[] Skil_Active = new bool[3];


    Sprite[] active_sprite; //Active Skil Image;
    Sprite[] passive_sprite; //Passive Skil Image;

    GameObject temp;

    void Start()
    {
        ui = GameObject.Find("UI");
        Skil_Box[0] = GameObject.Find("UI/Button_Item0");
        Skil_Box[1] = GameObject.Find("UI/Button_Item1");
        Skil_Box[2] = GameObject.Find("UI/Button_Item2");

        active_sprite = Resources.LoadAll<Sprite>("Active_Buttons/");
        passive_sprite = Resources.LoadAll<Sprite>("Passive_Buttons/");

        change_all();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void change_all()
    {
        Skil_Num[0] = 0;
        Skil_Num[1] = 0;
        Skil_Num[2] = 0;
        for (int i=0; i<3; i++)
        {
            int per = Random.Range(0, 100);
            int temp;
            if (per < 10) // active
            {
                Skil_Active[i] = true;
                do
                {
                    temp = Random.Range(1, Enum.GetNames(typeof(DataManager.Active_Skil)).Length);
                } while (Array.IndexOf(Skil_Num, temp) != -1);

                Skil_Num[i] = temp;
                Skil_Box[i].GetComponent<Image>().sprite = active_sprite[Skil_Num[i]];
            }
            else
            {
                Skil_Active[i] = false;
                do
                {
                    temp = Random.Range(1, Enum.GetNames(typeof(DataManager.Passive_Skil)).Length);
                } while (Array.IndexOf(Skil_Num, temp) != -1);

                Skil_Num[i] = temp;
                Skil_Box[i].GetComponent<Image>().sprite = passive_sprite[Skil_Num[i]];
            }
        }
    }

    public void button0()
    {
        if (Skil_Active[0])
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
    }

    public void button1()
    {
        if (Skil_Active[1])
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
    }

    public void button2()
    {
        if (Skil_Active[2])
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
    }

    void set_active(int num)
    {
        GameManager.Data.active = (DataManager.Active_Skil)num;
    }

    void set_passive(int num)
    {
        switch (num)
        {
            case 0:
                break;
            case 1: //Speed_UP
                GameManager.Data.speed += 1.0f;
                break;
            case 2:
                GameManager.Data.hp += 10f;
                break;
            case 3:
                GameManager.Data.jump += 1.0f;
                break;
        }
    }

    public void ReLoad()
    {
        GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = true;
        GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = true;
        GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = true;
        change_all();
    }
}
