using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //SkilManager sm;
    EventTrigger.Entry entry_Jump;
    EventTrigger.Entry entry_Down;
    EventTrigger event_jump;
    EventTrigger event_down;

    Button button_jump;
    Button button_down;


    //GameObject jump2;

    public float jump; // Jump Value
    public float down;
    public float speed;
    // 아래의 변수 값은 GameManager에서 받아온다.
    // GameManager.Instance.Player 
    int Use_Jump; // 플레이어의 현재 사용 점프 횟수

    Text player_hp;
    Text get_coin;
    Text Level;
    Text combo;
    Text stage;

    Color player_opacity; // 플레이어 투명도, 피격 후 깜박이기 위해 사용

    SpriteRenderer spriterenderer; // 스프라이트 변환을 위해 사용

    Animator animator;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            button_jump = GameObject.Find("UI/Button_Jump").GetComponent<Button>();
            button_down = GameObject.Find("UI/Button_Down").GetComponent<Button>();


            player_hp = GameObject.Find("UI/HP").GetComponent<Text>();
            get_coin = GameObject.Find("UI/Gold").GetComponent<Text>();
            combo = GameObject.Find("UI/Combo").GetComponent<Text>();
            Level = GameObject.Find("UI/LV").GetComponent<Text>();
            stage = GameObject.Find("UI/Stage").GetComponent<Text>();
            get_coin.text = $"{GameManager.Data.play_gold}";
            event_jump = GameObject.Find("UI/Button_Jump").GetComponent<EventTrigger>();
            event_down = GameObject.Find("UI/Button_Down").GetComponent<EventTrigger>();

            entry_Jump = new EventTrigger.Entry();
            entry_Jump.eventID = EventTriggerType.PointerDown;
            entry_Jump.callback.AddListener((data) => { OnJump((PointerEventData)data); });
            event_jump.triggers.Add(entry_Jump);

            entry_Down = new EventTrigger.Entry();
            entry_Down.eventID = EventTriggerType.PointerDown;
            entry_Down.callback.AddListener((data) => { OnDown((PointerEventData)data); });
            event_down.triggers.Add(entry_Down);

            player_opacity = gameObject.GetComponent<SpriteRenderer>().color;
            // Temp선언
            Use_Jump = 0;
            jump = 10.0f;
            down = 20.0f;

            spriterenderer = GetComponent<SpriteRenderer>();

            animator = GetComponent<Animator>();

            if (GameManager.Data.auto_jump)
            {
                StartCoroutine("Auto_Jump");
            }

            if (GameManager.Data.random_god)
            {
                StartCoroutine("Random_God");
            }
            if (GameManager.Data.auto_restore)
            {
                StartCoroutine("Auto_Restore");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
            player_hp.text = $"{GameManager.Data.hp}";
            Level.text = $"Lv : {GameManager.Data.lv} EXP : {GameManager.Data.now_Exp} / {GameManager.Data.EXP[GameManager.Data.lv]}";
            combo.text = $"Combo : {GameManager.Data.combo}";
            stage.text = $"{GameManager.Data.pattern[GameManager.Data.stage]}";
            animator.SetBool("StartGame", true);
        }
            

        if(GameManager.Data.hp <= 0) //GameEnd 조건
        {
            if(GameManager.Data.buwhal != 0)
            {
                // 부활 있슴
                StartCoroutine("Resurrection");
                GameManager.Data.buwhal -= 1;
            }
            else
            {
                //부활 없슴
                
                GameManager.Instance.Save();
                animator.SetBool("StartGame", false);
                SceneManager.LoadScene("End_Game");
                GameManager.Instance.Load();
            }
        }

        // 원활한 테스트 위함
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Use_Jump < GameManager.Data.max_jump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, GameManager.Data.jump, 0);
                Use_Jump += 1;
                animator.SetTrigger("Jumping");
                animator.SetBool("Landing", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.y > -2.62)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * GameManager.Data.down, 0);
            }
        }
      
    }

    
    IEnumerator Auto_Jump()
    {
        while (true)
        {
            int per = Random.Range(0, 100);
            if(per < 20)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, GameManager.Data.jump, 0);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator Resurrection()
    {
        for(int i=0; i<2; i++)
        {
            if(i == 0)
            {
                Time.timeScale = 0;
                GameManager.Data.hp = (GameManager.Data.max_hp / 2);
            }
            if(i == 1)
            {
                Time.timeScale = 1;
            }
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator Random_God()
    {
        while (true)
        {
            int per = Random.Range(0, 100);
            if (per < 10 + GameManager.Data.luck)
            {
                StartCoroutine("Small_God");
            }
            yield return new WaitForSeconds(8f);
        }
    }

    IEnumerator Small_God()
    {
        for(int i=0; i<2; i++)
        {
            if(i == 0)
            {
                gameObject.tag = "God";
                player_opacity.a = 0.5f;
                gameObject.GetComponent<SpriteRenderer>().color = player_opacity;
            }
            else
            {
                player_opacity.a = 1.0f;
                gameObject.GetComponent<SpriteRenderer>().color = player_opacity;
                gameObject.tag = "Player";
            }
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Auto_Restore()
    {
        while (true)
        {
            GameManager.Data.hp += 4 * GameManager.Data.restore_eff;
            yield return new WaitForSeconds(10f);
        }
    }

    void OnJump(PointerEventData data)
    {
        if (Use_Jump < GameManager.Data.max_jump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, GameManager.Data.jump, 0);
            Use_Jump += 1;
            animator.SetTrigger("Jumping");
            animator.SetBool("Landing", false);
        }
    }

    void OnDown(PointerEventData data)
    {
        if(transform.position.y > -2.62)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * GameManager.Data.down, 0);
        }
    }
    IEnumerator OnDodge(int t) // 피격시 일정시간 무적
    {
        for (int i = 0; i < t; i++)
        {
            if (i == 0)
            {
                gameObject.tag = "God";
            }

            if (i == (t - 1))
            {
                player_opacity.a = 1.0f;
                gameObject.GetComponent<SpriteRenderer>().color = player_opacity;
                gameObject.tag = "Player";
            }

            if (i % 2 == 0)
            {
                player_opacity.a = 0.5f;
                gameObject.GetComponent<SpriteRenderer>().color = player_opacity;
            }
            else
            {
                player_opacity.a = 1.0f;
                gameObject.GetComponent<SpriteRenderer>().color = player_opacity;
            }
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator OnBlood() // 피흘림
    {
        for(int i=0; i<5; i++)
        {
            GameManager.Data.hp -= 3;
            yield return new WaitForSeconds(2.0f);
        }

    }

    IEnumerator OnStun() //스턴
    {
        for(int i=0; i<2; i++)
        {
            if(i == 0)
            {
                event_jump.triggers.Remove(entry_Jump);
                event_down.triggers.Remove(entry_Down);
                button_jump.interactable = false;
                button_down.interactable = false;
            }
            else if(i == 1)
            {
                event_jump.triggers.Add(entry_Jump);
                event_down.triggers.Add(entry_Down);
                button_jump.interactable = true;
                button_down.interactable = true;
            }

            yield return new WaitForSeconds(3.0f);
        }
    }

    // 피격시 
    void OnHit()
    {
        StartCoroutine(OnDodge(GameManager.Data.dodge_time));

        int per = Random.Range(0, 100);
        //회피함
        if(per > GameManager.Data.luck)
        {
            if (GameManager.Data.combo > GameManager.Data.max_combo)
            {
                GameManager.Data.max_combo = GameManager.Data.combo;
            }
            GameManager.Data.combo = 0;

            GameManager.Data.hp -= (GameManager.Data.damage - GameManager.Data.defense);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Shield"))
        {
            gameObject.tag = "Player";
            gameObject.transform.Find("1").gameObject.SetActive(false);
            StartCoroutine(OnDodge(GameManager.Data.dodge_time));
        }
        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Trap_Blood"))
        {
            OnHit();
            StartCoroutine(OnBlood());
        }

        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Trap_Stun"))
        {
            OnHit();
            StartCoroutine(OnStun());
        }

        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Trap"))
        {
            OnHit();
        }

        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Jump"))
        {
            OnHit();
        }

        

        if (other.gameObject.CompareTag("HP"))
        {
            Debug.Log("체력!");

            GameManager.Data.hp = GameManager.Data.hp + 50 > GameManager.Data.max_hp ? GameManager.Data.max_hp : GameManager.Data.hp + 50;
            player_hp.text = $"{GameManager.Data.hp}";
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("LevelUp"))
        {
            Debug.Log("레벨업");

            GameManager.Data.now_Exp = 0;
            GameManager.Data.lv += 1;
            GameManager.Data.lvup = false;
            GameManager.Instance.Save();
            SceneManager.LoadScene("Select_Item");
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("돈!!!!");
            GameManager.Data.play_gold += 10 * (1 + (0.1f * GameManager.Data.multi_coin));
            get_coin.text = $"{GameManager.Data.play_gold}";
            Destroy(other.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            animator.SetBool("Landing", true);
            Use_Jump = 0;
        }
        if (collision.gameObject.CompareTag("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
            animator.SetBool("Landing", true);
            Use_Jump = 0;
            GameManager.Data.combo += 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Jump2"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
            Use_Jump = 0;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("돈!!!!");
            GameManager.Data.play_gold += 10 * (1 + (0.1f * GameManager.Data.multi_coin));
            get_coin.text = $"{GameManager.Data.play_gold}";
            Destroy(collision.gameObject);
        }
    }
}
