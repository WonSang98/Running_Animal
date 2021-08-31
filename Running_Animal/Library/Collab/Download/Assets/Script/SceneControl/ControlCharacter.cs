using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // enum index로 접근

public struct cost
{
    public cost(int g, int m)
    {
        gold = g;
        Money_Forest = m;
    }
    public int gold { get; }
    public int Money_Forest { get; }
}
public class ControlCharacter : MonoBehaviour
{
    string[] Ability = { "HP", "SPEED", "LUK", "JUMPP", "JUMPC", "RESTORE", "DEF" };
    //Scene 'Character'에 사용될 SceneManager

    // 표시될 캐릭터들의 배열
    GameObject[] characters;
    GameObject Show_Character;

    int idx; // 현재 보고 있는 캐릭터 순서

    Button Button_Upgrade;
    Button Button_Main;
    Button Button_Next;
    Button Button_Pre;
    Button Button_Buy;
    Button Button_Select;

    GameObject Panel_Upgrade; // 육성 누를때만 보이게...
    GameObject Text_Name; // 선택된 캐릭터의 이름 표시.

    bool flag_ON; // 육성창을 눌렀는지 안눌렀는지 확인용 

    // 게이지
    RectTransform[,] Gage;

    // 육성 창 infomations 내용 변경하기 위한 Texts
    Text[] Text_Points = new Text[7];
    Text[] Text_Value = new Text[7];
    Text Text_Level;
    Text Text_SP;
    Text Text_LVUP_G;
    Text Text_LVUP_S;
    Text Text_RESET_S;

    Animator Animator_UPanel; // Panel_Upgrade 관련 애니메이터
    AudioClip clip; // 메뉴사운드
    AudioClip clip2; // 족자 사운드
    AudioClip clip3; // 강화시 글써지는 사운드
    AudioClip clip4; // 레벨업, 초기화 사운드
    AudioClip clip5; // 메뉴 사운드2
    AudioClip clip6; // 구매 사운드
    // 레벨 업 비용
    cost[] LV_COST = {new cost(500, 1),
                      new cost(1000, 2),
                      new cost(1500, 3),
                      new cost(2000, 4),
                      new cost(2500, 5),
                      new cost(99999, 9999)};

    public string[] CHARACTER_NAME =
    {
        "돼지",
        "고양이",
        "원숭이",
        "토끼"
    };

    short[] SP = { 2, 4, 6, 8, 10 };


    void Start()
    {
        LoadSound();
        //모든 캐릭터 프리팹을 불러와서... ㅇㅇ
        characters = Resources.LoadAll<GameObject>("Character/");
        idx = (int)GameManager.Data.Preset.Character;
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = new Vector3(0, -2.5f, 0);
        Show_Character.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Show_Character.transform.Find("Foot").gameObject.SetActive(true);
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;

        Button_Upgrade = GameObject.Find("UI/Button_Upgrade").GetComponent<Button>();
        Button_Upgrade.onClick.AddListener(() => Click_Upgrade());
        Button_Next = GameObject.Find("UI").transform.Find("Button_Next").GetComponent<Button>();
        Button_Next.onClick.AddListener(() => nextCharacter());
        Button_Pre = GameObject.Find("UI").transform.Find("Button_Prev").GetComponent<Button>();
        Button_Pre.onClick.AddListener(() => prevCharacter());
        Button_Buy = GameObject.Find("UI").transform.Find("Button_Buy").GetComponent<Button>();
        Button_Buy.onClick.AddListener(() => buy());
        Button_Select = GameObject.Find("UI").transform.Find("Button_Select").GetComponent<Button>();
        Button_Select.onClick.AddListener(() => select());


        Button_Main = GameObject.Find("UI/Button_Back").GetComponent<Button>();
        Button_Main.onClick.AddListener(() => gameObject.GetComponent<LoadScene>().OnMain());
        //Panel Upgrade 변수화, 그리고 비활성화.
        Panel_Upgrade = GameObject.Find("UI").transform.Find("Panel_Upgrade").gameObject;
        Animator_UPanel = Panel_Upgrade.GetComponent<Animator>();
        Panel_Upgrade.SetActive(false);

        //캐릭터 이름 오브젝트 찾아서 등록..
        Text_Name = GameObject.Find("UI").transform.Find("Text_Name").gameObject;

        //안내를 위한 TEXT
        String info_path = "Panel_Upgrade/CONTENTS/Info/";
        Gage = new RectTransform[Ability.Length, 3];
        for(int i = 0; i < Ability.Length; i++)
        {
            Text_Points[i] = GameObject.Find("UI").transform.Find(info_path + "POINT_" + Ability[i] + "/Text").GetComponent<Text>();
            Text_Value[i] = GameObject.Find("UI").transform.Find("Panel_Upgrade/CONTENTS/Gage_" + Ability[i] + "/Text").GetComponent<Text>();
            Gage[i, 0] = GameObject.Find("UI").transform.Find("Panel_Upgrade/CONTENTS/Gage_" + Ability[i] + "/Normal").GetComponent<RectTransform>();
            Gage[i, 1] = GameObject.Find("UI").transform.Find("Panel_Upgrade/CONTENTS/Gage_" + Ability[i] + "/Talent").GetComponent<RectTransform>();
            Gage[i, 2] = GameObject.Find("UI").transform.Find("Panel_Upgrade/CONTENTS/Gage_" + Ability[i] + "/Upgrade").GetComponent<RectTransform>();

        }
        Text_Level = GameObject.Find("UI").transform.Find(info_path + "Text_Level").GetComponent<Text>();
        Text_SP = GameObject.Find("UI").transform.Find(info_path + "Text_SP").GetComponent<Text>();
        Text_LVUP_G = GameObject.Find("UI").transform.Find(info_path + "LVUP_GOLD/Text").GetComponent<Text>();
        Text_LVUP_S = GameObject.Find("UI").transform.Find(info_path + "LVUP_SPECIAL/Text").GetComponent<Text>();
        Text_RESET_S = GameObject.Find("UI").transform.Find(info_path + "RESET_SPECIAL/Text").GetComponent<Text>();

        flag_ON = false;
        show_info();
    }

    void Click_Upgrade()
    {
        if (flag_ON)
        {
            OffUpgrade();
        }
        else
        {
            OnUpgrade();
        }
    }

    // 캐릭터 강화 창 OPEN!!!!!!!!!!!!!!!!!!!!!!!
    void OnUpgrade()
    {
        Button_Pre.interactable = false;
        Button_Next.interactable = false;
        Button_Buy.interactable = false;
        Button_Select.interactable = false;
        // 1. 캐릭터를 중앙에서 좌측으로 밀어넣는다.
        StartCoroutine(MoveLeft());
        // 2. 업그레이드 패널을 열어준다
        StartCoroutine(OpenPanel());
        flag_ON = true;
    }

    void OffUpgrade()
    {
        StartCoroutine(MoveRight());
        StartCoroutine(ClosePanel());
        
        flag_ON = false;
    }

    IEnumerator MoveLeft() // 캐릭터 좌측으로 밀어넣는 루틴
    {
        Text_Name.SetActive(false);
        Button_Upgrade.interactable = false;
        Vector3 temp_Scale;
        Show_Character.GetComponent<SpriteRenderer>().flipX = true;
        while (Show_Character.transform.position.x > -7.0f)
        {
            if(Show_Character.transform.position.x > -3.5f)
            {
                temp_Scale = Show_Character.transform.localScale;
                Show_Character.transform.localScale = new Vector3(temp_Scale.x - Time.deltaTime, temp_Scale.y - Time.deltaTime, temp_Scale.z - Time.deltaTime);
            }
            else
            {
                temp_Scale = Show_Character.transform.localScale;
                Show_Character.transform.localScale = new Vector3(temp_Scale.x + Time.deltaTime, temp_Scale.y + Time.deltaTime, temp_Scale.z - Time.deltaTime);
            }
            Show_Character.transform.Translate(-8 *  Time.deltaTime, 0, 0);

            yield return null;
        }
        Show_Character.GetComponent<SpriteRenderer>().flipX = false;
        Show_Character.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

    }

    IEnumerator MoveRight()
    {
        Button_Upgrade.interactable = false;
        Text_Name.SetActive(true);
        Vector3 temp_Scale;
        while (Show_Character.transform.position.x < 0)
        {
            if (Show_Character.transform.position.x < -3.5f)
            {
                temp_Scale = Show_Character.transform.localScale;
                Show_Character.transform.localScale = new Vector3(temp_Scale.x - Time.deltaTime, temp_Scale.y - Time.deltaTime, temp_Scale.z - Time.deltaTime);
            }
            else
            {
                temp_Scale = Show_Character.transform.localScale;
                Show_Character.transform.localScale = new Vector3(temp_Scale.x + Time.deltaTime, temp_Scale.y + Time.deltaTime, temp_Scale.z - Time.deltaTime);
            }
            Show_Character.transform.Translate(8 * Time.deltaTime, 0, 0);

            yield return null;
        }
        Show_Character.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }
    IEnumerator OpenPanel()
    {
        Panel_Upgrade.transform.Find("CONTENTS").gameObject.SetActive(false);
        Panel_Upgrade.SetActive(true);
        
        for (int i = 0; i < 1; i++)
        {
            Animator_UPanel.SetBool("Window_Open", true);
            yield return new WaitForSeconds(1.0f);
        }
        GameManager.Sound.SFXPlay(clip2);
        Panel_Upgrade.transform.Find("CONTENTS").gameObject.SetActive(true);
        SetGage();
        Button_Upgrade.interactable = true;
    }

    IEnumerator ClosePanel()
    {
        Panel_Upgrade.transform.Find("CONTENTS").gameObject.SetActive(false);
        for (int i = 0; i < 1; i++)
        {
            Animator_UPanel.SetBool("Window_Open", false);
            yield return new WaitForSeconds(1.0f);
        }
        GameManager.Sound.SFXPlay(clip2);
        Panel_Upgrade.SetActive(false);
        Button_Upgrade.interactable = true;
        Button_Pre.interactable = true;
        Button_Next.interactable = true;
        Button_Buy.interactable = true;
        Button_Select.interactable = true;
    }

    void SetGage() // 능력치 게이지를 현재 캐릭터 능력치에 맞추어 적용한다.
    {
        //Gage 관련
        // 1. HP
        Gage[0, 0].sizeDelta = new Vector2(Character.Natural[idx].ability.MAX_HP.value * 0.6f, 32);
        Gage[0, 1].sizeDelta = new Vector2(GameManager.Data.Character_STAT[idx].ability.MAX_HP.value * 0.6f, 32);
        Gage[0, 2].sizeDelta = new Vector2(Gage[0, 1].sizeDelta.x + GameManager.Data.Talent.HP.value * 0.6f, 32);
        Text_Value[0].text = $"[{GameManager.Data.Character_STAT[idx].ability.MAX_HP.value + GameManager.Data.Talent.HP.value }]";

        // 2.SPEED
        Gage[1, 0].sizeDelta = new Vector2(Character.Natural[idx].ability.SPEED.value * 10f, 32);
        Gage[1, 1].sizeDelta = new Vector2(GameManager.Data.Character_STAT[idx].ability.SPEED.value * 10f, 32);
        Gage[1, 2].sizeDelta = new Vector2(Gage[1, 1].sizeDelta.x, 32);
        Text_Value[1].text = $"[{GameManager.Data.Character_STAT[idx].ability.SPEED.value }]";

        // 3.LUK
        Gage[2, 0].sizeDelta = new Vector2(Character.Natural[idx].ability.LUK.value * 4f, 32);
        Gage[2, 1].sizeDelta = new Vector2(GameManager.Data.Character_STAT[idx].ability.LUK.value * 4f, 32);
        Gage[2, 2].sizeDelta = new Vector2(Gage[2, 1].sizeDelta.x + GameManager.Data.Talent.LUK.value * 4f, 32);
        Text_Value[2].text = $"[{GameManager.Data.Character_STAT[idx].ability.LUK.value + GameManager.Data.Talent.LUK.value }]";

        // 4.JUMPP
        Gage[3, 0].sizeDelta = new Vector2(Character.Natural[idx].ability.JUMP.value * 8f, 32);
        Gage[3, 1].sizeDelta = new Vector2(GameManager.Data.Character_STAT[idx].ability.JUMP.value * 8f, 32);
        Gage[3, 2].sizeDelta = new Vector2(Gage[3, 1].sizeDelta.x, 32);
        Text_Value[3].text = $"[{GameManager.Data.Character_STAT[idx].ability.JUMP.value}]";

        // 5.JUMPC
        Gage[4, 0].sizeDelta = new Vector2(Character.Natural[idx].ability.MAX_JUMP.value * 80f, 32);
        Gage[4, 1].sizeDelta = new Vector2(GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.value * 80f, 32);
        Gage[4, 2].sizeDelta = new Vector2(Gage[4, 1].sizeDelta.x, 32);
        Text_Value[4].text = $"[{GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.value}]";

        // 6. RESTORE
        Gage[5, 0].sizeDelta = new Vector2((Character.Natural[idx].ability.RESTORE.value - 1) * 200f, 32);
        Gage[5, 1].sizeDelta = new Vector2((GameManager.Data.Character_STAT[idx].ability.RESTORE.value - 1) * 200f, 32);
        Gage[5, 2].sizeDelta = new Vector2(Gage[5, 1].sizeDelta.x + (GameManager.Data.Talent.RESTORE.value * 200f), 32);
        Text_Value[5].text = $"[{Math.Round((GameManager.Data.Character_STAT[idx].ability.RESTORE.value + GameManager.Data.Talent.RESTORE.value), 3)}]";

        // 7. DEF
        Gage[6, 0].sizeDelta = new Vector2(Character.Natural[idx].ability.DEF.value * 400f, 32);
        Gage[6, 1].sizeDelta = new Vector2(GameManager.Data.Character_STAT[idx].ability.DEF.value * 400f, 32);
        Gage[6, 2].sizeDelta = new Vector2(Gage[6, 1].sizeDelta.x + (GameManager.Data.Talent.DEF.value * 400f), 32);
        Text_Value[6].text = $"[{Math.Round((GameManager.Data.Character_STAT[idx].ability.DEF.value + GameManager.Data.Talent.DEF.value) * 100), 3}]";

        //Text관련
        // 1. 업그레이드 필요 비용 설명
        // 2. 레벨업 시 레벨 재표기
        // 3. 잔여 마늘 표시

        Text_Points[0].text = $"X{GameManager.Data.Character_STAT[idx].ability.MAX_HP.level + 1}";
        Text_Points[1].text = $"X{GameManager.Data.Character_STAT[idx].ability.SPEED.level + 1}";
        Text_Points[2].text = $"X{GameManager.Data.Character_STAT[idx].ability.LUK.level + 1}";
        Text_Points[3].text = $"X{GameManager.Data.Character_STAT[idx].ability.JUMP.level + 1}";
        Text_Points[4].text = $"X{(GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level + 1) * 15}";
        Text_Points[5].text = $"X{GameManager.Data.Character_STAT[idx].ability.RESTORE.level + 1}";
        Text_Points[6].text = $"X{GameManager.Data.Character_STAT[idx].ability.DEF.level + 1}";
        Text_Level.text = $"{GameManager.Data.Character_STAT[idx].LV}";
        Text_SP.text = $"{GameManager.Data.Character_STAT[idx].STAT_POINT}";
        Text_LVUP_G.text = (GameManager.Data.Character_STAT[idx].LV != 5) ? $"X{LV_COST[GameManager.Data.Character_STAT[idx].LV].gold}" : "-";
        Text_LVUP_S.text = (GameManager.Data.Character_STAT[idx].LV != 5) ? $"X{LV_COST[GameManager.Data.Character_STAT[idx].LV].Money_Forest}" : "-";
        Text_RESET_S.text = $"X{GameManager.Data.Character_STAT[idx].LV}";
    }

    void Change(int i)
    {
        Destroy(Show_Character);
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = new Vector3(0, -2.5f, 0);
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
        Show_Character.transform.Find("Foot").gameObject.SetActive(true);
        Panel_Upgrade.transform.Find("CONTENTS").gameObject.SetActive(false);
        Text_Name.SetActive(true);
        Panel_Upgrade.SetActive(false);
        show_info();
    }
    public void nextCharacter()
    {
        GameManager.Sound.SFXPlay(clip);
        if (++idx == characters.Length) idx = 0;
        Change(idx);
        
    }


    public void prevCharacter()
    {
        GameManager.Sound.SFXPlay(clip);
        if (--idx == -1) idx = characters.Length - 1;
        Change(idx);
    }

    public void show_info()
    {
        Text_Name.GetComponent<Text>().text = CHARACTER_NAME[idx];
        if ((int)GameManager.Data.Preset.Character == idx)
        {
            GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
            GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = false;

        }
        else
        {
            if(GameManager.Data.Purchase.Character[idx] == true)
            {
                GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
                GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
                GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = true;
            }
            else
            {
                GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(false);
                GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(true);
            }
        }
        
    }

    public void select()
    {
        GameManager.Data.Preset.Character = (Character.CHARACTER_CODE)(idx);
        show_info();
        GameManager.Sound.SFXPlay(clip5);
    }

    public void buy()
    {
        if(GameManager.Data.Money.Gold >= Character.COST[idx])
        {
            GameManager.Data.Purchase.Character[idx] = true;
            GameManager.Data.Money.Gold -= Character.COST[idx];
            GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
            show_info();
            GameManager.Sound.SFXPlay(clip6);
        }
    }

    public void UP_LV()
    {
        if(GameManager.Data.Character_STAT[idx].LV < 5 &&
            (GameManager.Data.Money.Gold >= LV_COST[GameManager.Data.Character_STAT[idx].LV].gold &&
            GameManager.Data.Money.Speacial[0] >= LV_COST[GameManager.Data.Character_STAT[idx].LV].Money_Forest))
        {
            GameManager.Data.Money.Gold -= LV_COST[GameManager.Data.Character_STAT[idx].LV].gold;
            GameManager.Data.Money.Speacial[0] -= LV_COST[GameManager.Data.Character_STAT[idx].LV].Money_Forest;

            
            GameManager.Data.Character_STAT[idx].STAT_POINT += SP[GameManager.Data.Character_STAT[idx].LV];
            GameManager.Data.Character_STAT[idx].LV += 1;
            GameManager.Sound.SFXPlay(clip4);
        }
        SetGage();
    }

    public void UP_HP()
    {
        if(GameManager.Data.Character_STAT[idx].STAT_POINT >= (GameManager.Data.Character_STAT[idx].ability.MAX_HP.level + 1))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.MAX_HP.level + 1);
            GameManager.Data.Character_STAT[idx].ability.MAX_HP.value += (GameManager.Data.Character_STAT[idx].ability.MAX_HP.level+1) * 10;
            GameManager.Data.Character_STAT[idx].ability.MAX_HP.level += 1;
            GameManager.Sound.SFXPlay(clip3);
        }
        SetGage();
    }

    public void UP_SPEED()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (GameManager.Data.Character_STAT[idx].ability.SPEED.level + 1))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.SPEED.level + 1);
            GameManager.Data.Character_STAT[idx].ability.SPEED.value += (GameManager.Data.Character_STAT[idx].ability.SPEED.level + 1) * 0.5f;
            GameManager.Data.Character_STAT[idx].ability.SPEED.level += 1;
            GameManager.Sound.SFXPlay(clip3);

        }
        SetGage();
    }

    public void UP_JUMP_P()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (short)(GameManager.Data.Character_STAT[idx].ability.JUMP.level + 1))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.JUMP.level + 1);
            GameManager.Data.Character_STAT[idx].ability.JUMP.value += (GameManager.Data.Character_STAT[idx].ability.JUMP.level + 1) * 0.5f;
            GameManager.Data.Character_STAT[idx].ability.JUMP.level += 1;
            GameManager.Sound.SFXPlay(clip3);
        }
        SetGage();

    }

    public void UP_RESTORE()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (short)(GameManager.Data.Character_STAT[idx].ability.RESTORE.level + 1))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.RESTORE.level + 1);
            GameManager.Data.Character_STAT[idx].ability.RESTORE.value += (GameManager.Data.Character_STAT[idx].ability.RESTORE.level + 1) * 0.05f;
            GameManager.Data.Character_STAT[idx].ability.RESTORE.level += 1;
            GameManager.Sound.SFXPlay(clip3);
        }
        SetGage();

    }

    public void UP_JUMP_C()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= ((GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level+1) * 15))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)((GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level+1) * 15);
            GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.value += 1;
            GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level += 1;
            GameManager.Sound.SFXPlay(clip3);
        }
            SetGage();

    }

    public void UP_DEF()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (short)(GameManager.Data.Character_STAT[idx].ability.DEF.level + 1))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.DEF.level + 1);
            GameManager.Data.Character_STAT[idx].ability.DEF.value += ((short)(GameManager.Data.Character_STAT[idx].ability.DEF.level + 1) * 0.01f);
            GameManager.Data.Character_STAT[idx].ability.DEF.level += 1;
            GameManager.Sound.SFXPlay(clip3);
        }
        SetGage();

    }

    public void UP_LUK()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (short)(GameManager.Data.Character_STAT[idx].ability.LUK.level + 1))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.LUK.level + 1);
            GameManager.Data.Character_STAT[idx].ability.LUK.value += (short)((GameManager.Data.Character_STAT[idx].ability.LUK.level + 1) * 1);
            GameManager.Data.Character_STAT[idx].ability.LUK.level += 1;
            GameManager.Sound.SFXPlay(clip3);
        }
        SetGage();

    }

    public void Reset_SP()
    {
        if(GameManager.Data.Money.Speacial[0] >= (GameManager.Data.Character_STAT[idx].LV))
        {
            GameManager.Data.Money.Speacial[0] -= (GameManager.Data.Character_STAT[idx].LV);
            GameManager.Data.Character_STAT[idx].STAT_POINT = 0;
            GameManager.Sound.SFXPlay(clip4);

            ToNatural();
            for (int i = 0; i < GameManager.Data.Character_STAT[idx].LV; i++)
            {
                GameManager.Data.Character_STAT[idx].STAT_POINT += SP[i];
            }
        }
        SetGage();

    }

    //스탯 초기화 하기.
    public void ToNatural()
    {
        GameManager.Data.Character_STAT[idx].STAT_POINT = 0;
        GameManager.Data.Character_STAT[idx].ability.MAX_HP.level = Character.Natural[idx].ability.MAX_HP.level;
        GameManager.Data.Character_STAT[idx].ability.MAX_HP.value = Character.Natural[idx].ability.MAX_HP.value;

        GameManager.Data.Character_STAT[idx].ability.HP.level = Character.Natural[idx].ability.HP.level;
        GameManager.Data.Character_STAT[idx].ability.HP.value = Character.Natural[idx].ability.HP.value;

        GameManager.Data.Character_STAT[idx].ability.SPEED.level = Character.Natural[idx].ability.SPEED.level;
        GameManager.Data.Character_STAT[idx].ability.SPEED.value = Character.Natural[idx].ability.SPEED.value;

        GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level = Character.Natural[idx].ability.MAX_JUMP.level;
        GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.value = Character.Natural[idx].ability.MAX_JUMP.value;

        GameManager.Data.Character_STAT[idx].ability.JUMP.level = Character.Natural[idx].ability.JUMP.level;
        GameManager.Data.Character_STAT[idx].ability.JUMP.value = Character.Natural[idx].ability.JUMP.value;

        GameManager.Data.Character_STAT[idx].ability.DOWN.level = Character.Natural[idx].ability.DOWN.level;
        GameManager.Data.Character_STAT[idx].ability.DOWN.value = Character.Natural[idx].ability.DOWN.value;

        GameManager.Data.Character_STAT[idx].ability.DEF.level = Character.Natural[idx].ability.DEF.level;
        GameManager.Data.Character_STAT[idx].ability.DEF.value = Character.Natural[idx].ability.DEF.value;

        GameManager.Data.Character_STAT[idx].ability.LUK.level = Character.Natural[idx].ability.LUK.level;
        GameManager.Data.Character_STAT[idx].ability.LUK.value = Character.Natural[idx].ability.LUK.value;

        GameManager.Data.Character_STAT[idx].ability.RESTORE.level = Character.Natural[idx].ability.RESTORE.level;
        GameManager.Data.Character_STAT[idx].ability.RESTORE.value = Character.Natural[idx].ability.RESTORE.value;
    }
    void LoadSound() //Sound Resoucres 경로 찾아와서 불러와놓기.
    {
        clip = Resources.Load<AudioClip>("Sound/Common/000_Manu_Sound");
        clip2 = Resources.Load<AudioClip>("Sound/Common/002_Paper");
        clip3 = Resources.Load<AudioClip>("Sound/Common/003_Write");
        clip4 = Resources.Load<AudioClip>("Sound/Common/001_CharacterUp");
        clip5 = Resources.Load<AudioClip>("Sound/Common/004_Manu_Sound2");
        clip6 = Resources.Load<AudioClip>("Sound/Common/005_Cash");
    }

}
