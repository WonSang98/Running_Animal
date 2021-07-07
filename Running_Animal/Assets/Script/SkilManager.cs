using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilManager : MonoBehaviour
{
    // 스킬 클래스
    Sprite[] active_buttons; //Active_Buttons prefebs array
    GameObject[] active_skils; //Active_Skil prefebs array
    Vector3 pos_first;
    
    GameObject Show_Button;
    GameObject player;
    int idx; // now_skil_idx;

    void Start()
    {
        player = GameObject.Find("Player");
        pos_first = player.transform.position;
        active_buttons = Resources.LoadAll<Sprite>("Active_Buttons/");
        idx = (int)GameManager.Data.active;
        Show_Button = GameObject.Find("UI/Button_Skil");
        Show_Button.GetComponent<Image>().sprite = active_buttons[idx];

    }

    public void Use_Skil()
    {
        switch (GameManager.Data.active)
        {
            case DataManager.Active_Skil.None:
                break;
            case DataManager.Active_Skil.Defense:
                OnDefense();
                break;
            case DataManager.Active_Skil.Flash:
                OnFlash();
                break;
            case DataManager.Active_Skil.Ghost:
                OnGhost();
                break;
        }
    }

    // Skil Code : 1 Defense
    public void OnDefense()
    {
        Show_Button.GetComponent<Button>().interactable = false;
        player.transform.Find("1").gameObject.SetActive(true);
        player.tag = "Shield";
        Invoke("OffDefense", 5.0f);
        
    }

    public void OffDefense()
    {
        if (player.CompareTag("Shield"))
        {
            player.tag = "Player";
            player.transform.Find("1").gameObject.SetActive(false);

        }
    }

    // Skil Code : 2 Flash
    public void OnFlash()
    {
        /*
        1.캐릭터 x 좌표를 수정해서 이동한다.
        2.캐릭터를 기존 맵 이동속도만큼 원래 위치로 이동시킨다.
           동시에 맵의 이동속도는 그동안 2배가 된다.
        */
        Vector3 pos_first = player.transform.position; ;
        Debug.Log(pos_first);
        player.transform.Find("2").gameObject.SetActive(true);
        player.transform.Translate(GameManager.Data.speed, 0, 0);
        player.GetComponent<PlayerMove>().speed = GameManager.Data.speed;
        GameObject.Find("BackGround").GetComponent<ScroolBackGround>().speed *= 2;
        Invoke("OffFlash", 1);
    }

    public void OffFlash()
    {
        GameObject.Find("BackGround").GetComponent<ScroolBackGround>().speed /= 2;
        player.GetComponent<PlayerMove>().speed = 0;
        StopCoroutine("Flash");
        player.transform.Find("2").gameObject.SetActive(false);
    }

    // Skil Code : 3 Ghost
    public void OnGhost()
    {
        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        Color c = spr.color;
        c.a = 0.5f;
        spr.color = c;

        player.tag = "God";
        Invoke("OffGhost", 3);
    }

    public void OffGhost()
    {
        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        Color c = spr.color;
        c.a = 1.0f;
        spr.color = c;

        player.tag = "Player";
    }


}
