using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScroolBackGround : MonoBehaviour
{
    public float startPosition;
    public float endPosition;
    float speed;
    public float ratio; // 배경 레이어 별로 움직이는 비율 -> ratio : 2 일시 캐릭터 이동속도 / 2 
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
