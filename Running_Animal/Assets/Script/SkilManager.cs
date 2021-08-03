using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkilManager : MonoBehaviour
{
    // 스킬 클래스
    Sprite[] active_buttons; //Active_Buttons prefebs array
    GameObject[] active_skils; //Active_Skil prefebs array
    Vector3 pos_first;
    
    GameObject Show_Button;
    GameObject player;
    GameObject Select_Skil;
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
                Invoke("CoolTime", 15f);
                break;
            case DataManager.Active_Skil.Flash:
                OnFlash();
                Invoke("CoolTime", 15f);
                break;
            case DataManager.Active_Skil.Ghost:
                OnGhost();
                Invoke("CoolTime", 15f);
                break;
            case DataManager.Active_Skil.Heal:
                OnHeal();
                Invoke("CoolTime", 20f);
                break;
            case DataManager.Active_Skil.Item_Change:
                Item_Change();
                Invoke("CoolTime", 20f);
                break;
            case DataManager.Active_Skil.Change_Coin:
                Change_Coin();
                Invoke("CoolTime", 20f);
                break;
            case DataManager.Active_Skil.The_World:
                StartCoroutine("OnSlow");
                Invoke("CoolTime", 10f);
                break;
            case DataManager.Active_Skil.Multiple_Combo:
                StartCoroutine("MultiCombo");
                Invoke("CoolTime", 5f);
                break;
            case DataManager.Active_Skil.Fly:
                StartCoroutine("OnFly");
                Invoke("CoolTime", 15f);
                break;

        }
    }
    public void CoolTime()
    {
        if (GameManager.Data.use_active < GameManager.Data.max_active)
        {
            Debug.Log("쿨타임 완료~");
            Show_Button.GetComponent<Button>().interactable = true;
        }
    }
    // Skil Code : 1 Defense
    public void OnDefense()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.
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
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.
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
        //GameObject.Find("BackGround").GetComponent<ScroolBackGround>().speed *= 2;
        Invoke("OffFlash", 1);
    }

    public void OffFlash()
    {
        //GameObject.Find("BackGround").GetComponent<ScroolBackGround>().speed /= 2;
        player.GetComponent<PlayerMove>().speed = 0;
        StopCoroutine("Flash");
        player.transform.Find("2").gameObject.SetActive(false);
    }

    // Skil Code : 3 Ghost
    public void OnGhost()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.

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

    // Skil Code : 4 Heal
    public void OnHeal()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.

        float plus_hp; // 회복할 체력의 양
        plus_hp = 50 * GameManager.Data.restore_eff; // 기본 회복량 50, 재능 및 다른 요소에 의해 회복량 변동될것
        if(GameManager.Data.hp + plus_hp > GameManager.Data.max_hp)
        {
            GameManager.Data.hp = GameManager.Data.max_hp;

        }
        else
        {
            GameManager.Data.hp += plus_hp; 
        }
        GameManager.Instance.BAR_HP();
    }

    // Skil Code : 5 Item_Change
    // 스킬 선택창에서 아이템 바꿀 수 있음...
    public void Item_Change()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.

        if (SceneManager.GetActiveScene().name == "Select_Item")
        {
            GameObject.Find("SceneManager").GetComponent<SelectSkil>().ReLoad();
        }
    }

    // Skil Code : 6 Change_Coin
    // 장애물을 코인으로 바꿔준다.
    public void Change_Coin()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.

        GameObject[] Trap = GameObject.FindGameObjectsWithTag("Trap");
        GameObject[] Trap_Blood = GameObject.FindGameObjectsWithTag("Trap_Blood");
        GameObject[] Trap_Stun = GameObject.FindGameObjectsWithTag("Trap_Stun");
        GameObject[] Jump = GameObject.FindGameObjectsWithTag("Jump");

        GameObject coin = Resources.Load<GameObject>("Item/Coin");

        foreach(GameObject t in Trap)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

        foreach (GameObject t in Trap_Blood)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

        foreach (GameObject t in Trap_Stun)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

        foreach (GameObject t in Jump)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            Destroy(t);
        }

    }

    //Skil Code : 7 The_World
    IEnumerator OnSlow()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.

        for (int i=0; i<5; i++)
        {
            if (i == 0) GameManager.Data.speed /= 10;
            else if (i == 4) GameManager.Data.speed *= 10;

            yield return new WaitForSeconds(1f);
        }
    }

    //Skil Code : 8 Multiple_Combe

    IEnumerator MultiCombo()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.

        for (int i=0; i<2; i++)
        {
            if(i == 0)
            {
                GameManager.Data.multi_combo *= 3;
            }
            if(i == 1)
            {
                GameManager.Data.multi_combo /= 3;
            }

            yield return new WaitForSeconds(10f);
        }
    }

    //Skil Code : 9 Fly

    IEnumerator OnFly()
    {
        Show_Button.GetComponent<Button>().interactable = false; // 액티브 스킬 버튼 비활성화
        GameManager.Data.use_active += 1; // 액티브 스킬 사용 횟수 1회 차감.

        int temp = GameManager.Data.max_jump;
        for (int i=0; i<2; i++)
        {
            if (i == 0)
            {
                temp = GameManager.Data.max_jump;
                GameManager.Data.max_jump = 999999;
            }
            else
            {
                GameManager.Data.max_jump = temp;
            }
            yield return new WaitForSeconds(5f);
        }
    }
   
    //Skil Code : 10 Run

    IEnumerator OnRun(float t)
    {
        float pre_speed = 0;
        for (int i = 0; i < 2; i++)
        {
            if(i == 0)
            {
                pre_speed = GameManager.Data.speed;
                player.tag = "Run";
                GameManager.Data.speed = 30.0f;
            }
            if(i == 1)
            {
                GameManager.Data.speed = pre_speed;
                player.tag = "Player";
                GameManager.Data.now_Exp += GameManager.Data.Exp_run;
                GameManager.Data.Exp_run = 0;
                GameManager.Instance.BAR_EXP();
            }
            yield return new WaitForSeconds(t);
        }
    }
}
