using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Image Component �޾ƿ��� ����.

public class SelectThme : MonoBehaviour
{
    //Image_Map �� ���� Script
    // �¿� ��ư�� ���� prev, nextTheme �Լ��� �ο��Ͽ� Click �ø��� Image������ ������ Ȯ���Ͽ� ������.

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
