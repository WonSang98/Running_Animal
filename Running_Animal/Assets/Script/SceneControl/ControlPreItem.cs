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
        "최대체력 15% 증가",
        "최대체력 30% 증가",
        "행운 5 증가",
        "행운 10 증가",
        "속도 15% 증가",
        "속도 30% 증가",
        "점프 20% 증가",
        "점프 40% 증가",
        "골드 획득량 25% 증가",
        "골드 획득량 50% 증가",
        "콤보 2배",
        "콤보 3배",
        "점프 횟수 1회 증가",
        "피해 10% 감소",
        "피해 15% 감소",
        "경험치 2배"
    };

    public string[] Info_AS =
    {
        "None",
        "방패",
        "점멸",
        "유령화",
        "회복",
        "다시, 또 한 번",
        "황금의 손",
        "영겁의 시간",
        "세 마리 같은 한 마리",
        "날개"
    };


    // 가격
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
            Text_info.text += $"환웅의 환약을 섭취했습니다.\n";
        }

        if (GameManager.Data.PreItem.Pre_Shield.USE)
        {
            Text_info.text += $"우사로부터 가호를 받았습니다.\n";
        }

        if (GameManager.Data.PreItem.Pre_100.USE)
        {
            Text_info.text += $"풍백으로부터 작은 축복을 받았습니다.\n";
        }

        if (GameManager.Data.PreItem.Pre_300.USE)
        {
            Text_info.text += $"풍백으로부터 큰 축복을 받았습니다.\n";
        }
        if (GameManager.Data.PreItem.Pre_Active != Active.ACTIVE_CODE.None)
        {
            Text_info.text += $"쑥과 마늘을 먹었습니다.\n'{Info_AS[(int)GameManager.Data.PreItem.Pre_Active]}' 능력이 생겼습니다.\n";
        }

        if (GameManager.Data.PreItem.Pre_Random != PreItem.Random_Item.None)
        {
            Text_info.text += $"환인으로부터 선물을 받았습니다.\n'{Info_RI[(int)GameManager.Data.PreItem.Pre_Random]}' 가 됩니다.";
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

    void Buy(short i) // 체력 10% 증가.
    {
        switch (i)
        {
            case 0:
                if (GameManager.Data.PreItem.Pre_HP.USE) // 사용하기로 선택했을 시
                {
                    GameManager.Data.PreItem.Pre_HP.CNT += 1;
                    GameManager.Data.PreItem.Pre_HP.USE = false;
                    GameManager.Sound.SFXPlay(clip2);
                    Shop_Owner.text = "나중에 다시 찾으러 오라고.";
                }
                else // 아직 사용하기로 하지 않았을 때
                {
                    if (GameManager.Data.PreItem.Pre_HP.CNT > 0)
                    {
                        GameManager.Data.PreItem.Pre_HP.CNT -= 1;
                        GameManager.Data.PreItem.Pre_HP.USE = true;
                        GameManager.Sound.SFXPlay(clip);
                        Shop_Owner.text = "여기, 맡아놨던 물건이야.";
                    }
                    else if (GameManager.Data.Money.Gold >= normal_price)
                    {
                        GameManager.Data.Money.Gold -= normal_price;
                        GameManager.Data.PreItem.Pre_HP.USE = true;
                        GameManager.Sound.SFXPlay(clip1);
                        Shop_Owner.text = "좋은 선택이야.";
                    }
                    else
                    {
                        Shop_Owner.text = "안 살거면 그만 만지고 나가주게.";
                    }
                }
                Item_Cnt[i].text = $"{GameManager.Data.PreItem.Pre_HP.CNT}";
                break;
            case 1:
                if (GameManager.Data.PreItem.Pre_Shield.USE) // 사용하기로 선택했을 시
                {
                    GameManager.Data.PreItem.Pre_Shield.CNT += 1;
                    GameManager.Data.PreItem.Pre_Shield.USE = false;
                    GameManager.Sound.SFXPlay(clip2);
                    Shop_Owner.text = "나중에 다시 찾으러 오라고.";
                }
                else // 아직 사용하기로 하지 않았을 때
                {
                    if (GameManager.Data.PreItem.Pre_Shield.CNT > 0)
                    {
                        GameManager.Data.PreItem.Pre_Shield.CNT -= 1;
                        GameManager.Data.PreItem.Pre_Shield.USE = true;
                        GameManager.Sound.SFXPlay(clip);
                        Shop_Owner.text = "여기, 맡아놨던 물건이야.";
                    }
                    else if (GameManager.Data.Money.Gold >= normal_price)
                    {
                        GameManager.Data.Money.Gold -= normal_price;
                        GameManager.Data.PreItem.Pre_Shield.USE = true;
                        GameManager.Sound.SFXPlay(clip1);
                        Shop_Owner.text = "좋은 선택이야.";
                    }
                    else
                    {
                        Shop_Owner.text = "뭐야, 당신 돈이 없군?";
                    }
                }
                Item_Cnt[i].text = $"{GameManager.Data.PreItem.Pre_Shield.CNT}";
                break;
            case 2:
                if (GameManager.Data.PreItem.Pre_300.USE)
                {
                    Shop_Owner.text = "... 풍백의 축복은 한 가지만 가능해.";
                }
                else
                {
                    if (GameManager.Data.PreItem.Pre_100.USE) // 사용하기로 선택했을 시
                    {
                        GameManager.Data.PreItem.Pre_100.CNT += 1;
                        GameManager.Data.PreItem.Pre_100.USE = false;
                        GameManager.Sound.SFXPlay(clip2);
                        Shop_Owner.text = "나중에 다시 찾으러 오라고.";
                    }
                    else // 아직 사용하기로 하지 않았을 때
                    {
                        if (GameManager.Data.PreItem.Pre_100.CNT > 0)
                        {
                            GameManager.Data.PreItem.Pre_100.CNT -= 1;
                            GameManager.Data.PreItem.Pre_100.USE = true;
                            GameManager.Sound.SFXPlay(clip);
                            Shop_Owner.text = "여기, 맡아놨던 물건이야.";
                        }
                        else if (GameManager.Data.Money.Gold >= normal_price)
                        {
                            GameManager.Data.Money.Gold -= normal_price;
                            GameManager.Data.PreItem.Pre_100.USE = true;
                            GameManager.Sound.SFXPlay(clip1);
                            Shop_Owner.text = "좋은 선택이야.";
                        }
                        else
                        {
                            Shop_Owner.text = "축복을 받으려면, 정성을 보이라고.";
                        }
                    }
                }
                Item_Cnt[i].text = $"{GameManager.Data.PreItem.Pre_100.CNT}";
                break;
            case 3:
                if (GameManager.Data.PreItem.Pre_100.USE)
                {
                    Shop_Owner.text = "... 풍백의 축복은 한 가지만 가능해.";
                }
                else
                {
                    if (GameManager.Data.PreItem.Pre_300.USE) // 사용하기로 선택했을 시
                    {
                        GameManager.Data.PreItem.Pre_300.CNT += 1;
                        GameManager.Data.PreItem.Pre_300.USE = false;
                        GameManager.Sound.SFXPlay(clip2);
                        Shop_Owner.text = "나중에 다시 찾으러 오라고.";
                    }
                    else // 아직 사용하기로 하지 않았을 때
                    {
                        if (GameManager.Data.PreItem.Pre_300.CNT > 0)
                        {
                            GameManager.Data.PreItem.Pre_300.CNT -= 1;
                            GameManager.Data.PreItem.Pre_300.USE = true;
                            GameManager.Sound.SFXPlay(clip);
                            Shop_Owner.text = "여기, 맡아놨던 물건이야.";
                        }
                        else if (GameManager.Data.Money.Gold >= normal_price)
                        {
                            GameManager.Data.Money.Gold -= normal_price;
                            GameManager.Data.PreItem.Pre_300.USE = true;
                            GameManager.Sound.SFXPlay(clip1);
                            Shop_Owner.text = "좋은 선택이야.";
                        }
                        else
                        {
                            Shop_Owner.text = "축복을 받으려면, 정성을 보이라고.";
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
                    Shop_Owner.text = "우리 집 마늘맛이 어때?";
                }
                else
                {
                    Shop_Owner.text = "일하지 않는 자, 먹지도 말라.";
                }
                break;
            case 5:
                if (GameManager.Data.Money.Gold >= random_price)
                {
                    GameManager.Data.Money.Gold -= random_price;
                    int idx = Random.Range(1, Enum.GetNames(typeof(PreItem.Random_Item)).Length);
                    GameManager.Data.PreItem.Pre_Random = (PreItem.Random_Item)idx;
                    GameManager.Sound.SFXPlay(clip1);
                    Shop_Owner.text = "좋은 것 좀 나왔나?";
                }
                else
                {
                    Shop_Owner.text = "순진하긴, 세상에 공짜는 없어.";
                }
                break;

        }
        Check_Select();
        Info();
    }
    void LoadSound() //Sound Resoucres 경로 찾아와서 불러와놓기.
    {
        clip = Resources.Load<AudioClip>("Sound/Common/004_Manu_Sound2");
        clip1 = Resources.Load<AudioClip>("Sound/Common/005_Cash");
        clip2 = Resources.Load<AudioClip>("Sound/Common/001_CharacterUp");
    }
}