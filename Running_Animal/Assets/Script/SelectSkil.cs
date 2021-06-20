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
    /*
     * 0 active
     * 1 prologue
     * 2 middle
     * 3 final
     * 4 book
     * 5 scroll
     */



    Sprite[] active_sprite; //Active Skil Image;
    Sprite[] passive_prologue; //Passive Skil - Prologue Image;
    Sprite[] passive_middle; //Passive Skil - Middle Image;
    Sprite[] passive_final; //Passive Skil - Final Image;
    Sprite[] passive_book; //Passive Skil - Book Image;
    Sprite[] passive_scroll; //Passive Skil - Scroll Image;



    GameObject temp;

    void Start()
    {
        ui = GameObject.Find("UI");
        Skil_Box[0] = GameObject.Find("UI/Button_Item0");
        Skil_Box[1] = GameObject.Find("UI/Button_Item1");
        Skil_Box[2] = GameObject.Find("UI/Button_Item2");

        active_sprite = Resources.LoadAll<Sprite>("Active_Buttons/");
        passive_prologue = Resources.LoadAll<Sprite>("Passive_Buttons/Prologue");
        passive_middle = Resources.LoadAll<Sprite>("Passive_Buttons/Middle");
        passive_final = Resources.LoadAll<Sprite>("Passive_Buttons/Final");
        passive_book = Resources.LoadAll<Sprite>("Passive_Buttons/Book");
        passive_scroll = Resources.LoadAll<Sprite>("Passive_Buttons/Scroll");

        change_all();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void change_all()
    {
        Boolean[] bool_active = new Boolean[Enum.GetNames(typeof(DataManager.Active_Skil)).Length];
        Boolean[] bool_prologue = new Boolean[Enum.GetNames(typeof(DataManager.Skil_Prologue)).Length];
        Boolean[] bool_middle = new Boolean[Enum.GetNames(typeof(DataManager.Skil_Middle)).Length];
        Boolean[] bool_final = new Boolean[Enum.GetNames(typeof(DataManager.Skil_Final)).Length];
        Boolean[] bool_book = new Boolean[Enum.GetNames(typeof(DataManager.Skil_Book)).Length];
        Boolean[] bool_scroll = new Boolean[Enum.GetNames(typeof(DataManager.Skil_Scroll)).Length];

        Skil_Num[0] = 0;
        Skil_Num[1] = 0;
        Skil_Num[2] = 0;
        for (int i=0; i<3; i++)
        {
            int per_active = Random.Range(0, 100);
            int temp;
            if (per_active < 10) // active
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
                
                int per_passive = Random.Range(0, 100);
                if (per_passive < 40) //Prologue
                {
                    Skil_Kinds[i] = 1;
                    do
                    {
                        temp = Random.Range(0, Enum.GetNames(typeof(DataManager.Skil_Prologue)).Length);
                    } while (bool_prologue[temp] == true);
                    bool_prologue[temp] = true;
                    Skil_Box[i].GetComponent<Image>().sprite = passive_prologue[temp];
                }
                else if (per_passive < 70) // Middle
                {
                    Skil_Kinds[i] = 2;
                    do
                    {
                        temp = Random.Range(0, Enum.GetNames(typeof(DataManager.Skil_Middle)).Length);
                    } while (bool_middle[temp] == true);
                    bool_middle[temp] = true;
                    Skil_Box[i].GetComponent<Image>().sprite = passive_middle[temp];
                }
                else if (per_passive < 85) // Final
                {
                    Skil_Kinds[i] = 3;
                    do
                    {
                        temp = Random.Range(0, Enum.GetNames(typeof(DataManager.Skil_Final)).Length);
                    } while (bool_final[temp] == true);
                    bool_final[temp] = true;
                    Skil_Box[i].GetComponent<Image>().sprite = passive_final[temp];
                }
                else if (per_passive < 90) //Book
                {
                    Skil_Kinds[i] = 4;
                    do
                    {
                        temp = Random.Range(0, Enum.GetNames(typeof(DataManager.Skil_Book)).Length);
                    } while (bool_book[temp] == true);
                    bool_book[temp] = true;
                    Debug.Log(temp);
                    Skil_Box[i].GetComponent<Image>().sprite = passive_book[temp];
                }
                else // Scroll
                {
                    Skil_Kinds[i] = 5;
                    do
                    {
                        temp = Random.Range(0, Enum.GetNames(typeof(DataManager.Skil_Scroll)).Length);
                    } while (bool_scroll[temp] == true);
                    bool_scroll[temp] = true;
                    Skil_Box[i].GetComponent<Image>().sprite = passive_scroll[temp];
                }
                
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
            set_passive(Skil_Kinds[0], Skil_Num[0]);
        }
        GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = false;
    }

    public void button1()
    {
        if (Skil_Kinds[1] == 0)
        {
            set_active(Skil_Num[1]);
        }
        else
        {
            set_passive(Skil_Kinds[1], Skil_Num[1]);
        }
        GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = false;
    }

    public void button2()
    {
        if (Skil_Kinds[2] == 0)
        {
            set_active(Skil_Num[2]);
        }
        else
        {
            set_passive(Skil_Kinds[2], Skil_Num[2]);
        }
        GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = false;
        GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = false;
    }

    void set_active(int num)
    {
        GameManager.Data.active = (DataManager.Active_Skil)num;
    }

    void set_passive(int kind, int num)
    {
        if (kind == 5) // 패시브 종류 Scroll 인경우
        {
            switch (num)
            {
                case 0:
                    Debug.Log($"스크롤{num}");
                    break;
                case 1:
                    Debug.Log($"스크롤{num}");
                    break;
                case 2:
                    Debug.Log($"스크롤{num}");
                    break;
                case 3:
                    Debug.Log($"스크롤{num}");
                    break;
                case 4:
                    Debug.Log($"스크롤{num}");
                    break;
                case 5:
                    Debug.Log($"스크롤{num}");
                    break;
            }
        }
        else // 패시브 종류 서,종,중장 책인 경우...
        {
            switch (num)
            {
                case 0:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 1:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 2:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 3:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 4:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 5:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 6:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 7:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 8:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 9:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 10:
                    Debug.Log($"패시브 책{num}");
                    break;
                case 11:
                    Debug.Log($"패시브 책{num}");
                    break;
            }
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
