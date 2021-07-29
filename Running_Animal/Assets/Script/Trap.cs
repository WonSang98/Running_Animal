using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    enum Forest_Trap
    {
        Jump_Bear = 0, // °õµ£
        Jump_Mole, // µÎ´õÁö ÇÔÁ¤
        Jump_Stump, // ³ª¹« ¹ØµÕ ÇÔÁ¤
        Jump_Banana, // ¹Ù³ª³ª ²®Áú
        Fly_Bird, // ¹öµå ½ºÆ®¶óÀÌÅ© (»çÀü°íÁö)
        Fly_Shot, // ¹Ğ·Æ²ÛÀÇ Á¶¿ëÇÑ ÇÑ¹ß
        Monster,// Á¤·É?
        NoneJump_Stone,// °õÀâ´Â µ¹µ£
        NoneJump_Bee, // ¹úÀÌ ¹«¸®Áö¾îÀÖÀ½ (°øÁß) - Á¡ÇÁÇÏ¸é À¸¾Ó Áê±İ
        Bridge_Stool, // ½£ Æ¯¼ö - ¹ßÆÇ
        Bridge_Trap // ½£ Æ¯¼ö - Àå¾Ö¹°
    }

    GameObject[] traps; // ÇÔÁ¤ ¸®¼Ò½º ÀúÀå
    GameObject warning_bird;
    GameObject warning_shot;
    GameObject LvUp;
    GameObject player;
    GameObject coin;
    GameObject hp;

    int[] map_pattern = { 255 }; // ÃßÈÄ ¹è¿­È­


    //BackGround Æ÷ÇÔ ÇÔÁ¤ ÀÚµ¿ ÀÌµ¿À§ÇÔ

    private void Start()
    {
        player = GameObject.Find("Player");
        // ½ÃÀÛ½Ã ÇÔÁ¤ ¸®¼Ò½º ºÒ·¯¿À±â.
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
            if (i == 0) // °æ°í »ı¼º.
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
            if (i < 20) // °æ°í »ı¼º.
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
    void LevelUP() // ·¹º§¾÷ Àå¼Ò.
    {
        GameObject tmp;
        tmp = Instantiate(LvUp);
        tmp.transform.position = new Vector3(35, -1.8f, 0);
    }
    void pattern0() // °õµ£ »ı¼º
    {
        //trap »ı¼º
        MakeTrap(0, new Vector3(36, -3.36f, 0));
        MakeTrap(0, new Vector3(29, -3.36f, 0));
        MakeTrap(0, new Vector3(22, -3.36f, 0));
    }

    void pattern1() // µÎ´õÁö »ı¼º
    {
        MakeTrap(1, new Vector3(36, -3.35f, 0));
        MakeTrap(1, new Vector3(28, -3.35f, 0));
        MakeTrap(1, new Vector3(20, -3.35f, 0));
    }

    void pattern2() // ³ª¹« »ı¼º
    {
        MakeTrap(2, new Vector3(36, -2.97f, 0));
        MakeTrap(2, new Vector3(28, -2.97f, 0));
        MakeTrap(2, new Vector3(20, -2.97f, 0));
    }

    void pattern3() // ¹Ù³ª³ª »ı¼º
    {
        MakeTrap(3, new Vector3(36, -3.16f, 0));
        MakeTrap(3, new Vector3(28, -3.16f, 0));
        MakeTrap(3, new Vector3(20, -3.16f, 0));
    }

    void pattern4() // ¹öµå½ºÆ®¶óÀÌÅ© »ı¼º
    {
        StartCoroutine(cotime(3, "MakeBird", 0.2f));
    }

    void pattern5() // ÃÑ ½î±â »ı¼º
    {
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }

    void pattern6() // ¸ó½ºÅÍ »ı¼º
    {
        MakeTrap(6, new Vector3(36, -2.85f, 0));
        MakeTrap(6, new Vector3(28, -2.85f, 0));
        MakeTrap(6, new Vector3(20, -2.85f, 0));
    }

    void pattern7() // µ¹¶¯ÀÌ »ı¼º
    {
        MakeTrap(7, new Vector3(36, 5.7f, 0));
        MakeTrap(7, new Vector3(21, 5.7f, 0));
    }

    void pattern8() // ²Ü¹ú »ı¼º
    {
        MakeTrap(8, new Vector3(36, 1.07f, 0));
        MakeTrap(8, new Vector3(30, 1.07f, 0));
        MakeTrap(8, new Vector3(24, 1.07f, 0));
        MakeTrap(8, new Vector3(33, 3.27f, 0));
        MakeTrap(8, new Vector3(27, 3.27f, 0));

    }

    void pattern9() // ½ºÆä¼È »ı¼º
    {
        StartCoroutine("MakeSpecial");
    }

    void pattern10() // °õµ£ + ´õÁöµÎ
    {
        MakeTrap(0, new Vector3(15.2f, -3.342f, 0));
        MakeTrap(0, new Vector3(27.1f, -3.342f, 0));
        MakeTrap(1, new Vector3(21.3f, -3.35f, 0));
        MakeTrap(1, new Vector3(32.8f, -3.35f, 0));
        MakeTrap(1, new Vector3(35.6f, -3.35f, 0));
    }

    void pattern11() // °õµ£ + ³ª¹«
    {
        MakeTrap(0, new Vector3(15.2f, -3.342f, 0));
        MakeTrap(0, new Vector3(18.8f, -3.342f, 0));
        MakeTrap(0, new Vector3(26.7f, -3.342f, 0));
        MakeTrap(2, new Vector3(17, -2.97f, 0));
        MakeTrap(2, new Vector3(24.6f, -2.97f, 0));
        MakeTrap(2, new Vector3(33, -2.97f, 0));
    }

    void pattern12() // °õµ£ + ¹Ù³ª³ª
    {
        MakeTrap(0, new Vector3(21, -3.342f, 0));
        MakeTrap(0, new Vector3(32, -3.342f, 0));
        MakeTrap(3, new Vector3(15, -3.16f, 0));
        MakeTrap(3, new Vector3(26, -3.16f, 0));
        MakeTrap(3, new Vector3(37, -3.16f, 0));
    }

    void pattern13() // °õµ£ + »õ
    {
        MakeTrap(0, new Vector3(36, -3.342f, 0));
        MakeTrap(0, new Vector3(29, -3.342f, 0));
        MakeTrap(0, new Vector3(22, -3.342f, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern14() // °õµ£ + ÃÑ
    {
        MakeTrap(0, new Vector3(36, -3.342f, 0));
        MakeTrap(0, new Vector3(29, -3.342f, 0));
        MakeTrap(0, new Vector3(22, -3.342f, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern15() // °õµ£ + ¸ó½ºÅÍ
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

    void pattern16() // °õµ£ + µ¹
    {
        MakeTrap(0, new Vector3(13.8f, -3.342f, 0));
        MakeTrap(0, new Vector3(18.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(20f, -3.342f, 0));
        MakeTrap(0, new Vector3(29f, -3.342f, 0));
        MakeTrap(0, new Vector3(30.5f, -3.342f, 0));
        MakeTrap(7, new Vector3(23, 5.7f, 0));
    }

    void pattern17() // °õµ£ + ²Ü¹ú
    {
        MakeTrap(0, new Vector3(19.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(21.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(28.5f, -3.342f, 0));
        MakeTrap(0, new Vector3(30.5f, -3.342f, 0));
        MakeTrap(8, new Vector3(16, 0, 0));
        MakeTrap(8, new Vector3(25, 0, 0));
        MakeTrap(8, new Vector3(34, 0, 0));
    }

    void pattern18() // µÎ´õÁö + ³ª¹«
    {
        MakeTrap(1, new Vector3(15f, -3.334f, 0));
        MakeTrap(1, new Vector3(27f, -3.334f, 0));
        MakeTrap(2, new Vector3(21f, -2.97f, 0));
        MakeTrap(2, new Vector3(33f, -2.97f, 0));
    }

    void pattern19() // µÎ´õÁö + ¹Ù³ª³ª
    {
        MakeTrap(1, new Vector3(15f, -3.334f, 0));
        MakeTrap(1, new Vector3(25f, -3.334f, 0));
        MakeTrap(3, new Vector3(13f, -3.16f, 0));
        MakeTrap(3, new Vector3(17f, -3.16f, 0));
        MakeTrap(3, new Vector3(23f, -3.16f, 0));
        MakeTrap(3, new Vector3(27f, -3.16f, 0));
    }

    void pattern20() // µÎ´õÁö + »õ
    {
        MakeTrap(21, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern21() // µÎ´õÁö + ÃÑ
    {
        MakeTrap(22, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern22() // µÎ´õÁö + ¸ó½ºÅÍ
    {
        MakeTrap(23, new Vector3(0, 0, 0));
    }

    void pattern23() // µÎ´õÁö + µ¹¶¯ÀÌ
    {
        MakeTrap(24, new Vector3(0, 0, 0));
    }

    void pattern24() // µÎ´õÁö + ²Ü¹ú
    {
        MakeTrap(25, new Vector3(0, 0, 0));
    }

    void pattern25() // ³ª¹« + ¹Ù³ª³ª
    {
        MakeTrap(26, new Vector3(0, 0, 0));
    }

    void pattern26() // ³ª¹« + »õ
    {
        MakeTrap(27, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern27() // ³ª¹« + ÃÑ
    {
        MakeTrap(28, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern28() // ³ª¹« + ¸ó½ºÅÍ
    {
        MakeTrap(29, new Vector3(0, 0, 0));
    }

    void pattern29() // ³ª¹« + µ¹¶¯ÀÌ
    {
        MakeTrap(30, new Vector3(0, 0, 0));
    }

    void pattern30() // ³ª¹« + ²Ü¹ú
    {
        MakeTrap(31, new Vector3(0, 0, 0));
    }

    void pattern31() // ¹Ù³ª³ª + »õ
    {
        MakeTrap(32, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern32() // ¹Ù³ª³ª + ÃÑ
    {
        MakeTrap(33, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern33() // ¹Ù³ª³ª + ¸ó½ºÅÍ
    {
        MakeTrap(34, new Vector3(0, 0, 0));
    }

    void pattern34() // ¹Ù³ª³ª + µ¹¶¯ÀÌ
    {
        MakeTrap(35, new Vector3(0, 0, 0));
    }

    void pattern35() // ¹Ù³ª³ª + ²Ü¹ú
    {
        MakeTrap(36, new Vector3(0, 0, 0));
    }

    void pattern36() // »õ + ÃÑ
    {
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern37() // »õ + ¸ó½ºÅÍ
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(38, new Vector3(0, 0, 0));
    }

    void pattern38() // »õ + µ¹¶¯ÀÌ
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(39, new Vector3(0, 0, 0));
    }

    void pattern39() // »õ + ²Ü¹ú
    {
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        MakeTrap(40, new Vector3(0, 0, 0));
    }

    void pattern40() // ÃÑ + ¸ó½ºÅÍ
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(41, new Vector3(0, 0, 0));
    }

    void pattern41() // ÃÑ + µ¹¶¯ÀÌ
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(42, new Vector3(0, 0, 0));
    }

    void pattern42() // ÃÑ + ²Ü¹ú
    {
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        MakeTrap(43, new Vector3(0, 0, 0));
    }

    void pattern43() // ¸ó½ºÅÍ+µ¹¶¯ÀÌ
    {
        MakeTrap(44, new Vector3(0, 0, 0));
    }

    void pattern44() // ¸ó½ºÅÍ+²Ü¹ú
    {
        MakeTrap(45, new Vector3(0, 0, 0));
    }

    void pattern45() // µ¹¶¯ÀÌ+²Ü¹ú
    {
        MakeTrap(46, new Vector3(0, 0, 0));
    }

    void pattern46() // °õµ£ µÎ´õÁö ³ª¹«
    {
        MakeTrap(47, new Vector3(0, 0, 0));
    }

    void pattern47() // °õµ£ µÎ´õÁö ¹Ù³ª³ª
    {
        MakeTrap(48, new Vector3(0, 0, 0));
    }

    void pattern48() // °õµ£ µÎ´õÁö »õ
    {
        MakeTrap(49, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern49() // °õµ£ µÎ´õÁö ÃÑ
    {
        MakeTrap(50, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern50() // °õµ£ µÎ´õÁö ¸ó½ºÅÍ
    {
        MakeTrap(51, new Vector3(0, 0, 0));
    }

    void pattern51() // °õµ£ µÎ´õÁö µ¹¶¯ÀÌ
    {
        MakeTrap(52, new Vector3(0, 0, 0));
    }

    void pattern52() // °õµ£ µÎ´õÁö ¹ú
    {
        MakeTrap(53, new Vector3(0, 0, 0));
    }

    void pattern53() // °õµ£ ³ª¹« ¹Ù³ª³ª
    {
        MakeTrap(54, new Vector3(0, 0, 0));
    }

    void pattern54() // °õµ£ ³ª¹« »õ
    {
        MakeTrap(55, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern55() // °õµ£ ³ª¹« ÃÑ
    {
        MakeTrap(56, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern56() // °õµ£ ³ª¹« ¸ó½ºÅÍ
    {
        MakeTrap(57, new Vector3(0, 0, 0));
    }

    void pattern57() // °õµ£ ³ª¹« µ¹¶¯ÀÌ
    {
        MakeTrap(58, new Vector3(0, 0, 0));
    }

    void pattern58() // °õµ£ ³ª¹« ¹ú
    {
        MakeTrap(59, new Vector3(0, 0, 0));
    }
    void pattern59() // °õµ£ ¹Ù³ª³ª »õ
    {
        MakeTrap(60, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern60() // °õµ£ ¹Ù³ª³ª ÃÑ
    {
        MakeTrap(61, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern61() // °õµ£ ¹Ù³ª³ª ¸ó½ºÅÍ
    {
        MakeTrap(62, new Vector3(0, 0, 0));
    }

    void pattern62() // °õµ£ ¹Ù³ª³ª µ¹¶¯ÀÌ
    {
        MakeTrap(63, new Vector3(0, 0, 0));
    }

    void pattern63() // °õµ£ ¹Ù³ª³ª ²Ü¹ú
    {
        MakeTrap(64, new Vector3(0, 0, 0));
    }

    void pattern64() // °õµ£ »õ ÃÑ
    {
        MakeTrap(65, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern65() // °õµ£ »õ ¸ó½ºÅÍ 
    {
        MakeTrap(66, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern66() // °õµ£ »õ µ¹¶¯ÀÌ
    {
        MakeTrap(67, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern67() // °õµ£ »õ ²Ü¹ú
    {
        MakeTrap(68, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern68() // °õµ£ ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(69, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }
    void pattern69() // °õµ£ ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(70, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern70() // °õµ£ ÃÑ ²Ü¹ú
    {
        MakeTrap(71, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern71() // °õµ£ ¸ó½ºÅÍ µ¹´óÀÌ
    {
        MakeTrap(72, new Vector3(0, 0, 0));
    }

    void pattern72() // °õµ£ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(73, new Vector3(0, 0, 0));
    }

    void pattern73() // °õµ£ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(74, new Vector3(0, 0, 0));
    }

    void pattern74() // µÎ´õÁö ³ª¹« ¹Ù³ª³ª
    {
        MakeTrap(75, new Vector3(0, 0, 0));
    }

    void pattern75() // µÎ´õÁö ³ª¹« »õ
    {
        MakeTrap(76, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.2f));
    }

    void pattern76() // µÎ´õÁö ³ª¹« ÃÑ
    {
        MakeTrap(77, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern77() // µÎ´õÁö ³ª¹« ¸ó½ºÅÍ
    {
        MakeTrap(78, new Vector3(0, 0, 0));
    }

    void pattern78() // µÎ´õÁö ³ª¹« µ¹¶¯ÀÌ
    {
        MakeTrap(79, new Vector3(0, 0, 0));
    }

    void pattern79() // µÎ´õÁö ³ª¹« ²Ü¹ú
    {
        MakeTrap(80, new Vector3(0, 0, 0));
    }

    void pattern80() // µÎ´õÁö ¹Ù³ª³ª »õ
    {
        MakeTrap(81, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern81() // µÎ´õÁö ¹Ù³ª³ª ÃÑ
    {
        MakeTrap(82, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern82() // µÎ´õÁö ¹Ù³ª³ª ¸ó½ºÅÍ
    {
        MakeTrap(83, new Vector3(0, 0, 0));
    }

    void pattern83() // µÎ´õÁö ¹Ù³ª³ª µ¹¶¯ÀÌ
    {
        MakeTrap(84, new Vector3(0, 0, 0));
    }

    void pattern84() // µÎ´õÁö ¹Ù³ª³ª ¹ú
    {
        MakeTrap(85, new Vector3(0, 0, 0));
    }

    void pattern85() // µÎ´õÁö »õ ÃÑ
    {
        MakeTrap(86, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
        StartCoroutine(cotime(3, "MakeShot", 0.3f));
    }

    void pattern86() // µÎ´õÁö »õ ¸ó½ºÅÍ
    {
        MakeTrap(87, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern87() // µÎ´õÁö »õ µ¹¶¯ÀÌ
    {
        MakeTrap(88, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern88() // µÎ´õÁö »õ ²Ü¹ú
    {
        MakeTrap(89, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern89() // µÎ´õÁö ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(90, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern90() // µÎ´õÁö ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(91, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }

    void pattern91() // µÎ´õÁö ÃÑ ²Ü¹ú
    {
        MakeTrap(92, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern92() // µÎ´õÁö ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(93, new Vector3(0, 0, 0));
    }

    void pattern93() // µÎ´õÁö ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(94, new Vector3(0, 0, 0));
    }

    void pattern94() // ³ª¹« ¹Ù³ª³ª »õ
    {
        MakeTrap(95, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern95() // ³ª¹« ¹Ù³ª³ª ÃÑ
    {
        MakeTrap(96, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern96() // ³ª¹« ¹Ù³ª³ª ¸ó½ºÅÍ
    {
        MakeTrap(97, new Vector3(0, 0, 0));
    }

    void pattern97() // ³ª¹« ¹Ù³ª³ª µ¹¶¯ÀÌ
    {
        MakeTrap(98, new Vector3(0, 0, 0));
    }

    void pattern98() // ³ª¹« ¹Ù³ª³ª ²Ü¹ú
    {
        MakeTrap(99, new Vector3(0, 0, 0));
    }

    void pattern99() // ³ª¹« »õ ÃÑ
    {
        MakeTrap(100, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern100() // ³ª¹« »õ ¸ó½ºÅÍ
    {
        MakeTrap(101, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern101() // ³ª¹« »õ µ¹¶¯ÀÌ
    {
        MakeTrap(102, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern102() // ³ª¹« »õ ²Ü¹ú
    {
        MakeTrap(103, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern103() // ³ª¹« ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(104, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern104() // ³ª¹« ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(105, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern105() // ³ª¹« ÃÑ ²Ü¹ú
    {
        MakeTrap(106, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern106() // ³ª¹« ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(107, new Vector3(0, 0, 0));
    }

    void pattern107() // ³ª¹« ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(108, new Vector3(0, 0, 0));
    }

    void pattern108() // ³ª¹« µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(109, new Vector3(0, 0, 0));
    }

    void pattern109() // ¹Ù³ª³ª »õ ÃÑ
    {
        MakeTrap(110, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern110() // ¹Ù³ª³ª »õ ¸ó½ºÅÍ
    {
        MakeTrap(111, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern111() // ¹Ù³ª³ª »õ µ¹´óÀÌ
    {
        MakeTrap(112, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern112() // ¹Ù³ª³ª »õ ²Ü¹ú
    {
        MakeTrap(113, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern113() // ¹Ù³ª³ª ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(114, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern114() // ¹Ù³ª³ª ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(115, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern115() // ¹Ù³ª³ª ÃÑ ²Ü¹ú
    {
        MakeTrap(116, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern116() // ¹Ù³ª³ª ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(117, new Vector3(0, 0, 0));
    }

    void pattern117() // ¹Ù³ª³ª ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(118, new Vector3(0, 0, 0));
    }

    void pattern118() // ¹Ù³ª³ª µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(119, new Vector3(0, 0, 0));
    }

    void pattern119() // »õ ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(120, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern120() // »õ ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(121, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern121() // »õ ÃÑ ²Ü¹ú
    {
        MakeTrap(122, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern122() // »õ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(123, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern123() // »õ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(124, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern124() // »õ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(125, new Vector3(0, 0, 0));
        StartCoroutine(cotime(4, "MakeBird", 0.3f));
    }

    void pattern125() // ÃÑ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(126, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.5f));
    }

    void pattern126() // ÃÑ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(127, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeShot", 0.2f));
    }

    void pattern127() // ÃÑ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(128, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern128() // ¸ó½ºÅÍ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(129, new Vector3(0, 0, 0));
    }

    void pattern129() // °õµ£ µÎ´õÁö ³ª¹« ¹Ù³ª³ª
    {
        MakeTrap(130, new Vector3(0, 0, 0));
    }

    void pattern130() // °õµ£ µÎ´õÁö ³ª¹« »õ
    {
        MakeTrap(131, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern131() // °õµ£ µÎ´õÁö ³ª¹« ÃÑ
    {
        MakeTrap(132, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern132() // °õµ£ µÎ´õÁö ³ª¹« ¸ó½ºÅÍ
    {
        MakeTrap(133, new Vector3(0, 0, 0));
    }

    void pattern133() // °õµ£ µÎ´õÁö ³ª¹« µ¹¶¯ÀÌ
    {
        MakeTrap(134, new Vector3(0, 0, 0));
    }

    void pattern134() // °õµ£ µÎ´õÁö ³ª¹« ²Ü¹ú
    {
        MakeTrap(135, new Vector3(0, 0, 0));
    }

    void pattern135() // °õµ£ µÎ´õÁö ¹Ù³ª³ª »õ
    {
        MakeTrap(136, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern136() // °õµ£ µÎ´õÁö ¹Ù³ª³ª ÃÑ
    {
        MakeTrap(137, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern137() // °õµ£ µÎ´õÁö ¹Ù³ª³ª ¸ó½ºÅÍ
    {
        MakeTrap(138, new Vector3(0, 0, 0));
    }

    void pattern138() // °õµ£ µÎ´õÁö ¹Ù³ª³ª µ¹¶¯ÀÌ
    {
        MakeTrap(139, new Vector3(0, 0, 0));
    }

    void pattern139() // °õµ£ µÎ´õÁö ¹Ù³ª³ª ²Ü¹ú
    {
        MakeTrap(140, new Vector3(0, 0, 0));
    }

    void pattern140() // °õµ£ µÎ´õÁö »õ ÃÑ
    {
        MakeTrap(141, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.1f));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern141() // °õµ£ µÎ´õÁö »õ ¸ó½ºÅÍ
    {
        MakeTrap(142, new Vector3(0, 0, 0));
        StartCoroutine(cotime(3, "MakeBird", 0.4f));
    }

    void pattern142() // °õµ£ µÎ´õÁö »õ µ¹¶¯ÀÌ
    {
        MakeTrap(143, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern143() // °õµ£ µÎ´õÁö »õ ²Ü¹ú
    {
        MakeTrap(144, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern144() // °õµ£ µÎ´õÁö ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(145, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern145() // °õµ£ µÎ´õÁö ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(146, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern146() // °õµ£ µÎ´õÁö ÃÑ ²Ü¹ú
    {
        MakeTrap(147, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern147() // °õµ£ µÎ´õÁö ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(148, new Vector3(0, 0, 0));
    }

    void pattern148() // °õµ£ µÎ´õÁö ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(149, new Vector3(0, 0, 0));
    }

    void pattern149() // °õµ£ µÎ´õÁö µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(150, new Vector3(0, 0, 0));
    }

    void pattern150() // °õµ£ ³ª¹« ¹Ù³ª³ª »õ
    {
        MakeTrap(151, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern151() // °õµ£ ³ª¹« ¹Ù³ª³ª ÃÑ
    {
        MakeTrap(152, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern152() // °õµ£ ³ª¹« ¹Ù³ª³ª ¸ó½ºÅÍ
    {
        MakeTrap(153, new Vector3(0, 0, 0));
    }

    void pattern153() // °õµ£ ³ª¹« ¹Ù³ª³ª µ¹¶¯ÀÌ
    {
        MakeTrap(154, new Vector3(0, 0, 0));
    }

    void pattern154() // °õµ£ ³ª¹« ¹Ù³ª³ª ²Ü¹ú
    {
        MakeTrap(155, new Vector3(0, 0, 0));
    }

    void pattern155() // °õµ£ ³ª¹« »õ ÃÑ
    {
        MakeTrap(156, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern156() // °õµ£ ³ª¹« »õ ¸ó½ºÅÍ
    {
        MakeTrap(157, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern157() // °õµ£ ³ª¹« »õ µ¹¶¯ÀÌ
    {
        MakeTrap(158, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.5f));
    }

    void pattern158() // °õµ£ ³ª¹« »õ ²Ü¹ú
    {
        MakeTrap(159, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.5f));
    }

    void pattern159() // °õµ£ ³ª¹« ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(160, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern160() // °õµ£ ³ª¹« ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(161, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern161() // °õµ£ ³ª¹« ÃÑ ²Ü¹ú
    {
        MakeTrap(162, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern162() // °õµ£ ³ª¹« ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(163, new Vector3(0, 0, 0));
    }

    void pattern163() // °õµ£ ³ª¹« ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(164, new Vector3(0, 0, 0));
    }

    void pattern164() // °õµ£ ³ª¹« µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(165, new Vector3(0, 0, 0));
    }

    void pattern165() // °õµ£ ¹Ù³ª³ª »õ ÃÑ
    {
        MakeTrap(166, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.2f));
    }

    void pattern166() // °õµ£ ¹Ù³ª³ª »õ ¸ó½ºÅÍ
    {
        MakeTrap(167, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern167() // °õµ£ ¹Ù³ª³ª »õ µ¹¶¯ÀÌ
    {
        MakeTrap(168, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern168() // °õµ£ ¹Ù³ª³ª »õ ²Ü¹ú
    {
        MakeTrap(169, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern169() // °õµ£ ¹Ù³ª³ª ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(170, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern170() // °õµ£ ¹Ù³ª³ª ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(171, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern171() // °õµ£ ¹Ù³ª³ª ÃÑ ²Ü¹ú
    {
        MakeTrap(172, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
    }

    void pattern172() // °õµ£ ¹Ù³ª³ª ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(173, new Vector3(0, 0, 0));
    }

    void pattern173() // °õµ£ ¹Ù³ª³ª ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(174, new Vector3(0, 0, 0));
    }

    void pattern174() // °õµ£ ¹Ù³ª³ª µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(175, new Vector3(0, 0, 0));
    }

    void pattern175() // °õµ£ »õ ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(176, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.2f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern176() // °õµ£ »õ ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(177, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern177() // °õµ£ »õ ÃÑ ²Ü¹ú
    {
        MakeTrap(178, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern178() // °õµ£ »õ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(179, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern179() // °õµ£ »õ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(180, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern180() // °õµ£ »õ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(181, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern181() // °õµ£ ÃÑ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(182, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern182() // °õµ£ ÃÑ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(183, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeShot", 0.4f));
    }

    void pattern183() // °õµ£ ÃÑ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(184, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern184() // °õµ£ ¸ó½ºÅÍ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(185, new Vector3(0, 0, 0));
    }

    void pattern185() // µÎ´õÁö ³ª¹« ¹Ù³ª³ª »õ
    {
        MakeTrap(186, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern186() // µÎ´õÁö ³ª¹« ¹Ù³ª³ª ÃÑ
    {
        MakeTrap(187, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern187() // µÎ´õÁö ³ª¹« ¹Ù³ª³ª ¸ó½ºÅÍ
    {
        MakeTrap(188, new Vector3(0, 0, 0));
    }

    void pattern188() // µÎ´õÁö ³ª¹« ¹Ù³ª³ª µ¹¶¯ÀÌ
    {
        MakeTrap(189, new Vector3(0, 0, 0));
    }

    void pattern189() // µÎ´õÁö ³ª¹« ¹Ù³ª³ª ²Ü¹ú
    {
        MakeTrap(190, new Vector3(0, 0, 0));
    }

    void pattern190() // µÎ´õÁö ³ª¹« »õ ÃÑ
    {
        MakeTrap(191, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern191() // µÎ´õÁö ³ª¹« »õ ¸ó½ºÅÍ
    {
        MakeTrap(192, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern192() // µÎ´õÁö ³ª¹« »õ µ¹¶¯ÀÌ
    {
        MakeTrap(193, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern193() // µÎ´õÁö ³ª¹« »õ ²Ü¹ú
    {
        MakeTrap(194, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern194() // µÎ´õÁö ³ª¹« ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(195, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern195() // µÎ´õÁö ³ª¹« ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(196, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern196() // µÎ´õÁö ³ª¹« ÃÑ ²Ü¹ú
    {
        MakeTrap(197, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
    }

    void pattern197() // µÎ´õÁö ³ª¹« ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(198, new Vector3(0, 0, 0));
    }

    void pattern198() // µÎ´õÁö ³ª¹« ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(199, new Vector3(0, 0, 0));
    }

    void pattern199() // µÎ´õÁö ³ª¹« µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(200, new Vector3(0, 0, 0));
    }

    void pattern200() // µÎ´õÁö ¹Ù³ª³ª »õ ÃÑ
    {
        MakeTrap(201, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.4f));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern201() // µÎ´õÁö ¹Ù³ª³ª »õ ¸ó½ºÅÍ
    {
        MakeTrap(202, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.4f));
    }

    void pattern202() // µÎ´õÁö ¹Ù³ª³ª »õ µ¹¶¯ÀÌ
    {
        MakeTrap(203, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern203() // µÎ´õÁö ¹Ù³ª³ª »õ ²Ü¹ú
    {
        MakeTrap(204, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.4f));
    }

    void pattern204() // µÎ´õÁö ¹Ù³ª³ª ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(205, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern205() // µÎ´õÁö ¹Ù³ª³ª ÃÑ µ¹´óÀÌ
    {
        MakeTrap(206, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern206() // µÎ´õÁö ¹Ù³ª³ª ÃÑ ²Ü¹ú
    {
        MakeTrap(207, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern207() // µÎ´õÁö ¹Ù³ª³ª ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(208, new Vector3(0, 0, 0));
    }

    void pattern208() // µÎ´õÁö ¹Ù³ª³ª ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(209, new Vector3(0, 0, 0));
    }

    void pattern209() // µÎ´õÁö ¹Ù³ª³ª µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(210, new Vector3(0, 0, 0));
    }

    void pattern210() // µÎ´õÁö »õ ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(211, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern211() // µÎ´õÁö »õ ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(212, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern212() // µÎ´õÁö »õ ÃÑ ²Ü¹ú
    {
        MakeTrap(213, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern213() // µÎ´õÁö »õ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(214, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern214() // µÎ´õÁö »õ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(215, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern215() // µÎ´õÁö »õ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(216, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.2f));
    }

    void pattern216() // µÎ´õÁö ÃÑ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(217, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern217() // µÎ´õÁö ÃÑ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(218, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern218() // µÎ´õÁö ÃÑ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(219, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
    }

    void pattern219() // µÎ´õÁö ¸ó½ºÅÍ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(220, new Vector3(0, 0, 0));
    }

    void pattern220() // ³ª¹« ¹Ù³ª³ª »õ ÃÑ
    {
        MakeTrap(221, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.2f));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern221() // ³ª¹« ¹Ù³ª³ª »õ ¸ó½ºÅÍ
    {
        MakeTrap(222, new Vector3(0, 0, 0));
        StartCoroutine(cotime(2, "MakeBird", 0.6f));
    }

    void pattern222() // ³ª¹« ¹Ù³ª³ª »õ µ¹´óÀÌ
    {
        MakeTrap(223, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern223() // ³ª¹« ¹Ù³ª³ª »õ ²Ü¹ú
    {
        MakeTrap(224, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 0.6f));
    }

    void pattern224() // ³ª¹« ¹Ù³ª³ª ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(225, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern225() // ³ª¹« ¹Ù³ª³ª ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(226, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern226() // ³ª¹« ¹Ù³ª³ª ÃÑ ²Ü¹ú
    {
        MakeTrap(227, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
    }

    void pattern227() // ³ª¹« ¹Ù³ª³ª ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(228, new Vector3(0, 0, 0));
    }

    void pattern228() // ³ª¹« ¹Ù³ª³ª ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(229, new Vector3(0, 0, 0));
    }

    void pattern229() // ³ª¹« ¹Ù³ª³ª µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(230, new Vector3(0, 0, 0));
    }

    void pattern230() // ³ª¹« »õ ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(231, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern231() // ³ª¹« »õ ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(232, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern232() // ³ª¹« »õ ÃÑ ²Ü¹ú
    {
        MakeTrap(233, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 0.6f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern233() // ³ª¹« »õ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(234, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern234() // ³ª¹« »õ ¸ó½ºÅÍ ±¼¹ú
    {
        MakeTrap(235, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern235() // ³ª¹« »õ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(236, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern236() // ³ª¹« ÃÑ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(237, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern237() // ³ª¹« ÃÑ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(238, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern238() // ³ª¹« ÃÑ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(239, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern239() // ³ª¹« ¸ó½ºÅÍ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(240, new Vector3(0, 0, 0));
    }

    void pattern240() // ¹Ù³ª³ª »õ ÃÑ ¸ó½ºÅÍ
    {
        MakeTrap(241, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern241() // ¹Ù³ª³ª »õ ÃÑ µ¹¶¯ÀÌ
    {
        MakeTrap(242, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern242() // ¹Ù³ª³ª »õ ÃÑ ²Ü¹ú
    {
        MakeTrap(243, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern243() // ¹Ù³ª³ª »õ ¸ó½ºÅÍ µ¹´óÀÌ
    {
        MakeTrap(244, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern244() // ¹Ù³ª³ª »õ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(245, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern245() // ¹Ù³ª³ª »õ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(246, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern246() // ¹Ù³ª³ª ÃÑ ¸ó½ºÅÍ µ¹´óÀÌ
    {
        MakeTrap(247, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern247() // ¹Ù³ª³ª ÃÑ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(248, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern248() // ¹Ù³ª³ª ÃÑ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(249, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }

    void pattern249() // ¹Ù³ª³ª ¸ó½ºÅÍ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(250, new Vector3(0, 0, 0));
    }

    void pattern250() // »õ ÃÑ ¸ó½ºÅÍ µ¹¶¯ÀÌ
    {
        MakeTrap(251, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(2, "MakeBird", 1.0f));
    }

    void pattern251() // »õ ÃÑ ¸ó½ºÅÍ ²Ü¹ú
    {
        MakeTrap(252, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern252() // »õ ÃÑ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(253, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern253() // »õ ¸ó½ºÅÍ µ¹¶¯ÀÌ ²Ü¹ú
    {
        MakeTrap(254, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeBird", 1.0f));
    }

    void pattern254() // ÃÑ ¸ó½ºÅÍ µ¹¶¯ÀÌ ²Ü»¹
    {
        MakeTrap(255, new Vector3(0, 0, 0));
        StartCoroutine(cotime(1, "MakeShot", 1.0f));
    }








}