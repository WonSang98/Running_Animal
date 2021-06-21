using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap_y : MonoBehaviour
{
    GameObject player;
    public float speed;
    int UpDown = -1;
    // Start is called before the first frame update
    void Start()
    {
        player = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        speed = player.GetComponent<Character>().init_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, UpDown * 0.1f * speed * Time.deltaTime, 0);
        if (transform.position.y <= -4.3) UpDown = 1;
        if (transform.position.y >= -2.7) UpDown = -1;
        /*
        if (transform.position.x <= -26)
        {
            Destroy(gameObject);
        }
        */
    }
}
