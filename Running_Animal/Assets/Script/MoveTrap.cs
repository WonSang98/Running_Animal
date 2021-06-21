using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    GameObject player;
    public float speed;
    public float more_speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        speed = player.GetComponent<Character>().init_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1 * (speed + more_speed) * Time.deltaTime, 0, 0);
        if (transform.position.x <= -26)
        {
            Destroy(gameObject);
        }
    }

}
