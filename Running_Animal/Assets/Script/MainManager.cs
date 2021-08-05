using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
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
    void Start()
    {
        // Data에 저장되어있는 사용하고있는 캐릭터에대한 정보를 받아온 후, 그 캐릭터를 생성.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Now_Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        // 기존에 선택해서 즐기고있던 테마를 로드한다.
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

    public void OnDiff() // 난이도 패널 오픈
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

    public void SetDiff(int i) //난이도 설정.
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
                    $"난이도      :{i} \n" +
                    $"피격 데미지 :{GameManager.Data.Forest_Diff[i].DMG}\n" +
                    $"코인 획득량 :{GameManager.Data.Forest_Diff[i].COIN}\n" +
                    $"회복효율감소:{GameManager.Data.Forest_Diff[i].RESTORE}\n" +
                    $"행운감소    :{GameManager.Data.Forest_Diff[i].LUK}\n" +
                    $"방어력감소  :{GameManager.Data.Forest_Diff[i].DEF}\n" +
                    $"속도 증가   :{GameManager.Data.Forest_Diff[i].SPEED}\n" +
                    $"필요경험치업:{GameManager.Data.Forest_Diff[i].EXP}\n" +
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
