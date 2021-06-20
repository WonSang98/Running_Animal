using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    EventTrigger event_jump;
    EventTrigger event_down;


    GameObject jump2;

    public float jump; // Jump Value
    public float down;
    public float speed;
    // 아래의 변수 값은 GameManager에서 받아온다.
    // GameManager.Instance.Player 
    int Max_Jump; // 플레이어의 최대 가능 점프 횟수
    int Use_Jump; // 플레이어의 현재 사용 점프 횟수

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            event_jump = GameObject.Find("UI/Button_Jump").GetComponent<EventTrigger>();
            event_down = GameObject.Find("UI/Button_Down").GetComponent<EventTrigger>();

            EventTrigger.Entry entry_Jump = new EventTrigger.Entry();
            entry_Jump.eventID = EventTriggerType.PointerDown;
            entry_Jump.callback.AddListener((data) => { OnJump((PointerEventData)data); });
            event_jump.triggers.Add(entry_Jump);

            EventTrigger.Entry entry_Down = new EventTrigger.Entry();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tile")
        {
            Use_Jump = 0;
        }
    }
}
