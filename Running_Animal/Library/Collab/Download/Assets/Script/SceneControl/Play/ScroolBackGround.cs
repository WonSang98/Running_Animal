using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScroolBackGround : MonoBehaviour
{
    public float startPosition;
    public float endPosition;
    float speed;
    public float ratio; // ��� ���̾� ���� �����̴� ���� -> ratio : 2 �Ͻ� ĳ���� �̵��ӵ� / 2 
    void Start()
    {
        speed = GameManager.Play.Status.ability.SPEED.value / ratio;
    }

    void Update()
    {
        speed = GameManager.Play.Status.ability.SPEED.value / ratio;
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0) ;
        if(transform.position.x <= endPosition) { transform.Translate(-1 * (endPosition - startPosition), 0, 0); }
    }
}
