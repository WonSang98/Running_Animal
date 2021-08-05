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
    //Scene 'Character'�� ���� SceneManager

    // ǥ�õ� ĳ���͵��� �迭
    GameObject[] characters;
    GameObject Show_Character;

    int idx; // ���� ���� �ִ� ĳ���� ����

    // ���� �� ���
    cost[] LV_COST = {new cost(500, 1),
                      new cost(1000, 2),
                      new cost(1500, 3),
                      new cost(2000, 4),
                      new cost(2500, 5),
                      new cost(99999, 9999)};

    int[] SP = { 2, 4, 6, 8, 10 };

    // ĳ���� DEFAULT ��
    Character[] Natural_STAT =
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
        Show_Character.transform.localPosition = Vector3.zero;
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
        show_info();
    }

    void Update()
    {
        
    }

    public void nextCharacter()
    {
        if (++idx == characters.Length) idx = 0;
        Destroy(Show_Character);
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = Vector3.zero;
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
        show_info();

    }


    public void prevCharacter()
    {
        if (--idx == -1) idx = characters.Length - 1;
        Destroy(Show_Character);
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = Vector3.zero;
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
        show_info();

    }

    public void show_info()
    {
        GameObject.Find("UI/Text_Name").GetComponent<Text>().text = Enum.GetName(typeof(DataManager.Characters), idx);
        string select;
        if ((int)GameManager.Data.Now_Character == idx)
        {
            select = "�̹̾�����!";
            GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
            GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = false;
            GameObject.Find("UI/Button_Select/Text_Select").GetComponent<Text>().text = select;

        }
        else
        {
            if(GameManager.Data.Buy_Character[idx] == true)
            {
                select = "����ҷ�!";
                GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
                GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
                GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = true;
                GameObject.Find("UI/Button_Select/Text_Select").GetComponent<Text>().text = select;
            }
            else
            {
                Debug.Log("TTTTT");
                GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(false);
                GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(true);
                GameObject.Find("UI/Button_Buy/Text_Buy").GetComponent<Text>().text = "�췡?";
            }
        }

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
        else
        {
            GameObject.Find("UI/Button_Buy/Text_Buy").GetComponent<Text>().text = "���;�?";
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
            show_info();
        }
    }

    public void UP_HP()
    {
        if(GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_MAX_HP)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_MAX_HP;
            GameManager.Data.Character_STAT[idx].MAX_HP += GameManager.Data.Character_STAT[idx].LV_MAX_HP * 10;
            GameManager.Data.Character_STAT[idx].LV_MAX_HP += 1;
            show_info();
        }
    }

    public void UP_SPEED()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_SPEED)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_SPEED;
            GameManager.Data.Character_STAT[idx].SPEED += GameManager.Data.Character_STAT[idx].LV_SPEED * 0.5f;
            GameManager.Data.Character_STAT[idx].LV_SPEED += 1;
            show_info();
        }
    }

    public void UP_JUMP_P()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_JUMP_POWER)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_JUMP_POWER;
            GameManager.Data.Character_STAT[idx].JUMP_POWER += GameManager.Data.Character_STAT[idx].LV_JUMP_POWER * 0.5f;
            GameManager.Data.Character_STAT[idx].LV_JUMP_POWER += 1;
            show_info();
        }
    }

    public void UP_DOWN_P()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_DOWN_POWER)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_DOWN_POWER;
            GameManager.Data.Character_STAT[idx].DOWN_POWER += GameManager.Data.Character_STAT[idx].LV_DOWN_POWER * 0.5f;
            GameManager.Data.Character_STAT[idx].LV_DOWN_POWER += 1;
            show_info();
        }
    }

    public void UP_JUMP_C()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= (GameManager.Data.Character_STAT[idx].LV_JUMP_COUNT * 15))
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_JUMP_COUNT * 15;
            GameManager.Data.Character_STAT[idx].JUMP_COUNT += 1;
            GameManager.Data.Character_STAT[idx].LV_JUMP_COUNT += 1;
            show_info();
        }
    }

    public void UP_DEF()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_DEF)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_DEF;
            GameManager.Data.Character_STAT[idx].DEF += GameManager.Data.Character_STAT[idx].LV_DEF * 0.01f;
            GameManager.Data.Character_STAT[idx].LV_DEF += 1;
            show_info();
        }
    }

    public void UP_LUK()
    {
        if (GameManager.Data.Character_STAT[idx].STAT_POINT >= GameManager.Data.Character_STAT[idx].LV_LUK)
        {
            GameManager.Data.Character_STAT[idx].STAT_POINT -= GameManager.Data.Character_STAT[idx].LV_LUK;
            GameManager.Data.Character_STAT[idx].LUK += GameManager.Data.Character_STAT[idx].LV_LUK * 1;
            GameManager.Data.Character_STAT[idx].LV_LUK += 1;
            show_info();
        }
    }

    public void Reset_SP()
    {
        if(GameManager.Data.Money_Forest >= 5)
        {
            GameManager.Data.Money_Forest -= 5;
            GameManager.Data.Character_STAT[idx].STAT_POINT = 0;
            for (int i = 0; i < GameManager.Data.Character_STAT[idx].LV; i++)
            {
                GameManager.Data.Character_STAT[idx].STAT_POINT += SP[i];
            }

            GameManager.Data.Character_STAT[idx] = Natural_STAT[idx];
            show_info();
        }

    }

}
