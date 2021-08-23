using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlMain : MonoBehaviour
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
    Button Select_Diff;

    int temp_diff; // �ӽ÷� ���õǾ��ִ� ���̵�
    void Start()
    {
        // Data�� ����Ǿ��ִ� ����ϰ��ִ� ĳ���Ϳ����� ������ �޾ƿ� ��, �� ĳ���͸� ����.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Preset.Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.transform.localPosition = new Vector3(0, -3.5f, 0);
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        // ������ �����ؼ� �����ִ� �׸��� �ε��Ѵ�.
        theme = GameObject.Find("UI/Image_Map");
        theme.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Select_Theme/" + GameManager.Data.Preset.Theme) as Sprite;

        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(false);
        temp_diff = GameManager.Data.Preset.Difficult;

        GameObject.Find("UI/Button_Difficulty/Text").GetComponent<Text>().text = 
            GameObject.Find("UI").transform.Find("Panel_Difficulty/Scroll View/Viewport/Content/Button_DIff_" + GameManager.Data.Preset.Difficult.ToString() + "/Text_Diff").GetComponent<Text>().text;

    }

    public void nextTheme()
    {
        switch (GameManager.Data.Preset.Theme)
        {
            case Theme.THEME_CODE.Forest:
                SetTheme(Theme.THEME_CODE.Desert);
                break;
            case Theme.THEME_CODE.Desert:
                SetTheme(Theme.THEME_CODE.Arctic);
                break;
            case Theme.THEME_CODE.Arctic:
                SetTheme(Theme.THEME_CODE.Forest);
                break;
        }
    }


    public void prevTheme()
    {
        switch (GameManager.Data.Preset.Theme)
        {
            case Theme.THEME_CODE.Forest:
                SetTheme(Theme.THEME_CODE.Arctic);
                break;
            case Theme.THEME_CODE.Desert:
                SetTheme(Theme.THEME_CODE.Forest);
                break;
            case Theme.THEME_CODE.Arctic:
                SetTheme(Theme.THEME_CODE.Desert);
                break;
        }
    }

    void SetTheme(Theme.THEME_CODE t)
    {
        GameManager.Data.Preset.Theme = t;
        string path = "Image/Select_Theme/" + t.ToString();
        theme.GetComponent<Image>().sprite = Resources.Load<Sprite>(path) as Sprite;
    }

    public void OnDiff() // ���̵� �г� ����
    {
        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            string p = "UI/Panel_Difficulty/Scroll View/Viewport/Content/Button_DIff_" + i.ToString();
            Level[i] = GameObject.Find(p).GetComponent<Button>();
        }

        for (int j = 0; j < 10; j++)
        {
            int temp = j;
            Level[j].onClick.RemoveAllListeners();
            Level[j].onClick.AddListener(() => SetDiff(temp));
        }

        Select_Diff = GameObject.Find("UI/Panel_Difficulty/Panel_Info/Button_Select").GetComponent<Button>();
        Select_Diff.onClick.RemoveAllListeners();
        Select_Diff.onClick.AddListener(Select);
        SetDiff(temp_diff);
    }
    void Select()
    {
        GameManager.Data.Preset.Difficult = temp_diff;
        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(false);
    }

    void SetDiff(int i) //���̵� ����.
    {
        temp_diff = i;
        string difficulty = GameObject.Find("UI/Panel_Difficulty/Scroll View/Viewport/Content/Button_DIff_" + i.ToString() + "/Text_Diff").GetComponent<Text>().text;
        Text info = GameObject.Find("UI/Panel_Difficulty/Panel_Info/Text_info").GetComponent<Text>();
        GameObject.Find("UI/Panel_Difficulty/Panel_Info/Text_Diff").GetComponent<Text>().text = difficulty;
        GameObject.Find("UI/Button_Difficulty/Text").GetComponent<Text>().text = difficulty;
        switch (GameManager.Data.Preset.Theme)
        {
            case Theme.THEME_CODE.Forest:
                info.text =
                    $"[�ǰ� ������]\n - {Difficulty.Forest[i].DMG} ����\n" +
                    $"[��� ȹ�淮]\n - {Difficulty.Forest[i].COIN * 100}% ����\n" +
                    $"[ȸ��ȿ��]\n - {Difficulty.Forest[i].RESTORE * 100}% ����\n" +
                    $"[���]\n - {Difficulty.Forest[i].LUK} ����\n" +
                    $"[����]\n - {Difficulty.Forest[i].DEF * 100}% ���� \n" +
                    $"[�ӷ�]\n - {Difficulty.Forest[i].SPEED * 100} ����\n" +
                    $"[�ʿ����ġ]\n - {Difficulty.Forest[i].EXP} ����\n";
                break;
        }
    }
}
