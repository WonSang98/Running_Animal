using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ControlPreItem : MonoBehaviour
{
    AudioClip clip;
    AudioClip clip1;
    AudioClip clip2;
    enum ID
    {
        HP = 0,
        Shield,
        RUN5,
        RUN10,
        Active,
        Gift
    }
    static short Num_ID = 6;

    public string[] Info_RI =
    {   "None",
        "�ִ�ü�� 15% ����",
        "�ִ�ü�� 30% ����",
        "��� 5 ����",
        "��� 10 ����",
        "�ӵ� 15% ����",
        "�ӵ� 30% ����",
        "���� 20% ����",
        "���� 40% ����",
        "��� ȹ�淮 25% ����",
        "��� ȹ�淮 50% ����",
        "�޺� 2��",
        "�޺� 3��",
        "���� Ƚ�� 1ȸ ����",
        "���� 10% ����",
        "���� 15% ����",
        "����ġ 2��"
    };

    public string[] Info_AS =
    {
        "None",
        "����",
        "����",
        "����ȭ",
        "ȸ��",
        "�ٽ�, �� �� ��",
        "Ȳ���� ��",
        "������ �ð�",
        "�� ���� ���� �� ����",
        "����"
    };


    // ����
    static int normal_price = 500;
    static int random_price = 1000;


    Button[] Item_Button;
    GameObject[] Item_Check;
    Text[] Item_Cnt;
    Text Shop_Owner;
    Text Text_info;

    private void Start()
    {
        GameManager.Instance.Load();
        var path_button = "UI/Scroll View_Item/Viewport/Content/Button_";

        Item_Button = new Button[Num_ID];
        Item_Check = new GameObject[Num_ID - 2];
        Item_Cnt = new Text[Num_ID - 2];
        for (short i = 0; i < Num_ID; i++)
        {
            short temp = i;
            Item_Button[temp] = GameObject.Find(path_button + ((ID)(temp)).ToString()).gameObject.GetComponent<Button>();
            Item_Button[i].onClick.AddListener(() => Buy(temp));
            if (temp < Num_ID - 2)
            {
                Item_Check[temp] = GameObject.Find(path_button + ((ID)(temp)).ToString() + "/Image_SELECT").gameObject;
                Item_Cnt[temp] = GameObject.Find(path_button + ((ID)(temp)).ToString() + "/Text_CNT").GetComponent<Text>();
            }
        }

        Shop_Owner = GameObject.Find("UI/Panel_Hi/Text").GetComponent<Text>();
        Text_info = GameObject.Find("UI/Panel_Info/Text").GetComponent<Text>();

        Check_Select();
        Info();
        LoadSound();

    }
    public void Info()
    {
        Text_info.text = "";
        if (GameManager.Data.PreItem.Pre_HP.USE)
        {
            Text_info.text += $"ȯ���� ȯ���� �����߽��ϴ�.\n";
        }

        if (GameManager.Data.PreItem.Pre_Shield.USE)
        {
            Text_info.text += $"���κ��� ��ȣ�� �޾ҽ��ϴ�.\n";
        }

        if (GameManager.Data.PreItem.Pre_100.USE)
        {
            Text_info.text += $"ǳ�����κ��� ���� �ູ�� �޾ҽ��ϴ�.\n";
        }

        if (GameManager.Data.PreItem.Pre_300.USE)
        {
            Text_info.text += $"ǳ�����κ��� ū �ູ�� �޾ҽ��ϴ�.\n";
        }
        if (GameManager.Data.PreItem.Pre_Active != Active.ACTIVE_CODE.None)
        {
            Text_info.text += $"���� ������ �Ծ����ϴ�.\n'{Info_AS[(int)GameManager.Data.PreItem.Pre_Active]}' �ɷ��� ������ϴ�.\n";
        }

        if (GameManager.Data.PreItem.Pre_Random != PreItem.Random_Item.None)
        {
            Text_info.text += $"ȯ�����κ��� ������ �޾ҽ��ϴ�.\n'{Info_RI[(int)GameManager.Data.PreItem.Pre_Random]}' �� �˴ϴ�.";
        }
        GameManager.Instance.Save();
    }
    public void Check_Select()
    {
        if (GameManager.Data.PreItem.Pre_HP.USE)
        {
            Item_Check[0].SetActive(true);
        }
        else
        {
            Item_Check[0].SetActive(false);
        }

        if (GameManager.Data.PreItem.Pre_Shield.USE)
        {
            Item_Check[1].SetActive(true);
        }
        else
        {
            Item_Check[1].SetActive(false);
        }
        if (GameManager.Data.PreItem.Pre_100.USE)
        {
            Item_Check[2].SetActive(true);
        }
        else
        {
            Item_Check[2].SetActive(false);
        }
        if (GameManager.Data.PreItem.Pre_300.USE)
        {
            Item_Check[3].SetActive(true);
        }
        else
        {
            Item_Check[3].SetActive(false);
        }
    }

    void Buy(short i) // ü�� 10% ����.
    {
        switch (i)
        {
            case 0:
                if (GameManager.Data.PreItem.Pre_HP.USE) // ����ϱ�� �������� ��
                {
                    GameManager.Data.PreItem.Pre_HP.CNT += 1;
                    GameManager.Data.PreItem.Pre_HP.USE = false;
                    GameManager.Sound.SFXPlay(clip2);
                    Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
                }
                else // ���� ����ϱ�� ���� �ʾ��� ��
                {
                    if (GameManager.Data.PreItem.Pre_HP.CNT > 0)
                    {
                        GameManager.Data.PreItem.Pre_HP.CNT -= 1;
                        GameManager.Data.PreItem.Pre_HP.USE = true;
                        GameManager.Sound.SFXPlay(clip);
                        Shop_Owner.text = "����, �þƳ��� �����̾�.";
                    }
                    else if (GameManager.Data.Money.Gold >= normal_price)
                    {
                        GameManager.Data.Money.Gold -= normal_price;
                        GameManager.Data.PreItem.Pre_HP.USE = true;
                        GameManager.Sound.SFXPlay(clip1);
                        Shop_Owner.text = "���� �����̾�.";
                    }
                    else
                    {
                        Shop_Owner.text = "�� ��Ÿ� �׸� ������ �����ְ�.";
                    }
                }
                Item_Cnt[i].text = $"{GameManager.Data.PreItem.Pre_HP.CNT}";
                break;
            case 1:
                if (GameManager.Data.PreItem.Pre_Shield.USE) // ����ϱ�� �������� ��
                {
                    GameManager.Data.PreItem.Pre_Shield.CNT += 1;
                    GameManager.Data.PreItem.Pre_Shield.USE = false;
                    GameManager.Sound.SFXPlay(clip2);
                    Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
                }
                else // ���� ����ϱ�� ���� �ʾ��� ��
                {
                    if (GameManager.Data.PreItem.Pre_Shield.CNT > 0)
                    {
                        GameManager.Data.PreItem.Pre_Shield.CNT -= 1;
                        GameManager.Data.PreItem.Pre_Shield.USE = true;
                        GameManager.Sound.SFXPlay(clip);
                        Shop_Owner.text = "����, �þƳ��� �����̾�.";
                    }
                    else if (GameManager.Data.Money.Gold >= normal_price)
                    {
                        GameManager.Data.Money.Gold -= normal_price;
                        GameManager.Data.PreItem.Pre_Shield.USE = true;
                        GameManager.Sound.SFXPlay(clip1);
                        Shop_Owner.text = "���� �����̾�.";
                    }
                    else
                    {
                        Shop_Owner.text = "����, ��� ���� ����?";
                    }
                }
                Item_Cnt[i].text = $"{GameManager.Data.PreItem.Pre_Shield.CNT}";
                break;
            case 2:
                if (GameManager.Data.PreItem.Pre_300.USE)
                {
                    Shop_Owner.text = "... ǳ���� �ູ�� �� ������ ������.";
                }
                else
                {
                    if (GameManager.Data.PreItem.Pre_100.USE) // ����ϱ�� �������� ��
                    {
                        GameManager.Data.PreItem.Pre_100.CNT += 1;
                        GameManager.Data.PreItem.Pre_100.USE = false;
                        GameManager.Sound.SFXPlay(clip2);
                        Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
                    }
                    else // ���� ����ϱ�� ���� �ʾ��� ��
                    {
                        if (GameManager.Data.PreItem.Pre_100.CNT > 0)
                        {
                            GameManager.Data.PreItem.Pre_100.CNT -= 1;
                            GameManager.Data.PreItem.Pre_100.USE = true;
                            GameManager.Sound.SFXPlay(clip);
                            Shop_Owner.text = "����, �þƳ��� �����̾�.";
                        }
                        else if (GameManager.Data.Money.Gold >= normal_price)
                        {
                            GameManager.Data.Money.Gold -= normal_price;
                            GameManager.Data.PreItem.Pre_100.USE = true;
                            GameManager.Sound.SFXPlay(clip1);
                            Shop_Owner.text = "���� �����̾�.";
                        }
                        else
                        {
                            Shop_Owner.text = "�ູ�� ��������, ������ ���̶��.";
                        }
                    }
                }
                Item_Cnt[i].text = $"{GameManager.Data.PreItem.Pre_100.CNT}";
                break;
            case 3:
                if (GameManager.Data.PreItem.Pre_100.USE)
                {
                    Shop_Owner.text = "... ǳ���� �ູ�� �� ������ ������.";
                }
                else
                {
                    if (GameManager.Data.PreItem.Pre_300.USE) // ����ϱ�� �������� ��
                    {
                        GameManager.Data.PreItem.Pre_300.CNT += 1;
                        GameManager.Data.PreItem.Pre_300.USE = false;
                        GameManager.Sound.SFXPlay(clip2);
                        Shop_Owner.text = "���߿� �ٽ� ã���� �����.";
                    }
                    else // ���� ����ϱ�� ���� �ʾ��� ��
                    {
                        if (GameManager.Data.PreItem.Pre_300.CNT > 0)
                        {
                            GameManager.Data.PreItem.Pre_300.CNT -= 1;
                            GameManager.Data.PreItem.Pre_300.USE = true;
                            GameManager.Sound.SFXPlay(clip);
                            Shop_Owner.text = "����, �þƳ��� �����̾�.";
                        }
                        else if (GameManager.Data.Money.Gold >= normal_price)
                        {
                            GameManager.Data.Money.Gold -= normal_price;
                            GameManager.Data.PreItem.Pre_300.USE = true;
                            GameManager.Sound.SFXPlay(clip1);
                            Shop_Owner.text = "���� �����̾�.";
                        }
                        else
                        {
                            Shop_Owner.text = "�ູ�� ��������, ������ ���̶��.";
                        }
                    }
                }
                Item_Cnt[i].text = $"{GameManager.Data.PreItem.Pre_300.CNT}";
                break;
            case 4:
                if (GameManager.Data.Money.Gold >= normal_price)
                {
                    GameManager.Data.Money.Gold -= normal_price;
                    int idx = Random.Range(1, Enum.GetNames(typeof(Active.ACTIVE_CODE)).Length);
                    GameManager.Data.PreItem.Pre_Active = (Active.ACTIVE_CODE)idx;
                    GameManager.Sound.SFXPlay(clip1);
                    Shop_Owner.text = "�츮 �� ���ø��� �?";
                }
                else
                {
                    Shop_Owner.text = "������ �ʴ� ��, ������ ����.";
                }
                break;
            case 5:
                if (GameManager.Data.Money.Gold >= random_price)
                {
                    GameManager.Data.Money.Gold -= random_price;
                    int idx = Random.Range(1, Enum.GetNames(typeof(PreItem.Random_Item)).Length);
                    GameManager.Data.PreItem.Pre_Random = (PreItem.Random_Item)idx;
                    GameManager.Sound.SFXPlay(clip1);
                    Shop_Owner.text = "���� �� �� ���Գ�?";
                }
                else
                {
                    Shop_Owner.text = "�����ϱ�, ���� ��¥�� ����.";
                }
                break;

        }
        Check_Select();
        Info();
    }
    void LoadSound() //Sound Resoucres ��� ã�ƿͼ� �ҷ��ͳ���.
    {
        clip = Resources.Load<AudioClip>("Sound/Common/004_Manu_Sound2");
        clip1 = Resources.Load<AudioClip>("Sound/Common/005_Cash");
        clip2 = Resources.Load<AudioClip>("Sound/Common/001_CharacterUp");
    }
}