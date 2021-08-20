using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveTrap : MonoBehaviour
{
    //GameObject player;
    public float speed;
    public float more_speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.Play.Status.ability.SPEED.value;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1 * (GameManager.Play.Status.ability.SPEED.value * more_speed) * Time.deltaTime, 0, 0);
    }

}
