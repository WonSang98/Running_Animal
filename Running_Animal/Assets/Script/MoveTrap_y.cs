using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap_y : MonoBehaviour
{
    // 두우더지
    int UpDown = -1;

    private void Start()
    {
        StartCoroutine("UD");
    }

    IEnumerator UD() // 일정 간격으로 뿅뿅 위로 갔다 아래로 갔다가.
    {
        while (true)
        {
            transform.Translate(0, UpDown * 1.6f, 0);
            UpDown *= -1;
            yield return new WaitForSeconds(1f);
        }
    }
}
