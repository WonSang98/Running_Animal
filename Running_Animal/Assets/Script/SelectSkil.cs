using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectSkil : MonoBehaviour
{
    /*액티브 스킬 설명*/
    String[,] Active_Explane =
    {
        { "None", "영구결번" },
        { "방패 [액티브]", "5초 동안 어떠한 함정이든 한 번 막을 수 있는 방패를 두릅니다."},
        { "점멸 [액티브]", "일정 거리 앞으로 점멸합니다."},
        { "유령화 [액티브]", "3초 동안 모든 함정으로부터 피해를 입지 않습니다."  },
        { "회복 [액티브", "50만큼의 체력을 회복합니다."},
        { "다시, 또 한번 [액티브]", "스킬 선택 시, 선택지를 1회 바꿀 수 있습니다."},
        { "황금의 손 [액티브]", "화면에 보이는 모든 함정을 골드로 바꿔버립니다."},
        { "영겁의 시간 [액티브]", "5초 동안 시간이 느리게 흐릅니다."},
        { "세 마리 같은 한 마리[액티브]", "함정 파괴 시 콤보가 이전의 3배만큼 쌓입니다."},
        { "날개 [액티브]", "5초 동안 점프 횟수의 한계가 없어집니다. 과연, 얼마나 올라갈 수 있을까요?"}
    };
    /*패시브 스킬 설명*/
    String[,] Passive_Explane =
    {
        { "None", "영구결번" },
        { "행운 증가 [패시브]", "행운이 증가합니다."},
        { "한 번 더 [패시브]", "액티브 스킬을 한 번 더 사용할 수 있습니다."},
        { "방어력 증가 [패시브]", "방어력이 증가합니다."  },
        { "하루 종일도 할 수 있어 [패시브]", $"최대 체력이 상승합니다."},
        { "속도 증가 [패시브]", "속도가 증가합니다."},
        { "속도 감소 [패시브]", "속도가 감소합니다."},
        { "점프력 증가 [패시브]", "점프력이 증가합니다."},
        { "점프력 감소 [패시브]", "점프력이 감소합니다."},
        { "빠른 착지 [패시브]", "'DONW'버튼을 통해 내려오는 속도가 증가합니다."},
        { "느린 착지 [패시브]", "'DOWN'버튼을 통해 내려오는 속도가 감소합니다."},
        { "난 돈이 좋아! [패시브]", "골드를 캐릭터쪽으로 끌어당깁니다."},
        { "일석이조 [패시브]", "콤보 획득량이 2배가 됩니다."},
        { "집행유예 [패시브]", "체력이 0이 됐을 시, 최대 체력의 절반을 갖고 다시 일어섭니다." },
        { "통화팽창 [패시브]", "골드 획득량이 상승합니다." },
        { "자율 점프 신발 [패시브]", "일정 확률로 점프합니다.\n단, 언제 될지는 모릅니다. 될 수도 있고 안 될 수도 있습니다.\n이로 인한 점프는 점프 횟수에 반영되지 않습니다." },
        { "슈뢰딩거의 고양이 [패시브]", "일정 확률로 잠시간 무적이 됩니다.\n단, 언제 될지는 모릅니다. 될 수도 있고 안 될 수도 있습니다." },
        { "피격시 무적시간 증가[패시브]", "피격시에 무적이 되는 시간이 늘어납니다." },
        { "허공답보 [패시브]", "최대 점프 가능한 횟수가 1회 증가합니다."},
        { "자가치유 [패시브]", $"10초 마다 4만큼의 체력을 회복합니다."},
        { "체질 개선 [패시브]", "체력 회복 계열 아이템 및 스킬의 효율이 증가합니다."}
    };



    GameObject[] Skil_Box = new GameObject[3];
    
    GameObject[] Skil_Panel = new GameObject[3];
    Text[] Skil_Title = new Text[3];
    Text[] Skil_Info = new Text[3];

    RectTransform[] Skil_Pos = new RectTransform[3];

    int[] Skil_Num = new int[3];
    short[] Skil_Kinds = new short[3];

    GameObject prfPlayer;
    GameObject Player;

    Sprite[] active_sprite; //Active Skil Image;
    Sprite[] passive_sprite; //Passive Skil Image;


    void Start()
    {
        // ITEM CHAGNE 스킬 사용을 위해 플레이어 생성, 단 이 플레이어는 화면 밖에 생성하여 보이지 않게 처리함.
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        Player.transform.Translate(-60.495f, -10.5f, 0);
        Player.transform.name = "Player";


        GameManager.Instance.Load();
        Skil_Box[0] = GameObject.Find("UI/Button_Item0");
        Skil_Box[1] = GameObject.Find("UI/Button_Item1");
        Skil_Box[2] = GameObject.Find("UI/Button_Item2");

        for(int i=0; i<3; i++)
        {
            Skil_Pos[i] = Skil_Box[i].GetComponent<RectTransform>();
            Skil_Panel[i] = GameObject.Find("UI").transform.Find("Panel_Item"+i.ToString()).gameObject;
            Skil_Title[i] = Skil_Panel[i].transform.Find("Text_Name").GetComponent<Text>();
            Skil_Info[i] = Skil_Panel[i].transform.Find("Text_Info").GetComponent<Text>();
            Skil_Panel[i].SetActive(false);
        }

        StartCoroutine(Swing());
        active_sprite = Resources.LoadAll<Sprite>("Active_Buttons/");
        passive_sprite = Resources.LoadAll<Sprite>("Passive_Buttons/");

        //패널 비활성화.

        GameManager.Data.use_active = 0;
        GameManager.Data.change_chance = 999;


        change_all();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 스킬 이미지 흔들리는 효과
    IEnumerator Swing() // 좌우 흔들림
    {
        int x = 0;
        int y = 0;
        int z = 0;
        int left = 1; // left 1일시 왼쪽, , -1일시 오른쪽,  이동
        int up = 1;
        int big = 1;
        Vector2[] Pre_pos = new Vector2[3];
        Vector3[] Pre_scale = new Vector3[3];
        while (true)
        {
            if (x == 25) 
            { 
                x = -25;
                left *= -1;
            }
            if(y == 100)
            {
                y = -100;
                up *= -1;
            }
            if(z == 50)
            {
                z = -50;
                big *= -1;
            }
            for(int j = 0; j < 3; j++)
            {
                Pre_pos[j] = Skil_Pos[j].anchoredPosition; // 스킬 박스의 현재 위치
                Pre_scale[j] = Skil_Pos[j].localScale; // 스킬 박스의 현재 scale

                Skil_Pos[j].anchoredPosition = new Vector2(Pre_pos[j].x + (-0.35f * left), Pre_pos[j].y + (-0.125f * up));
                Skil_Pos[j].localScale = new Vector3(Pre_scale[j].x - (-0.0006f * big), Pre_scale[j].y - (-0.0006f * big), 1);

            }
            x++;
            y++;
            z++;
            yield return new WaitForSeconds(0.01f);
        }
    }

   

    void change_all()
    {
        Boolean[] bool_active = new Boolean[Enum.GetNames(typeof(DataManager.Active_Skil)).Length];
        Boolean[] bool_passive = new Boolean[Enum.GetNames(typeof(DataManager.Passive_Skil)).Length];

        //이미 선택된 패시브 제하기.
        //이거는 나중에 데이터를 따로 간결하게 정리하면서 정리해야할듯.
        //패시브 부분만 따로 이제 배열화해서... 이건 나중에 코드 정리하면서 합시다... 우선은 하드코딩으로
        bool_passive[2] = GameManager.Data.passive_active;
        bool_passive[11] = GameManager.Data.magnet;
        bool_passive[13] = GameManager.Data.passive_buwhal;
        bool_passive[15] = GameManager.Data.auto_jump;
        bool_passive[16] = GameManager.Data.random_god;
        bool_passive[19] = GameManager.Data.auto_restore;
        


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
                Skil_Title[i].text = Active_Explane[temp, 0];
                Skil_Info[i].text = Active_Explane[temp, 1];
            }
            else //Passive
            {


                Skil_Kinds[i] = 1;
                do
                {
                    temp = Random.Range(1, Enum.GetNames(typeof(DataManager.Passive_Skil)).Length);
                } while (bool_passive[temp] == true);
                bool_passive[temp] = true;
                Skil_Box[i].GetComponent<Image>().sprite = passive_sprite[temp];
                Skil_Title[i].text = Passive_Explane[temp, 0];
                Skil_Info[i].text = Passive_Explane[temp, 1];

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
        GameManager.Instance.Save();
        SceneManager.LoadScene("Play");
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
        GameManager.Instance.Save();
        SceneManager.LoadScene("Play");
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
        GameManager.Instance.Save();
        SceneManager.LoadScene("Play");
    }

    void set_active(int num)
    {
        GameManager.Data.active = (DataManager.Active_Skil)num;
    }

    void save_passive(int num)
    {
        for(int i=0; i<12; i++)
        {
            if(GameManager.Data.passive[i] == 0)
            {
                GameManager.Data.passive[i] = (DataManager.Passive_Skil)num;
                break;
            }
        }
    }
    void set_passive(int num)
    {   
        switch (num)
            {
            case 0:
                // None
                break;
            case 1:
                // 행운 증가
                GameManager.Data.luck += 3;
                save_passive(1);
                break;
            case 2:
                // 액티브 스킬 두 번 사용
                GameManager.Data.max_active += 1;
                GameManager.Data.passive_active = true;
                save_passive(2);
                break;
            case 3:
                // 방어력 증가
                GameManager.Data.defense += 1;
                save_passive(3);
                break;
            case 4:
                // 최대 체력 증가
                GameManager.Data.max_hp += 20.0f;
                GameManager.Data.hp += 20.0f;
                save_passive(4);
                break;
            case 5:
                // 이동속도 증가
                GameManager.Data.speed += (GameManager.Data.speed / 5);
                save_passive(5); 
                break;
            case 6:
                // 이동속도 감소
                GameManager.Data.speed -= (GameManager.Data.speed / 5);
                save_passive(6);
                break;
            case 7:
                // 점프력 증가
                GameManager.Data.jump += (GameManager.Data.jump / 5);
                save_passive(7);
                break;
            case 8:
                // 점프력 감소
                GameManager.Data.jump -= (GameManager.Data.jump / 5);
                save_passive(8);
                break;
            case 9:
                // 낙하속도 증가
                GameManager.Data.down += (GameManager.Data.down / 5);
                save_passive(9);
                break;
            case 10:
                // 낙하속도 감소
                GameManager.Data.down -= (GameManager.Data.down / 5);
                save_passive(10);
                break;
            case 11:
                // 자석버그
                GameManager.Data.magnet = true;
                save_passive(11);
                break;
            case 12:
                // 콤보 획득량 증가
                GameManager.Data.multi_combo *= 2;
                save_passive(12);
                break;
            case 13:
                // 부활
                GameManager.Data.buwhal += 1;
                GameManager.Data.passive_buwhal = true;
                save_passive(13);
                break;
            case 14:
                //코인 획득량 증가
                GameManager.Data.multi_coin += 1;
                save_passive(14);
                break;
            case 15:
                // 자동 점프
                GameManager.Data.auto_jump = true;
                save_passive(15);
                break;
            case 16:
                // 낮은 확률로 랜덤 무적
                GameManager.Data.random_god = true;
                save_passive(16);
                break;
            case 17:
                // 피격시 무적시간 증가
                GameManager.Data.dodge_time += (GameManager.Data.dodge_time / 2);
                save_passive(17);
                break;
            case 18:
                // 점프 횟수 증가
                GameManager.Data.max_jump += 1;
                save_passive(18);
                break;
            case 19:
                // 자동 체력 재생
                GameManager.Data.auto_restore = true;
                save_passive(19);
                break;
            case 20:
                // 체력 회복량 증가.
                GameManager.Data.restore_eff += 0.1f;
                save_passive(20);
                break;
        }
    }

    public void ReLoad()
    {
        if (GameManager.Data.change_chance > 0)
        {
            GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = true;
            GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = true;
            GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = true;
            change_all();

            GameManager.Data.change_chance -= 1;
        }

        if(GameManager.Data.change_chance == 0)
        {
            GameObject.Find("UI/Button_Reload").GetComponent<Button>().interactable = false;
        }
    }
    public void Skil0_Explain() // 스킬 설명~
    {
        if (Skil_Panel[0].activeSelf == false)
        {
            Skil_Panel[0].SetActive(true);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
        else
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
    }

    public void Skil1_Explain() // 스킬 설명~
    {
        if (Skil_Panel[1].activeSelf == false)
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(true);
            Skil_Panel[2].SetActive(false);
        }
        else
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
    }

    public void Skil2_Explain() // 스킬 설명~
    {
        if (Skil_Panel[2].activeSelf == false)
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(true);
        }
        else
        {
            Skil_Panel[0].SetActive(false);
            Skil_Panel[1].SetActive(false);
            Skil_Panel[2].SetActive(false);
        }
    }
}
