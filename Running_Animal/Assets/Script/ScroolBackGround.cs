using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScroolBackGround : MonoBehaviour
{
    GameObject player;
    public float speed;
    public float startPosition;
    public float endPosition;

    void Start()
    {
        player = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        speed = player.GetComponent<Character>().init_Speed;

    }

    void Update()
    {
        transform.Translate(-1 * speed *Time.deltaTime, 0 , 0);
        if(transform.position.x <= endPosition) { transform.Translate(-1 * (endPosition - startPosition), 0, 0); }
    }
}
