using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMiss : MonoBehaviour
{
    // 회피에 성공했을 경우 Miss 뜸.
    // 근데 그 Miss가 등장하고 점차 옅게 사라져야함.

    Color a;

    void Start()
    {
        a = gameObject.GetComponent<SpriteRenderer>().color;
        StartCoroutine(OnMiss());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnMiss()
    {
        for(float i=10; i>0; i--)
        {
            a.a = (i / 10.0f);
            gameObject.GetComponent<SpriteRenderer>().color = a;
            gameObject.transform.Translate(0, 0.1f, 0);
            if (i == 1) Destroy(gameObject);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
