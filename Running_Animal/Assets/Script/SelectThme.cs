using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Image Component 받아오기 위함.

public class SelectThme : MonoBehaviour
{
    //Image_Map 에 쓰일 Script
    // 좌우 버튼에 각각 prev, nextTheme 함수를 부여하여 Click 시마다 Image파일의 순서를 확인하여 변경함.

    private void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Select_Theme/" + GameManager.Data.Theme) as Sprite;
    }
    private void Update()
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
        GetComponent<Image>().sprite = Resources.Load<Sprite>(path) as Sprite;
    }
}
