using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkilManager : MonoBehaviour
{
    // ��ų Ŭ����
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
            Debug.Log("��Ÿ�� �Ϸ�~");
            Show_Button.GetComponent<Button>().interactable = true;
        }
    }
    // Skil Code : 1 Defense
    public void OnDefense()
    {
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.
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
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.
        /*
        1.ĳ���� x ��ǥ�� �����ؼ� �̵��Ѵ�.
        2.ĳ���͸� ���� �� �̵��ӵ���ŭ ���� ��ġ�� �̵���Ų��.
           ���ÿ� ���� �̵��ӵ��� �׵��� 2�谡 �ȴ�.
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
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.

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
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.

        float plus_hp; // ȸ���� ü���� ��
        plus_hp = 50 * GameManager.Data.restore_eff; // �⺻ ȸ���� 50, ��� �� �ٸ� ��ҿ� ���� ȸ���� �����ɰ�
        if(GameManager.Data.hp + plus_hp > GameManager.Data.max_hp)
        {
            GameManager.Data.hp = GameManager.Data.max_hp;
        }
        else
        {
            GameManager.Data.hp += plus_hp; 
        }
    }

    // Skil Code : 5 Item_Change
    // ��ų ����â���� ������ �ٲ� �� ����...
    public void Item_Change()
    {
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.

        if (SceneManager.GetActiveScene().name == "Select_Item")
        {
            GameObject.Find("SceneManager").GetComponent<SelectSkil>().ReLoad();
        }
    }

    // Skil Code : 6 Change_Coin
    // ��ֹ��� �������� �ٲ��ش�.
    public void Change_Coin()
    {
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.

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
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.

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
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.

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
        Show_Button.GetComponent<Button>().interactable = false; // ��Ƽ�� ��ų ��ư ��Ȱ��ȭ
        GameManager.Data.use_active += 1; // ��Ƽ�� ��ų ��� Ƚ�� 1ȸ ����.

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
   
}
