using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMiss : MonoBehaviour
{
    // ȸ�ǿ� �������� ��� Miss ��.
    // �ٵ� �� Miss�� �����ϰ� ���� ���� ���������.

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
