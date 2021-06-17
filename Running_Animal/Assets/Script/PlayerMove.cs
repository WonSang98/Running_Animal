using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jump; // Jump Value
    public float down;
    public float speed;
    // 아래의 변수 값은 GameManager에서 받아온다.
    // GameManager.Instance.Player 
    int Max_Jump; // 플레이어의 최대 가능 점프 횟수
    int Use_Jump; // 플레이어의 현재 사용 점프 횟수

    void Start()
    {
        // Temp선언
        Max_Jump = 2;
        Use_Jump = 0;
        jump = 10.0f;
        down = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
    }

    public void Jump()
    {
        if (Use_Jump < Max_Jump)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump, 0);
            Use_Jump += 1;
        }

    }

    public void Down()
    {
        if(transform.position.y > -2.62)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * down, 0);
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
