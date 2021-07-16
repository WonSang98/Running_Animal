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

    public int idx; // �׽�Ʈ�Ϸ��� public
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
        //idx = 20; �׽�Ʈ�Ϸ��� �ּ�ó����.
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
                Invoke("pattern" + idx.ToString(), 0);
                //Invoke("pattern38", 0);
                //idx++;
            }
        }

        if (idx == 31)
        {
            idx = 20;
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

    void pattern20() // �δ��� + ��
    {
        MakeTrap(21, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern21() // �δ��� + ��
    {
        MakeTrap(22, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern22() // �δ��� + ����
    {
        MakeTrap(23, new Vector3(0, 0, 0));
    }

    void pattern23() // �δ��� + ������
    {
        MakeTrap(24, new Vector3(0, 0, 0));
    }

    void pattern24() // �δ��� + �ܹ�
    {
        MakeTrap(25, new Vector3(0, 0, 0));
    }

    void pattern25() // ���� + �ٳ���
    {
        MakeTrap(26, new Vector3(0, 0, 0));
    }

    void pattern26() // ���� + ��
    {
        MakeTrap(27, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern27() // ���� + ��
    {
        MakeTrap(28, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern28() // ���� + ����
    {
        MakeTrap(29, new Vector3(0, 0, 0));
    }

    void pattern29() // ���� + ������
    {
        MakeTrap(30, new Vector3(0, 0, 0));
    }

    void pattern30() // ���� + �ܹ�
    {
        MakeTrap(31, new Vector3(0, 0, 0));
    }

    void pattern31() // �ٳ��� + ��
    {
        MakeTrap(32, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern32() // �ٳ��� + ��
    {
        MakeTrap(33, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern33() // �ٳ��� + ����
    {
        MakeTrap(34, new Vector3(0, 0, 0));
    }

    void pattern34() // �ٳ��� + ������
    {
        MakeTrap(35, new Vector3(0, 0, 0));
    }

    void pattern35() // �ٳ��� + �ܹ�
    {
        MakeTrap(36, new Vector3(0, 0, 0));
    }

    void pattern36() // �� + ��
    {
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern37() // �� + ����
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(38, new Vector3(0, 0, 0));
    }

    void pattern38() // �� + ������
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(39, new Vector3(0, 0, 0));
    }

    void pattern39() // �� + �ܹ�
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(40, new Vector3(0, 0, 0));
    }

    void pattern40() // �� + ����
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(41, new Vector3(0, 0, 0));
    }

    void pattern41() // �� + ������
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(42, new Vector3(0, 0, 0));
    }

    void pattern42() // �� + �ܹ�
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(43, new Vector3(0, 0, 0));
    }

    void pattern43() // ����+������
    {
        MakeTrap(44, new Vector3(0, 0, 0));
    }

    void pattern44() // ����+�ܹ�
    {
        MakeTrap(45, new Vector3(0, 0, 0));
    }

    void pattern45() // ������+�ܹ�
    {
        MakeTrap(46, new Vector3(0, 0, 0));
    }

    void pattern46() // ���� �δ��� ����
    {
        MakeTrap(47, new Vector3(0, 0, 0));
    }

    void pattern47() // ���� �δ��� �ٳ���
    {
        MakeTrap(48, new Vector3(0, 0, 0));
    }

    void pattern48() // ���� �δ��� ��
    {
        MakeTrap(49, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern49() // ���� �δ��� ��
    {
        MakeTrap(50, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern50() // ���� �δ��� ����
    {
        MakeTrap(51, new Vector3(0, 0, 0));
    }

    void pattern51() // ���� �δ��� ������
    {
        MakeTrap(52, new Vector3(0, 0, 0));
    }

    void pattern52() // ���� �δ��� ��
    {
        MakeTrap(53, new Vector3(0, 0, 0));
    }

    void pattern53() // ���� ���� �ٳ���
    {
        MakeTrap(54, new Vector3(0, 0, 0));
    }

    void pattern54() // ���� ���� ��
    {
        MakeTrap(55, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern55() // ���� ���� ��
    {
        MakeTrap(56, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern56() // ���� ���� ����
    {
        MakeTrap(57, new Vector3(0, 0, 0));
    }

    void pattern57() // ���� ���� ������
    {
        MakeTrap(58, new Vector3(0, 0, 0));
    }

    void pattern58() // ���� ���� ��
    {
        MakeTrap(59, new Vector3(0, 0, 0));
    }
    void pattern59() // ���� �ٳ��� ��
    {
        MakeTrap(60, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern60() // ���� �ٳ��� ��
    {
        MakeTrap(61, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern61() // ���� �ٳ��� ����
    {
        MakeTrap(62, new Vector3(0, 0, 0));
    }

    void pattern62() // ���� �ٳ��� ������
    {
        MakeTrap(63, new Vector3(0, 0, 0));
    }

    void pattern63() // ���� �ٳ��� �ܹ�
    {
        MakeTrap(64, new Vector3(0, 0, 0));
    }

    void pattern64() // ���� �� ��
    {
        MakeTrap(65, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern65() // ���� �� ���� 
    {
        MakeTrap(66, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern66() // ���� �� ������
    {
        MakeTrap(67, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern67() // ���� �� �ܹ�
    {
        MakeTrap(68, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern68() // ���� �� ����
    {
        MakeTrap(69, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }
    void pattern69() // ���� �� ������
    {
        MakeTrap(70, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern70() // ���� �� �ܹ�
    {
        MakeTrap(71, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern71() // ���� ���� ������
    {
        MakeTrap(72, new Vector3(0, 0, 0));
    }

    void pattern72() // ���� ���� �ܹ�
    {
        MakeTrap(73, new Vector3(0, 0, 0));
    }

    void pattern73() // ���� ������ �ܹ�
    {
        MakeTrap(74, new Vector3(0, 0, 0));
    }

    void pattern74() // �δ��� ���� �ٳ���
    {
        MakeTrap(75, new Vector3(0, 0, 0));
    }

    void pattern75() // �δ��� ���� ��
    {
        MakeTrap(76, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.2f));
    }

    void pattern76() // �δ��� ���� ��
    {
        MakeTrap(77, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern77() // �δ��� ���� ����
    {
        MakeTrap(78, new Vector3(0, 0, 0));
    }

    void pattern78() // �δ��� ���� ������
    {
        MakeTrap(79, new Vector3(0, 0, 0));
    }

    void pattern79() // �δ��� ���� �ܹ�
    {
        MakeTrap(80, new Vector3(0, 0, 0));
    }

    void pattern80() // �δ��� �ٳ��� ��
    {
        MakeTrap(81, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern81() // �δ��� �ٳ��� ��
    {
        MakeTrap(82, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern82() // �δ��� �ٳ��� ����
    {
        MakeTrap(83, new Vector3(0, 0, 0));
    }

    void pattern83() // �δ��� �ٳ��� ������
    {
        MakeTrap(84, new Vector3(0, 0, 0));
    }

    void pattern84() // �δ��� �ٳ��� ��
    {
        MakeTrap(85, new Vector3(0, 0, 0));
    }

    void pattern85() // �δ��� �� ��
    {
        MakeTrap(86, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        StartCoroutine(cotime(3, "MakeShot", 0.3f));
    }

    void pattern86() // �δ��� �� ����
    {
        MakeTrap(87, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern87() // �δ��� �� ������
    {
        MakeTrap(88, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern88() // �δ��� �� �ܹ�
    {
        MakeTrap(89, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern89() // �δ��� �� ����
    {
        MakeTrap(90, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern90() // �δ��� �� ������
    {
        MakeTrap(91, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }


}