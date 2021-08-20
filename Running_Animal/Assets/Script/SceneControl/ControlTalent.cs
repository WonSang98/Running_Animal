using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTalent : MonoBehaviour
{
    Text[] Levels = new Text[4]; // 재능 별 레벨 표시
    Text[] Infos = new Text[4]; // 재능 별 현재 적용수치 표현.
    Text Upgrade; // 업그레이드 버튼 텍스트

    short Select; // 현재 선택한 업그레이드 종류
    Button[] Kinds = new Button[4]; // HP , DEF, LUKm, RESTORE 어떤 거 고를지 선택하는 버튼
    GameObject[] Effects = new GameObject[4];

    Button LevelUP;

    

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
        if (Select == i)
        {
            Select = -1;
            Upgrade.text = "체질개선";
        }
        else
        {
            switch (i)
            {
                case 0:
                    Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.HP.level]}XG";
                    break;
                case 1:
                    Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.DEF.level]}XG";
                    break;
                case 2:
                    Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.LUK.level]}XG";
                    break;
                case 3:
                    Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.RESTORE.level]}XG";
                    break;

            }
            Select = (short)i;
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
            switch (Select)
            {
                case 0: //HP
                    if (GameManager.Data.Talent.HP.level < 9 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.HP.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.HP.level];
                        GameManager.Data.Talent.HP.value = Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.HP.level];
                        GameManager.Data.Talent.HP.level += 1;

                        if (GameManager.Data.Talent.HP.level == 10)
                        {
                            Upgrade.text = "LV.MAX";
                        }
                        else
                        {
                            Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.HP.level]}XG";
                        }
                    }
                        break;
                case 1: //DEF
                    if (GameManager.Data.Talent.DEF.level < 9 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.DEF.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.DEF.level];
                        GameManager.Data.Talent.DEF.value = Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.DEF.level];
                        GameManager.Data.Talent.DEF.level += 1;

                        if (GameManager.Data.Talent.DEF.level == 10)
                        {
                            Upgrade.text = "LV.MAX";
                        }
                        else
                        {
                            Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.DEF.level]}XG";
                        }
                    }
                    break;
                case 2: //LUK
                    if (GameManager.Data.Talent.LUK.level < 9 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.LUK.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.LUK.level];
                        GameManager.Data.Talent.LUK.value = (short)Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.LUK.level];
                        GameManager.Data.Talent.LUK.level += 1;

                        if (GameManager.Data.Talent.LUK.level == 10)
                        {
                            Upgrade.text = "LV.MAX";
                        }
                        else
                        {
                            Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.LUK.level]}XG";
                        }
                    }
                    break;
                case 3: //RESTORE
                    if (GameManager.Data.Talent.RESTORE.level < 9 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.RESTORE.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.RESTORE.level];
                        GameManager.Data.Talent.RESTORE.value = Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.RESTORE.level];
                        GameManager.Data.Talent.RESTORE.level += 1;

                        if (GameManager.Data.Talent.RESTORE.level == 10)
                        {
                            Upgrade.text = "LV.MAX";
                        }
                        else
                        {
                            Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.RESTORE.level]}XG";
                        }
                    }
                    break;
            }
            Show_info();
        }
    }

    void Show_info()
    {
        Levels[0].text = (GameManager.Data.Talent.HP.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.HP.level}";
        Levels[1].text = (GameManager.Data.Talent.DEF.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.DEF.level}";
        Levels[2].text = (GameManager.Data.Talent.LUK.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.LUK.level}";
        Levels[3].text = (GameManager.Data.Talent.RESTORE.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.RESTORE.level}";
        Infos[0].text = $"체  력\n{Talent.UPGRADE_POWER[0, GameManager.Data.Talent.HP.level]} 증가.";
        Infos[1].text = $"방어율\n{Talent.UPGRADE_POWER[1, GameManager.Data.Talent.DEF.level]} 증가.";
        Infos[2].text = $"행  운\n{Talent.UPGRADE_POWER[2, GameManager.Data.Talent.LUK.level]} 증가.";
        Infos[3].text = $"회복율\n{Talent.UPGRADE_POWER[3, GameManager.Data.Talent.RESTORE.level]} 증가.";
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
