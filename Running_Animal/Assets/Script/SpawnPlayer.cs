using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    GameObject prfPlayer;
    GameObject Player;

    void Start()
    {
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.transform.localScale = new Vector3(1, 1, 1);
        Player.transform.Translate(-6.495f, -1.5f, 0);
        Player.transform.name = "Player";
    }
}
