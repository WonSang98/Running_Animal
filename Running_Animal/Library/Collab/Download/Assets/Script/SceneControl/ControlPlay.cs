using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlPlay : MonoBehaviour
{
    //Play Scene을 전체적으로 통제한다.
    TrapForest TrapForest;
    SetPlayer SetPlayer;
    UI_Play UI_Play;
    InterAction InterAction;

    void Start()
    {
        
        TrapForest = GameObject.Find("@Managers").GetComponent<TrapForest>();
        SetPlayer = GameObject.Find("@Managers").GetComponent<SetPlayer>();
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
        InterAction = GameObject.Find("@Managers").GetComponent<InterAction>();
        //Trap Resource 불러오기.
        TrapForest.GetResource();
        //플레이어 지정된 위치에 생성
        SetPlayer.Spawn();
        if (GameManager.Play.Playing == false)
        {
            SetPlayer.FirstSet();
            GameManager.Play.Playing = true;
        }
        //UI내 스크립트 지정.
        UI_Play.SetUI();
        StartCoroutine(UI_Play.ShowStage(GameManager.Play.DC.lv - 1));
        // 코루틴 있는 패시브 아이템 적용
        InterAction.Apply_Passive();
        StartCoroutine(TrapForest.TrapUpdate());
    }

    void Update()
    {
        //PC내 테스트 코드
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GameManager.Play.DS.jumpNow < GameManager.Play.Status.ability.MAX_JUMP.value)
            {
                GameManager.Play.Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, GameManager.Play.Status.ability.JUMP.value, 0);
                GameManager.Play.DS.jumpNow += 1;
                GameManager.Play.Player.GetComponent<Animator>().SetTrigger("Jumping");
                GameManager.Play.Player.GetComponent<Animator>().SetBool("Landing", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GameManager.Play.Player.transform.position.y > -2.62f)
            {
                GameManager.Play.Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * GameManager.Play.Status.ability.DOWN.value, 0);
            }
        }
    }

  

}
