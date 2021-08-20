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
    private void Start()
    {
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
        InterAction = GameObject.Find("@Managers").GetComponent<InterAction>();
        Active = GameObject.Find("@Managers").GetComponent<Active>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Shield"))
        {
            if (other.gameObject.CompareTag("Trap_Blood") || other.gameObject.CompareTag("Trap_Stun") || other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("Jump"))
            {
                gameObject.tag = "Player";
                gameObject.transform.Find("1").gameObject.SetActive(false);
                StartCoroutine(InterAction.OnDodge(GameManager.Play.DC.dodge));
            }
        }
        if (gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Trap_Blood"))
            {
                
                other.GetComponent<Animator>().SetTrigger("Bear");
                InterAction.OnHit(GameManager.Play.DC.damage);
                StartCoroutine(InterAction.OnBlood());

            }
            if (other.gameObject.CompareTag("Trap_Stun"))
            {
                InterAction.OnHit(GameManager.Play.DC.damage);
                StartCoroutine(InterAction.OnStun());
            }
            if (other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("Jump"))
            {
                InterAction.OnHit(GameManager.Play.DC.damage);
            }
        }

        if (gameObject.CompareTag("Run"))
        {
            if (other.gameObject.CompareTag("Trap_Blood") || other.gameObject.CompareTag("Trap_Stun") || other.gameObject.CompareTag("Trap") || other.gameObject.CompareTag("Jump"))
            {
                Destroy(other.gameObject);
                UI_Play.Trap_Combo(other.transform);
                GameManager.Play.DS.expRun += 1 * GameManager.Play.DC.expMulti; 
            }
        }



        if (other.gameObject.CompareTag("HP"))
        {
            Active.OnHeal();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("LevelUp"))
        {
            GameManager.Play.DC.expNow -= GameManager.Play.DC.expNeed[GameManager.Play.DC.lv];
            GameManager.Play.DC.lv += 1;
            if(GameManager.Play.DC.expNow >= GameManager.Play.DC.expNeed[GameManager.Play.DC.lv])
            {
                GameManager.Play.DC.lvup = true;
            }
            else
            {
                GameManager.Play.DC.lvup = false;
            }
            GameManager.Instance.Save();
            SceneManager.LoadScene("Select_Item");
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("µ∑!!!!");
            GameManager.Play.DC.goldNow += 10 * GameManager.Play.DC.goldMulti;
            UI_Play.Text_gold.text = $"{GameManager.Play.DC.goldNow}g";
            Destroy(other.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            GetComponent<Animator>().SetBool("Landing", true);
            GameManager.Play.DS.jumpNow = 0;
        }
        if (collision.gameObject.CompareTag("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
            GetComponent<Animator>().SetBool("Landing", true);
            GameManager.Play.DS.jumpNow = 0;
            StartCoroutine(UI_Play.Cam_ATT());
            UI_Play.Trap_Combo(collision.transform);
            GameManager.Play.DC.expNow += GameManager.Play.DC.expMulti;
            UI_Play.BAR_EXP();
        }

        if (collision.gameObject.CompareTag("Jump2"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
            GameManager.Play.DS.jumpNow = 0;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("µ∑!!!!");
            GameManager.Play.DC.goldNow += 10 * GameManager.Play.DC.goldMulti;
            UI_Play.Text_gold.text = $"{GameManager.Play.DC.goldNow}g";
            Destroy(collision.gameObject);
        }

        //¡˙¡÷ æ∆¿Ã≈€ ªÁøÎ Ω√
        if (gameObject.CompareTag("Run"))
        {
            if (collision.gameObject.CompareTag("Trap_Blood") || collision.gameObject.CompareTag("Trap_Stun") || collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Jump"))
            {
                Destroy(collision.gameObject);
                UI_Play.Trap_Combo(collision.transform);
                GameManager.Play.DS.expRun += 1 * GameManager.Play.DC.expMulti;
            }
        }
    }
}
