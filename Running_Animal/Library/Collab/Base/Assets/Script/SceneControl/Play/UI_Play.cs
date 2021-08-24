using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Play : MonoBehaviour
{
    public Image Image_HpBar;
    public Image Image_ExpBar;

    public GameObject combo;
    public TextMeshProUGUI Text_Combo;
    public GameObject combo_Count;
    public TextMeshProUGUI Text_ComboCNT;
    public GameObject Cam;
    public GameObject Miss;
    
    public GameObject Fail;
    public GameObject Fail_Content;
    public GameObject Success;
    public GameObject Success_Content;

    public GameObject Count;

    public GameObject Pause;
    public Button PauseYes;
    public Button PauseNo;
    public Image[] GetPassives;
    public Image[] Image_Fail;
    public Image[] Image_Success;
    public Sprite[] Sprite_DeadCharacter;
    public Sprite[] Sprite_DeadCause;
    public Sprite[] Sprite_GoodCharacter;

    public GameObject Skill;
    public Button Button_Skill;
    public Image Image_Skill;
    public Button Button_Jump;
    public Button Button_Down;

    public Button Button_FailMain; // 실패시 메인으로 가는 버튼~
    public Button Button_SuccessMain;

    public EventTrigger.Entry Entry_Jump;
    public EventTrigger.Entry Entry_Down;
    public EventTrigger Event_jump;
    public EventTrigger Event_down;

    public TextMeshProUGUI Text_gold;
    public TextMeshProUGUI Text_Count;
    public Text[] Text_Fail;
    public Text[] Text_Success;

    public Color player_opacity;

    public Camera MC;

    public Animator Ani_Player;
    public Rigidbody2D Rig_Player;

    public InterAction InterAction;
    public Passive Passive;
    IEnumerator Coroutine_Combo;

    public void SetUI()
    {
        Passive = gameObject.GetComponent<Passive>();
        InterAction = gameObject.GetComponent<InterAction>();
        Image_HpBar = GameObject.Find("UI/Bar_HP/Gage_HP").GetComponent<Image>();
        Image_ExpBar = GameObject.Find("UI/Bar_EXP/Gage_EXP").GetComponent<Image>();

        combo = GameObject.Find("UI/Text_Combo");
        Text_Combo = combo.GetComponent<TextMeshProUGUI>();
        combo_Count = GameObject.Find("UI/Text_Combo/Text_Count");
        Text_ComboCNT = combo_Count.GetComponent<TextMeshProUGUI>();
        Cam = GameObject.Find("Main Camera");
        Miss = Resources.Load<GameObject>("Item/Miss");

        Fail = GameObject.Find("UI").transform.Find("Panel_Fail").gameObject;
        Fail_Content = Fail.transform.Find("Content").gameObject;

        Success = GameObject.Find("UI").transform.Find("Panel_Success").gameObject;
        Success_Content = Fail.transform.Find("Content").gameObject;

        Count = GameObject.Find("UI").transform.Find("Text_Count").gameObject;

        Pause = GameObject.Find("UI").transform.Find("Button_Back").gameObject;
        Pause.GetComponent<Button>().onClick.AddListener(() => OnPause());
        PauseYes = GameObject.Find("UI").transform.Find("Panel_Pause/Button_Yes").GetComponent<Button>();
        PauseYes.onClick.AddListener(() => gameObject.GetComponent<LoadScene>().EndGame());
        PauseNo = GameObject.Find("UI").transform.Find("Panel_Pause/Button_No").GetComponent<Button>();
        PauseNo.onClick.AddListener(() => OffPause());
        GetPassives = new Image[14];
        for(int j = 0; j < 14; j++)
        {
            GetPassives[j] = GameObject.Find("UI").transform.Find("Panel_Pause/PASSIVE/Image" + j.ToString()).GetComponent<Image>();
        }
        
        Image_Fail = new Image[2];
        Image_Fail[0] = GameObject.Find("UI").transform.Find("Panel_Fail/Content/Image_DIE").GetComponent<Image>();
        Image_Fail[1] = GameObject.Find("UI").transform.Find("Panel_Fail/Content/Image_Trap/Image_Trap").GetComponent<Image>();

        Image_Success = new Image[1];
        Image_Success[0] = GameObject.Find("UI").transform.Find("Panel_Success/Content/Image_SUCCESS").GetComponent<Image>();

        Sprite_DeadCharacter = Resources.LoadAll<Sprite>("Image/GUI/Play/CharacterDead");
        Sprite_DeadCause = Resources.LoadAll<Sprite>("Image/GUI/Play/CauseDead");
        Sprite_GoodCharacter = Resources.LoadAll<Sprite>("Image/GUI/Play/CharacterDead"); //Temp

        Skill = GameObject.Find("UI/Button_Skill").gameObject;
        Image_Skill = Skill.GetComponent<Image>();
        Image_Skill.sprite = gameObject.GetComponent<Active>().Active_Sprites[(int)GameManager.Play.Status.ACTIVE];
        Button_Skill = Skill.GetComponent<Button>();
        Button_Skill.onClick.AddListener(Use_Active);
        Button_Jump = GameObject.Find("UI/Button_Jump").GetComponent<Button>();
        Button_Down = GameObject.Find("UI/Button_Down").GetComponent<Button>();

        Button_FailMain = GameObject.Find("UI").transform.Find("Panel_Fail/Content/Button_MAIN").GetComponent<Button>();
        Button_FailMain.onClick.AddListener(() => gameObject.GetComponent<LoadScene>().EndGame());
        
        Button_SuccessMain = GameObject.Find("UI").transform.Find("Panel_Success/Content/Button_MAIN").GetComponent<Button>();
        Button_FailMain.onClick.AddListener(() => gameObject.GetComponent<LoadScene>().EndGame());

        Event_jump = GameObject.Find("UI/Button_Jump").GetComponent<EventTrigger>();
        Event_down = GameObject.Find("UI/Button_Down").GetComponent<EventTrigger>();

        Entry_Jump = new EventTrigger.Entry();
        Entry_Jump.eventID = EventTriggerType.PointerDown;
        Entry_Jump.callback.AddListener((data) => { OnJump((PointerEventData)data); });
        Event_jump.triggers.Add(Entry_Jump);

        Entry_Down = new EventTrigger.Entry();
        Entry_Down.eventID = EventTriggerType.PointerDown;
        Entry_Down.callback.AddListener((data) => { OnDown((PointerEventData)data); });
        Event_down.triggers.Add(Entry_Down);

        Text_gold = GameObject.Find("UI/Image_GOLD/Text_Gold").GetComponent<TextMeshProUGUI>();
        Text_gold.text = $"{GameManager.Play.DC.goldNow}";

        Text_Count = Count.GetComponent<TextMeshProUGUI>();

        Text_Fail = new Text[8];
        string path_TF = "Panel_Fail/Content/Result_Unit/";
        Text_Fail[0] = GameObject.Find("UI").transform.Find(path_TF + "Text_Stage/Text_Value").GetComponent<Text>();
        Text_Fail[1] = GameObject.Find("UI").transform.Find(path_TF + "Text_Stage_NoHit/Text_Value").GetComponent<Text>();
        Text_Fail[2] = GameObject.Find("UI").transform.Find(path_TF + "Text_Difficult/Text_Value").GetComponent<Text>();
        Text_Fail[3] = GameObject.Find("UI").transform.Find(path_TF + "Text_Combo/Text_Value").GetComponent<Text>();
        Text_Fail[4] = GameObject.Find("UI").transform.Find(path_TF + "Text_Trap/Text_Value").GetComponent<Text>();
        Text_Fail[5] = GameObject.Find("UI").transform.Find(path_TF + "Text_Result").GetComponent<Text>();
        Text_Fail[6] = GameObject.Find("UI").transform.Find("Panel_Fail/Content/Image_Gold/Text_Value").GetComponent<Text>();
        Text_Fail[7] = GameObject.Find("UI").transform.Find("Panel_Fail/Content/Image_Speacial/Text_Value").GetComponent<Text>();

        Text_Success = new Text[8];
        string path_TS = "Panel_Success/Content/Result_Unit/";
        Text_Success[0] = GameObject.Find("UI").transform.Find(path_TS + "Text_Stage/Text_Value").GetComponent<Text>();
        Text_Success[1] = GameObject.Find("UI").transform.Find(path_TS + "Text_Stage_NoHit/Text_Value").GetComponent<Text>();
        Text_Success[2] = GameObject.Find("UI").transform.Find(path_TS + "Text_Difficult/Text_Value").GetComponent<Text>();
        Text_Success[3] = GameObject.Find("UI").transform.Find(path_TS + "Text_Combo/Text_Value").GetComponent<Text>();
        Text_Success[4] = GameObject.Find("UI").transform.Find(path_TS + "Text_Trap/Text_Value").GetComponent<Text>();
        Text_Success[5] = GameObject.Find("UI").transform.Find(path_TS + "Text_Result").GetComponent<Text>();
        Text_Success[6] = GameObject.Find("UI").transform.Find("Panel_Success/Content/Image_Gold/Text_Value").GetComponent<Text>();
        Text_Success[7] = GameObject.Find("UI").transform.Find("Panel_Success/Content/Image_Speacial/Text_Value").GetComponent<Text>();

        player_opacity = GameManager.Play.Player.GetComponent<SpriteRenderer>().color;

        MC = GameObject.Find("Main Camera").GetComponent<Camera>();

        Ani_Player = GameManager.Play.Player.GetComponent<Animator>();
        Rig_Player = GameManager.Play.Player.GetComponent<Rigidbody2D>();
        BAR_EXP();
        BAR_HP();
    }

    // 점프 버튼
    void OnJump(PointerEventData data)
    {
        if (GameManager.Play.DS.jumpNow < GameManager.Play.Status.ability.MAX_JUMP.value)
        {
            Rig_Player.velocity = new Vector3(0, GameManager.Play.Status.ability.JUMP.value, 0);
            GameManager.Play.DS.jumpNow += 1;
            Ani_Player.SetTrigger("Jumping");
            Ani_Player.SetBool("Landing", false);
        }
    }

    // 하강 버튼
    void OnDown(PointerEventData data)
    {
        if (GameManager.Play.Player.transform.position.y > -2.62f)
        {
            Rig_Player.velocity = new Vector3(0, -1 * GameManager.Play.Status.ability.DOWN.value, 0);
        }
    }

    // 스킬 사용.
    public void Use_Active()
    {
        if (GameManager.Play.DS.activeUse < GameManager.Play.DC.activeMax)
        {
            GameManager.Play.DS.activeUse += 1;
            switch (GameManager.Play.Status.ACTIVE)
            {
                case Active.ACTIVE_CODE.None:
                    break;
                case Active.ACTIVE_CODE.Defense:
                    gameObject.GetComponent<Active>().OnDefense();
                    StartCoroutine(CoolTime(15));
                    break;
                case Active.ACTIVE_CODE.Flash:
                    StartCoroutine(gameObject.GetComponent<Active>().OnFlash());
                    StartCoroutine(CoolTime(15));
                    break;
                case Active.ACTIVE_CODE.Ghost:
                    StartCoroutine(gameObject.GetComponent<Active>().OnGhost());
                    StartCoroutine(CoolTime(15));
                    break;
                case Active.ACTIVE_CODE.Heal:
                    gameObject.GetComponent<Active>().OnHeal();
                    //체력바 UI UPDATE!
                    StartCoroutine(CoolTime(20));
                    break;
                case Active.ACTIVE_CODE.Item_Change:
                    gameObject.GetComponent<Active>().Item_Change();
                    StartCoroutine(CoolTime(20));
                    break;
                case Active.ACTIVE_CODE.Change_Coin:
                    gameObject.GetComponent<Active>().Change_Coin();
                    StartCoroutine(CoolTime(20));
                    break;
                case Active.ACTIVE_CODE.The_World:
                    StartCoroutine(gameObject.GetComponent<Active>().OnSlow());
                    StartCoroutine(CoolTime(10));
                    break;
                case Active.ACTIVE_CODE.Multiple_Combo:
                    StartCoroutine(gameObject.GetComponent<Active>().MultiCombo());
                    StartCoroutine(CoolTime(5));
                    break;
                case Active.ACTIVE_CODE.Fly:
                    StartCoroutine(gameObject.GetComponent<Active>().OnFly());
                    StartCoroutine(CoolTime(15));
                    break;

                    /* case Active.ID.Run:
                           StartCoroutine(OnRun);
                           StartCoroutine(CoolTime(15));
                           + 경험치 바 UI 넣어주세요~
                           break;
                            */
            }
            Image_Skill.fillAmount = 0;
        }

    }
    public IEnumerator CoolTime(float t)
    {
        if (GameManager.Play.DS.activeUse < GameManager.Play.DC.activeMax)
        {
            for (float i = 0; i <= t; i++)
            {
                if (i == t)
                {
                    Button_Skill.interactable = true;
                }
                Image_Skill.fillAmount += (1.0f / t);
                yield return new WaitForSeconds(1);
            }
        }
    }
   
    //체력 바 
    public void BAR_HP()
    {
        Image_HpBar.fillAmount = GameManager.Play.Status.ability.HP.value / GameManager.Play.Status.ability.MAX_HP.value;
    }

    // 경험치 바
    public void BAR_EXP()
    {
        Image_ExpBar.fillAmount = (GameManager.Play.DC.expNow / GameManager.Play.DC.expNeed[GameManager.Play.DC.lv]);
    }

    public void OnPause()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            Time.timeScale = 0;
            GameObject.Find("UI").transform.Find("Panel_Pause").gameObject.SetActive(true);

            ShowPassive();
            // 현재 획득한 패시브 아이템 내역 보여주기.
        }
    }

    public void OffPause()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            Time.timeScale = 1;
            GameObject.Find("UI").transform.Find("Panel_Pause").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Text_Count").gameObject.SetActive(true);
            StartCoroutine("Count_Time");
        }
    }

    public IEnumerator Count_Time()
    {
        float temp_speed = 0.0f;
        for (int i = 3; i > -1; i--)
        {
            if (i == 3)
            {
                temp_speed = GameManager.Play.Status.ability.SPEED.value;
                GameManager.Play.Status.ability.SPEED.value = 0.0f;
            }
            Text_Count.text = $"{i}";
            if (i == 0)
            {
                Count.SetActive(false);
                GameManager.Play.Status.ability.SPEED.value = temp_speed;
            }


            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator ComboEffect()
    {
        for (int i = 0; i <= 100; i++)
        {
            if (SceneManager.GetActiveScene().name == "Play")
            {
                if (i == 0)
                {
                    combo.transform.localScale = new Vector3(1.5f, 1.5f, 0);
                }
                else
                {
                    combo.transform.localScale -= new Vector3(0.005f, 0.005f, 0);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 트랩 파괴 시 콤보 적용
    public void Trap_Combo(Transform other)
    {
        InterAction.MakeCoin(other.transform);
        Destroy(other.gameObject); // 그 장애물을 파! 괘!
        int per = Random.Range(0, 100);
        if (per < GameManager.Play.Status.ability.LUK.value) // 행운 수치에 따라 크리티컬 적용.
        {
            GameManager.Play.DC.combo += GameManager.Play.DC.comboMulti * 2;
            Text_Combo.color = Color.red;
            Text_ComboCNT.color = Color.red;
            Text_Combo.text = "ComboX2";
            Text_ComboCNT.text = $"{GameManager.Play.DC.combo}!";
            if (Coroutine_Combo != null)
            {
                StopCoroutine(Coroutine_Combo);
                Coroutine_Combo = ComboEffect();
                StartCoroutine(Coroutine_Combo);
            }
            else
            {
                Coroutine_Combo = ComboEffect();
                StartCoroutine(Coroutine_Combo);
            }
        }
        else
        {
            GameManager.Play.DC.combo += GameManager.Play.DC.comboMulti;
            Text_Combo.color = Color.white;
            Text_ComboCNT.color = Color.white;
            Text_Combo.text = "Combo";
            Text_ComboCNT.text = $"{GameManager.Play.DC.combo}!";
            if (Coroutine_Combo != null)
            {
                StopCoroutine(Coroutine_Combo);
                Coroutine_Combo = ComboEffect();
                StartCoroutine(Coroutine_Combo);
            }
            else
            {
                Coroutine_Combo = ComboEffect();
                StartCoroutine(Coroutine_Combo);
            }
        }
    }
    // 피격시 카메라 흔들
    public IEnumerator Cam_Hit()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                MC.orthographicSize = 4.75f;
            }
            else
            {
                MC.orthographicSize = 5.0f;
            }
            yield return new WaitForSeconds(0.0075f);
        }
    }

    // 타격시 카메라 흔들
    public IEnumerator Cam_ATT()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                MC.orthographicSize = 4.75f;
            }
            else
            {
                MC.orthographicSize = 5.0f;
            }
            yield return new WaitForSeconds(0.0075f);
        }
    }

    // 게임 오버.
    public IEnumerator GameOver()
    {
        float temp_speed = GameManager.Play.Status.ability.SPEED.value;
        GameManager.Play.Status.ability.SPEED.value = 0;
        Button_Jump.interactable = false;
        Button_Down.interactable = false;

        Count.SetActive(true);
        for (int i = 5; i > 0; i--)
        {
            Text_Count.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Count.SetActive(false);
        OnFail();
    }

    public IEnumerator ReGame()
    {
        float temp_speed = GameManager.Play.Status.ability.SPEED.value;
        GameManager.Play.Status.ability.SPEED.value = 0;
        Count.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            Text_Count.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Count.SetActive(false);
        GameManager.Play.Status.ability.SPEED.value = temp_speed;
    }

    // 실패 시 실패 족자 열기.
    // 애니메이션 추가 후 코드 수정하기.
    public void OnFail()
    {
        ShowFail();
        Fail.SetActive(true);
        Fail_Content.SetActive(true);
    }

    public void OnSuccess()
    {
        ShowSuccess();
        Success.SetActive(true);
        Success_Content.SetActive(true);
    }

    // 얻은 패시브 보여주기...
    public void ShowPassive()
    {
        for (int i = 0; i < 14; i++)
        {
            if (i < GameManager.Play.DC.passiveGet.Count)
            {
                Color temp = GetPassives[i].color;
                temp.a = 1;
                GetPassives[i].color = temp;
                GetPassives[i].sprite = Passive.Passive_Sprites[(int)GameManager.Play.DC.passiveGet[i]];
            }
            else
            {
                Color temp = GetPassives[i].color;
                temp.a = 0;
                GetPassives[i].color = temp;
            }
        }
    }

    //실패시 족자에 보여질 내역.
    public void ShowFail()
    {
        int result = GameManager.Play.DC.passTrap * ((GameManager.Play.DC.stage - GameManager.Play.DC.noHitStage) + (2 * GameManager.Play.DC.noHitStage)) * (GameManager.Data.Preset.Difficult + 1) + GameManager.Play.DC.comboMax * 10000 + (int)GameManager.Play.DC.goldNow * 10; // 게임 결과점수.
        int money_speacial = 0; // 특수재화 얻는 갯수.
        Text_Fail[0].text = GameManager.Play.DC.stage.ToString();
        Text_Fail[1].text = $"{GameManager.Play.DC.noHitStage}";
        Text_Fail[2].text = Difficulty.DIFF_CODE[GameManager.Data.Preset.Difficult];
        Text_Fail[3].text = $"{GameManager.Play.DC.comboMax}";
        Text_Fail[4].text = $"{GameManager.Play.DC.passTrap}";
        Text_Fail[5].text = $"{result}";
        Text_Fail[6].text = $"{(int)GameManager.Play.DC.goldNow}G";
        Text_Fail[7].text = $"{money_speacial}개";

        Image_Fail[0].sprite = Sprite_DeadCharacter[(int)GameManager.Data.Preset.Character];
        Image_Fail[1].sprite = Sprite_DeadCause[GameManager.Play.DC.lastHit];

        GameManager.Data.Money.Gold += (int)GameManager.Play.DC.goldNow;
        GameManager.Data.Money.Speacial[0] += money_speacial;

    }

    public void ShowSuccess()
    {
        int result = GameManager.Play.DC.passTrap * ((GameManager.Play.DC.stage - GameManager.Play.DC.noHitStage) + (2 * GameManager.Play.DC.noHitStage)) * (GameManager.Data.Preset.Difficult + 1) + GameManager.Play.DC.comboMax * 10000 + (int)GameManager.Play.DC.goldNow * 10; // 게임 결과점수.
        int money_speacial =result/10; // 특수재화 얻는 갯수.
        Text_Success[0].text = $"{GameManager.Play.DC.stage}";
        Text_Success[1].text = $"{GameManager.Play.DC.noHitStage}";
        Text_Success[2].text = Difficulty.DIFF_CODE[GameManager.Data.Preset.Difficult];
        Text_Success[3].text = $"{GameManager.Play.DC.comboMax}";
        Text_Success[4].text = $"{GameManager.Play.DC.passTrap}";
        Text_Success[5].text = $"{result}";
        Text_Success[6].text = $"{(int)GameManager.Play.DC.goldNow}G";
        Text_Success[7].text = $"{money_speacial}개";

        Image_Success[0].sprite = Sprite_DeadCharacter[(int)GameManager.Data.Preset.Character];

        GameManager.Data.Money.Gold += (int)GameManager.Play.DC.goldNow;
        GameManager.Data.Money.Speacial[0] += money_speacial;
    }

    public void Stop_UiPlay()
    {
        StopAllCoroutines();
    }

}
