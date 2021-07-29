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
            GameManager.Data.hp = GameManager.Data.max_hp;
            GameManager.Data.defense += GameManager.Data.Talent_DEF;
            GameManager.Data.luck += GameManager.Data.Talent_LUK;
            GameManager.Data.restore_eff += (GameManager.Data.Talent_Restore - 1);

            // ∏ ¿Ã Forest¿œ∂ß
            for(int i=0; i<255; i++)
            {
                GameManager.Data.pattern.Add(i);
            }

            GameManager.Data.pattern = GameManager.Instance.ShuffleList(GameManager.Data.pattern);
            
            GameManager.Data.playing = true;


        }
        prfPlayer = Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Now_Character).ToString());
        Player = Instantiate(prfPlayer) as GameObject;
        Player.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Player.transform.Translate(-6.495f, -1.5f, 0);
        Player.transform.name = "Player";
    }

    
}
