using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTalent : MonoBehaviour
{
    Text[] Levels = new Text[4]; // ��� �� ���� ǥ��
    Text[] Infos = new Text[4]; // ��� �� ���� �����ġ ǥ��.
    Text Upgrade; // ���׷��̵� ��ư �ؽ�Ʈ

    short Select; // ���� ������ ���׷��̵� ����
    Button[] Kinds = new Button[4]; // HP , DEF, LUKm, RESTORE � �� ���� �����ϴ� ��ư
    GameObject[] Effects = new GameObject[4];

    Button LevelUP;

    AudioClip clip;
    AudioClip clip2;
    //Ʃ�丮�� ����
    GameObject Canvas_Tuto;
    GameObject[] Text_Tuto;
    GameObject Button_Tuto;
    int cnt;
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
            Effects[i] = GameObject.Find("UI/Select_Effect").transform.Find("Effect_" + i.ToString()).gameObject;
            Effects[i].SetActive(false);
        }

        //Ʃ�丮��
        Canvas_Tuto = GameObject.Find("UI-Tutorial").transform.Find("Panel").gameObject;
        Text_Tuto = new GameObject[3];
        Button_Tuto = GameObject.Find("UI-Tutorial").transform.Find("Button_OK").gameObject;
        for (int i = 0; i < 1; i++)
        {
            Text_Tuto[i] = GameObject.Find("UI-Tutorial").transform.Find("Text" + i.ToString()).gameObject;
        }
        if (GameManager.Data.TutoData.tuto_talent == false)
        {
            cnt = 0;
            Canvas_Tuto.SetActive(true);
            for (int i = 0; i < 1; i++) Text_Tuto[i].SetActive(false);
            Text_Tuto[0].SetActive(true);
            Button_Tuto.SetActive(true);
            Button_Tuto.GetComponent<Button>().onClick.AddListener(() => NextTuto());
        }
        else
        {
            Canvas_Tuto.SetActive(false);
            for (int i = 0; i < 1; i++) Text_Tuto[i].SetActive(false);
            Button_Tuto.SetActive(false);
        }

        Show_info();
        LoadSound();
    }

    void NextTuto()
    {
        cnt += 1;
        for (int i = 0; i < 1; i++) Text_Tuto[i].SetActive(false);
        if (cnt >= 1)
        {
            Canvas_Tuto.SetActive(false);
            Button_Tuto.SetActive(false);
            GameManager.Data.TutoData.tuto_talent = true;
        }
        else
        {
            Text_Tuto[cnt].SetActive(true);
        }
    }

    void Choice(int i)
    {
        if (Select == i)
        {
            Select = -1;
            Upgrade.text = "ü������";
        }
        else
        {
            switch (i)
            {
                case 0:
                    if (GameManager.Data.Talent.HP.level == 10)
                    {
                        Upgrade.text = "LV.MAX";
                    }
                    else
                    {
                        Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.HP.level]}XG";
                    }
                    GameManager.Sound.SFXPlay(clip);
                    break;
                case 1:
                    if (GameManager.Data.Talent.DEF.level == 10)
                    {
                        Upgrade.text = "LV.MAX";
                    }
                    else
                    {
                        Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.DEF.level]}XG";
                    }
                    GameManager.Sound.SFXPlay(clip);
                    break;
                case 2:
                    if (GameManager.Data.Talent.LUK.level == 10)
                    {
                        Upgrade.text = "LV.MAX";
                    }
                    else
                    {
                        Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.LUK.level]}XG";
                    }
                    GameManager.Sound.SFXPlay(clip);
                    break;
                case 3:
                    if (GameManager.Data.Talent.RESTORE.level == 10)
                    {
                        Upgrade.text = "LV.MAX";
                    }
                    else
                    {
                        Upgrade.text = $"{Talent.COST[GameManager.Data.Talent.RESTORE.level]}XG";
                    }
                    GameManager.Sound.SFXPlay(clip);
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
                    if (GameManager.Data.Talent.HP.level < 10 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.HP.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.HP.level];
                        GameManager.Data.Talent.HP.level += 1;
                        GameManager.Data.Talent.HP.value = Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.HP.level];

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
                    if (GameManager.Data.Talent.DEF.level < 10 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.DEF.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.DEF.level];
                        GameManager.Data.Talent.DEF.level += 1;
                        GameManager.Data.Talent.DEF.value = Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.DEF.level];

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
                    if (GameManager.Data.Talent.LUK.level < 10 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.LUK.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.LUK.level];
                        GameManager.Data.Talent.LUK.level += 1;
                        GameManager.Data.Talent.LUK.value = (short)Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.LUK.level];

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
                    if (GameManager.Data.Talent.RESTORE.level < 10 && GameManager.Data.Money.Gold > Talent.COST[GameManager.Data.Talent.RESTORE.level])
                    {
                        GameManager.Data.Money.Gold -= Talent.COST[GameManager.Data.Talent.RESTORE.level];
                        GameManager.Data.Talent.RESTORE.level += 1;
                        GameManager.Data.Talent.RESTORE.value = Talent.UPGRADE_POWER[Select, GameManager.Data.Talent.RESTORE.level];

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
        GameManager.Sound.SFXPlay(clip2);
    }

    void Show_info()
    {
        Levels[0].text = (GameManager.Data.Talent.HP.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.HP.level}";
        Levels[1].text = (GameManager.Data.Talent.DEF.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.DEF.level}";
        Levels[2].text = (GameManager.Data.Talent.LUK.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.LUK.level}";
        Levels[3].text = (GameManager.Data.Talent.RESTORE.level == 10) ? "LV. MAX" : $"LV. {GameManager.Data.Talent.RESTORE.level}";
        Infos[0].text = $"ü  ��\n{Talent.UPGRADE_POWER[0, GameManager.Data.Talent.HP.level]} ����.";
        Infos[1].text = $"���� \n{Talent.UPGRADE_POWER[1, GameManager.Data.Talent.DEF.level] * 100}% ����.";
        Infos[2].text = $"��  ��\n{Talent.UPGRADE_POWER[2, GameManager.Data.Talent.LUK.level]} ����.";
        Infos[3].text = $"ȸ����\n{Talent.UPGRADE_POWER[3, GameManager.Data.Talent.RESTORE.level] * 100}% ����.";
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
    void LoadSound()
    {
        clip = Resources.Load<AudioClip>("Sound/Common/004_Manu_Sound2");
        clip2 = Resources.Load<AudioClip>("Sound/Common/007_Stamp");
    }

}
