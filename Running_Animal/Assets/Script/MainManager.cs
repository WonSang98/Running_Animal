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
    Button Select_Diff;

    int temp_diff; // �ӽ÷� ���õǾ��ִ� ���̵�
    void Start()
    {
        // Data�� ����Ǿ��ִ� ����ϰ��ִ� ĳ���Ϳ����� ������ �޾ƿ� ��, �� ĳ���͸� ����.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Now_Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.transform.localPosition = new Vector3(0, -3.5f, 0);
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        // ������ �����ؼ� �����ִ� �׸��� �ε��Ѵ�.
        theme = GameObject.Find("UI/Image_Map");
        theme.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Select_Theme/" + GameManager.Data.Theme) as Sprite;

        GameManager.Instance.Set_Stat();

        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(false);
        temp_diff = GameManager.Data.Diff_LV;

        GameObject.Find("UI/Button_Difficulty/Text").GetComponent<Text>().text = 
            GameObject.Find("UI").transform.Find("Panel_Difficulty/Scroll View/Viewport/Content/Button_DIff_" + GameManager.Data.Diff_LV.ToString() + "/Text_Diff").GetComponent<Text>().text;

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
        for (int i = 0; i < 10; i++)
        {
            string p = "UI/Panel_Difficulty/Scroll View/Viewport/Content/Button_DIff_" + i.ToString();
            Debug.Log(p);
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
        GameManager.Data.Diff_LV = temp_diff;
        GameManager.Instance.Set_Stat();
        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(false);
    }

    void SetDiff(int i) //���̵� ����.
    {
        temp_diff = i;
        string difficulty = GameObject.Find("UI/Panel_Difficulty/Scroll View/Viewport/Content/Button_DIff_" + i.ToString() + "/Text_Diff").GetComponent<Text>().text;
        Text info = GameObject.Find("UI/Panel_Difficulty/Panel_Info/Text_info").GetComponent<Text>();
        GameObject.Find("UI/Panel_Difficulty/Panel_Info/Text_Diff").GetComponent<Text>().text = difficulty;
        GameObject.Find("UI/Button_Difficulty/Text").GetComponent<Text>().text = difficulty;
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
                    $"[�ǰ� ������]\n - {GameManager.Data.Forest_Diff[i].DMG} ����\n" +
                    $"[��� ȹ�淮]\n - {GameManager.Data.Forest_Diff[i].COIN * 100}% ����\n" +
                    $"[ȸ��ȿ��]\n - {GameManager.Data.Forest_Diff[i].RESTORE * 100}% ����\n" +
                    $"[���]\n - {GameManager.Data.Forest_Diff[i].LUK} ����\n" +
                    $"[����]\n - {GameManager.Data.Forest_Diff[i].DEF * 100}% ���� \n" +
                    $"[�ӷ�]\n - {GameManager.Data.Forest_Diff[i].SPEED * 100} ����\n" +
                    $"[�ʿ����ġ]\n - {GameManager.Data.Forest_Diff[i].EXP} ����\n";
                    
                break;
        }
    }
}
