using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    EventTrigger.Entry entry_Jump;
    EventTrigger.Entry entry_Down;
    EventTrigger event_jump;
    EventTrigger event_down;

    Button button_jump;
    Button button_down;


    GameObject jump2;

    public float jump; // Jump Value
    public float down;
    public float speed;
    // 아래의 변수 값은 GameManager에서 받아온다.
    // GameManager.Instance.Player 
    int Max_Jump; // 플레이어의 최대 가능 점프 횟수
    int Use_Jump; // 플레이어의 현재 사용 점프 횟수

    Text player_hp;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            button_jump = GameObject.Find("UI/Button_Jump").GetComponent<Button>();
            button_down = GameObject.Find("UI/Button_Down").GetComponent<Button>();


            player_hp = GameObject.Find("UI/Gold").GetComponent<Text>();
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
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        player_hp.text = $"{GameManager.Data.hp}";
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
        if (other.gameObject.CompareTag("Trap_Blood"))
        {
            GameManager.Data.hp -= GameManager.Data.damage;
            StartCoroutine(OnBlood());
        }

        if (other.gameObject.CompareTag("Trap_Stun"))
        {
            GameManager.Data.hp -= GameManager.Data.damage;
            StartCoroutine(OnStun());
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            Debug.Log("충돌!!!!");
            GameManager.Data.hp -= GameManager.Data.damage;
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            Debug.Log("충돌!!!!");
            GameManager.Data.hp -= GameManager.Data.damage;
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
    }
}
