using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ControlTutorial2 : MonoBehaviour
{
    Button Button_Skill;
    GameObject[] Skill_Box = new GameObject[3];


    GameObject[] Skill_Panel = new GameObject[3];
    Text[] Skill_Title = new Text[3];
    Text[] Skill_Info = new Text[3];

    RectTransform[] Skill_Pos = new RectTransform[3];

    int[] Skill_Num = new int[3];
    short[] Skill_Kinds = new short[3];

    Active Active;
    Passive Passive;
    UI_Play UI_Play;
    LoadScene LS;

    AudioClip clip;
    AudioClip clip2;
    AudioClip clip3;
    void Start()
    {
        GameManager.Instance.Load();
        Active = GameObject.Find("@Managers").GetComponent<Active>();
        Passive = GameObject.Find("@Managers").GetComponent<Passive>();
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
        LS = GameObject.Find("@Managers").GetComponent<LoadScene>();
        Skill_Box[0] = GameObject.Find("UI/Button_Item0");
        Skill_Box[1] = GameObject.Find("UI/Button_Item1");
        Skill_Box[2] = GameObject.Find("UI/Button_Item2");

        Button_Skill = GameObject.Find("UI/Button_Skill").GetComponent<Button>();
        Button_Skill.GetComponent<Image>().sprite = Active.Active_Sprites[(int)GameManager.Play.Status.ACTIVE];
        Button_Skill.onClick.AddListener(() => UI_Play.Use_Active());

        for (int i = 0; i < 3; i++)
        {
            Skill_Pos[i] = Skill_Box[i].GetComponent<RectTransform>();
            Skill_Panel[i] = GameObject.Find("UI").transform.Find("Panel_Item" + i.ToString()).gameObject;
            Skill_Title[i] = Skill_Panel[i].transform.Find("Text_Name").GetComponent<Text>();
            Skill_Info[i] = Skill_Panel[i].transform.Find("Text_Info").GetComponent<Text>();
            Skill_Panel[i].SetActive(false);
        }

        StartCoroutine(Swing());
        change_all();
        LoadSound();
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
            if (y == 100)
            {
                y = -100;
                up *= -1;
            }
            if (z == 50)
            {
                z = -50;
                big *= -1;
            }
            for (int j = 0; j < 3; j++)
            {
                Pre_pos[j] = Skill_Pos[j].anchoredPosition; // 스킬 박스의 현재 위치
                Pre_scale[j] = Skill_Pos[j].localScale; // 스킬 박스의 현재 scale

                Skill_Pos[j].anchoredPosition = new Vector2(Pre_pos[j].x + (-0.35f * left), Pre_pos[j].y + (-0.125f * up));
                Skill_Pos[j].localScale = new Vector3(Pre_scale[j].x - (-0.0006f * big), Pre_scale[j].y - (-0.0006f * big), 1);

            }
            x++;
            y++;
            z++;
            yield return new WaitForSeconds(0.01f);
        }
    }



    void change_all()
    {
        Boolean[] bool_active = new Boolean[Enum.GetNames(typeof(Active.ACTIVE_CODE)).Length];
        Boolean[] bool_passive = new Boolean[Enum.GetNames(typeof(Passive.PASSIVE_CODE)).Length];

        //이미 선택된 패시브 제하기.
        foreach (KeyValuePair<Passive.PASSIVE_CODE, bool> data in GameManager.Skill.passive_once)
        {
            bool_passive[(int)data.Key] = data.Value;
        }


        Skill_Num[0] = 0;
        Skill_Num[1] = 0;
        Skill_Num[2] = 0;
        for (int i = 0; i < 3; i++)
        {
            int per_active = Random.Range(0, 100);
            int temp;
            if (per_active < 25) // active
            {
                Skill_Kinds[i] = 0;
                do
                {
                    temp = Random.Range(1, Enum.GetNames(typeof(Active.ACTIVE_CODE)).Length);
                } while (bool_active[temp] == true);
                bool_active[temp] = true;
                Skill_Box[i].GetComponent<Image>().sprite = Active.Active_Sprites[temp];
                Skill_Title[i].text = Active.Active_Explane[temp, 0];
                Skill_Info[i].text = Active.Active_Explane[temp, 1];
            }
            else //Passive
            {


                Skill_Kinds[i] = 1;
                do
                {
                    temp = Random.Range(1, Enum.GetNames(typeof(Passive.PASSIVE_CODE)).Length);
                } while (bool_passive[temp] == true);
                bool_passive[temp] = true;
                Skill_Box[i].GetComponent<Image>().sprite = Passive.Passive_Sprites[temp];
                Skill_Title[i].text = Passive.Passive_Explane[temp, 0];
                Skill_Info[i].text = Passive.Passive_Explane[temp, 1];

            }

            Skill_Num[i] = temp;
        }
    }

    public void button0()
    {
        if (Skill_Kinds[0] == 0)
        {
            set_active(Skill_Num[0]);
        }
        else
        {
            Passive.set_passive(Skill_Num[0]);
        }
        LS.OnTutorial3();
    }

    public void button1()
    {
        if (Skill_Kinds[1] == 0)
        {
            set_active(Skill_Num[1]);
        }
        else
        {
            Passive.set_passive(Skill_Num[1]);
        }
        LS.OnTutorial3();
    }

    public void button2()
    {
        if (Skill_Kinds[2] == 0)
        {
            set_active(Skill_Num[2]);
        }
        else
        {
            Passive.set_passive(Skill_Num[2]);
        }
        LS.OnTutorial3();
    }

    void set_active(int num)
    {
        GameManager.Play.Status.ACTIVE = (Active.ACTIVE_CODE)num;
    }

    public void ReLoad()
    {
        if (GameManager.Play.DS.time_change > 0)
        {
            GameObject.Find("UI/Button_Item0").GetComponent<Button>().interactable = true;
            GameObject.Find("UI/Button_Item1").GetComponent<Button>().interactable = true;
            GameObject.Find("UI/Button_Item2").GetComponent<Button>().interactable = true;
            change_all();
            GameManager.Sound.SFXPlay(clip3);

            GameManager.Play.DS.time_change -= 1;
        }

        if (GameManager.Play.DS.time_change == 0)
        {
            GameObject.Find("UI/Button_Reload").GetComponent<Button>().interactable = false;
        }
    }
    public void Skill0_Explain() // 스킬 설명~
    {
        if (Skill_Panel[0].activeSelf == false)
        {
            Skill_Panel[0].SetActive(true);
            Skill_Panel[1].SetActive(false);
            Skill_Panel[2].SetActive(false);
            GameManager.Sound.SFXPlay(clip);
        }
        else
        {
            Skill_Panel[0].SetActive(false);
            Skill_Panel[1].SetActive(false);
            Skill_Panel[2].SetActive(false);
            GameManager.Sound.SFXPlay(clip);
        }
    }

    public void Skill1_Explain() // 스킬 설명~
    {
        if (Skill_Panel[1].activeSelf == false)
        {
            Skill_Panel[0].SetActive(false);
            Skill_Panel[1].SetActive(true);
            Skill_Panel[2].SetActive(false);
            GameManager.Sound.SFXPlay(clip);
        }
        else
        {
            Skill_Panel[0].SetActive(false);
            Skill_Panel[1].SetActive(false);
            Skill_Panel[2].SetActive(false);
            GameManager.Sound.SFXPlay(clip);
        }
    }

    public void Skill2_Explain() // 스킬 설명~
    {
        if (Skill_Panel[2].activeSelf == false)
        {
            Skill_Panel[0].SetActive(false);
            Skill_Panel[1].SetActive(false);
            Skill_Panel[2].SetActive(true);
            GameManager.Sound.SFXPlay(clip);
        }
        else
        {
            Skill_Panel[0].SetActive(false);
            Skill_Panel[1].SetActive(false);
            Skill_Panel[2].SetActive(false);
            GameManager.Sound.SFXPlay(clip);
        }
    }

    void LoadSound()
    {
        clip = Resources.Load<AudioClip>("Sound/Common/004_Manu_Sound2");
        clip2 = Resources.Load<AudioClip>("Sound/Common/007_Stamp");
        clip3 = Resources.Load<AudioClip>("Sound/Common/005_Cash");
    }
}
