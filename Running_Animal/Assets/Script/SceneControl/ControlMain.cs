using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlMain : MonoBehaviour
{
    // Main Scene을 제어하는 스크립트
    // SceneManager GameObject에 부착하여 사용할 예정이다.
    /*
     * 주요 기능
     * 좌우 버튼 클릭하여 테마 변경.
     * 씬 실행시 화면 중앙에 현재 설정되어있는 캐릭터 표시
     * 그 외 기타 버튼 UI 동작
     */

    Text Level_info;
    GameObject character; // 화면 중앙에 표시할 캐릭터
    GameObject theme;
    Button[] Level = new Button[10]; // Level 선택 버튼
    Button Select_Diff;

    int temp_diff; // 임시로 선택되어있는 난이도
    void Start()
    {
        // Data에 저장되어있는 사용하고있는 캐릭터에대한 정보를 받아온 후, 그 캐릭터를 생성.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Preset.Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.transform.localPosition = new Vector3(0, -3.5f, 0);
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        // 기존에 선택해서 즐기고있던 테마를 로드한다.
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

    public void OnDiff() // 난이도 패널 오픈
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

    void SetDiff(int i) //난이도 설정.
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
                    $"[피격 데미지]\n - {Difficulty.Forest[i].DMG} 증가\n" +
                    $"[골드 획득량]\n - {Difficulty.Forest[i].COIN * 100}% 증가\n" +
                    $"[회복효율]\n - {Difficulty.Forest[i].RESTORE * 100}% 감소\n" +
                    $"[행운]\n - {Difficulty.Forest[i].LUK} 증가\n" +
                    $"[방어력]\n - {Difficulty.Forest[i].DEF * 100}% 감소 \n" +
                    $"[속력]\n - {Difficulty.Forest[i].SPEED * 100} 증가\n" +
                    $"[필요경험치]\n - {Difficulty.Forest[i].EXP} 증가\n";
                break;
        }
    }
}
