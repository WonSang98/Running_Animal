using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Vector3 vel = Vector3.zero;
    bool cnt;
    // Start is called before the first frame update
    void Start()
    {
        cnt = false;
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(30, 20, 0));
        Invoke("DelCoin", 10);

    }

    private void Update()
    {
        if (GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Magenet])
        {
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, GameManager.Play.Player.transform.position, ref vel, 0.1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile") && cnt == false)
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(-40, 40, 0));
            cnt = true;
        }
    }

    void DelCoin()
    {
        Destroy(gameObject);
    }
}
