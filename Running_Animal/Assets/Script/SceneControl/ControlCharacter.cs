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
    string[] Ability = { "HP", "SPEED", "LUK", "JUMPP", "DOWNP", "JUMPC", "DEF" };
    //Scene 'Character'에 사용될 SceneManager

    // 표시될 캐릭터들의 배열
    GameObject[] characters;
    GameObject Show_Character;

    int idx; // 현재 보고 있는 캐릭터 순서

    Button Button_Upgrade;
    Button Button_Main;

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

    short[] SP = { 2, 4, 6, 8, 10 };


    void Start()
    {
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

        Button_Main = GameObject.Find("UI/Button_Back").GetComponent<Button>();
        Button_Main.onClick.AddListener(() => gameObject.GetComponent<LoadScene>().OnMain());
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
        Show_Character.GetComponent<SpriteRenderer>().flipX = true;
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
        Panel_Upgrade.SetActive(false);
        Button_Upgrade.interactable = true;
    }

    void SetGage() // 능력치 게이지를 현재 캐릭터 능력치에 맞추어 적용한다.
    {
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
                    idx_normal = (int)(Character.Natural[idx].ability.MAX_HP.value / 10);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].ability.MAX_HP.value - Character.Natural[idx].ability.MAX_HP.value) / 10);
                    idx_talent = idx_upgrade + (int)(GameManager.Data.Talent.HP.value / 10);
                    break;
                case "SPEED":
                    idx_normal = (int)(Character.Natural[idx].ability.SPEED.value * 2);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].ability.SPEED.value - Character.Natural[idx].ability.SPEED.value) * 2);
                    idx_talent = idx_upgrade;
                    break;
                case "LUK":
                    idx_normal = (Character.Natural[idx].ability.LUK.value);
                    idx_upgrade = idx_normal + (GameManager.Data.Character_STAT[idx].ability.LUK.value - Character.Natural[idx].ability.LUK.value);
                    idx_talent = idx_upgrade + (GameManager.Data.Talent.LUK.value);
                    break;
                case "JUMPP":
                    idx_normal = (int)(Character.Natural[idx].ability.JUMP.value * 2);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].ability.JUMP.value - Character.Natural[idx].ability.JUMP.value) * 2);
                    idx_talent = idx_upgrade;
                    break;
                case "DOWNP":
                    idx_normal = (int)(Character.Natural[idx].ability.DOWN.value * 2);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].ability.DOWN.value - Character.Natural[idx].ability.DOWN.value) * 2);
                    idx_talent = idx_upgrade;
                    break;
                case "JUMPC":
                    idx_normal = (Character.Natural[idx].ability.MAX_JUMP.value * 20);
                    idx_upgrade = idx_normal + ((GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.value - Character.Natural[idx].ability.MAX_JUMP.value) * 20);
                    idx_talent = idx_upgrade;
                    break;
                case "DEF":
                    idx_normal = (int)(Character.Natural[idx].ability.DEF.value * 100);
                    idx_upgrade = idx_normal + (int)((GameManager.Data.Character_STAT[idx].ability.DEF.value - Character.Natural[idx].ability.DEF.value) * 100);
                    Debug.Log(GameManager.Data.Character_STAT[idx].ability.DEF.value);
                    idx_talent = idx_upgrade + (int)(GameManager.Data.Talent.DEF.value * 100);
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

        Text_Points[0].text = $"X{GameManager.Data.Character_STAT[idx].ability.MAX_HP.level}";
        Text_Points[1].text = $"X{GameManager.Data.Character_STAT[idx].ability.SPEED.level}";
        Text_Points[2].text = $"X{GameManager.Data.Character_STAT[idx].ability.LUK.level}";
        Text_Points[3].text = $"X{GameManager.Data.Character_STAT[idx].ability.JUMP.level}";
        Text_Points[4].text = $"X{GameManager.Data.Character_STAT[idx].ability.DOWN.level}";
        Text_Points[5].text = $"X{GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level * 15}";
        Text_Points[6].text = $"X{GameManager.Data.Character_STAT[idx].ability.DEF.level}";
        Text_Level.text = $"{GameManager.Data.Character_STAT[idx].LV}";
        Text_SP.text = $"{GameManager.Data.Character_STAT[idx].STAT_POINT}";
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
        Text_Name.GetComponent<Text>().text = Enum.GetName(typeof(Character.CHARACTER_CODE), idx);
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
        }
        SetGage();

    }

    public void UP_DOWN_P()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (short)(GameManager.Data.Character_STAT[idx].ability.DOWN.level + 1))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.DOWN.level + 1);
            GameManager.Data.Character_STAT[idx].ability.DOWN.value += (GameManager.Data.Character_STAT[idx].ability.DOWN.level + 1) * 0.5f;
            GameManager.Data.Character_STAT[idx].ability.DOWN.level += 1;
        }
        SetGage();

    }

    public void UP_JUMP_C()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= ((GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level+1) * 15))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= (short)(GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level * 15);
            GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.value += 1;
            GameManager.Data.Character_STAT[idx].ability.MAX_JUMP.level += 1;
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
        }
        SetGage();

    }

    public void Reset_SP()
    {
        if(GameManager.Data.Money.Speacial[0] >= (GameManager.Data.Character_STAT[idx].LV + 1))
        {
            GameManager.Data.Money.Speacial[0] -= (GameManager.Data.Character_STAT[idx].LV + 1);
            GameManager.Data.Character_STAT[idx].STAT_POINT = 0;

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

}
