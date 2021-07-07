using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScroolBackGround : MonoBehaviour
{
    public float startPosition;
    public float endPosition;
    public float speed;

    void Start()
    {
        speed = GameManager.Data.speed;
    }

    void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0) ;
        if(transform.position.x <= endPosition) { transform.Translate(-1 * (endPosition - startPosition), 0, 0); }
    }
}
