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
    int Max_Jump; // 플레이어의 최대 가능 점프 횟수
    int Use_Jump; // 플레이어의 현재 사용 점프 횟수

    Text player_hp;
    Text get_coin;
    Text Level;
    Text Stage; // 임시 테스트용
    Color player_opacity; // 플레이어 투명도, 피격 후 깜박이기 위해 사용

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            button_jump = GameObject.Find("UI/Button_Jump").GetComponent<Button>();
            button_down = GameObject.Find("UI/Button_Down").GetComponent<Button>();


            player_hp = GameObject.Find("UI/HP").GetComponent<Text>();
            get_coin = GameObject.Find("UI/Gold").GetComponent<Text>();
            Level = GameObject.Find("UI/LV").GetComponent<Text>();
            Stage = GameObject.Find("UI/STAGE").GetComponent<Text>();
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
            Max_Jump = 2;
            Use_Jump = 0;
            jump = 10.0f;
            down = 20.0f;

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
            Stage.text = $"{GameManager.Data.stage}";
        }
            

        if(GameManager.Data.hp <= 0) //GameEnd 조건
        {
            GameManager.Data.hp = GameManager.Data.max_hp;
            GameManager.Data.Gold += GameManager.Data.play_gold;
            GameManager.Data.play_gold = 0;
            GameManager.Data.speed = 8.0f; // 임시 !! 추후 캐릭터 속도로 받아 적용하도록 수정예정.
            GameManager.Data.lv = 0;
            GameManager.Data.now_Exp = 0;
            GameManager.Instance.Save();
            SceneManager.LoadScene("End_Game");
            GameManager.Instance.Load();
        }

        // 원활한 테스트 위함
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Use_Jump < Max_Jump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump, 0);
                Use_Jump += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.position.y > -2.62)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * down, 0);
            }
        }
      
    }
    
    void OnJump(PointerEventData data)
    {
        if (Use_Jump < Max_Jump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump, 0);
            Use_Jump += 1;
        }

    }

    void OnDown(PointerEventData data)
    {
        if(transform.position.y > -2.62)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * down, 0);
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



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Shield"))
        {
            gameObject.tag = "Player";
            gameObject.transform.Find("1").gameObject.SetActive(false);
            StartCoroutine(OnDodge(12));
        }
        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Trap_Blood"))
        {
            GameManager.Data.hp -= GameManager.Data.damage;
            StartCoroutine(OnBlood());
            StartCoroutine(OnDodge(12));
        }

        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Trap_Stun"))
        {
            GameManager.Data.hp -= GameManager.Data.damage;
            StartCoroutine(OnStun());
            StartCoroutine(OnDodge(12));
        }

        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Trap"))
        {
            Debug.Log("충돌!!!!");
            GameManager.Data.hp -= GameManager.Data.damage;
            StartCoroutine(OnDodge(12));
        }

        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Jump"))
        {
            Debug.Log("충돌!!!!");
            GameManager.Data.hp -= GameManager.Data.damage;
            StartCoroutine(OnDodge(12));
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("돈!!!!");
            GameManager.Data.play_gold += 10;
            get_coin.text = $"{GameManager.Data.play_gold}";
            Destroy(other.gameObject);
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


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            Use_Jump = 0;
        }
        if (collision.gameObject.CompareTag("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 4, 0);
            Use_Jump = 0;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Jump2"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 10, 0);
            Use_Jump = 0;
        }
    }
}
