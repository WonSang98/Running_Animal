using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap_rotate : MonoBehaviour
{
    float x, y, r2;
    float pm = -1;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent.transform;
        x = 4;
        r2 = 16;
    }

    // Update is called once per frame
    void Update()
    {
        if (x <= -4) pm = 1;
        else if (x >= 4) pm = -1;
        x += pm * Time.deltaTime * 6;
        y = Mathf.Sqrt(Mathf.Abs(r2 - Mathf.Pow(x, 2)));
        transform.localPosition = new Vector3(x, -1 * y, 0);


        Vector2 direction = 
            new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 20 * Time.deltaTime);
        transform.rotation = rotation;
        }
}
