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
        Bridge_Stool, // �� Ư�� - ����
        Bridge_Trap // �� Ư�� - ��ֹ�
    }

    int idx;
    GameObject[] traps; // ���� ���ҽ� ����
    GameObject warning_bird;
    GameObject warning_shot;
    GameObject LvUp;
    GameObject player;
    GameObject coin;
    GameObject hp;

    //BackGround ���� ���� �ڵ� �̵�����

    private void Start()
    {
        idx = 0;
        player = GameObject.Find("Player");
        // ���۽� ���� ���ҽ� �ҷ�����.
        traps = Resources.LoadAll<GameObject>("Trap/Forest");
        warning_bird = Resources.Load<GameObject>("Trap/Warning_Bird");
        warning_shot = Resources.Load<GameObject>("Trap/Warning_Shot");
        LvUp = Resources.Load<GameObject>("Trap/LvUp");

        //test
        coin = Resources.Load<GameObject>("Item/Coin");
        hp = Resources.Load<GameObject>("Item/HP");
        //MakeTrap((int)Forest_Trap.NoneJump_Bee , new Vector3(34, 0, 0));
        StartCoroutine("hptime");


    }

    private void Update()
    {
        transform.Translate(-1 * GameManager.Data.speed * Time.deltaTime, 0, 0);
        if (transform.position.x <= 9)
        {
            transform.Translate(-1 * (9 - 37), 0, 0);
            if (GameManager.Data.lvup == true)
            {
                LevelUP();
            }
            else 
            {
                //Invoke("pattern" + idx.ToString(), 0);
                Invoke("pattern19", 0);
                idx++;
            }
        }

        if (idx == 10)
        {
            idx = 0;
            GameManager.Data.speed += 0.2f;
        }
    }

    public void MakeTrap(int trap_num, Vector3 pos)
    {
        GameObject tmp;
        tmp = Instantiate(traps[trap_num]);
        //tmp.transform.parent = transform;
        tmp.transform.position = pos;
    }

    public void MakeCoin(Vector3 pos)
    {
        GameObject tmp;
        tmp = Instantiate(coin);
        //tmp.transform.parent = transform;
        tmp.transform.position = pos;
    }

    public void MakeHP(Vector3 pos)
    {
        GameObject tmp;
        tmp = Instantiate(hp);
        //tmp.transform.parent = transform;
        tmp.transform.position = pos;
    }

    IEnumerator MakeBird()
    {
        float rand_y = Random.Range(-2.5f, 3.0f);
        GameObject bird;
        GameObject warn = null;
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
                //bird.transform.parent = transform;
                bird.transform.position = new Vector3(37, rand_y, 0);
                bird.GetComponent<MoveTrap>().more_speed = 15.0f;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator MakeShot()
    {
        GameObject shot;
        GameObject warn = null;
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
                //shot.transform.parent = transform;
                shot.transform.position = new Vector3(37, shot_y, 0);
                shot.GetComponent<MoveTrap>().more_speed = 80.0f;
            }
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator MakeSpecial()
    {
        GameObject[] Brdige = new GameObject[9];
        GameObject[] Trap = new GameObject[9];

        float start_x = 16.0f;
        float interval_x = 2.5f;

        const float bridge_y = -3.8f;
        const float trap_y = 2.65f;

        for(int i=0; i<9; i++)
        {
            int temp = Random.Range(0, 100);
            if (i == 0)
            {
                Brdige[i] = Instantiate(traps[(int)Forest_Trap.Bridge_Stool]);
                //Brdige[i].transform.parent = transform;
                Brdige[i].transform.position = new Vector3(start_x, bridge_y, 0);
                if (temp < 75)
                {
                    Trap[i] = Instantiate(traps[(int)Forest_Trap.Bridge_Trap]);
                    //Trap[i].transform.parent = transform;
                    Trap[i].transform.position = new Vector3(start_x, trap_y, 0);
                }
            }
            else
            {
                Brdige[i] = Instantiate(traps[(int)Forest_Trap.Bridge_Stool]);
                //Brdige[i].transform.parent = transform;
                Brdige[i].transform.position = new Vector3(Brdige[i-1].transform.position.x + interval_x, bridge_y, 0);
                if (temp < 50)
                {
                    Trap[i] = Instantiate(traps[(int)Forest_Trap.Bridge_Trap]);
                    //Trap[i].transform.parent = transform;
                    Trap[i].transform.position = new Vector3(Brdige[i - 1].transform.position.x + interval_x, trap_y, 0);
                }
            }
            yield return null;
        }
    }

    IEnumerator cotime(int n, string name, float time)
    {
        for(int i=0; i<n; i++)
        {
            StartCoroutine(name);
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator hptime()
    {
        while (true)
        {
            MakeHP(new Vector3(36, 0.5f, 0));
            yield return new WaitForSeconds(20.0f);

        }
    }
    void LevelUP() // ������ ���.
    {
        GameObject tmp;
        tmp = Instantiate(LvUp);
        tmp.transform.position = new Vector3(35, -1.8f, 0);
    }
    void pattern0() // ���� ����
    {
        //trap ����
        MakeTrap(0, new Vector3(36, -3.36f, 0));
        MakeTrap(0, new Vector3(29, -3.36f, 0));
        MakeTrap(0, new Vector3(22, -3.36f, 0));
        // coint ����
        MakeCoin(new Vector3(11, -2.82f ,0));
        MakeCoin(new Vector3(13, -2.82f, 0));
        MakeCoin(new Vector3(15, -2.82f, 0));
        MakeCoin(new Vector3(17, -2.82f, 0));
        MakeCoin(new Vector3(19, -2.82f, 0));
        MakeCoin(new Vector3(22, -1, 0));
        MakeCoin(new Vector3(25, -2.82f, 0));
        MakeCoin(new Vector3(26, -2.82f, 0));
        MakeCoin(new Vector3(29, -1, 0));
        MakeCoin(new Vector3(32, -2.82f, 0));
        MakeCoin(new Vector3(33, -2.82f, 0));
        MakeCoin(new Vector3(34, -2.82f, 0));
    }

    void pattern1() // �δ��� ����
    {
        MakeTrap(1, new Vector3(36, -3.35f, 0));
        MakeTrap(1, new Vector3(28, -3.35f, 0));
        MakeTrap(1, new Vector3(20, -3.35f, 0));
    }

    void pattern2() // ���� ����
    {
        MakeTrap(2, new Vector3(36, -2.97f, 0));
        MakeTrap(2, new Vector3(28, -2.97f, 0));
        MakeTrap(2, new Vector3(20, -2.97f, 0));
    }

    void pattern3() // �ٳ��� ����
    {
        MakeTrap(3, new Vector3(36, -3.16f, 0));
        MakeTrap(3, new Vector3(28, -3.16f, 0));
        MakeTrap(3, new Vector3(20, -3.16f, 0));
    }

    void pattern4() // ���彺Ʈ����ũ ����
    {
        StartCoroutine(cotime(3, "MakeBird", 0.2f));
    }

    void pattern5() // �� ��� ����
    {
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }

    void pattern6() // ���� ����
    {
        MakeTrap(6, new Vector3(36, -2.85f, 0));
        MakeTrap(6, new Vector3(28, -2.85f, 0));
        MakeTrap(6, new Vector3(20, -2.85f, 0));
    }

    void pattern7() // ������ ����
    {
        MakeTrap(7, new Vector3(36, 5.7f, 0));
        MakeTrap(7, new Vector3(21, 5.7f, 0));
    }

    void pattern8() // �ܹ� ����
    {
        MakeTrap(8, new Vector3(36, 1.07f, 0));
        MakeTrap(8, new Vector3(30, 1.07f, 0));
        MakeTrap(8, new Vector3(24, 1.07f, 0));
        MakeTrap(8, new Vector3(33, 3.27f, 0));
        MakeTrap(8, new Vector3(27, 3.27f, 0));

    }

    void pattern9() // ����� ����
    {
        StartCoroutine("MakeSpecial");
    }

    void pattern10() // ���� + ������
    {
        MakeTrap(0, new Vector3(15.2f, -3.342f, 0));
        MakeTrap(0, new Vector3(27.1f, -3.342f, 0));
        MakeTrap(1, new Vector3(21.3f, -3.35f, 0));
        MakeTrap(1, new Vector3(32.8f, -3.35f, 0));
        MakeTrap(1, new Vector3(35.6f, -3.35f, 0));
    }

    void pattern11() // ���� + ����
    {
        MakeTrap(0, new Vector3(15.2f, -3.342f, 0));
        MakeTrap(0, new Vector3(18.8f, -3.342f, 0));
        MakeTrap(0, new Vector3(26.7f, -3.342f, 0));
        MakeTrap(2, new Vector3(17, -2.97f, 0));
        MakeTrap(2, new Vector3(24.6f, -2.97f, 0));
        MakeTrap(2, new Vector3(33, -2.97f, 0));
    }

    void pattern12() // ���� + �ٳ���
    {
        MakeTrap(0, new Vector3(21, -3.342f, 0));
        MakeTrap(0, new Vector3(32, -3.342f, 0));
        MakeTrap(3, new Vector3(15, -3.16f, 0));
        MakeTrap(3, new Vector3(26, -3.16f, 0));
        MakeTrap(3, new Vector3(37, -3.16f, 0));
    }

    void pattern13() // ���� + ��
    {
        MakeTrap(0, new Vector3(36, -3.342f, 0));
        MakeTrap(0, new Vector3(29, -3.342f, 0));
        MakeTrap(0, new Vector3(22, -3.342f, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern14() // ���� + ��
    {
        MakeTrap(0, new Vector3(36, -3.342f, 0));
        MakeTrap(0, new Vector3(29, -3.342f, 0));
        MakeTrap(0, new Vector3(22, -3.342f, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern15() // ���� + ����
    {
        MakeTrap(0, new Vector3(13.8f, -3.342f, 0));
        MakeTrap(0, new Vector3(15.4f, -3.342f, 0));
        MakeTrap(0, new Vector3(17, -3.342f, 0));
        MakeTrap(0, new Vector3(18.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(20f, -3.342f, 0));
        MakeTrap(0, new Vector3(21.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(23f, -3.342f, 0));
        MakeTrap(0, new Vector3(24.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(26f, -3.342f, 0));
        MakeTrap(0, new Vector3(27.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(29f, -3.342f, 0));
        MakeTrap(0, new Vector3(30.5f, -3.342f, 0));
        MakeTrap(6, new Vector3(12, -2.8f, 0));
        MakeTrap(6, new Vector3(32, -2.8f, 0));
    }

    void pattern16() // ���� + ��
    {
        MakeTrap(0, new Vector3(13.8f, -3.342f, 0));
        MakeTrap(0, new Vector3(18.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(20f, -3.342f, 0));
        MakeTrap(0, new Vector3(29f, -3.342f, 0));
        MakeTrap(0, new Vector3(30.5f, -3.342f, 0));
        MakeTrap(7, new Vector3(23, 5.7f, 0));
    }

    void pattern17() // ���� + �ܹ�
    {
        MakeTrap(0, new Vector3(19.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(21.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(28.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(30.5f, -3.342f, 0));
        MakeTrap(8, new Vector3(16, 0, 0));
        MakeTrap(8, new Vector3(25, 0, 0));
        MakeTrap(8, new Vector3(34, 0, 0));
    }

    void pattern18() // �δ��� + ����
    {
        MakeTrap(1, new Vector3(15f, -3.334f, 0));
        MakeTrap(1, new Vector3(27f, -3.334f, 0));
        MakeTrap(2, new Vector3(21f, -2.97f, 0));
        MakeTrap(2, new Vector3(33f, -2.97f, 0));
    }

    void pattern19() // �δ��� + �ٳ���
    {
        MakeTrap(1, new Vector3(15f, -3.334f, 0));
        MakeTrap(1, new Vector3(25f, -3.334f, 0));
        MakeTrap(3, new Vector3(13f, -3.16f, 0));
        MakeTrap(3, new Vector3(17f, -3.16f, 0));
        MakeTrap(3, new Vector3(23f, -3.16f, 0));
        MakeTrap(3, new Vector3(27f, -3.16f, 0));
    }
}
