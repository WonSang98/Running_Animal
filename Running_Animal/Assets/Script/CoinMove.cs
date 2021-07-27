using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    public float speed;
    public float more_speed = 0;

    GameObject target; // 자석버그시 플레이어 위치 알아내기 위함.
    Vector3 vel = Vector3.zero;

    void Start()
    {
        speed = GameManager.Data.speed;
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Data.magnet == false)
        {
            transform.Translate(-1 * (GameManager.Data.speed + more_speed) * Time.deltaTime, 0, 0);
            if (transform.position.x <= -14)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, target.transform.position, ref vel, 1f);
        }
    }
}
