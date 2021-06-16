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
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
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
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
        show_info();

    }


    public void prevCharacter()
    {
        if (--idx == -1) idx = characters.Length - 1;
        Destroy(Show_Character);
        Show_Character = Instantiate(characters[idx]) as GameObject;
        Show_Character.transform.localPosition = Vector3.zero;
        Show_Character.GetComponent<Rigidbody2D>().gravityScale = 0;
        show_info();

    }

    public void show_info()
    {
        GameObject.Find("UI/Text_Name").GetComponent<Text>().text = Enum.GetName(typeof(DataManager.Characters), idx);
        string select;
        if ((int)GameManager.Data.Now_Character == idx)
        {
            select = "이미쓰는중!";
            GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
            GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = false;
            GameObject.Find("UI/Button_Select/Text_Select").GetComponent<Text>().text = select;

        }
        else
        {
            if(GameManager.Data.Buy_Character[idx] == true)
            {
                select = "사용할래!";
                GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
                GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
                GameObject.Find("UI/Button_Select").GetComponent<Button>().interactable = true;
                GameObject.Find("UI/Button_Select/Text_Select").GetComponent<Text>().text = select;
            }
            else
            {
                Debug.Log("TTTTT");
                GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(false);
                GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(true);
                GameObject.Find("UI/Button_Buy/Text_Buy").GetComponent<Text>().text = "살래?";
            }
        }
        


    }

    public void select()
    {

        GameManager.Data.Now_Character = (DataManager.Characters)(idx);
        show_info();
    }

    public void buy()
    {
        if(GameManager.Data.Gold >= GameManager.Data.Cost_Character[idx])
        {
            GameManager.Data.Buy_Character[idx] = true;
            GameManager.Data.Gold -= GameManager.Data.Cost_Character[idx];
            GameObject.Find("UI").transform.Find("Button_Buy").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Button_Select").gameObject.SetActive(true);
            show_info();
        }
        else
        {
            GameObject.Find("UI/Button_Buy/Text_Buy").GetComponent<Text>().text = "사고싶어?";
        }
    }
}
