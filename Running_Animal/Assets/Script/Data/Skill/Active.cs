using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Active : MonoBehaviour
{
    AudioClip clip;
    AudioClip clip2;
    AudioClip clip3;
    AudioClip clip4;
    AudioClip clip5;
    AudioClip clip6;
    AudioClip clip7;
    AudioClip clip8;
    UI_Play UP;
    public enum ACTIVE_CODE
    {
        None = 0,
        Defense, // 피격 1회 무시, Player Tag를 Sheild로 변경, 1회 피격시 일정시간 이후 원래 태그 변경
        Flash, // 일정 거리 앞으로 점멸 백그라운드 포함 기믹까지 플레이어 앞으로 땡겨오면 됨. (좌표수정)
        Ghost, // 일정 시간 유체화 
        Heal, // 체력 회복
        Item_Change, // 아이템 체인지
        Change_Coin, // 장애물 코인화
        The_World, // 느-려-져
        Multiple_Combo, // 콤보3배
        Fly, // 나는 날 수 잇서요
    }
    /*액티브 스킬 설명*/
    public String[,] Active_Explane =
    {
        { "None", "영구결번" },
        { "방패 [액티브]", "5초 동안 어떠한 함정이든 한 번 막을 수 있는 방패를 두릅니다."},
        { "점멸 [액티브]", "일정 거리 앞으로 점멸합니다."},
        { "유령화 [액티브]", "3초 동안 모든 함정으로부터 피해를 입지 않습니다."  },
        { "회복 [액티브", "50만큼의 체력을 회복합니다."},
        { "다시, 또 한번 [액티브]", "스킬 선택 시, 선택지를 1회 바꿀 수 있습니다."},
        { "황금의 손 [액티브]", "화면에 보이는 모든 함정을 골드로 바꿔버립니다."},
        { "영겁의 시간 [액티브]", "5초 동안 시간이 느리게 흐릅니다."},
        { "세 마리 같은 한 마리[액티브]", "함정 파괴 시 콤보가 이전의 3배만큼 쌓입니다."},
        { "날개 [액티브]", "5초 동안 점프 횟수의 한계가 없어집니다. 과연, 얼마나 올라갈 수 있을까요?"}
    };
    //액티브 스킬 이미지
    public Sprite[] Active_Sprites;
    private void Start()
    {
        Active_Sprites = Resources.LoadAll<Sprite>("Active_Buttons/");
        UP = gameObject.GetComponent<UI_Play>();
        LoadSound();
    }

    // Skill ID : 1 Defense
    public void OnDefense()
    {
        GameManager.Play.Player.transform.Find("1").gameObject.SetActive(true);
        GameManager.Sound.SFXPlay(clip);
        GameManager.Play.Player.tag = "Shield";
        Invoke("OffDefense", 5.0f);

    }

    public void OffDefense()
    {
        if (GameManager.Play.Player.CompareTag("Shield"))
        {
            GameManager.Play.Player.tag = "Player";
            GameManager.Play.Player.transform.Find("1").gameObject.SetActive(false);

        }
    }

    // Skill ID : 2 Flash
    public IEnumerator OnFlash()
    {
        StartCoroutine(gameObject.GetComponent<InterAction>().OnDodge(20));
        for (int i=0; i<2; i++)
        {
            if(i == 0)
            {
                GameManager.Play.Status.ability.SPEED.value = 150;
                GameManager.Play.Player.GetComponent<Rigidbody2D>().gravityScale = 0;
                GameManager.Play.Player.transform.Find("2").gameObject.SetActive(true);
            }
            else
            {
                GameManager.Play.Status.ability.SPEED.value = GameManager.Play.DC.pre_speed;
                GameManager.Play.Player.GetComponent<Rigidbody2D>().gravityScale = 2;
                GameManager.Play.Player.transform.Find("2").gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.15f);
        }

    }

    // Skill ID : 3 Ghost
    public IEnumerator OnGhost()
    {
        SpriteRenderer spr = GameManager.Play.Player.GetComponent<SpriteRenderer>();
        GameManager.Sound.SFXPlay(clip2);
        for (int i = 0; i < 2; i ++)
        {
            if(i == 0)
            {
                spr = GameManager.Play.Player.GetComponent<SpriteRenderer>();
                Color c = spr.color;
                c.a = 0.5f;
                spr.color = c;
                GameManager.Play.Player.tag = "God";
            }
            else if(i == 1)
            {
                spr = GameManager.Play.Player.GetComponent<SpriteRenderer>();
                Color c = spr.color;
                c.a = 1.0f;
                spr.color = c;

                GameManager.Play.Player.tag = "Player";
            }
            yield return new WaitForSeconds(5);
        }
    }

    // Skill ID : 4 Heal
    public void OnHeal()
    {
        float plus_hp; // 회복할 체력의 양
        plus_hp = 50 * GameManager.Play.Status.ability.RESTORE.value; // 기본 회복량 50, 재능 및 다른 요소에 의해 회복량 변동될것
        GameManager.Sound.SFXPlay(clip3);
        if (GameManager.Play.Status.ability.HP.value + plus_hp >= GameManager.Play.Status.ability.MAX_HP.value)
        {
            GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
        }
        else
        {
            GameManager.Play.Status.ability.HP.value += plus_hp;
        }
        gameObject.GetComponent<UI_Play>().BAR_HP();
    }

    // Skill ID : 5 Item_Change
    // 스킬 선택창에서 아이템 바꿀 수 있음...
    public void Item_Change()
    {
        if (SceneManager.GetActiveScene().name == "Select_Item")
        {
            GameObject.Find("SceneManager").GetComponent<ControlSelectSkill>().ReLoad();
            GameManager.Sound.SFXPlay(clip4);
        }
        else
        {
            GameManager.Play.DS.activeUse -= 1;
        }
    }

    // Skill ID : 6 Change_Coin
    // 장애물을 코인으로 바꿔준다.
    public void Change_Coin()
    {
        GameManager.Sound.SFXPlay(clip5);
        GameObject[] Trap = GameObject.FindGameObjectsWithTag("Trap");
        GameObject[] Trap_Blood = GameObject.FindGameObjectsWithTag("Trap_Blood");
        GameObject[] Trap_Stun = GameObject.FindGameObjectsWithTag("Trap_Stun");
        GameObject[] Jump = GameObject.FindGameObjectsWithTag("Jump");

        GameObject coin = Resources.Load<GameObject>("Item/Coin");

        foreach (GameObject t in Trap)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;

            UP.Trap_Combo(t.transform);

            GameManager.Play.DC.expNow += 1;
            UP.BAR_EXP();
        }

        foreach (GameObject t in Trap_Blood)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;
            
            UP.Trap_Combo(t.transform);

            GameManager.Play.DC.expNow += 1;
            UP.BAR_EXP();
        }

        foreach (GameObject t in Trap_Stun)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;

            UP.Trap_Combo(t.transform);

            GameManager.Play.DC.expNow += 1;
            UP.BAR_EXP();
        }

        foreach (GameObject t in Jump)
        {
            GameObject tmp;
            tmp = Instantiate(coin);
            tmp.transform.position = t.transform.position;

            UP.Trap_Combo(t.transform);

            GameManager.Play.DC.expNow += 1;
            UP.BAR_EXP();
        }

    }

    //Skill ID : 7 The_World
    public IEnumerator OnSlow()
    {
        GameManager.Sound.SFXPlay(clip6);
        for (int i = 0; i < 2; i++)
        {
            if (i == 0) Time.timeScale = 0.5f;
            else if (i == 1) Time.timeScale = 1.0f;

            yield return new WaitForSeconds(2.5f);
        }
    }

    //Skill ID : 8 Multiple_Combe

    public IEnumerator MultiCombo()
    {
        GameManager.Sound.SFXPlay(clip7);
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                GameManager.Play.DS.AC_multicombo = 3;
            }
            if (i == 1)
            {
                GameManager.Play.DS.AC_multicombo = 1;
            }

            yield return new WaitForSeconds(10f);
        }
    }

    //Skill ID : 9 Fly

    public IEnumerator OnFly()
    {
        GameManager.Sound.SFXPlay(clip8);
        float t = 0;
        while(t <= 5.0f)
        {
            t += Time.deltaTime;
            GameManager.Play.DS.jumpNow = 0;
            yield return null;
        }
    }

    //Skill ID : 10 Run

    public IEnumerator OnRun(float t)
    {
        bool isshield = (GameManager.Play.Player.CompareTag("Shield"));
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                GameManager.Play.Player.tag = "Run";
                GameManager.Play.Status.ability.SPEED.value = 30.0f;
            }
            if (i == 1)
            {
                GameManager.Play.Status.ability.SPEED.value = GameManager.Play.DC.pre_speed;
                if (isshield)
                {
                    GameManager.Play.Player.tag = "Shield";
                }
                else
                {
                    GameManager.Play.Player.tag = "Player";
                }
                GameManager.Play.DC.expNow += GameManager.Play.DS.expRun;
                GameManager.Play.DS.expRun = 0;
            }
            yield return new WaitForSeconds(t);
        }
    }

    public void Stop_Active()
    {
        StopAllCoroutines();
    }

    void LoadSound() 
    {
        clip = Resources.Load<AudioClip>("Sound/Active_Skills/000_Skill01");
        clip2 = Resources.Load<AudioClip>("Sound/Active_Skills/001_Skill03");
        clip3 = Resources.Load<AudioClip>("Sound/Active_Skills/002_Skill04");
        clip4 = Resources.Load<AudioClip>("Sound/Active_Skills/003_Skill05");
        clip5 = Resources.Load<AudioClip>("Sound/Active_Skills/004_Skill06");
        clip6 = Resources.Load<AudioClip>("Sound/Active_Skills/005_Skill07");
        clip7 = Resources.Load<AudioClip>("Sound/Active_Skills/006_Skill08");
        clip8 = Resources.Load<AudioClip>("Sound/Active_Skills/007_Skill09");
    }
}
