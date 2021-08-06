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
        // 맵이 Forest일때
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
            Text_info.text += $"환웅의 환약을 섭취했습니다.\n 체력이 늘어 {GameManager.Data.max_hp * 1.1f}가 되었습니다.\n";
        }

        if (GameManager.Data.Pre_Shield.USE)
        {
            Text_info.text += $"우사로부터 가호를 받았습니다.\n ";
        }
       
        if (GameManager.Data.Pre_100.USE)
        {
            Text_info.text += $"풍백으로부터 작은 축복을 받았습니다.\n ";
        }
       
        if (GameManager.Data.Pre_300.USE)
        {
            Text_info.text += $"풍백으로부터 큰 축복을 받았습니다.\n ";
        }
        Debug.Log(GameManager.Data.active);
        if(GameManager.Data.Pre_Active != DataManager.Active_Skil.None)
        {
            Text_info.text += $"쑥과 마늘을 먹었습니다. \n '{GameManager.Data.Pre_Active}' 능력이 생겼습니다.";
        }

        if (GameManager.Data.Pre_Random != DataManager.Random_Item.None)
        {
            Text_info.text += $"환인으로부터 선물을 받았습니다. \n {GameManager.Data.Pre_Random} 가 됩니다.";
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
    
    public void Buy_HP() // 체력 10% 증가.
    {
        if(GameManager.Data.Pre_HP.USE) // 사용하기로 선택했을 시
        {
            GameManager.Data.Pre_HP.CNT += 1;
            GameManager.Data.Pre_HP.USE = false;
            Shop_Owner.text = "나중에 다시 찾으러 오라고.";
        }
        else // 아직 사용하기로 하지 않았을 때
        {
            if(GameManager.Data.Pre_HP.CNT > 0)
            {
                GameManager.Data.Pre_HP.CNT -= 1;
                GameManager.Data.Pre_HP.USE = true;
                Shop_Owner.text = "여기, 맡아놨던 물건이야.";
            }
            else if(GameManager.Data.Gold > normal_price)
            {
                GameManager.Data.Gold -= normal_price;
                GameManager.Data.Pre_HP.USE = true;
                Shop_Owner.text = "좋은 선택이야.";
            }
        }
        Check_Select();
        Info();
    }

    public void Buy_Shield() // 시작 시 1회 쉴드
    {
        if (GameManager.Data.Pre_Shield.USE) // 사용하기로 선택했을 시
        {
            GameManager.Data.Pre_Shield.CNT += 1;
            GameManager.Data.Pre_Shield.USE = false;
            Shop_Owner.text = "나중에 다시 찾으러 오라고.";
        }
        else // 아직 사용하기로 하지 않았을 때
        {
            if (GameManager.Data.Pre_Shield.CNT > 0)
            {
                GameManager.Data.Pre_Shield.CNT -= 1;
                GameManager.Data.Pre_Shield.USE = true;
                Shop_Owner.text = "여기, 맡아놨던 물건이야.";
            }
            else if (GameManager.Data.Gold > normal_price)
            {
                GameManager.Data.Gold -= normal_price;
                GameManager.Data.Pre_Shield.USE = true;
                Shop_Owner.text = "좋은 선택이야.";
            }
        }
        Check_Select();
        Info();
    }

    public void Buy_100() //100미터 달리기 이용권
    {
        if (GameManager.Data.Pre_300.USE)
        {
            Shop_Owner.text = "... 풍백의 축복은 한 가지만 가능해.";
        }
        else 
        { 
            if (GameManager.Data.Pre_100.USE) // 사용하기로 선택했을 시
            {
                GameManager.Data.Pre_100.CNT += 1;
                GameManager.Data.Pre_100.USE = false;
                Shop_Owner.text = "나중에 다시 찾으러 오라고.";
            }
            else // 아직 사용하기로 하지 않았을 때
            {
                if (GameManager.Data.Pre_100.CNT > 0)
                {
                    GameManager.Data.Pre_100.CNT -= 1;
                    GameManager.Data.Pre_100.USE = true;
                    Shop_Owner.text = "여기, 맡아놨던 물건이야.";
                }
                else if (GameManager.Data.Gold > normal_price)
                {
                    GameManager.Data.Gold -= normal_price;
                    GameManager.Data.Pre_100.USE = true;
                    Shop_Owner.text = "좋은 선택이야.";
                }
            }
            Check_Select();
            Info();
        }
    }

    public void Buy_300() //300미터 달리기 이용권
    {
        if (GameManager.Data.Pre_100.USE)
        {
            Shop_Owner.text = "... 풍백의 축복은 한 가지만 가능해.";
        }
        else
        {
            if (GameManager.Data.Pre_300.USE) // 사용하기로 선택했을 시
            {
                GameManager.Data.Pre_300.CNT += 1;
                GameManager.Data.Pre_300.USE = false;
                Shop_Owner.text = "나중에 다시 찾으러 오라고.";
            }
            else // 아직 사용하기로 하지 않았을 때
            {
                if (GameManager.Data.Pre_300.CNT > 0)
                {
                    GameManager.Data.Pre_300.CNT -= 1;
                    GameManager.Data.Pre_300.USE = true;
                    Shop_Owner.text = "여기, 맡아놨던 물건이야.";
                }
                else if (GameManager.Data.Gold > normal_price)
                {
                    GameManager.Data.Gold -= normal_price;
                    GameManager.Data.Pre_300.USE = true;
                    Shop_Owner.text = "좋은 선택이야.";
                }
            }
            Check_Select();
            Info();
        }
        
    }

    public void Buy_Skil() //랜덤 액티브 스킬 구매권
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            int idx = Random.Range(1, Enum.GetNames(typeof(DataManager.Active_Skil)).Length);
            GameManager.Data.Pre_Active = (DataManager.Active_Skil)idx;
        }
        Info();
    }

    public void Buy_Random() // 두근 두근 랜덤 시작전 아이템 갓챠~
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
