using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // enum index�� ����

public class SelectCharacter : MonoBehaviour
{
    //Scene 'Character'�� ���� SceneManager

    // ǥ�õ� ĳ���͵��� �迭
    GameObject[] characters;
    GameObject Show_Character;
    int idx;
    void Start()
    {
        //��� ĳ���� �������� �ҷ��ͼ�... ����
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
            select = "�̹̾�����!";
            GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = false;

        }
        else
        {
            select = "����ҷ�!";
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
