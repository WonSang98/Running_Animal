using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlRecord : MonoBehaviour
{
    
    Image[] passive;
    Text[] stat;
    Image character;
    Image active;

    Button Select_Recent;
    Color Color_Recent;
    Button Select_Best;
    Color Color_Best;
    Button Back;

    GameObject prf_Board;
    GameObject Parent_recent;
    GameObject Parent_best;
    GameObject Scroll_Recent;
    GameObject Scroll_Best;

    Sprite[] Sprite_GoodCharacter;

    bool now_recent;

    Passive Passive;
    Active Active;

    public enum stat_order
    {
        _HP = 0,
        _SPEED,
        _JUMPP,
        _JUMPC,
        _DOWN,
        _DEF,
        _LUK,
        _RESTORE
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Passive = GameManager.Instance.GetComponent<Passive>();
        Active = GameManager.Instance.GetComponent<Active>();
        now_recent = true;
        passive = new Image[14];
        for(int i=0; i<14; i++)
        {
            passive[i] = GameObject.Find("UI/Panel_Info/PASSIVE/" + i.ToString()).gameObject.GetComponent<Image>();
        }
        
        stat = new Text[8];
        for(int j=0; j<8; j++)
        {
            stat[j] = GameObject.Find("UI/Panel_Info/STATUS/Text" + Enum.GetName(typeof(stat_order), j)+ "/Text_Value").gameObject.GetComponent<Text>();
        }

        character = GameObject.Find("UI/Panel_Info/Image_Character").gameObject.GetComponent<Image>();
        active = GameObject.Find("UI/Panel_Info/Image_Active").gameObject.GetComponent<Image>();

        Select_Recent = GameObject.Find("UI/Panel_Record/Button_Recent").gameObject.GetComponent<Button>();
        Color_Recent = GameObject.Find("UI/Panel_Record/Button_Recent").gameObject.GetComponent<Image>().color;
        Select_Best = GameObject.Find("UI/Panel_Record/Button_Best").gameObject.GetComponent<Button>();
        Color_Best = GameObject.Find("UI/Panel_Record/Button_Best").gameObject.GetComponent<Image>().color;
        Back = GameObject.Find("UI/Button_Main").gameObject.GetComponent<Button>();

        prf_Board = Resources.Load<GameObject>("Record/board");
        Parent_recent = GameObject.Find("UI/Panel_Record").transform.Find("Scroll View_Recent/Viewport/Content").gameObject;
        Parent_best = GameObject.Find("UI/Panel_Record").transform.Find("Scroll View_Best/Viewport/Content").gameObject;

        Scroll_Recent = GameObject.Find("UI/Panel_Record").transform.Find("Scroll View_Recent").gameObject;
        Scroll_Best = GameObject.Find("UI/Panel_Record").transform.Find("Scroll View_Best").gameObject;

        Sprite_GoodCharacter = Resources.LoadAll<Sprite>("Image/GUI/Play/CharacterSuccess");

        Select_Recent.onClick.AddListener(() => Button_recent());
        Select_Best.onClick.AddListener(() => Button_best());
        Back.onClick.AddListener(() => GameManager.Instance.GetComponent<LoadScene>().OnMain());
        Scroll_Recent.SetActive(true);
        Scroll_Best.SetActive(false);
        Make_RecentBoard();
        Make_BestBoard();
        show_default();
    }

    void Make_RecentBoard()
    {
        int cnt = GameManager.Data.Recent_Data.Count;
        for (int i=1; i <= cnt; i++)
        {
            Record tmp_REC = GameManager.Data.Recent_Data[cnt - i];
            GameObject tmp;
            tmp = Instantiate(prf_Board);

            tmp.transform.Find("Data").gameObject.GetComponent<BoardData>().data = tmp_REC;

            // ���� ���� �߰��ϱ�.
            tmp.transform.GetChild(0).gameObject.GetComponent<Text>().text = $"{tmp_REC.Score}"; //Score
            tmp.transform.GetChild(1).gameObject.GetComponent<Text>().text = tmp_REC.Date; //Date
            tmp.transform.GetChild(2).gameObject.GetComponent<Text>().text = Difficulty.DIFF_CODE[(int)tmp_REC.REC_pre.Difficult]; //Diff
            tmp.transform.GetChild(3).gameObject.GetComponent<Text>().text = $" {tmp_REC.REC_dc.goldNow}G"; //Gold
            tmp.transform.GetChild(4).gameObject.GetComponent<Text>().text = $" {tmp_REC.Special}��"; //Seed
            tmp.transform.GetChild(5).gameObject.GetComponent<Text>().text = $"{tmp_REC.REC_dc.comboMax}"; //Combo
            tmp.transform.GetChild(6).gameObject.GetComponent<Text>().text = $"{tmp_REC.REC_dc.passTrap}"; //Trap
            tmp.transform.GetChild(7).gameObject.GetComponent<Text>().text = $"{i}"; //Rank
            tmp.transform.GetChild(8).gameObject.GetComponent<Text>().text = tmp_REC.Clear; //Success

            tmp.transform.SetParent(Parent_recent.transform); // �ڽ����� ���� �߰�.
            tmp.transform.localScale= new Vector3(1, 1, 1);
            tmp.gameObject.GetComponent<Button>().onClick.AddListener(() => Button_show(tmp));
        }
    }

    void Make_BestBoard()
    {
        int cnt = GameManager.Data.Best_Data.Count;
        for (int i = 1; i <= cnt; i++)
        {
            Record tmp_REC = GameManager.Data.Best_Data[cnt - i];
            GameObject tmp;
            tmp = Instantiate(prf_Board);

            tmp.transform.Find("Data").gameObject.GetComponent<BoardData>().data = tmp_REC;

            // ���� ���� �߰��ϱ�.
            tmp.transform.GetChild(0).gameObject.GetComponent<Text>().text = $"{tmp_REC.Score}"; //Score
            tmp.transform.GetChild(1).gameObject.GetComponent<Text>().text = tmp_REC.Date; //Date
            tmp.transform.GetChild(2).gameObject.GetComponent<Text>().text = Difficulty.DIFF_CODE[(int)tmp_REC.REC_pre.Difficult]; //Diff
            tmp.transform.GetChild(3).gameObject.GetComponent<Text>().text = $" {tmp_REC.REC_dc.goldNow}G"; //Gold
            tmp.transform.GetChild(4).gameObject.GetComponent<Text>().text = $" {tmp_REC.Special}��"; //Seed
            tmp.transform.GetChild(5).gameObject.GetComponent<Text>().text = $"{tmp_REC.REC_dc.comboMax}"; //Combo
            tmp.transform.GetChild(6).gameObject.GetComponent<Text>().text = $"{tmp_REC.REC_dc.passTrap}"; //Trap
            tmp.transform.GetChild(7).gameObject.GetComponent<Text>().text = $"{i}"; //Rank
            tmp.transform.GetChild(8).gameObject.GetComponent<Text>().text = tmp_REC.Clear; //Success

            tmp.transform.SetParent(Parent_best.transform); // �ڽ����� ���� �߰�.
            tmp.transform.localScale = new Vector3(1, 1, 1);
            tmp.gameObject.GetComponent<Button>().onClick.AddListener(() => Button_show(tmp));
        }
    }

    // ��� ������ Ŭ����.
    void Button_show(GameObject btn)
    {
        Record _data = btn.transform.Find("Data").GetComponent<BoardData>().data;
        //�нú� ��ų �̹��� ����
        for (int i = 0; i < 14; i++)
        {
            if (i < _data.REC_dc.passiveGet.Count)
            {
                passive[i].sprite = Passive.Passive_Sprites[(int)_data.REC_dc.passiveGet[i]];
            }
            else
            {
                passive[i].sprite = Passive.Passive_Sprites[0];
            }
        }
        //�ɷ�ġ
        stat[0].text = _data.REC_ab.ability.MAX_HP.value.ToString();
        stat[1].text = _data.REC_ab.ability.SPEED.value.ToString();
        stat[2].text = _data.REC_ab.ability.JUMP.value.ToString();
        stat[3].text = _data.REC_ab.ability.MAX_JUMP.value.ToString();
        stat[4].text = _data.REC_ab.ability.DOWN.value.ToString();
        stat[5].text = _data.REC_ab.ability.DEF.value.ToString();
        stat[6].text = _data.REC_ab.ability.LUK.value.ToString();
        stat[7].text = _data.REC_ab.ability.RESTORE.value.ToString();

        //ĳ����
        character.sprite = Sprite_GoodCharacter[(int)_data.REC_pre.Character];

        //��Ƽ�꽺ų
        active.sprite = Active.Active_Sprites[(int)_data.REC_ab.ACTIVE];


    }

    void Button_recent()
    {
        Scroll_Best.SetActive(false);
        Scroll_Recent.SetActive(true);

        if (!now_recent)
        {
            Color tmp_color;
            tmp_color = GameObject.Find("UI/Panel_Record/Button_Recent").gameObject.GetComponent<Image>().color;
            GameObject.Find("UI/Panel_Record/Button_Recent").gameObject.GetComponent<Image>().color = GameObject.Find("UI/Panel_Record/Button_Best").gameObject.GetComponent<Image>().color;
            GameObject.Find("UI/Panel_Record/Button_Best").gameObject.GetComponent<Image>().color = tmp_color;

            now_recent = true;
        }
        show_default();
    }

    void Button_best()
    {
        Scroll_Best.SetActive(true);
        Scroll_Recent.SetActive(false);

        if (now_recent)
        {
            Color tmp_color;
            tmp_color = GameObject.Find("UI/Panel_Record/Button_Recent").gameObject.GetComponent<Image>().color;
            GameObject.Find("UI/Panel_Record/Button_Recent").gameObject.GetComponent<Image>().color = GameObject.Find("UI/Panel_Record/Button_Best").gameObject.GetComponent<Image>().color;
            GameObject.Find("UI/Panel_Record/Button_Best").gameObject.GetComponent<Image>().color = tmp_color;

            now_recent = false;
        }
        show_default();
    }

    void show_default()
    {
        for (int i = 0; i < 14; i++)
        {
            passive[i].sprite = Passive.Passive_Sprites[0];
        }
        //�ɷ�ġ
        for (int j=0; j<8; j++)
        {
            stat[j].text = "";
        }

        //ĳ����
        character.sprite = Sprite_GoodCharacter[0];

        //��Ƽ�꽺ų
        active.sprite = Active.Active_Sprites[0];
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }
}
