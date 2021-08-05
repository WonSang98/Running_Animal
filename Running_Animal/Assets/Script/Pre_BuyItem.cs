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

    Text[] temp_info = new Text[6];

    private void Start()
    {
        //TEMP
        temp_info[0] = GameObject.Find("UI/Button_HP/Text_HP").GetComponent<Text>();
        temp_info[1] = GameObject.Find("UI/Button_Shield/Text_Shield").GetComponent<Text>();
        temp_info[2] = GameObject.Find("UI/Button_RUN0/Text_RUN0").GetComponent<Text>();
        temp_info[3] = GameObject.Find("UI/Button_RUN1/Text_RUN1").GetComponent<Text>();
        temp_info[4] = GameObject.Find("UI/Button_ACTIVE/Text_ACTIVE").GetComponent<Text>();
        temp_info[5] = GameObject.Find("UI/Text_RandomEFF").GetComponent<Text>();
        //TEMP

        // 맵이 Forest일때
        for (int i = 0; i < 255; i++)
        {
            GameManager.Data.pattern.Add(i);
        }

        GameManager.Data.pattern = GameManager.Instance.ShuffleList(GameManager.Data.pattern);
    }

    public void Buy_HP() // 체력 10% 증가.
    {
        if(GameManager.Data.Gold > normal_price && !GameManager.Data.Pre_HP)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_HP = true;
            temp_info[0].text = $"체력{GameManager.Data.max_hp * 1.1f}";
            GameManager.Instance.Save();
        }
    }

    public void Buy_Shield() // 시작 시 1회 쉴드
    {
        if (GameManager.Data.Gold > normal_price && !GameManager.Data.Pre_Shield)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_Shield = true;
            temp_info[1].text = "구매";
            GameManager.Instance.Save();
        }
    }

    public void Buy_100() //100미터 달리기 이용권
    {
        if (GameManager.Data.Gold > normal_price && !(GameManager.Data.Pre_100 || GameManager.Data.Pre_300))
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_100 = true;
            temp_info[2].text = "구매";
            GameManager.Instance.Save();
        }
    }

    public void Buy_300() //300미터 달리기 이용권
    {
        if (GameManager.Data.Gold > normal_price && !(GameManager.Data.Pre_100 || GameManager.Data.Pre_300))
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_300 = true;
            temp_info[3].text = "구매";
            GameManager.Instance.Save();
        }
    }

    public void Buy_Skil() //랜덤 액티브 스킬 구매권
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            int idx = Random.Range(1, Enum.GetNames(typeof(DataManager.Active_Skil)).Length);
            GameManager.Data.active = (DataManager.Active_Skil)idx;
            temp_info[4].text = $"{GameManager.Data.active}";
            GameManager.Instance.Save();
        }
    }

    public void Buy_Random() // 두근 두근 랜덤 시작전 아이템 갓챠~
    {
        if (GameManager.Data.Gold > random_price)
        {
            GameManager.Data.Gold -= random_price;
            int idx = Random.Range(1, Enum.GetNames(typeof(DataManager.Random_Item)).Length);
            GameManager.Data.Pre_Random = (DataManager.Random_Item)idx;
            temp_info[5].text = $"{(DataManager.Random_Item)Enum.ToObject(typeof(DataManager.Random_Item), idx)}";
            GameManager.Instance.Save();
        }
    }

}
