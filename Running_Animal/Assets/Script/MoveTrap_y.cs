using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap_y : MonoBehaviour
{
    int UpDown = -1;
    void Update()
    {
        transform.Translate(0, UpDown * 0.1f * GameManager.Data.speed * Time.deltaTime, 0);
        if (transform.position.y <= -4.3) UpDown = 1;
        if (transform.position.y >= -2.7) UpDown = -1;
    }
}
