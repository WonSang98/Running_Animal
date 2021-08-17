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
public class SelectCharacter : MonoBehaviour
{
    string[] Ability = { "HP", "SPEED", "LUK", "JUMPP", "DOWNP", "JUMPC", "DEF" };
    //Scene 'Character'에 사용될 SceneManager

    // 표시될 캐릭터들의 배열
    GameObject[] characters;
    GameObject Show_Character;

    int idx; // 현재 보고 있는 캐릭터 순서

    Button Button_Upgrade;

    GameObject Panel_Upgrade; // 육성 누를때만 보이게...
    GameObject Text_Name; // 선택된 캐릭터의 이름 표시.

    bool flag_ON; // 육성창을 눌렀는지 안눌렀는지 확인용 

    // 게이지 채우기 위한 이미지
    Sprite Gage_None;
    Sprite Gage_Normal;
    Sprite Gage_Upgrade;
    Sprite Gage_Talent;

    // 육성 창 infomations 내용 변경하기 위한 Texts
    Text[] Text_Points = new Text[7];
    Text Text_Level;
    Text Text_SP;
    Text Text_LVUP_G;
    Text Text_LVUP_S;
    Text Text_RESET_S;

    Animator Animator_UPanel; // Panel_Upgrade 관련 애니메이터
    // 레벨 업 비용
    cost[] LV_COST = {new cost(500, 1),
                      new cost(1000, 2),
                      new cost(1500, 3),
                      new cost(2000, 4),
                      new cost(2500, 5),
                      new cost(99999, 9999)};

    int[] SP = { 2, 4, 6, 8, 10 };

    // 캐릭터 DEFAULT 값
    static Character[] Natural_STAT =
    {
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None)
    };

    void Start()
    {
        //모든 캐릭터 프리팹을 불러와서... ㅇㅇ
        characters = Resources.LoadAll<GameObject>("Character/");
        idx = (int)GameManager.Data.Now_Character;
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = new Vector3(0, -2.5f, 0);
        Show_Character.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Show_Character.transform.Find("Foot").gameObject.SetActive(true);
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;

        Button_Upgrade = GameObject.Find("UI/Button_Upgrade").GetComponent<Button>();
        Button_Upgrade.onClick.AddListener(() => Click_Upgrade());

        //Panel Upgrade 변수화, 그리고 비활성화.
        Panel_Upgrade = GameObject.Find("UI").transform.Find("Panel_Upgrade").gameObject;
        Animator_UPanel = Panel_Upgrade.GetComponent<Animator>();
        Panel_Upgrade.SetActive(false);

        //GageBar 채우기 위한 이미지 리소스 불러오기
        Gage_None = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_None");
        Gage_Normal = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_Normal");
        Gage_Upgrade = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_Upgrade");
        Gage_Talent = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_Talent");

        //캐릭터 이름 오브젝트 찾아서 등록..
        Text_Name = GameObject.Find("UI").transform.Find("Text_Name").gameObject;

        //안내를 위한 TEXT
        String info_path = "Panel_Upgrade/CONTENTS/Info/";
        for(int i = 0; i < Ability.Length; i++)
        {
            Text_Points[i] = GameObject.Find("UI").transform.Find(info_path + "POINT_" + Ability[i] + "/Text").GetComponent<Text>();
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
        Vector3 temp_Scale;
        Show_Character.GetComponent<SpriteRenderer>().flipX = true;
        while (Show_Character.transform.position.x > -8.1f)
        {
            if(Show_Character.transform.position.x > -4)
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
        Show_Character.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

    }

    IEnumerator MoveRight()
    {
        Text_Name.SetActive(true);
        Vector3 temp_Scale;
        while (Show_Character.transform.position.x < 0)
        {
            if (Show_Character.transform.position.x < -4)
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
        Show_Character.GetComponent<SpriteRenderer>().flipX = true;
        Show_Character.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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
        Panel_Upgrade.transform.Find("CONTENTS").gameObject.SetActive(true);
        SetGage();
    }

    IEnumerator ClosePanel()
    {
        Panel_Upgrade.transform.Find("CONTENTS").gameObject.SetActive(false);
        for (int i = 0; i < 1; i++)
        {
            Animator_UPanel.SetBool("Window_Open", false);
            yield return new WaitForSeconds(1.0f);
        }
        Panel_Upgrade.SetActive(false);
    }

    void SetGage() // 능력치 게이지를 현재 캐릭터 능력치에 맞추어 적용한다.
    {
        Debug.Log(Natural_STAT[idx].JUMP_COUNT);
        //Gage 관련
        foreach (string s in Ability)
        {
            string path = "UI/Panel_Upgrade/CONTENTS/Image_Gage_" + s + "/Gage/Gage";
            int idx_normal = 0; // 캐릭터 기본 능력치의 차지 비율.
            int idx_upgrade = 0; // 캐릭터 업그레이드 된 능력치의 차지 비율.
            int idx_talent = 0; // 캐릭터 재능 능력치의 차지 비율.
            //각 옵션별 수치별로 십분화
            switch (s)
            {
                case "HP":
                    idx_normal = (int)(Natural_STAT[idx].MAX_HP / 10);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].MAX_HP - Natural_STAT[idx].MAX_HP) / 10);
                    idx_talent = idx_upgrade + (int)(GameManager.Data.Talent_HP / 10);
                    break;
                case "SPEED":
                    idx_normal = (int)(Natural_STAT[idx].SPEED * 2);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].SPEED - Natural_STAT[idx].SPEED) * 2);
                    idx_talent = idx_upgrade;
                    break;
                case "LUK":
                    idx_normal = (Natural_STAT[idx].LUK);
                    idx_upgrade = idx_normal + (GameManager.Data.Character_STAT[idx].LUK - Natural_STAT[idx].LUK);
                    idx_talent = idx_upgrade + (GameManager.Data.Talent_LUK);
                    break;
                case "JUMPP":
                    idx_normal = (int)(Natural_STAT[idx].JUMP_POWER * 2);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].JUMP_POWER - Natural_STAT[idx].JUMP_POWER) * 2);
                    idx_talent = idx_upgrade;
                    break;
                case "DOWNP":
                    idx_normal = (int)(Natural_STAT[idx].DOWN_POWER * 2);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].DOWN_POWER - Natural_STAT[idx].DOWN_POWER) * 2);
                    idx_talent = idx_upgrade;
                    break;
                case "JUMPC":
                    idx_normal = (Natural_STAT[idx].JUMP_COUNT * 20);
                    idx_upgrade = idx_normal + ((GameManager.Data.Character_STAT[idx].JUMP_COUNT - Natural_STAT[idx].JUMP_COUNT) * 20);
                    idx_talent = idx_upgrade;
                    break;
                case "DEF":
                    idx_normal = (int)(Natural_STAT[idx].DEF * 100);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].DEF - Natural_STAT[idx].DEF) * 100);
                    idx_talent = idx_upgrade + (int)(GameManager.Data.Talent_DEF * 100);
                    break;
            }

            for(int i = 10; i <= 100; i += 10)
            {
                string gage_path = path;
                gage_path += i.ToString();
                for(int j = 0; j < 10; j++)
                {
                    if(i + j - 10 < idx_normal)
                    {
                        GameObject.Find(gage_path + "/Gage" + j.ToString()).GetComponent<Image>().sprite = Gage_Normal;
                    }
                    else if(i + j - 10 < idx_upgrade)
                    {
                        GameObject.Find(gage_path + "/Gage" + j.ToString()).GetComponent<Image>().sprite = Gage_Upgrade;
                    }
                    else if(i + j - 10 < idx_talent)
                    {
                        Debug.Log("Talent GAGE");
                        GameObject.Find(gage_path + "/Gage" + j.ToString()).GetComponent<Image>().sprite = Gage_Talent;
                    }
                    else
                    {
                        GameObject.Find(gage_path + "/Gage" + j.ToString()).GetComponent<Image>().sprite = Gage_None;
                    }
                }
            }

        }
        //Text관련
        // 1. 업그레이드 필요 비용 설명
        // 2. 레벨업 시 레벨 재표기
        // 3. 잔여 마늘 표시

        Text_Points[0].text = $"X{GameManager.Data.Character_STAT[idx].LV_MAX_HP}";
        Text_Points[1].text = $"X{GameManager.Data.Character_STAT[idx].LV_SPEED}";
        Text_Points[2].text = $"X{GameManager.Data.Character_STAT[idx].LV_LUK}";
        Text_Points[3].text = $"X{GameManager.Data.Character_STAT[idx].LV_JUMP_POWER}";
        Text_Points[4].text = $"X{GameManager.Data.Character_STAT[idx].LV_DOWN_POWER}";
        Text_Points[5].text = $"X{GameManager.Data.Character_STAT[idx].LV_JUMP_COUNT * 15}";
        Text_Points[6].text = $"X{GameManager.Data.Character_STAT[idx].LV_DEF}";
        Text_Level.text = $"X{GameManager.Data.Character_STAT[idx].LV}";
        Text_SP.text = $"X{GameManager.Data.Character_STAT[idx].STAT_POINT}";
        Text_LVUP_G.text = $"X{LV_COST[GameManager.Data.Character_STAT[idx].LV].gold}";
        Text_LVUP_S.text = $"X{LV_COST[GameManager.Data.Character_STAT[idx].LV].Money_Forest}";
        Text_RESET_S.text = "X5";




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
        if (++idx == characters.Length) idx = 0;
        Change(idx);
        
    }


    public void prevCharacter()
    {
        if (--idx == -1) idx = characters.Length - 1;
        Change(idx);
    }

    public void show_info()
    {
        Text_Name.GetComponent<Text>().text = Enum.GetName(typeof(DataManager.Characters), idx);
        if ((int)GameManager.Data.Now_Character == idx)
        {
            GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
            GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = false;

        }
        else
        {
            if(GameManager.Data.Buy_Character[idx] == true)
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
        /*
        GameObject.Find("UI/Text_Perform").GetComponent<Text>().text =
              $"    LV     : {GameManager.Data.Character_STAT[idx].LV}\n"
            + $"STAT POINT : {GameManager.Data.Character_STAT[idx].STAT_POINT}\n"
            + $"  MAX HP   : {GameManager.Data.Character_STAT[idx].MAX_HP + GameManager.Data.Talent_HP}\n"
            + $"  SPEED    : {GameManager.Data.Character_STAT[idx].SPEED}\n"
            + $"   JUMP    : {GameManager.Data.Character_STAT[idx].JUMP_POWER}\n"
            + $"   DOWN    : {GameManager.Data.Character_STAT[idx].DOWN_POWER}\n"
            + $" JUMP CNT  : {GameManager.Data.Character_STAT[idx].JUMP_COUNT}\n"
            + $"   DEF     : {GameManager.Data.Character_STAT[idx].DEF + GameManager.Data.Talent_DEF}\n"
            + $"   LUK     : {GameManager.Data.Character_STAT[idx].LUK + GameManager.Data.Talent_LUK}\n"
            + $"  ACTIVE   : {GameManager.Data.Character_STAT[idx].ACTIVE}";
        */
    }

    public void select()
    {

        GameManager.Data.Now_Character = (DataManager.Characters)(idx);

        //temp
        GameManager.Data.luck = GameManager.Data.Character_STAT[idx].LUK;
        show_info();
    }

    public void buy()
    {
        if(GameManager.Data.Gold >= GameManager.Data.Cost_Character[idx])
        {
            GameManager.Data.Buy_Character[idx] = true;
            GameManager.Data.Gold -= GameManager.Data.Cost_Character[idx];
            GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
            show_info();
        }
    }

    public void UP_LV()
    {
        if(GameManager.Data.Character_STAT[idx].LV < 6 &&
            (GameManager.Data.Gold >= LV_COST[GameManager.Data.Character_STAT[idx].LV].gold &&
            GameManager.Data.Money_Forest >= LV_COST[GameManager.Data.Character_STAT[idx].LV].Money_Forest))
        {
            GameManager.Data.Gold -= LV_COST[GameManager.Data.Character_STAT[idx].LV].gold;
            GameManager.Data.Money_Forest -= LV_COST[GameManager.Data.Character_STAT[idx].LV].Money_Forest;

            GameManager.Data.Character_STAT[idx].STAT_POINT += SP[GameManager.Data.Character_STAT[idx].LV];
            GameManager.Data.Character_STAT[idx].LV += 1;
            
        }
        SetGage();
    }

    public void UP_HP()
    {
        if(GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_MAX_HP)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_MAX_HP;
            GameManager.Data.Character_STAT[idx].MAX_HP += GameManager.Data.Character_STAT[idx].LV_MAX_HP * 10;
            GameManager.Data.Character_STAT[idx].LV_MAX_HP += 1;
            
        }
        SetGage();
    }

    public void UP_SPEED()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_SPEED)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_SPEED;
            GameManager.Data.Character_STAT[idx].SPEED += GameManager.Data.Character_STAT[idx].LV_SPEED * 0.5f;
            GameManager.Data.Character_STAT[idx].LV_SPEED += 1;
            
        }
        SetGage();
    }

    public void UP_JUMP_P()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_JUMP_POWER)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_JUMP_POWER;
            GameManager.Data.Character_STAT[idx].JUMP_POWER += GameManager.Data.Character_STAT[idx].LV_JUMP_POWER * 0.5f;
            GameManager.Data.Character_STAT[idx].LV_JUMP_POWER += 1;
        }
        SetGage();

    }

    public void UP_DOWN_P()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_DOWN_POWER)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_DOWN_POWER;
            GameManager.Data.Character_STAT[idx].DOWN_POWER += GameManager.Data.Character_STAT[idx].LV_DOWN_POWER * 0.5f;
            GameManager.Data.Character_STAT[idx].LV_DOWN_POWER += 1;
        }
        SetGage();

    }

    public void UP_JUMP_C()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (GameManager.Data.Character_STAT[idx].LV_JUMP_COUNT * 15))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_JUMP_COUNT * 15;
            GameManager.Data.Character_STAT[idx].JUMP_COUNT += 1;
            GameManager.Data.Character_STAT[idx].LV_JUMP_COUNT += 1;
        }
            SetGage();

    }

    public void UP_DEF()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_DEF)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_DEF;
            GameManager.Data.Character_STAT[idx].DEF += GameManager.Data.Character_STAT[idx].LV_DEF * 0.01f;
            GameManager.Data.Character_STAT[idx].LV_DEF += 1;
        }
        SetGage();

    }

    public void UP_LUK()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_LUK)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_LUK;
            GameManager.Data.Character_STAT[idx].LUK += GameManager.Data.Character_STAT[idx].LV_LUK * 1;
            GameManager.Data.Character_STAT[idx].LV_LUK += 1;
        }
        SetGage();

    }

    public void Reset_SP()
    {
        Debug.Log("리셋!"+Natural_STAT[idx].JUMP_COUNT);
        if(GameManager.Data.Money_Forest >= 5)
        {
            GameManager.Data.Money_Forest -= 5;
            GameManager.Data.Character_STAT[idx].STAT_POINT = 0;
            for (int i = 0; i < GameManager.Data.Character_STAT[idx].LV; i++)
            {
                GameManager.Data.Character_STAT[idx].STAT_POINT += SP[i];
            }

            GameManager.Data.Character_STAT[idx] = new Character(Natural_STAT[idx]);
            
        }
        SetGage();

    }

}
