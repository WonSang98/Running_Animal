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
        Bridge_Stool, // 숲 특수 - 발판
        Bridge_Trap // 숲 특수 - 장애물
    }

    public int idx; // 테스트하려고 public
    GameObject[] traps; // 함정 리소스 저장
    GameObject warning_bird;
    GameObject warning_shot;
    GameObject LvUp;
    GameObject player;
    GameObject coin;
    GameObject hp;

    //BackGround 포함 함정 자동 이동위함

    private void Start()
    {
        //idx = 20; 테스트하려고 주석처리함.
        player = GameObject.Find("Player");
        // 시작시 함정 리소스 불러오기.
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
            if (i == 0) // 경고 생성.
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
    void LevelUP() // 레벨업 장소.
    {
        GameObject tmp;
        tmp = Instantiate(LvUp);
        tmp.transform.position = new Vector3(35, -1.8f, 0);
    }
    void pattern0() // 곰덫 생성
    {
        //trap 생성
        MakeTrap(0, new Vector3(36, -3.36f, 0));
        MakeTrap(0, new Vector3(29, -3.36f, 0));
        MakeTrap(0, new Vector3(22, -3.36f, 0));
        // coint 생성
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

    void pattern1() // 두더지 생성
    {
        MakeTrap(1, new Vector3(36, -3.35f, 0));
        MakeTrap(1, new Vector3(28, -3.35f, 0));
        MakeTrap(1, new Vector3(20, -3.35f, 0));
    }

    void pattern2() // 나무 생성
    {
        MakeTrap(2, new Vector3(36, -2.97f, 0));
        MakeTrap(2, new Vector3(28, -2.97f, 0));
        MakeTrap(2, new Vector3(20, -2.97f, 0));
    }

    void pattern3() // 바나나 생성
    {
        MakeTrap(3, new Vector3(36, -3.16f, 0));
        MakeTrap(3, new Vector3(28, -3.16f, 0));
        MakeTrap(3, new Vector3(20, -3.16f, 0));
    }

    void pattern4() // 버드스트라이크 생성
    {
        StartCoroutine(cotime(3, "MakeBird", 0.2f));
    }

    void pattern5() // 총 쏘기 생성
    {
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }

    void pattern6() // 몬스터 생성
    {
        MakeTrap(6, new Vector3(36, -2.85f, 0));
        MakeTrap(6, new Vector3(28, -2.85f, 0));
        MakeTrap(6, new Vector3(20, -2.85f, 0));
    }

    void pattern7() // 돌땡이 생성
    {
        MakeTrap(7, new Vector3(36, 5.7f, 0));
        MakeTrap(7, new Vector3(21, 5.7f, 0));
    }

    void pattern8() // 꿀벌 생성
    {
        MakeTrap(8, new Vector3(36, 1.07f, 0));
        MakeTrap(8, new Vector3(30, 1.07f, 0));
        MakeTrap(8, new Vector3(24, 1.07f, 0));
        MakeTrap(8, new Vector3(33, 3.27f, 0));
        MakeTrap(8, new Vector3(27, 3.27f, 0));

    }

    void pattern9() // 스페셜 생성
    {
        StartCoroutine("MakeSpecial");
    }

    void pattern10() // 곰덫 + 더지두
    {
        MakeTrap(0, new Vector3(15.2f, -3.342f, 0));
        MakeTrap(0, new Vector3(27.1f, -3.342f, 0));
        MakeTrap(1, new Vector3(21.3f, -3.35f, 0));
        MakeTrap(1, new Vector3(32.8f, -3.35f, 0));
        MakeTrap(1, new Vector3(35.6f, -3.35f, 0));
    }

    void pattern11() // 곰덫 + 나무
    {
        MakeTrap(0, new Vector3(15.2f, -3.342f, 0));
        MakeTrap(0, new Vector3(18.8f, -3.342f, 0));
        MakeTrap(0, new Vector3(26.7f, -3.342f, 0));
        MakeTrap(2, new Vector3(17, -2.97f, 0));
        MakeTrap(2, new Vector3(24.6f, -2.97f, 0));
        MakeTrap(2, new Vector3(33, -2.97f, 0));
    }

    void pattern12() // 곰덫 + 바나나
    {
        MakeTrap(0, new Vector3(21, -3.342f, 0));
        MakeTrap(0, new Vector3(32, -3.342f, 0));
        MakeTrap(3, new Vector3(15, -3.16f, 0));
        MakeTrap(3, new Vector3(26, -3.16f, 0));
        MakeTrap(3, new Vector3(37, -3.16f, 0));
    }

    void pattern13() // 곰덫 + 새
    {
        MakeTrap(0, new Vector3(36, -3.342f, 0));
        MakeTrap(0, new Vector3(29, -3.342f, 0));
        MakeTrap(0, new Vector3(22, -3.342f, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern14() // 곰덫 + 총
    {
        MakeTrap(0, new Vector3(36, -3.342f, 0));
        MakeTrap(0, new Vector3(29, -3.342f, 0));
        MakeTrap(0, new Vector3(22, -3.342f, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern15() // 곰덫 + 몬스터
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

    void pattern16() // 곰덫 + 돌
    {
        MakeTrap(0, new Vector3(13.8f, -3.342f, 0));
        MakeTrap(0, new Vector3(18.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(20f, -3.342f, 0));
        MakeTrap(0, new Vector3(29f, -3.342f, 0));
        MakeTrap(0, new Vector3(30.5f, -3.342f, 0));
        MakeTrap(7, new Vector3(23, 5.7f, 0));
    }

    void pattern17() // 곰덫 + 꿀벌
    {
        MakeTrap(0, new Vector3(19.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(21.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(28.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(30.5f, -3.342f, 0));
        MakeTrap(8, new Vector3(16, 0, 0));
        MakeTrap(8, new Vector3(25, 0, 0));
        MakeTrap(8, new Vector3(34, 0, 0));
    }

    void pattern18() // 두더지 + 나무
    {
        MakeTrap(1, new Vector3(15f, -3.334f, 0));
        MakeTrap(1, new Vector3(27f, -3.334f, 0));
        MakeTrap(2, new Vector3(21f, -2.97f, 0));
        MakeTrap(2, new Vector3(33f, -2.97f, 0));
    }

    void pattern19() // 두더지 + 바나나
    {
        MakeTrap(1, new Vector3(15f, -3.334f, 0));
        MakeTrap(1, new Vector3(25f, -3.334f, 0));
        MakeTrap(3, new Vector3(13f, -3.16f, 0));
        MakeTrap(3, new Vector3(17f, -3.16f, 0));
        MakeTrap(3, new Vector3(23f, -3.16f, 0));
        MakeTrap(3, new Vector3(27f, -3.16f, 0));
    }

    void pattern20() // 두더지 + 새
    {
        MakeTrap(21, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern21() // 두더지 + 총
    {
        MakeTrap(22, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern22() // 두더지 + 몬스터
    {
        MakeTrap(23, new Vector3(0, 0, 0));
    }

    void pattern23() // 두더지 + 돌땡이
    {
        MakeTrap(24, new Vector3(0, 0, 0));
    }

    void pattern24() // 두더지 + 꿀벌
    {
        MakeTrap(25, new Vector3(0, 0, 0));
    }

    void pattern25() // 나무 + 바나나
    {
        MakeTrap(26, new Vector3(0, 0, 0));
    }

    void pattern26() // 나무 + 새
    {
        MakeTrap(27, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern27() // 나무 + 총
    {
        MakeTrap(28, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern28() // 나무 + 몬스터
    {
        MakeTrap(29, new Vector3(0, 0, 0));
    }

    void pattern29() // 나무 + 돌땡이
    {
        MakeTrap(30, new Vector3(0, 0, 0));
    }

    void pattern30() // 나무 + 꿀벌
    {
        MakeTrap(31, new Vector3(0, 0, 0));
    }

    void pattern31() // 바나나 + 새
    {
        MakeTrap(32, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern32() // 바나나 + 총
    {
        MakeTrap(33, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern33() // 바나나 + 몬스터
    {
        MakeTrap(34, new Vector3(0, 0, 0));
    }

    void pattern34() // 바나나 + 돌땡이
    {
        MakeTrap(35, new Vector3(0, 0, 0));
    }

    void pattern35() // 바나나 + 꿀벌
    {
        MakeTrap(36, new Vector3(0, 0, 0));
    }

    void pattern36() // 새 + 총
    {
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern37() // 새 + 몬스터
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(38, new Vector3(0, 0, 0));
    }

    void pattern38() // 새 + 돌땡이
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(39, new Vector3(0, 0, 0));
    }

    void pattern39() // 새 + 꿀벌
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(40, new Vector3(0, 0, 0));
    }

    void pattern40() // 총 + 몬스터
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(41, new Vector3(0, 0, 0));
    }

    void pattern41() // 총 + 돌땡이
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(42, new Vector3(0, 0, 0));
    }

    void pattern42() // 총 + 꿀벌
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(43, new Vector3(0, 0, 0));
    }

    void pattern43() // 몬스터+돌땡이
    {
        MakeTrap(44, new Vector3(0, 0, 0));
    }

    void pattern44() // 몬스터+꿀벌
    {
        MakeTrap(45, new Vector3(0, 0, 0));
    }

    void pattern45() // 돌땡이+꿀벌
    {
        MakeTrap(46, new Vector3(0, 0, 0));
    }

    void pattern46() // 곰덫 두더지 나무
    {
        MakeTrap(47, new Vector3(0, 0, 0));
    }

    void pattern47() // 곰덫 두더지 바나나
    {
        MakeTrap(48, new Vector3(0, 0, 0));
    }

    void pattern48() // 곰덫 두더지 새
    {
        MakeTrap(49, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern49() // 곰덫 두더지 총
    {
        MakeTrap(50, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern50() // 곰덫 두더지 몬스터
    {
        MakeTrap(51, new Vector3(0, 0, 0));
    }

    void pattern51() // 곰덫 두더지 돌땡이
    {
        MakeTrap(52, new Vector3(0, 0, 0));
    }

    void pattern52() // 곰덫 두더지 벌
    {
        MakeTrap(53, new Vector3(0, 0, 0));
    }

    void pattern53() // 곰덫 나무 바나나
    {
        MakeTrap(54, new Vector3(0, 0, 0));
    }

    void pattern54() // 곰덫 나무 새
    {
        MakeTrap(55, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern55() // 곰덫 나무 총
    {
        MakeTrap(56, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern56() // 곰덫 나무 몬스터
    {
        MakeTrap(57, new Vector3(0, 0, 0));
    }

    void pattern57() // 곰덫 나무 돌땡이
    {
        MakeTrap(58, new Vector3(0, 0, 0));
    }

    void pattern58() // 곰덫 나무 벌
    {
        MakeTrap(59, new Vector3(0, 0, 0));
    }
    void pattern59() // 곰덫 바나나 새
    {
        MakeTrap(60, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern60() // 곰덫 바나나 총
    {
        MakeTrap(61, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern61() // 곰덫 바나나 몬스터
    {
        MakeTrap(62, new Vector3(0, 0, 0));
    }

    void pattern62() // 곰덫 바나나 돌땡이
    {
        MakeTrap(63, new Vector3(0, 0, 0));
    }

    void pattern63() // 곰덫 바나나 꿀벌
    {
        MakeTrap(64, new Vector3(0, 0, 0));
    }

    void pattern64() // 곰덫 새 총
    {
        MakeTrap(65, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern65() // 곰덫 새 몬스터 
    {
        MakeTrap(66, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern66() // 곰덫 새 돌땡이
    {
        MakeTrap(67, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern67() // 곰덫 새 꿀벌
    {
        MakeTrap(68, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern68() // 곰덫 총 몬스터
    {
        MakeTrap(69, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }
    void pattern69() // 곰덫 총 돌땡이
    {
        MakeTrap(70, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern70() // 곰덫 총 꿀벌
    {
        MakeTrap(71, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern71() // 곰덫 몬스터 돌댕이
    {
        MakeTrap(72, new Vector3(0, 0, 0));
    }

    void pattern72() // 곰덫 몬스터 꿀벌
    {
        MakeTrap(73, new Vector3(0, 0, 0));
    }

    void pattern73() // 곰덫 돌땡이 꿀벌
    {
        MakeTrap(74, new Vector3(0, 0, 0));
    }

    void pattern74() // 두더지 나무 바나나
    {
        MakeTrap(75, new Vector3(0, 0, 0));
    }

    void pattern75() // 두더지 나무 새
    {
        MakeTrap(76, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.2f));
    }

    void pattern76() // 두더지 나무 총
    {
        MakeTrap(77, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern77() // 두더지 나무 몬스터
    {
        MakeTrap(78, new Vector3(0, 0, 0));
    }

    void pattern78() // 두더지 나무 돌땡이
    {
        MakeTrap(79, new Vector3(0, 0, 0));
    }

    void pattern79() // 두더지 나무 꿀벌
    {
        MakeTrap(80, new Vector3(0, 0, 0));
    }

    void pattern80() // 두더지 바나나 새
    {
        MakeTrap(81, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern81() // 두더지 바나나 총
    {
        MakeTrap(82, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern82() // 두더지 바나나 몬스터
    {
        MakeTrap(83, new Vector3(0, 0, 0));
    }

    void pattern83() // 두더지 바나나 돌땡이
    {
        MakeTrap(84, new Vector3(0, 0, 0));
    }

    void pattern84() // 두더지 바나나 벌
    {
        MakeTrap(85, new Vector3(0, 0, 0));
    }

    void pattern85() // 두더지 새 총
    {
        MakeTrap(86, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        StartCoroutine(cotime(3, "MakeShot", 0.3f));
    }

    void pattern86() // 두더지 새 몬스터
    {
        MakeTrap(87, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern87() // 두더지 새 돌땡이
    {
        MakeTrap(88, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern88() // 두더지 새 꿀벌
    {
        MakeTrap(89, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern89() // 두더지 총 몬스터
    {
        MakeTrap(90, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern90() // 두더지 총 돌땡이
    {
        MakeTrap(91, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }


}