using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterAction : MonoBehaviour
{
    // �÷��� �� ��ȣ�ۿ뿡 ���� �͵�....
    /*
     * ���� ����
     * ���� ����
     * �ı� 
     */
    public void MakeCoin(Transform other)
    {
        Instantiate(gameObject.GetComponent<TrapForest>().coin, new Vector3(other.position.x + 0.5f, other.position.y + 4.0f, other.position.z), Quaternion.identity);
    }

    public IEnumerator OnDodge(int t) // �ǰݽ� �����ð� ����
    {
        for (int i = 0; i < t; i++)
        {
            if (i == 0)
            {
                GameManager.Play.Player.tag = "God";
            }

            if (i == (t - 1))
            {
                gameObject.GetComponent<UI_Play>().player_opacity.a = 1.0f;
                GameManager.Play.Player.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<UI_Play>().player_opacity;
                GameManager.Play.Player.tag = "Player";
            }

            if (i % 2 == 0)
            {
                gameObject.GetComponent<UI_Play>().player_opacity.a = 0.5f;
                GameManager.Play.Player.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<UI_Play>().player_opacity;
            }
            else
            {
                gameObject.GetComponent<UI_Play>().player_opacity.a = 1.0f;
                GameManager.Play.Player.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<UI_Play>().player_opacity;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator OnBlood() // ���긲
    {
        for (int i = 0; i < 5; i++)
        {
            if (GameManager.Play.Player.CompareTag("Die"))
            {
                break;
            }
            GameManager.Play.Status.ability.HP.value -= 3;
            StartCoroutine(gameObject.GetComponent<UI_Play>().Cam_Hit());
            gameObject.GetComponent<UI_Play>().BAR_HP();
            Die();
            yield return new WaitForSeconds(2.0f);
        }
    }

    public IEnumerator OnStun() //����
    {
        for (int i = 0; i < 2; i++)
        {
            if (GameManager.Play.Player.CompareTag("Die"))
            {
                break;
            }
            if (i == 0)
            {
                gameObject.GetComponent<UI_Play>().Event_jump.triggers.Remove(gameObject.GetComponent<UI_Play>().Entry_Jump);
                gameObject.GetComponent<UI_Play>().Event_down.triggers.Remove(gameObject.GetComponent<UI_Play>().Entry_Down);
                gameObject.GetComponent<UI_Play>().Button_Jump.interactable = false;
                gameObject.GetComponent<UI_Play>().Button_Down.interactable = false;
            }
            else if (i == 1)
            {
                gameObject.GetComponent<UI_Play>().Event_jump.triggers.Add(gameObject.GetComponent<UI_Play>().Entry_Jump);
                gameObject.GetComponent<UI_Play>().Event_down.triggers.Add(gameObject.GetComponent<UI_Play>().Entry_Down);
                gameObject.GetComponent<UI_Play>().Button_Jump.interactable = true;
                gameObject.GetComponent<UI_Play>().Button_Down.interactable = true;
            }

            yield return new WaitForSeconds(1.5f);
        }
    }


    // �ǰݽ� 
    public bool OnHit(float DMG)
    {
        StartCoroutine(OnDodge(GameManager.Play.DC.dodge));
        Animator animator = GameManager.Play.Player.GetComponent<Animator>();

        int per = Random.Range(0, 100);
        //ȸ�� ���� ��
        if (per > GameManager.Play.Status.ability.LUK.value)
        {
            if (GameManager.Play.DC.combo > GameManager.Play.DC.comboMax)
            {
                GameManager.Play.DC.comboMax = GameManager.Play.DC.combo;
            }
            GameManager.Play.DC.combo = 0;
            GameManager.Play.DS.nohit = false;

            GameManager.Play.Status.ability.HP.value -= DMG;
            Debug.Log(DMG + "��ŭ�� ����! ���� ü�� " + GameManager.Play.Status.ability.HP.value);
            StartCoroutine(gameObject.GetComponent<UI_Play>().Cam_Hit());
            gameObject.GetComponent<UI_Play>().BAR_HP();
            Die();
            animator.SetTrigger("Attacked");
            return true;
        }
        else // ȸ����. ����
        {
            Instantiate(gameObject.GetComponent<UI_Play>().Miss, GameManager.Play.Player.transform);
            return false;
        }
    }

    public void Die()
    {
        if (GameManager.Play.Status.ability.HP.value <= 0)
        {
            GameManager.Play.Player.tag = "Die";
            GameManager.Instance.AllStop();
            GameManager.Play.Player.GetComponent<Animator>().SetTrigger("Die");
            
            if (GameManager.Play.DC.revive > 0)
            {
                // ��Ȱ ���� Ƚ�� ����.
                StartCoroutine(gameObject.GetComponent<Passive>().Resurrection());

                GameManager.Play.DC.revive -= 1;
            }
            else
            {
                GameManager.Play.Player.GetComponent<Animator>().SetBool("StartGame", false);
                StartCoroutine(gameObject.GetComponent<UI_Play>().GameOver());
            }
        }
    }

    // ��� �����Ǵ� �нú꽺ų��, ��ƾ �۵���Ű��.
    public void Apply_Passive()
    {
       
        if (GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Auto_Jump] == true)
        {
            StartCoroutine(gameObject.GetComponent<Passive>().Auto_Jump());
        }
        if (GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Random_God] == true)
        {
            StartCoroutine(gameObject.GetComponent<Passive>().Random_God());
        }
        if (GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Auto_Restore] == true)
        {
            StartCoroutine(gameObject.GetComponent<Passive>().Auto_Restore());
        }
        StartCoroutine(gameObject.GetComponent<TrapForest>().hptime());
    }

    public void Stop_InterAction()
    {
        StopAllCoroutines();
    }
}
