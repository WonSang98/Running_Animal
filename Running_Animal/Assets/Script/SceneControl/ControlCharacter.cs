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
public class ControlCharacter : MonoBehaviour
{
    string[] Ability = { "HP", "SPEED", "LUK", "JUMPP", "DOWNP", "JUMPC", "DEF" };
    //Scene 'Character'�� ���� SceneManager

    // ǥ�õ� ĳ���͵��� �迭
    GameObject[] characters;
    GameObject Show_Character;

    int idx; // ���� ���� �ִ� ĳ���� ����

    Button Button_Upgrade;
    Button Button_Main;

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

    short[] SP = { 2, 4, 6, 8, 10 };


    void Start()
    {
        //��� ĳ���� �������� �ҷ��ͼ�... ����
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

    void SetGage() // �ɷ�ġ �������� ���� ĳ���� �ɷ�ġ�� ���߾� �����Ѵ�.
    {
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
        //Text����
        // 1. ���׷��̵� �ʿ� ��� ����
        // 2. ������ �� ���� ��ǥ��
        // 3. �ܿ� ���� ǥ��

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

    //���� �ʱ�ȭ �ϱ�.
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
