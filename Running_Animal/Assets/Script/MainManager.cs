using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    // Main Scene�� �����ϴ� ��ũ��Ʈ
    // SceneManager GameObject�� �����Ͽ� ����� �����̴�.
    /*
     * �ֿ� ���
     * �¿� ��ư Ŭ���Ͽ� �׸� ����.
     * �� ����� ȭ�� �߾ӿ� ���� �����Ǿ��ִ� ĳ���� ǥ��
     * �� �� ��Ÿ ��ư UI ����
     */

    Text Level_info;
    GameObject character; // ȭ�� �߾ӿ� ǥ���� ĳ����
    GameObject theme;
    Button[] Level = new Button[10]; // Level ���� ��ư
    void Start()
    {
        // Data�� ����Ǿ��ִ� ����ϰ��ִ� ĳ���Ϳ����� ������ �޾ƿ� ��, �� ĳ���͸� ����.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Now_Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        // ������ �����ؼ� �����ִ� �׸��� �ε��Ѵ�.
        theme = GameObject.Find("UI/Image_Map");
        theme.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Select_Theme/" + GameManager.Data.Theme) as Sprite;

        GameManager.Instance.Set_Stat();

        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(false);

    }

    public void nextTheme()
    {
        switch (GameManager.Data.Theme)
        {
            case DataManager.Themes.Forest:
                SetTheme(DataManager.Themes.Desert);
                break;
            case DataManager.Themes.Desert:
                SetTheme(DataManager.Themes.Arctic);
                break;
            case DataManager.Themes.Arctic:
                SetTheme(DataManager.Themes.Forest);
                break;
        }
    }


    public void prevTheme()
    {
        switch (GameManager.Data.Theme)
        {
            case DataManager.Themes.Forest:
                SetTheme(DataManager.Themes.Arctic);
                break;
            case DataManager.Themes.Desert:
                SetTheme(DataManager.Themes.Forest);
                break;
            case DataManager.Themes.Arctic:
                SetTheme(DataManager.Themes.Desert);
                break;
        }

    }

    void SetTheme(DataManager.Themes t)
    {
        GameManager.Data.Theme = t;
        string path = "Image/Select_Theme/" + t.ToString();
        Debug.Log(path);
        theme.GetComponent<Image>().sprite = Resources.Load<Sprite>(path) as Sprite;
    }

    public void OnDiff() // ���̵� �г� ����
    {
        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(true);
        for (int i = 1; i <= 10; i++)
        {
            string p = "UI/Panel_Difficulty/Button_DIff_" + i.ToString();
            Debug.Log(p);
            Level[i - 1] = GameObject.Find(p).GetComponent<Button>();
        }

        for (int j = 0; j < 10; j++)
        {
            int temp = j;
            Level[j].onClick.RemoveAllListeners();
            Level[j].onClick.AddListener(() => SetDiff(temp));
        }
    }

    public void SetDiff(int i) //���̵� ����.
    {
        GameManager.Data.Diff_LV = i;
        GameManager.Instance.Set_Stat();
        Text info = GameObject.Find("UI/Panel_Difficulty/Panel_Info/Text").GetComponent<Text>();
        switch (GameManager.Data.Theme)
        {
            case DataManager.Themes.Forest:
                float[] temp = { 0, 30, 30, 30, 43, 43, 70, 70, 85, 125, 125, 125, 256, 999999 };
                GameManager.Data.EXP = temp;
                for (int j=1; j<13; j++)
                {
                    GameManager.Data.EXP[j] += GameManager.Data.Forest_Diff[i].EXP;
                }
                info.text =
                    $"���̵�      :{i} \n" +
                    $"�ǰ� ������ :{GameManager.Data.Forest_Diff[i].DMG}\n" +
                    $"���� ȹ�淮 :{GameManager.Data.Forest_Diff[i].COIN}\n" +
                    $"ȸ��ȿ������:{GameManager.Data.Forest_Diff[i].RESTORE}\n" +
                    $"����    :{GameManager.Data.Forest_Diff[i].LUK}\n" +
                    $"���°���  :{GameManager.Data.Forest_Diff[i].DEF}\n" +
                    $"�ӵ� ����   :{GameManager.Data.Forest_Diff[i].SPEED}\n" +
                    $"�ʿ����ġ��:{GameManager.Data.Forest_Diff[i].EXP}\n" +
                    $"MAXHP{GameManager.Data.max_hp}\n" +
                    $"SPEED{GameManager.Data.speed}\n" +
                    $"JUMP{GameManager.Data.jump}\n" +
                    $"down{GameManager.Data.down}\n" +
                    $"max_jump{GameManager.Data.max_jump}\n" +
                    $"defense{GameManager.Data.defense}\n" +
                    $"luck{GameManager.Data.luck}\n" +
                    $"active{GameManager.Data.active}\n" +
                    $"restore_eff{GameManager.Data.restore_eff}\n" +
                    $"damage{GameManager.Data.damage}\n" +
                    $"multi_coin{GameManager.Data.multi_coin}\n";
                break;
        }
    }
}
