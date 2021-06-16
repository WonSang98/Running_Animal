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

    GameObject character; // ȭ�� �߾ӿ� ǥ���� ĳ����
    GameObject theme;
    void Start()
    {
        // Data�� ����Ǿ��ִ� ����ϰ��ִ� ĳ���Ϳ����� ������ �޾ƿ� ��, �� ĳ���͸� ����.
        var path_character = Resources.Load("Character/" + (int)GameManager.Data.Now_Character, typeof(GameObject));
        character = Instantiate(path_character) as GameObject;
        character.GetComponent<Rigidbody2D>().gravityScale = 0;

        // ������ �����ؼ� �����ִ� �׸��� �ε��Ѵ�.
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
