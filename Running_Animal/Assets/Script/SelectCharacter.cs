using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // enum index�� ����

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
    //Scene 'Character'�� ���� SceneManager

    // ǥ�õ� ĳ���͵��� �迭
    GameObject[] characters;
    GameObject Show_Character;

    int idx; // ���� ���� �ִ� ĳ���� ����

    Button Button_Upgrade;

    GameObject Panel_Upgrade; // ���� �������� ���̰�...
    GameObject Text_Name; // ���õ� ĳ������ �̸� ǥ��.

    bool flag_ON; // ����â�� �������� �ȴ������� Ȯ�ο� 

    // ������ ä��� ���� �̹���
    Sprite Gage_None;
    Sprite Gage_Normal;
    Sprite Gage_Upgrade;
    Sprite Gage_Talent;

    // ���� â infomations ���� �����ϱ� ���� Texts
    Text[] Text_Points = new Text[7];
    Text Text_Level;
    Text Text_SP;
    Text Text_LVUP_G;
    Text Text_LVUP_S;
    Text Text_RESET_S;

    Animator Animator_UPanel; // Panel_Upgrade ���� �ִϸ�����
    // ���� �� ���
    cost[] LV_COST = {new cost(500, 1),
                      new cost(1000, 2),
                      new cost(1500, 3),
                      new cost(2000, 4),
                      new cost(2500, 5),
                      new cost(99999, 9999)};

    int[] SP = { 2, 4, 6, 8, 10 };

    // ĳ���� DEFAULT ��
    static Character[] Natural_STAT =
    {
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None),
        new Character(0, 0, 100, 1, 8, 1, 10, 1, 20, 1, 2, 1, 0, 1, 5, 1, DataManager.Active_Skil.None)
    };

    void Start()
    {
        //��� ĳ���� �������� �ҷ��ͼ�... ����
        characters = Resources.LoadAll<GameObject>("Character/");
        idx = (int)GameManager.Data.Now_Character;
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = new Vector3(0, -2.5f, 0);
        Show_Character.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Show_Character.transform.Find("Foot").gameObject.SetActive(true);
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;

        Button_Upgrade = GameObject.Find("UI/Button_Upgrade").GetComponent<Button>();
        Button_Upgrade.onClick.AddListener(() => Click_Upgrade());

        //Panel Upgrade ����ȭ, �׸��� ��Ȱ��ȭ.
        Panel_Upgrade = GameObject.Find("UI").transform.Find("Panel_Upgrade").gameObject;
        Animator_UPanel = Panel_Upgrade.GetComponent<Animator>();
        Panel_Upgrade.SetActive(false);

        //GageBar ä��� ���� �̹��� ���ҽ� �ҷ�����
        Gage_None = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_None");
        Gage_Normal = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_Normal");
        Gage_Upgrade = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_Upgrade");
        Gage_Talent = Resources.Load<Sprite>("Image/GUI/Character_Select/Gage_Talent");

        //ĳ���� �̸� ������Ʈ ã�Ƽ� ���..
        Text_Name = GameObject.Find("UI").transform.Find("Text_Name").gameObject;

        //�ȳ��� ���� TEXT
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

    // ĳ���� ��ȭ â OPEN!!!!!!!!!!!!!!!!!!!!!!!
    void OnUpgrade()
    {
        // 1. ĳ���͸� �߾ӿ��� �������� �о�ִ´�.
        StartCoroutine(MoveLeft());
        // 2. ���׷��̵� �г��� �����ش�
        StartCoroutine(OpenPanel());
        flag_ON = true;
    }

    void OffUpgrade()
    {
        StartCoroutine(MoveRight());
        StartCoroutine(ClosePanel());
        flag_ON = false;
    }

    IEnumerator MoveLeft() // ĳ���� �������� �о�ִ� ��ƾ
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

    void SetGage() // �ɷ�ġ �������� ���� ĳ���� �ɷ�ġ�� ���߾� �����Ѵ�.
    {
        Debug.Log(Natural_STAT[idx].JUMP_COUNT);
        //Gage ����
        foreach (string s in Ability)
        {
            string path = "UI/Panel_Upgrade/CONTENTS/Image_Gage_" + s + "/Gage/Gage";
            int idx_normal = 0; // ĳ���� �⺻ �ɷ�ġ�� ���� ����.
            int idx_upgrade = 0; // ĳ���� ���׷��̵� �� �ɷ�ġ�� ���� ����.
            int idx_talent = 0; // ĳ���� ��� �ɷ�ġ�� ���� ����.
            //�� �ɼǺ� ��ġ���� �ʺ�ȭ
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
        //Text����
        // 1. ���׷��̵� �ʿ� ��� ����
        // 2. ������ �� ���� ��ǥ��
        // 3. �ܿ� ���� ǥ��

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
        Debug.Log("����!"+Natural_STAT[idx].JUMP_COUNT);
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
