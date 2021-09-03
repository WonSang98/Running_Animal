using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTutorial : MonoBehaviour
{
    TrapForest TrapForest;
    SetPlayer SetPlayer;
    UI_Play UI_Play;
    LoadScene LS;
    InterAction InterAction;

    float timer = 25;
    int test_idx = 0;
    int cnt = 0;
    Sprite[] Stages;
    GameObject[] Infos;
    private void Awake()
    {
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        TrapForest = GameObject.Find("@Managers").GetComponent<TrapForest>();
        SetPlayer = GameObject.Find("@Managers").GetComponent<SetPlayer>();
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
        LS = GameManager.Instance.GetComponent<LoadScene>();
        InterAction = GameObject.Find("@Managers").GetComponent<InterAction>();
    }
    void Start()
    {
        //Trap Resource 불러오기.
        TrapForest.GetResource();
        //플레이어 지정된 위치에 생성
        SetPlayer.Spawn();
        if (GameManager.Play.Playing == false)
        {
            SetPlayer.FirstSet();
            GameManager.Play.Playing = true;
        }
        UI_Play.SetUI();

        Stages = Resources.LoadAll<Sprite>("Image/GUI/Tutorial");
        Infos = new GameObject[4];
        Infos[0] = GameObject.Find("UI").transform.Find("Image_DIrect/Image_JUMP").gameObject;
        Infos[1] = GameObject.Find("UI").transform.Find("Image_DIrect/Image_DOWN").gameObject;
        Infos[2] = GameObject.Find("UI").transform.Find("Image_DIrect/Image_SKILL").gameObject;
        Infos[3] = GameObject.Find("UI").transform.Find("Image_DIrect/Image_EXPGAGE").gameObject;
        show(-1, 0);
    }

    void Update()
    {
        timer -= GameManager.Play.Status.ability.SPEED.value * Time.deltaTime;
        if(timer <= 0)
        {
            if (GameManager.Play.Status.ability.HP.value != GameManager.Play.Status.ability.MAX_HP.value) //튜토리얼 중 피격 당한 경우
            {
                GameManager.Instance.AllStop();
                GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
                UI_Play.BAR_HP();
                cnt += 1;
                if(cnt == 2)
                {
                    cnt = 0;
                    test_idx += 1;
                }
                else
                {
                    show(-1, 6);

                }
            }
            else
            {
                test_idx += 1;
            }

            Invoke("test" + test_idx.ToString(), 2);
            timer = 70; 

        }
    }

    void show(int direct_idx, int idx)
    {
        for(int i = 0; i < 4; i++)
        {
            Infos[i].SetActive(false);
        }
        if (direct_idx != -1)
        {
            Infos[direct_idx].SetActive(true);
        }
        StartCoroutine(ShowStage(idx));
    }
    void test1() // JUMP 훈련
    {

        show(0, 1);
        TrapForest.MakeTrap(166, new Vector3(0, 0, 0));
    }

    void test2() // DONW 훈련
    {
        show(1, 2);
        StartCoroutine(TrapForest.MakeSpecial());
    }
    void test3() // 스킬
    {
        show(2, 3);
        GameManager.Play.Status.ACTIVE = Active.ACTIVE_CODE.Flash;
        UI_Play.Image_Skill.sprite = UI_Play.Active.Active_Sprites[(int)GameManager.Play.Status.ACTIVE];
        TrapForest.MakeTrap(101, new Vector3(0, 0, 0));
    }

    void test4()
    {
        show(3, 4);
        GameObject tmp;
        tmp = Instantiate(TrapForest.LvUp);
        tmp.transform.position = new Vector3(30, -1.8f, 0);
        StartCoroutine(TrapForest.GoTuto2());
    }

    public IEnumerator ShowStage(int idx)
    {
        //현재 스테이지가 어딘지 보여준다.
        UI_Play.GO_Stage.SetActive(true);
        UI_Play.Image_Stage.sprite = Stages[idx];
        float time = 0;

        while (time < 4.5f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        UI_Play.GO_Stage.SetActive(false);

    }
}
