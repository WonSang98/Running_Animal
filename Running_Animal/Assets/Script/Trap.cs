using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    enum Forest_Trap
    {
        Jump_Bear = 0, // ����
        Jump_Mole, // �δ��� ����
        Jump_Stump, // ���� �ص� ����
        Jump_Banana, // �ٳ��� ����
        Fly_Bird, // ���� ��Ʈ����ũ (��������)
        Fly_Shot, // �зƲ��� ������ �ѹ�
        Monster,// ����?
        NoneJump_Stone,// ����� ����
        NoneJump_Bee, // ���� ������������ (����) - �����ϸ� ���� ���
        Bridge // �� Ư�� ��ֹ�
    }

    GameObject[] traps; // ���� ���ҽ� ����
    GameObject warning_bird;
    GameObject warning_shot;
    GameObject player;
    GameObject warn;


    //BackGround ���� ���� �ڵ� �̵�����

    private void Start()
    {
        player = GameObject.Find("Player");
        // ���۽� ���� ���ҽ� �ҷ�����.
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
            if (i == 0) // ��� ����.
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
            if (i < 20) // ��� ����.
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
