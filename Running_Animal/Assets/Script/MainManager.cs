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

    GameObject character; // 화면 중앙에 표시할 캐릭터
    GameObject theme;
    void Start()
    {
        // Data에 저장되어있는 사용하고있는 캐릭터에대한 정보를 받아온 후, 그 캐릭터를 생성.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Now_Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        // 기존에 선택해서 즐기고있던 테마를 로드한다.
        theme = GameObject.Find("UI/Image_Map");
        theme.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Select_Theme/" + GameManager.Data.Theme) as Sprite;
    }

    void Update()
    {
        
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
}
