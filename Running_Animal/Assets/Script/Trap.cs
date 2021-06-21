using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    enum Forest_Trap
    {
        Jump_Bear = 0, // 곰덫
        Jump_Mole, // 두더지 함정
        Jump_Stump, // 나무 밑둥 함정
        Jump_Banana, // 바나나 껍질
        Fly_Bird, // 버드 스트라이크 (사전고지)
        Fly_Shot, // 밀렵꾼의 조용한 한발
        Monster,// 정령?
        NoneJump_Stone,// 곰잡는 돌덫
        NoneJump_Bee, // 벌이 무리지어있음 (공중) - 점프하면 으앙 쥬금
        Bridge // 숲 특수 장애물
    }

    GameObject[] traps; // 함정 리소스 저장
    GameObject warning_bird;
    GameObject warning_shot;
    GameObject player;
    GameObject warn;


    //BackGround 포함 함정 자동 이동위함

    private void Start()
    {
        player = GameObject.Find("Player");
        // 시작시 함정 리소스 불러오기.
        traps = Resources.LoadAll<GameObject>("Trap/Forest");
        warning_bird = Resources.Load<GameObject>("Trap/Warning_Bird");
        warning_shot = Resources.Load<GameObject>("Trap/Warning_Shot");
        MakeTrap((int)Forest_Trap.NoneJump_Stone , new Vector3(34, 2.47f, 0));
        //StartCoroutine("MakeShot");

    }

    public void MakeTrap(int trap_num, Vector3 pos)
    {
        GameObject tmp;
        tmp = Instantiate(traps[trap_num]);
        tmp.transform.parent = gameObject.transform;
        tmp.transform.position = pos;
    }

    IEnumerator MakeBird()
    {
        float rand_y = Random.Range(-2.5f, 3.0f);
        GameObject bird;
        for(int i=0; i<2; i++)
        {
            if (i == 0) // 경고 생성.
            {
                warn = Instantiate(warning_bird);
                warn.transform.position = new Vector3(9, rand_y, 0);
            }
            else if(i == 1)
            {
                Destroy(warn);
                bird = Instantiate(traps[(int)Forest_Trap.Fly_Bird]);
                bird.transform.parent = gameObject.transform;
                bird.transform.position = new Vector3(37, rand_y, 0);
                bird.GetComponent<MoveTrap>().more_speed = 15.0f;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator MakeShot()
    {
        GameObject shot;
        float shot_y = 0;
        for (int i = 0; i < 30; i++)
        {
            float y = player.transform.position.y;
            if (i < 20) // 경고 생성.
            {
                Destroy(warn);
                warn = Instantiate(warning_shot);
                warn.transform.position = new Vector3(warn.transform.position.x, y, warn.transform.position.z);
                shot_y = y;
            }
            else if (i < 24)
            {
                Destroy(warn);
                warn = Instantiate(warning_shot);
                warn.transform.position = new Vector3(warn.transform.position.x, shot_y, warn.transform.position.z);
            }
            else if( i == 25)
            {
                Destroy(warn);
            }
            else if (i == 29)
            {
                shot = Instantiate(traps[(int)Forest_Trap.Fly_Shot]);
                shot.transform.parent = gameObject.transform;
                shot.transform.position = new Vector3(37, shot_y, 0);
                shot.GetComponent<MoveTrap>().more_speed = 80.0f;
            }
            yield return new WaitForSeconds(0.15f);
        }
    }

}
