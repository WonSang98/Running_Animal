using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentManager : MonoBehaviour
{
    Text[] Levels = new Text[4]; // 재능 별 레벨 표시
    Text[] Infos = new Text[4]; // 재능 별 현재 적용수치 표현.
    Text Upgrade; // 업그레이드 버튼 텍스트

    short Select; // 현재 선택한 업그레이드 종류
    Button[] Kinds = new Button[4]; // HP , DEF, LUKm, RESTORE 어떤 거 고를지 선택하는 버튼
    GameObject[] Effects = new GameObject[4];

    Button LevelUP;

    // 재능 업그레이드 비용
    int[] costs = { 500, 1000, 1500, 2000, 2500, 3000, 4000, 5000, 6000, 7000};
    // 재능 업그레이드 시 능력치
    float[,] Ability =
    {
        { 20, 40, 60, 80, 100, 120, 140, 160, 180, 200}, // HP
        { 0.01f, 0.02f, 0.03f, 0.04f, 0.05f, 0.06f, 0.07f, 0.08f, 0.09f, 0.10f}, // DEF
        { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, // LUK
        { 1.1f, 1,15f, 1.2f, 1.25f, 1.3f, 1.35f, 1.4f, 1.45f, 1.5f} // RESTORE
    };

    void Start()
    {
        Select = -1;
        Upgrade = GameObject.Find("UI/Button_Upgrade/Text").GetComponent<Text>();
        LevelUP = GameObject.Find("UI/Button_Upgrade").GetComponent<Button>();
        LevelUP.onClick.AddListener(Upgrade_Button);
        for (int i = 0; i < 4; i++)
        {
            int temp = i;
            Levels[i] = GameObject.Find("UI/Panel/Panel_" + i.ToString() + "/Level/Text").GetComponent<Text>();
            Infos[i] = GameObject.Find("UI/Panel/Panel_" + i.ToString() + "/Info/Text").GetComponent<Text>();
            Kinds[i] = GameObject.Find("UI/Button_Ability/Button_" + i.ToString()).GetComponent<Button>();
            Kinds[i].onClick.AddListener(() => Choice(temp));
            Debug.Log("i is...." + i);
            Effects[i] = GameObject.Find("UI/Select_Effect").transform.Find("Effect_" + i.ToString()).gameObject;
            Effects[i].SetActive(false);
        }
        Show_info();
    }
    void Choice(int i)
    {
        Debug.Log("선택된 버튼" + i);
        if(Select == i)
        {
            Select = -1;
            Upgrade.text = "체질개선";
        }
        else
        {
            Select = (short)i;
            Upgrade.text = $"{costs[GameManager.Data.Talent_LV[i] - 1]}XG";
            StartCoroutine(Shine(i));
        }

    }

    void Upgrade_Button()
    {
        if (Select == -1)
        {

        }
        else
        {
            if (GameManager.Data.Talent_LV[Select] < 10 && GameManager.Data.Gold > costs[GameManager.Data.Talent_LV[Select] - 1])
            {
                GameManager.Data.Gold -= costs[GameManager.Data.Talent_LV[Select] - 1];
                GameManager.Data.Talent_LV[Select] += 1;

                switch (Select)
                {
                    case 0: //HP
                        GameManager.Data.Talent_HP = Ability[Select, GameManager.Data.Talent_LV[Select] - 1];
                        break;
                    case 1: //DEF
                        GameManager.Data.Talent_DEF = Ability[Select, GameManager.Data.Talent_LV[Select] - 1];
                        break;
                    case 2: //LUK
                        GameManager.Data.Talent_LUK = (int)Ability[Select, GameManager.Data.Talent_LV[Select] - 1];
                        break;
                    case 3: //RESTORE
                        GameManager.Data.Talent_Restore = Ability[Select, GameManager.Data.Talent_LV[Select] - 1];
                        break;
                }
                if (GameManager.Data.Talent_LV[Select] == 10)
                {
                    Upgrade.text = "LV.MAX";
                }
                else
                {
                    Upgrade.text = $"{costs[GameManager.Data.Talent_LV[Select] - 1]}XG";
                }
                Show_info();
            }
        }
    }

    void Show_info()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameManager.Data.Talent_LV[i] != 10)
            {
                Levels[i].text = $"LV. {GameManager.Data.Talent_LV[i]}";
            }
            else
            {
                Levels[i].text = "LV. MAX";
            }
        }
        Infos[0].text = $"체  력\n{Ability[0, GameManager.Data.Talent_LV[0] - 1]} 증가.";
        Infos[1].text = $"방어율\n{Ability[1, GameManager.Data.Talent_LV[1] - 1]} 증가.";
        Infos[2].text = $"행  운\n{Ability[2, GameManager.Data.Talent_LV[2] - 1]} 증가.";
        Infos[3].text = $"회복율\n{Ability[3, GameManager.Data.Talent_LV[3] - 1]} 증가.";
    }
 
    IEnumerator Shine(int i)
    {
        short cnt_size = 0;
        short cnt_alpha = 0;
        short flag_size = -1;
        short flag_alpha = -1;
        Vector3 pre_size;
        Color pre_a;
        Effects[i].SetActive(true);
        while (Select == (short)i)
        {
            if(cnt_size == 100)
            {
                flag_size *= -1;
                cnt_size = 0;
            }

            if(cnt_alpha == 100)
            {
                flag_alpha *= -1;
                cnt_alpha = 0;
            }


            pre_size = Effects[i].GetComponent<RectTransform>().localScale;
            Effects[i].GetComponent<RectTransform>().localScale = new Vector3(pre_size.x + (flag_size * 0.0005f), pre_size.y + (flag_size * 0.0005f), 1);

            pre_a = Effects[i].GetComponent<Image>().color;
            pre_a.a += (0.004f * flag_alpha);
            Effects[i].GetComponent<Image>().color = pre_a;

            cnt_size++;
            cnt_alpha++;
            yield return new WaitForSeconds(0.008f);
        }
        pre_a = Effects[i].GetComponent<Image>().color;
        pre_a.a = 0.6f;
        pre_size = new Vector3(1, 1, 1);
        Effects[i].GetComponent<Image>().color = pre_a;
        Effects[i].GetComponent<RectTransform>().localScale = pre_size;
        Effects[i].SetActive(false);
    }

}
