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

    GameObject[] traps; // 함정 리소스 저장
    GameObject warning_bird;
    GameObject warning_shot;
    GameObject warning_shot2;
    GameObject LvUp;
    GameObject player;
    GameObject coin;
    GameObject hp;

    int[] map_pattern = { 255 }; // 추후 배열화


    //BackGround 포함 함정 자동 이동위함

    private void Start()
    {
        player = GameObject.Find("Player");
        // 시작시 함정 리소스 불러오기.
        traps = Resources.LoadAll<GameObject>("Trap/Forest");
        warning_bird = Resources.Load<GameObject>("Trap/Warning_Bird");
        warning_shot = Resources.Load<GameObject>("Trap/Warning_Shot");
        warning_shot2 = Resources.Load<GameObject>("Trap/Warning_Shot2");
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
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator MakeShot()
    {
        GameObject shot;
        GameObject warn = null; // 실시간 좌표 수정
        float shot_y = 0;
        for (int i = 0; i < 450; i++)
        {
            float y = player.transform.position.y;
            if (i < 300) // 경고 생성.
            {
                Destroy(warn);
                warn = Instantiate(warning_shot);
                warn.transform.position = new Vector3(warn.transform.position.x, y, warn.transform.position.z);
                shot_y = y;
            }
            else if (i < 360)
            {
                Destroy(warn);
                warn = Instantiate(warning_shot2);
                warn.transform.position = new Vector3(warn.transform.position.x, shot_y, warn.transform.position.z);
            }
            else if( i == 375)
            {
                Destroy(warn);
            }
            else if (i == 435)
            {
                shot = Instantiate(traps[(int)Forest_Trap.Fly_Shot]);
                //shot.transform.parent = transform;
                shot.transform.position = new Vector3(37, shot_y, 0);
            }
            yield return new WaitForSeconds(0.01f);
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

    void pattern91() // 두더지 총 꿀벌
    {
        MakeTrap(92, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern92() // 두더지 몬스터 돌땡이
    {
        MakeTrap(93, new Vector3(0, 0, 0));
    }

    void pattern93() // 두더지 몬스터 꿀벌
    {
        MakeTrap(94, new Vector3(0, 0, 0));
    }

    void pattern94() // 나무 바나나 새
    {
        MakeTrap(95, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern95() // 나무 바나나 총
    {
        MakeTrap(96, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern96() // 나무 바나나 몬스터
    {
        MakeTrap(97, new Vector3(0, 0, 0));
    }

    void pattern97() // 나무 바나나 돌땡이
    {
        MakeTrap(98, new Vector3(0, 0, 0));
    }

    void pattern98() // 나무 바나나 꿀벌
    {
        MakeTrap(99, new Vector3(0, 0, 0));
    }

    void pattern99() // 나무 새 총
    {
        MakeTrap(100, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern100() // 나무 새 몬스터
    {
        MakeTrap(101, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern101() // 나무 새 돌땡이
    {
        MakeTrap(102, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern102() // 나무 새 꿀벌
    {
        MakeTrap(103, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern103() // 나무 총 몬스터
    {
        MakeTrap(104, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern104() // 나무 총 돌땡이
    {
        MakeTrap(105, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern105() // 나무 총 꿀벌
    {
        MakeTrap(106, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern106() // 나무 몬스터 돌땡이
    {
        MakeTrap(107, new Vector3(0, 0, 0));
    }

    void pattern107() // 나무 몬스터 꿀벌
    {
        MakeTrap(108, new Vector3(0, 0, 0));
    }

    void pattern108() // 나무 돌땡이 꿀벌
    {
        MakeTrap(109, new Vector3(0, 0, 0));
    }

    void pattern109() // 바나나 새 총
    {
        MakeTrap(110, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern110() // 바나나 새 몬스터
    {
        MakeTrap(111, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern111() // 바나나 새 돌댕이
    {
        MakeTrap(112, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern112() // 바나나 새 꿀벌
    {
        MakeTrap(113, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern113() // 바나나 총 몬스터
    {
        MakeTrap(114, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern114() // 바나나 총 돌땡이
    {
        MakeTrap(115, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern115() // 바나나 총 꿀벌
    {
        MakeTrap(116, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern116() // 바나나 몬스터 돌땡이
    {
        MakeTrap(117, new Vector3(0, 0, 0));
    }

    void pattern117() // 바나나 몬스터 꿀벌
    {
        MakeTrap(118, new Vector3(0, 0, 0));
    }

    void pattern118() // 바나나 돌땡이 꿀벌
    {
        MakeTrap(119, new Vector3(0, 0, 0));
    }

    void pattern119() // 새 총 몬스터
    {
        MakeTrap(120, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern120() // 새 총 돌땡이
    {
        MakeTrap(121, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern121() // 새 총 꿀벌
    {
        MakeTrap(122, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern122() // 새 몬스터 돌땡이
    {
        MakeTrap(123, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern123() // 새 몬스터 꿀벌
    {
        MakeTrap(124, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern124() // 새 돌땡이 꿀벌
    {
        MakeTrap(125, new Vector3(0, 0, 0));
        StartCoroutine(cotime(4, "MakeBird", 0.3f));
    }

    void pattern125() // 총 몬스터 돌땡이
    {
        MakeTrap(126, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.5f));
    }

    void pattern126() // 총 몬스터 꿀벌
    {
        MakeTrap(127, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }

    void pattern127() // 총 돌땡이 꿀벌
    {
        MakeTrap(128, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern128() // 몬스터 돌땡이 꿀벌
    {
        MakeTrap(129, new Vector3(0, 0, 0));
    }

    void pattern129() // 곰덫 두더지 나무 바나나
    {
        MakeTrap(130, new Vector3(0, 0, 0));
    }

    void pattern130() // 곰덫 두더지 나무 새
    {
        MakeTrap(131, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern131() // 곰덫 두더지 나무 총
    {
        MakeTrap(132, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern132() // 곰덫 두더지 나무 몬스터
    {
        MakeTrap(133, new Vector3(0, 0, 0));
    }

    void pattern133() // 곰덫 두더지 나무 돌땡이
    {
        MakeTrap(134, new Vector3(0, 0, 0));
    }

    void pattern134() // 곰덫 두더지 나무 꿀벌
    {
        MakeTrap(135, new Vector3(0, 0, 0));
    }

    void pattern135() // 곰덫 두더지 바나나 새
    {
        MakeTrap(136, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern136() // 곰덫 두더지 바나나 총
    {
        MakeTrap(137, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern137() // 곰덫 두더지 바나나 몬스터
    {
        MakeTrap(138, new Vector3(0, 0, 0));
    }

    void pattern138() // 곰덫 두더지 바나나 돌땡이
    {
        MakeTrap(139, new Vector3(0, 0, 0));
    }

    void pattern139() // 곰덫 두더지 바나나 꿀벌
    {
        MakeTrap(140, new Vector3(0, 0, 0));
    }

    void pattern140() // 곰덫 두더지 새 총
    {
        MakeTrap(141, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.1f));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern141() // 곰덫 두더지 새 몬스터
    {
        MakeTrap(142, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern142() // 곰덫 두더지 새 돌땡이
    {
        MakeTrap(143, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern143() // 곰덫 두더지 새 꿀벌
    {
        MakeTrap(144, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern144() // 곰덫 두더지 총 몬스터
    {
        MakeTrap(145, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern145() // 곰덫 두더지 총 돌땡이
    {
        MakeTrap(146, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern146() // 곰덫 두더지 총 꿀벌
    {
        MakeTrap(147, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern147() // 곰덫 두더지 몬스터 돌땡이
    {
        MakeTrap(148, new Vector3(0, 0, 0));
    }

    void pattern148() // 곰덫 두더지 몬스터 꿀벌
    {
        MakeTrap(149, new Vector3(0, 0, 0));
    }

    void pattern149() // 곰덫 두더지 돌땡이 꿀벌
    {
        MakeTrap(150, new Vector3(0, 0, 0));
    }

    void pattern150() // 곰덫 나무 바나나 새
    {
        MakeTrap(151, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern151() // 곰덫 나무 바나나 총
    {
        MakeTrap(152, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern152() // 곰덫 나무 바나나 몬스터
    {
        MakeTrap(153, new Vector3(0, 0, 0));
    }

    void pattern153() // 곰덫 나무 바나나 돌땡이
    {
        MakeTrap(154, new Vector3(0, 0, 0));
    }

    void pattern154() // 곰덫 나무 바나나 꿀벌
    {
        MakeTrap(155, new Vector3(0, 0, 0));
    }

    void pattern155() // 곰덫 나무 새 총
    {
        MakeTrap(156, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern156() // 곰덫 나무 새 몬스터
    {
        MakeTrap(157, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern157() // 곰덫 나무 새 돌땡이
    {
        MakeTrap(158, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.5f));
    }

    void pattern158() // 곰덫 나무 새 꿀벌
    {
        MakeTrap(159, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.5f));
    }

    void pattern159() // 곰덫 나무 총 몬스터
    {
        MakeTrap(160, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern160() // 곰덫 나무 총 돌땡이
    {
        MakeTrap(161, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern161() // 곰덫 나무 총 꿀벌
    {
        MakeTrap(162, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern162() // 곰덫 나무 몬스터 돌땡이
    {
        MakeTrap(163, new Vector3(0, 0, 0));
    }

    void pattern163() // 곰덫 나무 몬스터 꿀벌
    {
        MakeTrap(164, new Vector3(0, 0, 0));
    }

    void pattern164() // 곰덫 나무 돌땡이 꿀벌
    {
        MakeTrap(165, new Vector3(0, 0, 0));
    }

    void pattern165() // 곰덫 바나나 새 총
    {
        MakeTrap(166, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern166() // 곰덫 바나나 새 몬스터
    {
        MakeTrap(167, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern167() // 곰덫 바나나 새 돌땡이
    {
        MakeTrap(168, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern168() // 곰덫 바나나 새 꿀벌
    {
        MakeTrap(169, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern169() // 곰덫 바나나 총 몬스터
    {
        MakeTrap(170, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern170() // 곰덫 바나나 총 돌땡이
    {
        MakeTrap(171, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern171() // 곰덫 바나나 총 꿀벌
    {
        MakeTrap(172, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern172() // 곰덫 바나나 몬스터 돌땡이
    {
        MakeTrap(173, new Vector3(0, 0, 0));
    }

    void pattern173() // 곰덫 바나나 몬스터 꿀벌
    {
        MakeTrap(174, new Vector3(0, 0, 0));
    }

    void pattern174() // 곰덫 바나나 돌땡이 꿀벌
    {
        MakeTrap(175, new Vector3(0, 0, 0));
    }

    void pattern175() // 곰덫 새 총 몬스터
    {
        MakeTrap(176, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern176() // 곰덫 새 총 돌땡이
    {
        MakeTrap(177, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern177() // 곰덫 새 총 꿀벌
    {
        MakeTrap(178, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern178() // 곰덫 새 몬스터 돌땡이
    {
        MakeTrap(179, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern179() // 곰덫 새 몬스터 꿀벌
    {
        MakeTrap(180, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern180() // 곰덫 새 돌땡이 꿀벌
    {
        MakeTrap(181, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern181() // 곰덫 총 몬스터 돌땡이
    {
        MakeTrap(182, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern182() // 곰덫 총 몬스터 꿀벌
    {
        MakeTrap(183, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern183() // 곰덫 총 돌땡이 꿀벌
    {
        MakeTrap(184, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern184() // 곰덫 몬스터 돌땡이 꿀벌
    {
        MakeTrap(185, new Vector3(0, 0, 0));
    }

    void pattern185() // 두더지 나무 바나나 새
    {
        MakeTrap(186, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern186() // 두더지 나무 바나나 총
    {
        MakeTrap(187, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern187() // 두더지 나무 바나나 몬스터
    {
        MakeTrap(188, new Vector3(0, 0, 0));
    }

    void pattern188() // 두더지 나무 바나나 돌땡이
    {
        MakeTrap(189, new Vector3(0, 0, 0));
    }

    void pattern189() // 두더지 나무 바나나 꿀벌
    {
        MakeTrap(190, new Vector3(0, 0, 0));
    }

    void pattern190() // 두더지 나무 새 총
    {
        MakeTrap(191, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern191() // 두더지 나무 새 몬스터
    {
        MakeTrap(192, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern192() // 두더지 나무 새 돌땡이
    {
        MakeTrap(193, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern193() // 두더지 나무 새 꿀벌
    {
        MakeTrap(194, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern194() // 두더지 나무 총 몬스터
    {
        MakeTrap(195, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern195() // 두더지 나무 총 돌땡이
    {
        MakeTrap(196, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern196() // 두더지 나무 총 꿀벌
    {
        MakeTrap(197, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern197() // 두더지 나무 몬스터 돌땡이
    {
        MakeTrap(198, new Vector3(0, 0, 0));
    }

    void pattern198() // 두더지 나무 몬스터 꿀벌
    {
        MakeTrap(199, new Vector3(0, 0, 0));
    }

    void pattern199() // 두더지 나무 돌땡이 꿀벌
    {
        MakeTrap(200, new Vector3(0, 0, 0));
    }

    void pattern200() // 두더지 바나나 새 총
    {
        MakeTrap(201, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern201() // 두더지 바나나 새 몬스터
    {
        MakeTrap(202, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern202() // 두더지 바나나 새 돌땡이
    {
        MakeTrap(203, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern203() // 두더지 바나나 새 꿀벌
    {
        MakeTrap(204, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern204() // 두더지 바나나 총 몬스터
    {
        MakeTrap(205, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern205() // 두더지 바나나 총 돌댕이
    {
        MakeTrap(206, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern206() // 두더지 바나나 총 꿀벌
    {
        MakeTrap(207, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern207() // 두더지 바나나 몬스터 돌땡이
    {
        MakeTrap(208, new Vector3(0, 0, 0));
    }

    void pattern208() // 두더지 바나나 몬스터 꿀벌
    {
        MakeTrap(209, new Vector3(0, 0, 0));
    }

    void pattern209() // 두더지 바나나 돌땡이 꿀벌
    {
        MakeTrap(210, new Vector3(0, 0, 0));
    }

    void pattern210() // 두더지 새 총 몬스터
    {
        MakeTrap(211, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern211() // 두더지 새 총 돌땡이
    {
        MakeTrap(212, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern212() // 두더지 새 총 꿀벌
    {
        MakeTrap(213, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern213() // 두더지 새 몬스터 돌땡이
    {
        MakeTrap(214, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern214() // 두더지 새 몬스터 꿀벌
    {
        MakeTrap(215, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern215() // 두더지 새 돌땡이 꿀벌
    {
        MakeTrap(216, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern216() // 두더지 총 몬스터 돌땡이
    {
        MakeTrap(217, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern217() // 두더지 총 몬스터 꿀벌
    {
        MakeTrap(218, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern218() // 두더지 총 돌땡이 꿀벌
    {
        MakeTrap(219, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern219() // 두더지 몬스터 돌땡이 꿀벌
    {
        MakeTrap(220, new Vector3(0, 0, 0));
    }

    void pattern220() // 나무 바나나 새 총
    {
        MakeTrap(221, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern221() // 나무 바나나 새 몬스터
    {
        MakeTrap(222, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern222() // 나무 바나나 새 돌댕이
    {
        MakeTrap(223, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern223() // 나무 바나나 새 꿀벌
    {
        MakeTrap(224, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern224() // 나무 바나나 총 몬스터
    {
        MakeTrap(225, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern225() // 나무 바나나 총 돌땡이
    {
        MakeTrap(226, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern226() // 나무 바나나 총 꿀벌
    {
        MakeTrap(227, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern227() // 나무 바나나 몬스터 돌땡이
    {
        MakeTrap(228, new Vector3(0, 0, 0));
    }

    void pattern228() // 나무 바나나 몬스터 꿀벌
    {
        MakeTrap(229, new Vector3(0, 0, 0));
    }

    void pattern229() // 나무 바나나 돌땡이 꿀벌
    {
        MakeTrap(230, new Vector3(0, 0, 0));
    }

    void pattern230() // 나무 새 총 몬스터
    {
        MakeTrap(231, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern231() // 나무 새 총 돌땡이
    {
        MakeTrap(232, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern232() // 나무 새 총 꿀벌
    {
        MakeTrap(233, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern233() // 나무 새 몬스터 돌땡이
    {
        MakeTrap(234, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern234() // 나무 새 몬스터 굴벌
    {
        MakeTrap(235, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern235() // 나무 새 돌땡이 꿀벌
    {
        MakeTrap(236, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern236() // 나무 총 몬스터 돌땡이
    {
        MakeTrap(237, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern237() // 나무 총 몬스터 꿀벌
    {
        MakeTrap(238, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern238() // 나무 총 돌땡이 꿀벌
    {
        MakeTrap(239, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern239() // 나무 몬스터 돌땡이 꿀벌
    {
        MakeTrap(240, new Vector3(0, 0, 0));
    }

    void pattern240() // 바나나 새 총 몬스터
    {
        MakeTrap(241, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern241() // 바나나 새 총 돌땡이
    {
        MakeTrap(242, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern242() // 바나나 새 총 꿀벌
    {
        MakeTrap(243, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern243() // 바나나 새 몬스터 돌댕이
    {
        MakeTrap(244, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern244() // 바나나 새 몬스터 꿀벌
    {
        MakeTrap(245, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern245() // 바나나 새 돌땡이 꿀벌
    {
        MakeTrap(246, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern246() // 바나나 총 몬스터 돌댕이
    {
        MakeTrap(247, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern247() // 바나나 총 몬스터 꿀벌
    {
        MakeTrap(248, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern248() // 바나나 총 돌땡이 꿀벌
    {
        MakeTrap(249, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern249() // 바나나 몬스터 돌땡이 꿀벌
    {
        MakeTrap(250, new Vector3(0, 0, 0));
    }

    void pattern250() // 새 총 몬스터 돌땡이
    {
        MakeTrap(251, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(2, "MakeBird", 1.0f));
    }

    void pattern251() // 새 총 몬스터 꿀벌
    {
        MakeTrap(252, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern252() // 새 총 돌땡이 꿀벌
    {
        MakeTrap(253, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern253() // 새 몬스터 돌땡이 꿀벌
    {
        MakeTrap(254, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern254() // 총 몬스터 돌땡이 꿀뻘
    {
        MakeTrap(255, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }








}