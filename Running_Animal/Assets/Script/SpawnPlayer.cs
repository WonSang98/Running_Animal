using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    GameObject prfPlayer;
    GameObject Player;

    void Start()
    {
        if(GameManager.Data.playing == false)
        {
            GameManager.Data.max_hp += GameManager.Data.Talent_HP;
            GameManager.Data.defense += GameManager.Data.Talent_DEF;
            GameManager.Data.luck += GameManager.Data.Talent_LUK;
            GameManager.Data.restore_eff += (GameManager.Data.Talent_HP - 1);
            
            GameManager.Data.playing = true;


        }
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Player.transform.Translate(-6.495f, -1.5f, 0);
        Player.transform.name = "Player";
    }
}
