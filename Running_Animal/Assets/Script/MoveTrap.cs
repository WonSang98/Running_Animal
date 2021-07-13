using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveTrap : MonoBehaviour
{
    //GameObject player;
    public float speed;
    public float more_speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.Data.speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1 * (speed + more_speed) * Time.deltaTime, 0, 0);
        if (transform.position.x <= -10)
        {
            Destroy(gameObject);
            if (GameManager.Data.lv != 12)
            {
                GameManager.Data.now_Exp += 1;
                if (GameManager.Data.now_Exp >= GameManager.Data.EXP[GameManager.Data.lv])
                {
                    GameManager.Data.lvup = true;
                }
            }
        }
    }

}
