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

    AudioClip clip;
    AudioClip clip2;
    GameObject Player;
    UI_Play UP;
    SpriteRenderer Player_Color;
    private void Start()
    {
        UP = gameObject.GetComponent<UI_Play>();
        LoadSound();
    }
    public void MakeCoin(Transform other)
    {
        Instantiate(gameObject.GetComponent<TrapForest>().coin, new Vector3(other.position.x + 0.5f, other.position.y + 4.0f, other.position.z), Quaternion.identity);
    }

    public IEnumerator OnDodge(int t) // �ǰݽ� �����ð� ����
    {
        Player = GameManager.Play.Player.gameObject;
        Player_Color = Player.GetComponent<SpriteRenderer>();

        for (int i = 0; i < t; i++)
        {
            if (i == 0)
            {
                Player.tag = "God";
            }

            if (i == (t - 1))
            {
                UP.player_opacity.a = 1.0f;
                Player_Color.color = UP.player_opacity;
                Player.tag = "Player";
            }

            if (i % 2 == 0)
            {
                UP.player_opacity.a = 0.5f;
                Player_Color.color = UP.player_opacity;
            }
            else
            {
                UP.player_opacity.a = 1.0f;
                Player_Color.color = UP.player_opacity;
            }
            yield return new WaitForSeconds(0.1f);
        }
        UP.player_opacity.a = 1.0f;
        Player_Color.color = UP.player_opacity;
    }

    public IEnumerator OnBlood() // ���긲
    {
        Player = GameManager.Play.Player.gameObject;
        Player_Color = Player.GetComponent<SpriteRenderer>();
        UP.Cool_BearTrap.fillAmount = 1;

        float time_long = 0;
        float time_short = 0;

        while(time_long <= 10)
        {
            time_short += Time.deltaTime;
            if(time_short >= 2)
            {
                time_long += time_short;
                time_short = 0;
                GameManager.Play.Status.ability.HP.value -= (GameManager.Play.Status.ability.MAX_HP.value * 0.01f);
                StartCoroutine(UP.Cam_Hit());
                GameManager.Sound.SFXPlay(clip2);
                UP.BAR_HP();
                Die();
            }
            if (Player.CompareTag("Die"))
            {
                break;
            }
            UP.Cool_BearTrap.fillAmount -= (Time.deltaTime / 10);
            yield return null;
        }
        UP.Cool_BearTrap.fillAmount = 0;
    }

    public IEnumerator OnStun() //����
    {
        Player = GameManager.Play.Player.gameObject;
        Player_Color = Player.GetComponent<SpriteRenderer>();
        UP.Cool_Banana.fillAmount = 1;
        float time = 0;
        UP.RemoveButton();
        while (time < 0.5f)
        {
            if (Player.CompareTag("Die"))
            {
                break;
            }
            time += Time.deltaTime;
            UP.Cool_Banana.fillAmount -= (Time.deltaTime / 0.5f);
            yield return null;
        }
        UP.RemakeButton();
    }


    // �ǰݽ� 
    public bool OnHit(float DMG)
    {
        StartCoroutine(OnDodge(GameManager.Play.DC.dodge));
        Animator animator = Player.GetComponent<Animator>();
        Player = GameManager.Play.Player.gameObject;
        Player_Color = Player.GetComponent<SpriteRenderer>();

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
            StartCoroutine(UP.Cam_Hit());
            UP.BAR_HP();
            Die();
            animator.SetTrigger("Attacked");
            return true;
        }
        else // ȸ����. ����
        {
            Instantiate(UP.Miss, GameManager.Play.Player.transform);
            GameManager.Sound.SFXPlay(clip);
            return false;
        }
    }

    public void Die()
    {
        Player = GameManager.Play.Player.gameObject;
        Player_Color = Player.GetComponent<SpriteRenderer>();

        if (GameManager.Play.Status.ability.HP.value <= 0)
        {
            GameManager.Play.Player.tag = "Die";
            GameManager.Instance.AllStop();
            GameManager.Play.Player.GetComponent<Animator>().SetTrigger("Die");
            
            if (GameManager.Play.DC.revive > 0)
            {
                // ��Ȱ ���� Ƚ�� ����.
                StartCoroutine(gameObject.GetComponent<Passive>().Resurrection());
                GameManager.Play.Player.GetComponent<Animator>().SetTrigger("Revive");
                GameManager.Play.DC.revive -= 1;
            }
            else
            {
                StartCoroutine(UP.GameOver());
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
    void LoadSound()
    {
        clip = Resources.Load<AudioClip>("Sound/Play/011_Play");
        clip2 = Resources.Load<AudioClip>("Sound/Play/012_Play");
    }
}
