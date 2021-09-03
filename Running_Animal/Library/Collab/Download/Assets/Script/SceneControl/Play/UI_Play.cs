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
    public Image Cool_Banana;
    public Image Cool_BearTrap;
    public Image Cool_Active;
    public GameObject GO_Stage;
    public Image Image_Stage;
    public Sprite[] Stages;

    public GameObject combo;
    public TextMeshProUGUI Text_Combo;
    public GameObject combo_Count;
    public TextMeshProUGUI Text_ComboCNT;
    public GameObject Cam;
    public GameObject Miss;

    public GameObject Count;

    public GameObject Pause;
    public Button PauseYes;
    public Button PauseNo;
    public Image[] GetPassives;

    public GameObject Skill;
    public Button Button_Skill;
    public Image Image_Skill;
    public Button Button_Jump;
    public Button Button_Down;


    public EventTrigger.Entry Entry_Jump;
    public EventTrigger.Entry Entry_Down;
    public EventTrigger Event_jump;
    public EventTrigger Event_down;

    public TextMeshProUGUI Text_gold;
    public TextMeshProUGUI Text_Count;

    public Color player_opacity;

    public Camera MC;

    public Animator Ani_Player;
    public Rigidbody2D Rig_Player;

    public InterAction InterAction;
    public Passive Passive;
    public Active Active;
    IEnumerator Coroutine_Combo;

    AudioClip clip;
    AudioClip clip2;
    AudioClip clip3;
    AudioClip clip4;
    AudioClip clip5;
    AudioClip clip6;

    private void Start()
    {
        LoadSound();
    }
    public void SetUI()
    {
        Passive = gameObject.GetComponent<Passive>();
        InterAction = gameObject.GetComponent<InterAction>();
        Active = gameObject.GetComponent<Active>();
        Image_HpBar = GameObject.Find("UI/Bar_HP/Gage_HP").GetComponent<Image>();
        Image_ExpBar = GameObject.Find("UI/Bar_EXP/Gage_EXP").GetComponent<Image>();
        GO_Stage = GameObject.Find("UI").transform.Find("Image_Stage").gameObject;
        GO_Stage.SetActive(false);
        Image_Stage = GameObject.Find("UI").transform.Find("Image_Stage").GetComponent<Image>();
        Stages = Resources.LoadAll<Sprite>("Image/GUI/Play/Stage");

        Cool_Banana = GameObject.Find("UI/CoolTime/Banana").GetComponent<Image>();
        Cool_BearTrap = GameObject.Find("UI/CoolTime/BearTrap").GetComponent<Image>();
        Cool_Active = GameObject.Find("UI/CoolTime/Active").GetComponent<Image>();
        Cool_Banana.fillAmount = 0;
        Cool_BearTrap.fillAmount = 0;
        Cool_Active.fillAmount = 0;

        combo = GameObject.Find("UI/Text_Combo");
        Text_Combo = combo.GetComponent<TextMeshProUGUI>();
        combo_Count = GameObject.Find("UI/Text_Combo/Text_Count");
        Text_ComboCNT = combo_Count.GetComponent<TextMeshProUGUI>();
        Text_ComboCNT.text = GameManager.Play.DC.combo.ToString();
        Cam = GameObject.Find("Main Camera");
        Miss = Resources.Load<GameObject>("Item/Miss");

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
        
        Skill = GameObject.Find("UI/Button_Skill").gameObject;
        Image_Skill = Skill.GetComponent<Image>();
        Image_Skill.sprite = Active.Active_Sprites[(int)GameManager.Play.Status.ACTIVE];
        Button_Skill = Skill.GetComponent<Button>();
        Button_Skill.interactable = true;
        Button_Skill.onClick.AddListener(Use_Active);
        Button_Jump = GameObject.Find("UI/Button_Jump").GetComponent<Button>();
        Button_Down = GameObject.Find("UI/Button_Down").GetComponent<Button>();

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
        Text_gold.text = $"{GameManager.Play.DC.goldNow}G";

        Text_Count = Count.GetComponent<TextMeshProUGUI>();

        player_opacity = GameManager.Play.Player.GetComponent<SpriteRenderer>().color;

        MC = GameObject.Find("Main Camera").GetComponent<Camera>();

        Ani_Player = GameManager.Play.Player.GetComponent<Animator>();
        Rig_Player = GameManager.Play.Player.GetComponent<Rigidbody2D>();
        BAR_EXP();
        BAR_HP();
    }
    public void show()
    {

        Text_ComboCNT.text = GameManager.Play.DC.combo.ToString();
        Image_Skill.sprite = Active.Active_Sprites[(int)GameManager.Play.Status.ACTIVE];
        Text_gold.text = $"{GameManager.Play.DC.goldNow}G";
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
            GameManager.Sound.SFXPlay(clip);
        }
    }

    // 하강 버튼
    void OnDown(PointerEventData data)
    {
        if (GameManager.Play.Player.transform.position.y > -2.62f)
        {
            Rig_Player.velocity = new Vector3(0, -1 * GameManager.Play.Status.ability.DOWN.value, 0);
            GameManager.Sound.SFXPlay(clip2);
        }
    }

    // 스킬 사용.
    public void Use_Active()
    {
        if (GameManager.Play.DS.activeUse < GameManager.Play.DC.activeMax)
        {
            Button_Skill.interactable = false;
            
            GameManager.Play.DS.activeUse += 1;
            switch (GameManager.Play.Status.ACTIVE)
            {
                case Active.ACTIVE_CODE.None:
                    Button_Skill.interactable = true;
                    break;
                case Active.ACTIVE_CODE.Defense:
                    Active.OnDefense();
                    StartCoroutine(CoolTime(10));
                    break;
                case Active.ACTIVE_CODE.Flash:
                    StartCoroutine(Active.OnFlash());
                    StartCoroutine(CoolTime(10));
                    break;
                case Active.ACTIVE_CODE.Ghost:
                    StartCoroutine(Active.OnGhost());
                    StartCoroutine(CoolTime(10));
                    StartCoroutine(Left_Active(5, (int)Active.ACTIVE_CODE.Ghost));
                    break;
                case Active.ACTIVE_CODE.Heal:
                    Active.OnHeal();
                    //체력바 UI UPDATE!
                    StartCoroutine(CoolTime(10));
                    break;
                case Active.ACTIVE_CODE.Item_Change:
                    Active.Item_Change();
                    break;
                case Active.ACTIVE_CODE.Change_Coin:
                    Active.Change_Coin();
                    StartCoroutine(CoolTime(10));
                    break;
                case Active.ACTIVE_CODE.The_World:
                    StartCoroutine(Active.OnSlow());
                    StartCoroutine(CoolTime(10));
                    StartCoroutine(Left_Active(2.5f, (int)Active.ACTIVE_CODE.The_World));
                    break;
                case Active.ACTIVE_CODE.Multiple_Combo:
                    StartCoroutine(Active.MultiCombo());
                    StartCoroutine(CoolTime(10));
                    break;
                case Active.ACTIVE_CODE.Fly:
                    StartCoroutine(Active.OnFly());
                    StartCoroutine(CoolTime(10));
                    StartCoroutine(Left_Active(5, (int)Active.ACTIVE_CODE.Fly));
                    break;

                    /* case Active.ID.Run:
                           StartCoroutine(OnRun);
                           StartCoroutine(CoolTime(15));
                           + 경험치 바 UI 넣어주세요~
                           break;
                            */
            }
        }

    }
    public IEnumerator ShowStage(int idx)
    {
        //현재 스테이지가 어딘지 보여준다.
        GO_Stage.SetActive(true);
        Image_Stage.sprite = Stages[idx];
        Color temp;
        float time = 0;
        temp = Image_Stage.color;
        temp.a = 1;
        Image_Stage.color = temp;

        while (time < 5)
        {
            time += Time.deltaTime;
            temp = Image_Stage.color;
            temp.a -= (Time.deltaTime / 5);
            Image_Stage.color = temp;

            yield return null;
        }
        GO_Stage.SetActive(false);

    }

    public IEnumerator CoolTime(float t)
    {
        Image_Skill.fillAmount = 0;
        float time = 0;
        if (GameManager.Play.DS.activeUse < GameManager.Play.DC.activeMax)
        {
            while(time < t)
            {
                Image_Skill.fillAmount = (time / t);
                time += Time.deltaTime;
                yield return null;
            }
        }
        Button_Skill.interactable = true;
    }
   
    public IEnumerator Left_Active(float t, int idx)
    {
        // 액티브스킬 남은시간 표시.
        float time = 0;
        Cool_Active.sprite = Active.Active_Sprites[idx];
        Cool_Active.fillAmount = 1;
        while (time <= t)
        {
            time += Time.deltaTime;
            Cool_Active.fillAmount -= (Time.deltaTime / t);
            yield return null;
        }
        Cool_Active.fillAmount = 0;
    }
    //체력 바 
    public void BAR_HP()
    {
        Image_HpBar.fillAmount = GameManager.Play.Status.ability.HP.value / GameManager.Play.Status.ability.MAX_HP.value;
    }

    // 경험치 바
    public void BAR_EXP()
    {
        Image_ExpBar.fillAmount = (GameManager.Play.DS.expNow / GameManager.Play.DC.expNeed[GameManager.Play.DC.lv]);
    }
    public void RemoveButton()
    {
        Event_jump.triggers.Remove(Entry_Jump);
        Entry_Jump.callback.RemoveAllListeners();

        Event_down.triggers.Remove(Entry_Down);
        Entry_Down.callback.RemoveAllListeners();
        Button_Jump.interactable = false;
        Button_Down.interactable = false;
    }

    public void RemakeButton()
    {
        Entry_Jump.callback.AddListener((data) => { OnJump((PointerEventData)data); });
        Event_jump.triggers.Add(Entry_Jump);

        Entry_Down.callback.AddListener((data) => { OnDown((PointerEventData)data); });
        Event_down.triggers.Add(Entry_Down);
        Button_Jump.interactable = true;
        Button_Down.interactable = true;
    }
    public void OnPause()
    {
        if (SceneManager.GetActiveScene().name == "Play" || SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "Tutorial3")
        {
            GameManager.Sound.SFXPlay(clip5);
            GameObject.Find("UI").transform.Find("Panel_Pause").gameObject.SetActive(true);

            Event_jump.triggers.Remove(Entry_Jump);
            Entry_Jump.callback.RemoveAllListeners();

            Event_down.triggers.Remove(Entry_Down);
            Entry_Down.callback.RemoveAllListeners();

            Button_Skill.interactable = false;

            Time.timeScale = 0;
            ShowPassive();
            // 현재 획득한 패시브 아이템 내역 보여주기.
        }
    }

    public void OffPause()
    {
        if (SceneManager.GetActiveScene().name == "Play")
            GameManager.Sound.SFXPlay(clip4);
        {
            GameObject.Find("UI").transform.Find("Panel_Pause").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Text_Count").gameObject.SetActive(true);
            StartCoroutine(Count_Time());
        }
    }

    public IEnumerator Count_Time()
    {
        for (int i = 3; i > -1; i--)
        {
            Text_Count.text = $"{i}";
            GameManager.Sound.SFXPlay(clip3);
            if (i == 0)
            {
                Count.SetActive(false);

                Entry_Jump.callback.AddListener((data) => { OnJump((PointerEventData)data); });
                Event_jump.triggers.Add(Entry_Jump);

                Entry_Down.callback.AddListener((data) => { OnDown((PointerEventData)data); });
                Event_down.triggers.Add(Entry_Down);

                Button_Skill.interactable = true;
                Time.timeScale = 1;
            }
            yield return new WaitForSecondsRealtime(1.0f);
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
        GameManager.Play.DC.passTrap += 1;
        int per = Random.Range(0, 100);
        if (per < GameManager.Play.Status.ability.LUK.value) // 행운 수치에 따라 크리티컬 적용.
        {
            GameManager.Play.DC.combo += GameManager.Play.DC.comboMulti *  GameManager.Play.DS.AC_multicombo * 2;
            GameManager.Sound.SFXPlay(clip6);
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
            GameManager.Play.DC.combo += GameManager.Play.DC.comboMulti * GameManager.Play.DS.AC_multicombo;
            GameManager.Sound.SFXPlay(clip6);
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
            GameManager.Sound.SFXPlay(clip3);
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
        gameObject.GetComponent<LoadScene>().OnFail();
    }

    public void OnSuccess()
    {
        gameObject.GetComponent<LoadScene>().OnSuccess();
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
                //Color temp = GetPassives[i].color;
                //temp.a = 0;
                //GetPassives[i].color = temp;
            }
        }
    }

    public void Stop_UiPlay()
    {
        StopAllCoroutines();
    }
    void LoadSound()
    {
        clip = Resources.Load<AudioClip>("Sound/Play/006_Play");
        clip2 = Resources.Load<AudioClip>("Sound/Play/008_Play");
        clip3 = Resources.Load<AudioClip>("Sound/Common/009_Count");
        clip4 = Resources.Load<AudioClip>("Sound/Common/000_Manu_Sound");
        clip5 = Resources.Load<AudioClip>("Sound/Common/004_Manu_Sound2");
        clip6 = Resources.Load<AudioClip>("Sound/Play/013_Play");
    }
}
