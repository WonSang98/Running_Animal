using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap_y : MonoBehaviour
{
    // �ο����
    int UpDown = -1;

    private void Start()
    {
        StartCoroutine("UD");
    }

    IEnumerator UD() // ���� �������� �л� ���� ���� �Ʒ��� ���ٰ�.
    {
        while (true)
        {
            transform.Translate(0, UpDown * 1.6f, 0);
            UpDown *= -1;
            yield return new WaitForSeconds(1f);
        }
    }
}
