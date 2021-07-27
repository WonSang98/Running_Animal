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
                    temp = Random.Range(0, Enum.GetNames(typeof(DataManager.Passive_Skil)).Length);
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
                    Debug.Log($"胶农费{num}");
                    break;
                case 1:
                    Debug.Log($"胶农费{num}");
                    break;
                case 2:
                    Debug.Log($"胶农费{num}");
                    break;
                case 3:
                    Debug.Log($"胶农费{num}");
                    break;
                case 4:
                    Debug.Log($"胶农费{num}");
                    break;
                case 5:
                    Debug.Log($"胶农费{num}");
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
