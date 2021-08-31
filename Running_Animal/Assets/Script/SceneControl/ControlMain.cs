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

    Button Button_Shop;
    Button Button_ThemeNext;
    Button Button_ThemePrev;
    Button Button_Mission;

    // Sound 변수 선언
    AudioClip clip; //사운드 클립 <- *변수명 변경 추천* + 여러개를 넣어야한다면 배열을 사용하자.
    AudioClip clip2;
    AudioClip clip3;
    AudioClip clip4;
    AudioClip Clip_BGM; // BackGroundMusic
    
    int temp_diff; // 임시로 선택되어있는 난이도

    //튜토리얼 관련
    GameObject Canvas_Tuto;
    GameObject[] Text_Tuto;
    GameObject Button_Tuto;
    short cnt;


    LoadScene LS;
    void Start()
    {
        LS = GameManager.Instance.GetComponent<LoadScene>();
        if(GameManager.Data.TutoData.tuto0 == false)
        {
            LS.OnTutorial();
        }

        // Data에 저장되어있는 사용하고있는 캐릭터에대한 정보를 받아온 후, 그 캐릭터를 생성.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Preset.Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.transform.localPosition = new Vector3(0, -3.5f, 0);
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        Button_Shop = GameObject.Find("UI/Button_Shop").GetComponent<Button>();
        Button_ThemeNext = GameObject.Find("UI/Image_Map/next").GetComponent<Button>();
        Button_ThemePrev = GameObject.Find("UI/Image_Map/prev").GetComponent<Button>();
        Button_Mission = GameObject.Find("UI/Button_Mission").GetComponent<Button>();

        Button_Shop.interactable = false;
        Button_ThemeNext.interactable = false;
        Button_ThemePrev.interactable = false;
        Button_Mission.interactable = false;

        // 기존에 선택해서 즐기고있던 테마를 로드한다.
        /*
        theme = GameObject.Find("UI/Image_Map");
        theme.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Select_Theme/" + GameManager.Data.Preset.Theme) as Sprite;
        */
        temp_diff = GameManager.Data.Preset.Difficult;
        GameObject.Find("UI").transform.Find("Panel_Difficulty").gameObject.SetActive(false);
        GameObject.Find("UI/Button_Difficulty/Text").GetComponent<Text>().text =
        GameObject.Find("UI").transform.Find("Panel_Difficulty/Scroll View/Viewport/Content/Button_DIff_" + GameManager.Data.Preset.Difficult.ToString() + "/Text_Diff").GetComponent<Text>().text;
        LoadSound();
        GameManager.Sound.BGMPlay(Clip_BGM);

        Canvas_Tuto = GameObject.Find("UI-Tutorial").transform.Find("Panel").gameObject;
        Text_Tuto = new GameObject[4];
        Button_Tuto = GameObject.Find("UI-Tutorial").transform.Find("Button_OK").gameObject;
        for (int i = 0; i < 4; i++)
        {
            Text_Tuto[i] = GameObject.Find("UI-Tutorial").transform.Find("Text" + i.ToString()).gameObject;
        }
        if (GameManager.Data.TutoData.tuto1 == false)
        {
            cnt = 0;
            Canvas_Tuto.SetActive(true);
            Text_Tuto[0].SetActive(true);
            Text_Tuto[1].SetActive(false);
            Text_Tuto[2].SetActive(false);
            Text_Tuto[3].SetActive(false);
            Button_Tuto.SetActive(true);
            Button_Tuto.GetComponent<Button>().onClick.AddListener(() => NextTuto());
        }
        else
        {
            Canvas_Tuto.SetActive(false);
            Text_Tuto[0].SetActive(false);
            Text_Tuto[1].SetActive(false);
            Text_Tuto[2].SetActive(false);
            Text_Tuto[3].SetActive(false);
            Button_Tuto.SetActive(false);
        }
    }
    void NextTuto()
    {
        cnt += 1;
        if (cnt >= 4)
        {
            Canvas_Tuto.SetActive(false);
            Text_Tuto[0].SetActive(false);
            Text_Tuto[1].SetActive(false);
            Text_Tuto[2].SetActive(false);
            Text_Tuto[3].SetActive(false);
            Button_Tuto.SetActive(false);
            GameManager.Data.TutoData.tuto1 = true;
        }
        else
        {
            for(int i=0; i<4; i++)
            {
                Text_Tuto[i].SetActive(false);
            }
            Text_Tuto[cnt].SetActive(true);
        }
    }
    void LoadSound() //Sound Resoucres 경로 찾아와서 불러와놓기.
    {
        clip = Resources.Load<AudioClip>("Sound/Common/000_Manu_Sound");
        clip2 = Resources.Load<AudioClip>("Sound/Common/002_Paper");
        clip3 = Resources.Load<AudioClip>("Sound/Common/006_Glass");
        clip4 = Resources.Load<AudioClip>("Sound/Common/007_Stamp");
        Clip_BGM = Resources.Load<AudioClip>("Sound/BGM/000_Main_BGM");
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
        GameManager.Sound.SFXPlay(clip); // 난이도 패널 오픈 시 버튼 클릭 소리 재생
        GameManager.Sound.SFXPlay(clip2);
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
        GameManager.Sound.SFXPlay(clip4);
    }

    void SetDiff(int i) //난이도 설정.
    {
        if(temp_diff != i)
        {
            temp_diff = i;
            GameManager.Sound.SFXPlay(clip3);
        }
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
                    $"[행운]\n - {Difficulty.Forest[i].LUK} 감소\n" +
                    $"[방어력]\n - {Difficulty.Forest[i].DEF * 100}% 감소 \n" +
                    $"[속력]\n - {Difficulty.Forest[i].SPEED * 100} 증가\n" +
                    $"[필요경험치]\n - {Difficulty.Forest[i].EXP} 증가\n";
                break;
        }
    }
}
