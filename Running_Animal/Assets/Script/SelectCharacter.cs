using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // enum index로 접근

public class SelectCharacter : MonoBehaviour
{
    //Scene 'Character'에 사용될 SceneManager

    // 표시될 캐릭터들의 배열
    GameObject[] characters;
    GameObject Show_Character;
    int idx;
    void Start()
    {
        //모든 캐릭터 프리팹을 불러와서... ㅇㅇ
        characters = Resources.LoadAll<GameObject>("Character/");
        idx = (int)GameManager.Data.Now_Character;
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = Vector3.zero;
        show_info();
    }

    void Update()
    {
        
    }

    public void nextCharacter()
    {
        if (++idx == characters.Length) idx = 0;
        Destroy(Show_Character);
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = Vector3.zero;
        show_info();

    }


    public void prevCharacter()
    {
        if (--idx == -1) idx = characters.Length - 1;
        Destroy(Show_Character);
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = Vector3.zero;
        show_info();

    }

    public void show_info()
    {
        GameObject.Find("UI/Text_Name").GetComponent<Text>().text = Enum.GetName(typeof(DataManager.Characters), idx);
        string select;
        if ((int)GameManager.Data.Now_Character == idx)
        {
            select = "이미쓰는중!";
            GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = false;

        }
        else
        {
            select = "사용할래!";
            GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = true;
        }
        GameObject.Find("UI/Button_Select/Text_Select").GetComponent<Text>().text = select;
        


    }

    public void select()
    {

        GameManager.Data.Now_Character = (DataManager.Characters)(idx);
        show_info();
    }
}
