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

    GameObject[] traps; // ���� ���ҽ� ����
    GameObject warning_bird;
    GameObject warning_shot;
    GameObject LvUp;
    GameObject player;
    GameObject coin;
    GameObject hp;

    int[] map_pattern = { 255 }; // ���� �迭ȭ


    //BackGround ���� ���� �ڵ� �̵�����

    private void Start()
    {
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
                Invoke("pattern" + GameManager.Data.pattern[GameManager.Data.stage], 0);
                GameManager.Data.stage++;
                if(GameManager.Data.stage == (map_pattern[0] + 1))
                {
                    GameManager.Data.stage = 0;
                    GameManager.Data.pattern = GameManager.Instance.ShuffleList(GameManager.Data.pattern);
                }
                GameManager.Data.speed += 0.001f;
            }
        }        
    }


    public void MakeTrap(int trap_num, Vector3 pos)
    {
        GameObject tmp;
        tmp = Instantiate(traps[trap_num]);
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
        float rand_y = Random.Range(-1.25f, 3.0f);
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
            yield return new WaitForSeconds(60.0f);

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

    void pattern91() // �δ��� �� �ܹ�
    {
        MakeTrap(92, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern92() // �δ��� ���� ������
    {
        MakeTrap(93, new Vector3(0, 0, 0));
    }

    void pattern93() // �δ��� ���� �ܹ�
    {
        MakeTrap(94, new Vector3(0, 0, 0));
    }

    void pattern94() // ���� �ٳ��� ��
    {
        MakeTrap(95, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern95() // ���� �ٳ��� ��
    {
        MakeTrap(96, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern96() // ���� �ٳ��� ����
    {
        MakeTrap(97, new Vector3(0, 0, 0));
    }

    void pattern97() // ���� �ٳ��� ������
    {
        MakeTrap(98, new Vector3(0, 0, 0));
    }

    void pattern98() // ���� �ٳ��� �ܹ�
    {
        MakeTrap(99, new Vector3(0, 0, 0));
    }

    void pattern99() // ���� �� ��
    {
        MakeTrap(100, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern100() // ���� �� ����
    {
        MakeTrap(101, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern101() // ���� �� ������
    {
        MakeTrap(102, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern102() // ���� �� �ܹ�
    {
        MakeTrap(103, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern103() // ���� �� ����
    {
        MakeTrap(104, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern104() // ���� �� ������
    {
        MakeTrap(105, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern105() // ���� �� �ܹ�
    {
        MakeTrap(106, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern106() // ���� ���� ������
    {
        MakeTrap(107, new Vector3(0, 0, 0));
    }

    void pattern107() // ���� ���� �ܹ�
    {
        MakeTrap(108, new Vector3(0, 0, 0));
    }

    void pattern108() // ���� ������ �ܹ�
    {
        MakeTrap(109, new Vector3(0, 0, 0));
    }

    void pattern109() // �ٳ��� �� ��
    {
        MakeTrap(110, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern110() // �ٳ��� �� ����
    {
        MakeTrap(111, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern111() // �ٳ��� �� ������
    {
        MakeTrap(112, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern112() // �ٳ��� �� �ܹ�
    {
        MakeTrap(113, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern113() // �ٳ��� �� ����
    {
        MakeTrap(114, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern114() // �ٳ��� �� ������
    {
        MakeTrap(115, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern115() // �ٳ��� �� �ܹ�
    {
        MakeTrap(116, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern116() // �ٳ��� ���� ������
    {
        MakeTrap(117, new Vector3(0, 0, 0));
    }

    void pattern117() // �ٳ��� ���� �ܹ�
    {
        MakeTrap(118, new Vector3(0, 0, 0));
    }

    void pattern118() // �ٳ��� ������ �ܹ�
    {
        MakeTrap(119, new Vector3(0, 0, 0));
    }

    void pattern119() // �� �� ����
    {
        MakeTrap(120, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern120() // �� �� ������
    {
        MakeTrap(121, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern121() // �� �� �ܹ�
    {
        MakeTrap(122, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern122() // �� ���� ������
    {
        MakeTrap(123, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern123() // �� ���� �ܹ�
    {
        MakeTrap(124, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern124() // �� ������ �ܹ�
    {
        MakeTrap(125, new Vector3(0, 0, 0));
        StartCoroutine(cotime(4, "MakeBird", 0.3f));
    }

    void pattern125() // �� ���� ������
    {
        MakeTrap(126, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.5f));
    }

    void pattern126() // �� ���� �ܹ�
    {
        MakeTrap(127, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }

    void pattern127() // �� ������ �ܹ�
    {
        MakeTrap(128, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern128() // ���� ������ �ܹ�
    {
        MakeTrap(129, new Vector3(0, 0, 0));
    }

    void pattern129() // ���� �δ��� ���� �ٳ���
    {
        MakeTrap(130, new Vector3(0, 0, 0));
    }

    void pattern130() // ���� �δ��� ���� ��
    {
        MakeTrap(131, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern131() // ���� �δ��� ���� ��
    {
        MakeTrap(132, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern132() // ���� �δ��� ���� ����
    {
        MakeTrap(133, new Vector3(0, 0, 0));
    }

    void pattern133() // ���� �δ��� ���� ������
    {
        MakeTrap(134, new Vector3(0, 0, 0));
    }

    void pattern134() // ���� �δ��� ���� �ܹ�
    {
        MakeTrap(135, new Vector3(0, 0, 0));
    }

    void pattern135() // ���� �δ��� �ٳ��� ��
    {
        MakeTrap(136, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern136() // ���� �δ��� �ٳ��� ��
    {
        MakeTrap(137, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern137() // ���� �δ��� �ٳ��� ����
    {
        MakeTrap(138, new Vector3(0, 0, 0));
    }

    void pattern138() // ���� �δ��� �ٳ��� ������
    {
        MakeTrap(139, new Vector3(0, 0, 0));
    }

    void pattern139() // ���� �δ��� �ٳ��� �ܹ�
    {
        MakeTrap(140, new Vector3(0, 0, 0));
    }

    void pattern140() // ���� �δ��� �� ��
    {
        MakeTrap(141, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.1f));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern141() // ���� �δ��� �� ����
    {
        MakeTrap(142, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern142() // ���� �δ��� �� ������
    {
        MakeTrap(143, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern143() // ���� �δ��� �� �ܹ�
    {
        MakeTrap(144, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern144() // ���� �δ��� �� ����
    {
        MakeTrap(145, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern145() // ���� �δ��� �� ������
    {
        MakeTrap(146, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern146() // ���� �δ��� �� �ܹ�
    {
        MakeTrap(147, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern147() // ���� �δ��� ���� ������
    {
        MakeTrap(148, new Vector3(0, 0, 0));
    }

    void pattern148() // ���� �δ��� ���� �ܹ�
    {
        MakeTrap(149, new Vector3(0, 0, 0));
    }

    void pattern149() // ���� �δ��� ������ �ܹ�
    {
        MakeTrap(150, new Vector3(0, 0, 0));
    }

    void pattern150() // ���� ���� �ٳ��� ��
    {
        MakeTrap(151, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern151() // ���� ���� �ٳ��� ��
    {
        MakeTrap(152, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern152() // ���� ���� �ٳ��� ����
    {
        MakeTrap(153, new Vector3(0, 0, 0));
    }

    void pattern153() // ���� ���� �ٳ��� ������
    {
        MakeTrap(154, new Vector3(0, 0, 0));
    }

    void pattern154() // ���� ���� �ٳ��� �ܹ�
    {
        MakeTrap(155, new Vector3(0, 0, 0));
    }

    void pattern155() // ���� ���� �� ��
    {
        MakeTrap(156, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern156() // ���� ���� �� ����
    {
        MakeTrap(157, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern157() // ���� ���� �� ������
    {
        MakeTrap(158, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.5f));
    }

    void pattern158() // ���� ���� �� �ܹ�
    {
        MakeTrap(159, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.5f));
    }

    void pattern159() // ���� ���� �� ����
    {
        MakeTrap(160, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern160() // ���� ���� �� ������
    {
        MakeTrap(161, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern161() // ���� ���� �� �ܹ�
    {
        MakeTrap(162, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern162() // ���� ���� ���� ������
    {
        MakeTrap(163, new Vector3(0, 0, 0));
    }

    void pattern163() // ���� ���� ���� �ܹ�
    {
        MakeTrap(164, new Vector3(0, 0, 0));
    }

    void pattern164() // ���� ���� ������ �ܹ�
    {
        MakeTrap(165, new Vector3(0, 0, 0));
    }

    void pattern165() // ���� �ٳ��� �� ��
    {
        MakeTrap(166, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern166() // ���� �ٳ��� �� ����
    {
        MakeTrap(167, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern167() // ���� �ٳ��� �� ������
    {
        MakeTrap(168, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern168() // ���� �ٳ��� �� �ܹ�
    {
        MakeTrap(169, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern169() // ���� �ٳ��� �� ����
    {
        MakeTrap(170, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern170() // ���� �ٳ��� �� ������
    {
        MakeTrap(171, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern171() // ���� �ٳ��� �� �ܹ�
    {
        MakeTrap(172, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern172() // ���� �ٳ��� ���� ������
    {
        MakeTrap(173, new Vector3(0, 0, 0));
    }

    void pattern173() // ���� �ٳ��� ���� �ܹ�
    {
        MakeTrap(174, new Vector3(0, 0, 0));
    }

    void pattern174() // ���� �ٳ��� ������ �ܹ�
    {
        MakeTrap(175, new Vector3(0, 0, 0));
    }

    void pattern175() // ���� �� �� ����
    {
        MakeTrap(176, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern176() // ���� �� �� ������
    {
        MakeTrap(177, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern177() // ���� �� �� �ܹ�
    {
        MakeTrap(178, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern178() // ���� �� ���� ������
    {
        MakeTrap(179, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern179() // ���� �� ���� �ܹ�
    {
        MakeTrap(180, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern180() // ���� �� ������ �ܹ�
    {
        MakeTrap(181, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern181() // ���� �� ���� ������
    {
        MakeTrap(182, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern182() // ���� �� ���� �ܹ�
    {
        MakeTrap(183, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern183() // ���� �� ������ �ܹ�
    {
        MakeTrap(184, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern184() // ���� ���� ������ �ܹ�
    {
        MakeTrap(185, new Vector3(0, 0, 0));
    }

    void pattern185() // �δ��� ���� �ٳ��� ��
    {
        MakeTrap(186, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern186() // �δ��� ���� �ٳ��� ��
    {
        MakeTrap(187, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern187() // �δ��� ���� �ٳ��� ����
    {
        MakeTrap(188, new Vector3(0, 0, 0));
    }

    void pattern188() // �δ��� ���� �ٳ��� ������
    {
        MakeTrap(189, new Vector3(0, 0, 0));
    }

    void pattern189() // �δ��� ���� �ٳ��� �ܹ�
    {
        MakeTrap(190, new Vector3(0, 0, 0));
    }

    void pattern190() // �δ��� ���� �� ��
    {
        MakeTrap(191, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern191() // �δ��� ���� �� ����
    {
        MakeTrap(192, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern192() // �δ��� ���� �� ������
    {
        MakeTrap(193, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern193() // �δ��� ���� �� �ܹ�
    {
        MakeTrap(194, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern194() // �δ��� ���� �� ����
    {
        MakeTrap(195, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern195() // �δ��� ���� �� ������
    {
        MakeTrap(196, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern196() // �δ��� ���� �� �ܹ�
    {
        MakeTrap(197, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern197() // �δ��� ���� ���� ������
    {
        MakeTrap(198, new Vector3(0, 0, 0));
    }

    void pattern198() // �δ��� ���� ���� �ܹ�
    {
        MakeTrap(199, new Vector3(0, 0, 0));
    }

    void pattern199() // �δ��� ���� ������ �ܹ�
    {
        MakeTrap(200, new Vector3(0, 0, 0));
    }

    void pattern200() // �δ��� �ٳ��� �� ��
    {
        MakeTrap(201, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern201() // �δ��� �ٳ��� �� ����
    {
        MakeTrap(202, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern202() // �δ��� �ٳ��� �� ������
    {
        MakeTrap(203, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern203() // �δ��� �ٳ��� �� �ܹ�
    {
        MakeTrap(204, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern204() // �δ��� �ٳ��� �� ����
    {
        MakeTrap(205, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern205() // �δ��� �ٳ��� �� ������
    {
        MakeTrap(206, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern206() // �δ��� �ٳ��� �� �ܹ�
    {
        MakeTrap(207, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern207() // �δ��� �ٳ��� ���� ������
    {
        MakeTrap(208, new Vector3(0, 0, 0));
    }

    void pattern208() // �δ��� �ٳ��� ���� �ܹ�
    {
        MakeTrap(209, new Vector3(0, 0, 0));
    }

    void pattern209() // �δ��� �ٳ��� ������ �ܹ�
    {
        MakeTrap(210, new Vector3(0, 0, 0));
    }

    void pattern210() // �δ��� �� �� ����
    {
        MakeTrap(211, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern211() // �δ��� �� �� ������
    {
        MakeTrap(212, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern212() // �δ��� �� �� �ܹ�
    {
        MakeTrap(213, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern213() // �δ��� �� ���� ������
    {
        MakeTrap(214, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern214() // �δ��� �� ���� �ܹ�
    {
        MakeTrap(215, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern215() // �δ��� �� ������ �ܹ�
    {
        MakeTrap(216, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern216() // �δ��� �� ���� ������
    {
        MakeTrap(217, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern217() // �δ��� �� ���� �ܹ�
    {
        MakeTrap(218, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern218() // �δ��� �� ������ �ܹ�
    {
        MakeTrap(219, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern219() // �δ��� ���� ������ �ܹ�
    {
        MakeTrap(220, new Vector3(0, 0, 0));
    }

    void pattern220() // ���� �ٳ��� �� ��
    {
        MakeTrap(221, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern221() // ���� �ٳ��� �� ����
    {
        MakeTrap(222, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern222() // ���� �ٳ��� �� ������
    {
        MakeTrap(223, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern223() // ���� �ٳ��� �� �ܹ�
    {
        MakeTrap(224, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern224() // ���� �ٳ��� �� ����
    {
        MakeTrap(225, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern225() // ���� �ٳ��� �� ������
    {
        MakeTrap(226, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern226() // ���� �ٳ��� �� �ܹ�
    {
        MakeTrap(227, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern227() // ���� �ٳ��� ���� ������
    {
        MakeTrap(228, new Vector3(0, 0, 0));
    }

    void pattern228() // ���� �ٳ��� ���� �ܹ�
    {
        MakeTrap(229, new Vector3(0, 0, 0));
    }

    void pattern229() // ���� �ٳ��� ������ �ܹ�
    {
        MakeTrap(230, new Vector3(0, 0, 0));
    }

    void pattern230() // ���� �� �� ����
    {
        MakeTrap(231, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern231() // ���� �� �� ������
    {
        MakeTrap(232, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern232() // ���� �� �� �ܹ�
    {
        MakeTrap(233, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern233() // ���� �� ���� ������
    {
        MakeTrap(234, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern234() // ���� �� ���� ����
    {
        MakeTrap(235, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern235() // ���� �� ������ �ܹ�
    {
        MakeTrap(236, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern236() // ���� �� ���� ������
    {
        MakeTrap(237, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern237() // ���� �� ���� �ܹ�
    {
        MakeTrap(238, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern238() // ���� �� ������ �ܹ�
    {
        MakeTrap(239, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern239() // ���� ���� ������ �ܹ�
    {
        MakeTrap(240, new Vector3(0, 0, 0));
    }

    void pattern240() // �ٳ��� �� �� ����
    {
        MakeTrap(241, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern241() // �ٳ��� �� �� ������
    {
        MakeTrap(242, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern242() // �ٳ��� �� �� �ܹ�
    {
        MakeTrap(243, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern243() // �ٳ��� �� ���� ������
    {
        MakeTrap(244, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern244() // �ٳ��� �� ���� �ܹ�
    {
        MakeTrap(245, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern245() // �ٳ��� �� ������ �ܹ�
    {
        MakeTrap(246, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern246() // �ٳ��� �� ���� ������
    {
        MakeTrap(247, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern247() // �ٳ��� �� ���� �ܹ�
    {
        MakeTrap(248, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern248() // �ٳ��� �� ������ �ܹ�
    {
        MakeTrap(249, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern249() // �ٳ��� ���� ������ �ܹ�
    {
        MakeTrap(250, new Vector3(0, 0, 0));
    }

    void pattern250() // �� �� ���� ������
    {
        MakeTrap(251, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(2, "MakeBird", 1.0f));
    }

    void pattern251() // �� �� ���� �ܹ�
    {
        MakeTrap(252, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern252() // �� �� ������ �ܹ�
    {
        MakeTrap(253, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern253() // �� ���� ������ �ܹ�
    {
        MakeTrap(254, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern254() // �� ���� ������ �ܻ�
    {
        MakeTrap(255, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }








}