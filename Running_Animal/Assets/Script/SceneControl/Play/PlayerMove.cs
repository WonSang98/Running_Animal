using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    UI_Play UI_Play;
    InterAction InterAction;
    Active Active;
    LoadScene LS;

    Animator Ani_Player;
    Rigidbody2D Rig_Player;

    AudioClip clip;
    AudioClip clip2;
    AudioClip clip3;
    AudioClip clip4;
    AudioClip clip5;
    AudioClip clip6;
    AudioClip clip7;
    private void Start()
    {
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
        InterAction = GameObject.Find("@Managers").GetComponent<InterAction>();
        Active = GameObject.Find("@Managers").GetComponent<Active>();
        LS = GameObject.Find("@Managers").GetComponent<LoadScene>();
        Ani_Player = gameObject.GetComponent<Animator>();
        Rig_Player = gameObject.GetComponent<Rigidbody2D>();
        LoadSound();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Ani_Player == null)
        {
            Ani_Player = gameObject.GetComponent<Animator>();
        }
        if (gameObject.CompareTag("Shield"))
        {
            if (other.gameObject.CompareTag("Trap_Blood") || other.gameObject.CompareTag("Trap_Stun") || other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("Jump"))
            {
                gameObject.tag = "Player";
                gameObject.transform.Find("1").gameObject.SetActive(false);
                GameManager.Sound.SFXPlay(clip4);
                StartCoroutine(InterAction.OnDodge(GameManager.Play.DC.dodge));
            }
        }
        if (gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Trap_Blood"))
            {
                
                other.GetComponent<Animator>().SetTrigger("Bear");
                if (InterAction.OnHit(GameManager.Play.DC.damage))
                {
                    GameManager.Sound.SFXPlay(clip2);
                    GameManager.Play.DC.lastHit = other.GetComponent<TRAP_CODE>().ID;
                    StartCoroutine(InterAction.OnBlood());
                }

            }
            if (other.gameObject.CompareTag("Trap_Stun"))
            {
                if (InterAction.OnHit(GameManager.Play.DC.damage))
                {
                    GameManager.Sound.SFXPlay(clip7);
                    StartCoroutine(InterAction.OnStun());
                    GameManager.Play.DC.lastHit = other.GetComponent<TRAP_CODE>().ID;
                }
            }
            if (other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("Jump"))
            {
                if (InterAction.OnHit(GameManager.Play.DC.damage))
                {
                    GameManager.Sound.SFXPlay(clip3);
                    GameManager.Play.DC.lastHit = other.GetComponent<TRAP_CODE>().ID;
                }
            }
        }

        if (gameObject.CompareTag("Run"))
        {
            if (other.gameObject.CompareTag("Trap_Blood") || other.gameObject.CompareTag("Trap_Stun") || other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("Jump"))
            {
                GameManager.Sound.SFXPlay(clip6);
                UI_Play.Trap_Combo(other.transform);
                GameManager.Play.DS.expRun += 1 * GameManager.Play.DC.expMulti; 
            }
        }



        if (other.gameObject.CompareTag("HP"))
        {
            float RESTORE_HP = (GameManager.Play.Status.ability.MAX_HP.value * 0.08f) * GameManager.Play.Status.ability.RESTORE.value;
            if (GameManager.Play.Status.ability.HP.value + RESTORE_HP > GameManager.Play.Status.ability.MAX_HP.value)
            {
                GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
            }
            else
            {
                GameManager.Play.Status.ability.HP.value += RESTORE_HP;
            }
            UI_Play.BAR_HP();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Play.DC.goldNow += 10 * GameManager.Play.DC.goldMulti;
            UI_Play.Text_gold.text = $"{GameManager.Play.DC.goldNow}g";
            Destroy(other.gameObject);
            GameManager.Sound.SFXPlay(clip);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Ani_Player == null)
        {
            Ani_Player = gameObject.GetComponent<Animator>();
        }
        if (collision.gameObject.CompareTag("Tile"))
        {
            
            Ani_Player.SetBool("Landing", true);
            GameManager.Play.DS.jumpNow = 0;
        }
        if (collision.gameObject.CompareTag("Jump"))
        {
            Rig_Player.velocity = new Vector3(0, 4, 0);
            Ani_Player.SetBool("Landing", true);
            GameManager.Play.DS.jumpNow = 0;
            StartCoroutine(UI_Play.Cam_ATT());
            UI_Play.Trap_Combo(collision.transform);
            GameManager.Play.DS.expNow += GameManager.Play.DC.expMulti;
            GameManager.Sound.SFXPlay(clip6);
            UI_Play.BAR_EXP();
        }

        if (collision.gameObject.CompareTag("Jump2"))
        {
            Rig_Player.velocity = new Vector3(0, 10, 0);
            GameManager.Sound.SFXPlay(clip5);
            GameManager.Play.DS.jumpNow = 0;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            GameManager.Play.DC.goldNow += 10 * GameManager.Play.DC.goldMulti;
            UI_Play.Text_gold.text = $"{GameManager.Play.DC.goldNow}g";
            Destroy(collision.gameObject);
            GameManager.Sound.SFXPlay(clip);
        }

        //질주 아이템 사용 시
        if (gameObject.CompareTag("Run"))
        {
            if (collision.gameObject.CompareTag("Trap_Blood") || collision.gameObject.CompareTag("Trap_Stun") || collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Jump"))
            {
                GameManager.Sound.SFXPlay(clip6);
                UI_Play.Trap_Combo(collision.transform);
                GameManager.Play.DS.expRun += 1 * GameManager.Play.DC.expMulti;
            }
        }
    }
    void LoadSound()
    {
        clip = Resources.Load<AudioClip>("Sound/Play/002_Play");
        clip2 = Resources.Load<AudioClip>("Sound/Play/003_Play");
        clip3 = Resources.Load<AudioClip>("Sound/Play/004_Play");
        clip4 = Resources.Load<AudioClip>("Sound/Active_Skills/000_Skill01_02");
        clip5 = Resources.Load<AudioClip>("Sound/Play/000_Play");
        clip6 = Resources.Load<AudioClip>("Sound/Play/007_Play");
        clip7 = Resources.Load<AudioClip>("Sound/Play/010_Play");
    }
}
