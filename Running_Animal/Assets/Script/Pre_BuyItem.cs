using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Pre_BuyItem : MonoBehaviour
{
    int normal_price = 500;
    int random_price = 1000;


    GameObject[] Select_Item = new GameObject[4];
    Text[] CNT_Item = new Text[4];

    Text Shop_Owner;
    Text Text_info;

    private void Start()
    {
        GameManager.Instance.Load();
        //TEMP
        Select_Item[0] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_HP").transform.Find("Image_SELECT").gameObject;
        Select_Item[1] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_Shield").transform.Find("Image_SELECT").gameObject;
        Select_Item[2] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_RUN0").transform.Find("Image_SELECT").gameObject;
        Select_Item[3] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_RUN1").transform.Find("Image_SELECT").gameObject;

        CNT_Item[0] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_HP/Text_CNT").GetComponent<Text>();
        CNT_Item[1] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_Shield/Text_CNT").GetComponent<Text>();
        CNT_Item[2] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_RUN0/Text_CNT").GetComponent<Text>();
        CNT_Item[3] = GameObject.Find("UI/Scroll View_Item/Viewport/Content/Button_RUN1/Text_CNT").GetComponent<Text>();
        //TEMP
        Shop_Owner = GameObject.Find("UI/Panel_Hi/Text").GetComponent<Text>();
        Text_info = GameObject.Find("UI/Panel_Info/Text").GetComponent<Text>();

        Check_Select();
        Info();
        // ���� Forest�϶�
        for (int i = 0; i < 255; i++)
        {
            GameManager.Data.pattern.Add(i);
        }

        GameManager.Data.pattern = GameManager.Instance.ShuffleList(GameManager.Data.pattern);
    }
    public void Info()
    {
        Text_info.text = "";
        if (GameManager.Data.Pre_HP.USE)
        {
            Text_info.text += $"ȯ���� ȯ���� �����߽��ϴ�.\n ü���� �þ� {GameManager.Data.max_hp * 1.1f}�� �Ǿ����ϴ�.\n";
        }

        if (GameManager.Data.Pre_Shield.USE)
        {
            Text_info.text += $"���κ��� ��ȣ�� �޾ҽ��ϴ�.\n ";
        }
       
        if (GameManager.Data.Pre_100.USE)
        {
            Text_info.text += $"ǳ�����κ��� ���� �ູ�� �޾ҽ��ϴ�.\n ";
        }
       
        if (GameManager.Data.Pre_300.USE)
        {
            Text_info.text += $"ǳ�����κ��� ū �ູ�� �޾ҽ��ϴ�.\n ";
        }
        Debug.Log(GameManager.Data.active);
        if(GameManager.Data.Pre_Active != DataManager.Active_Skil.None)
        {
            Text_info.text += $"���� ������ �Ծ����ϴ�. \n '{GameManager.Data.Pre_Active}' �ɷ��� ������ϴ�.";
        }

        if (GameManager.Data.Pre_Random != DataManager.Random_Item.None)
        {
            Text_info.text += $"ȯ�����κ��� ������ �޾ҽ��ϴ�. \n {GameManager.Data.Pre_Random} �� �˴ϴ�.";
        }
        CNT_Item[0].text = $"{GameManager.Data.Pre_HP.CNT}";
        CNT_Item[1].text = $"{GameManager.Data.Pre_Shield.CNT}";
        CNT_Item[2].text = $"{GameManager.Data.Pre_100.CNT}";
        CNT_Item[3].text = $"{GameManager.Data.Pre_300.CNT}";
        GameManager.Instance.Save();
    }
    public void Check_Select()
    {
        if (GameManager.Data.Pre_HP.USE)
        {
            Select_Item[0].SetActive(true);
        }
        else
        {
            Select_Item[0].SetActive(false);
        }

        if (GameManager.Data.Pre_Shield.USE)
        {
            Select_Item[1].SetActive(true);
        }
        else
        {
            Select_Item[1].SetActive(false);
        }
        if (GameManager.Data.Pre_100.USE)
        {
            Select_Item[2].SetActive(true);
        }
        else
        {
            Select_Item[2].SetActive(false);
        }
        if (GameManager.Data.Pre_300.USE)
        {
            Select_Item[3].SetActive(true);
        }
        else
        {
            Select_Item[3].SetActive(false);
        }
    }
    
    public void Buy_HP() // ü�� 10% ����.
    {
        if(GameManager.Data.Pre_HP.USE) // ����ϱ�� �������� ��
        {
            GameManager.Data.Pre_HP.CNT += 1;
            GameManager.Data.Pre_HP.USE = false;
            Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
        }
        else // ���� ����ϱ�� ���� �ʾ��� ��
        {
            if(GameManager.Data.Pre_HP.CNT > 0)
            {
                GameManager.Data.Pre_HP.CNT -= 1;
                GameManager.Data.Pre_HP.USE = true;
                Shop_Owner.text = "����, �þƳ��� �����̾�.";
            }
            else if(GameManager.Data.Gold > normal_price)
            {
                GameManager.Data.Gold -= normal_price;
                GameManager.Data.Pre_HP.USE = true;
                Shop_Owner.text = "���� �����̾�.";
            }
        }
        Check_Select();
        Info();
    }

    public void Buy_Shield() // ���� �� 1ȸ ����
    {
        if (GameManager.Data.Pre_Shield.USE) // ����ϱ�� �������� ��
        {
            GameManager.Data.Pre_Shield.CNT += 1;
            GameManager.Data.Pre_Shield.USE = false;
            Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
        }
        else // ���� ����ϱ�� ���� �ʾ��� ��
        {
            if (GameManager.Data.Pre_Shield.CNT > 0)
            {
                GameManager.Data.Pre_Shield.CNT -= 1;
                GameManager.Data.Pre_Shield.USE = true;
                Shop_Owner.text = "����, �þƳ��� �����̾�.";
            }
            else if (GameManager.Data.Gold > normal_price)
            {
                GameManager.Data.Gold -= normal_price;
                GameManager.Data.Pre_Shield.USE = true;
                Shop_Owner.text = "���� �����̾�.";
            }
        }
        Check_Select();
        Info();
    }

    public void Buy_100() //100���� �޸��� �̿��
    {
        if (GameManager.Data.Pre_300.USE)
        {
            Shop_Owner.text = "... ǳ���� �ູ�� �� ������ ������.";
        }
        else 
        { 
            if (GameManager.Data.Pre_100.USE) // ����ϱ�� �������� ��
            {
                GameManager.Data.Pre_100.CNT += 1;
                GameManager.Data.Pre_100.USE = false;
                Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
            }
            else // ���� ����ϱ�� ���� �ʾ��� ��
            {
                if (GameManager.Data.Pre_100.CNT > 0)
                {
                    GameManager.Data.Pre_100.CNT -= 1;
                    GameManager.Data.Pre_100.USE = true;
                    Shop_Owner.text = "����, �þƳ��� �����̾�.";
                }
                else if (GameManager.Data.Gold > normal_price)
                {
                    GameManager.Data.Gold -= normal_price;
                    GameManager.Data.Pre_100.USE = true;
                    Shop_Owner.text = "���� �����̾�.";
                }
            }
            Check_Select();
            Info();
        }
    }

    public void Buy_300() //300���� �޸��� �̿��
    {
        if (GameManager.Data.Pre_100.USE)
        {
            Shop_Owner.text = "... ǳ���� �ູ�� �� ������ ������.";
        }
        else
        {
            if (GameManager.Data.Pre_300.USE) // ����ϱ�� �������� ��
            {
                GameManager.Data.Pre_300.CNT += 1;
                GameManager.Data.Pre_300.USE = false;
                Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
            }
            else // ���� ����ϱ�� ���� �ʾ��� ��
            {
                if (GameManager.Data.Pre_300.CNT > 0)
                {
                    GameManager.Data.Pre_300.CNT -= 1;
                    GameManager.Data.Pre_300.USE = true;
                    Shop_Owner.text = "����, �þƳ��� �����̾�.";
                }
                else if (GameManager.Data.Gold > normal_price)
                {
                    GameManager.Data.Gold -= normal_price;
                    GameManager.Data.Pre_300.USE = true;
                    Shop_Owner.text = "���� �����̾�.";
                }
            }
            Check_Select();
            Info();
        }
        
    }

    public void Buy_Skil() //���� ��Ƽ�� ��ų ���ű�
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            int idx = Random.Range(1, Enum.GetNames(typeof(DataManager.Active_Skil)).Length);
            GameManager.Data.Pre_Active = (DataManager.Active_Skil)idx;
        }
        Info();
    }

    public void Buy_Random() // �α� �α� ���� ������ ������ ��í~
    {
        if (GameManager.Data.Gold > random_price)
        {
            GameManager.Data.Gold -= random_price;
            int idx = Random.Range(1, Enum.GetNames(typeof(DataManager.Random_Item)).Length);
            GameManager.Data.Pre_Random = (DataManager.Random_Item)idx;
        }
        Info();
    }

}
