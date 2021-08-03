using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;// 유일성이 보장.
    public static GameManager Instance { get { Init(); return s_Instance; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    //Prefabs
    GameObject coin;
    public static GameObject combo;
    public static GameObject combo_Count;

    public static Image HP_Bar;
    public static Image EXP_Bar;

    IEnumerator Coroutine_Combo;

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (SceneManager.GetActiveScene().name == "Play") 
            {
                Time.timeScale = 0;
                GameObject.Find("UI").transform.Find("Panel_Pause").gameObject.SetActive(true);
            }
        }
    }
    static void Init()
    {
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            //만약 Manager Object를 생성 한 적이 없다.
            go = new GameObject { name = "@Managers" };
            go.AddComponent<GameManager>();
        };
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<GameManager>();

        //Load Prefabs
    }


    void Awake()
    {
        Init();
        string path = Application.persistentDataPath + "/save.xml";
        coin = Resources.Load<GameObject>("Item/Coin");
        if (System.IO.File.Exists(path)) {
            Debug.Log("아아...소환되었따");
            Load(); }

        if(SceneManager.GetActiveScene().name == "Play")
        {
            Debug.Log("글자 불러오기 완료");
            combo = GameObject.Find("UI/Text_Combo");
            combo_Count = GameObject.Find("UI/Text_Combo/Text_Count");
            Coroutine_Combo = ComboEffect();
            HP_Bar = GameObject.Find("UI/Bar_HP/Gage_HP").GetComponent<Image>();
            EXP_Bar = GameObject.Find("UI/Bar_EXP/Gage_EXP").GetComponent<Image>();
            BAR_HP();
            BAR_EXP();
        }
    }

    public void OnPause()
    {
        Debug.Log("onpause");
        if (SceneManager.GetActiveScene().name == "Play")
        {
            Time.timeScale = 0;
            GameObject.Find("UI").transform.Find("Panel_Pause").gameObject.SetActive(true);
        }
    }

    public void OffPause()
    {
        Debug.Log("offpause");
        if (SceneManager.GetActiveScene().name == "Play")
        {
            GameObject.Find("UI").transform.Find("Panel_Pause").gameObject.SetActive(false);
            GameObject.Find("UI").transform.Find("Text_Count").gameObject.SetActive(true);
            StartCoroutine("Count_Time");
        }
    }

    IEnumerator Count_Time()
    {
        float temp_speed = 0.0f;
        for (int i = 3; i > -1; i--)
        {
            if(i == 3)
            {
                temp_speed = Data.speed;
                Data.speed = 0.0f;
                Time.timeScale = 1;
            }
            GameObject.Find("UI/Text_Count").GetComponent<Text>().text = $"{i}";
            if (i == 0)
            {
                GameObject.Find("UI/Text_Count").SetActive(false);
                Data.speed = temp_speed;
            }
            

            yield return new WaitForSeconds(1.0f);
        }
    }

    public List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }
    public void Save()
    {
        Data saveData = new Data();

        //재화
        saveData.Cash = Data.Cash;
        saveData.Gold = Data.Gold;
        saveData.Money_Forest = Data.Money_Forest;
        saveData.Money_Desert = Data.Money_Desert;
        saveData.Money_Arctic = Data.Money_Arctic;

        //테마
        saveData.Theme = Data.Theme;
        
        //캐릭터 구매 관리
        saveData.Buy_Character = Data.Buy_Character;
        saveData.Cost_Character = Data.Cost_Character;
        saveData.Now_Character = Data.Now_Character;

        //캐릭터 스탯 관리
        saveData.Character_STAT = Data.Character_STAT;
        
        // 게임 플레잉
        saveData.play_gold = Data.play_gold;
        saveData.multi_coin = Data.multi_coin;
        saveData.active = Data.active;
        saveData.passive = Data.passive;
        saveData.speed = Data.speed;
        saveData.jump = Data.jump;
        saveData.down = Data.down;
        saveData.defense = Data.defense;
        saveData.max_hp = Data.max_hp;
        saveData.hp = Data.hp;
        saveData.damage = Data.damage;
        saveData.EXP = Data.EXP;
        saveData.now_Exp = Data.now_Exp;
        saveData.lv = Data.lv;
        saveData.lvup = Data.lvup;
        saveData.stage = Data.stage;
        saveData.combo = Data.combo;
        saveData.max_combo = Data.max_combo;
        saveData.multi_combo = Data.multi_combo;
        saveData.max_jump = Data.max_jump;
        saveData.luck = Data.luck;
        saveData.max_active = Data.max_active;
        saveData.use_active = Data.use_active;
        saveData.dodge_time = Data.dodge_time;

        // 패시브
        saveData.magnet = Data.magnet;
        saveData.buwhal = Data.buwhal;
        saveData.auto_jump = Data.auto_jump;
        saveData.random_god = Data.random_god;
        saveData.auto_restore = Data.auto_restore;
        saveData.passive_active = Data.passive_active;
        saveData.passive_buwhal = Data.passive_buwhal;

        saveData.playing = Data.playing;
        saveData.restore_eff = Data.restore_eff;

        saveData.pattern = Data.pattern;

        saveData.change_chance = Data.change_chance;

        // 재능 관련
        saveData.Talent_HP = Data.Talent_HP;
        saveData.Talent_DEF = Data.Talent_DEF;
        saveData.Talent_LUK = Data.Talent_LUK;
        saveData.Talent_Restore = Data.Talent_Restore;
        saveData.Talent_LV = Data.Talent_LV;

        // 시작 전 구매 아이템.
        saveData.Pre_Shield = Data.Pre_Shield;
        saveData.Pre_100 = Data.Pre_100;
        saveData.Pre_300 = Data.Pre_300;
        saveData.Exp_run = Data.Exp_run;

        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<Data>(saveData, path);

        Debug.Log("SAVE!");
        Debug.Log(path);
    }

    public void Load() 
    {
        Data saveData = new Data();
        string path = Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<Data>(path);

        //재화
        Data.Cash = saveData.Cash;
        Data.Gold = saveData.Gold;
        Data.Money_Forest = saveData.Money_Forest;
        Data.Money_Desert = saveData.Money_Desert;
        Data.Money_Arctic = saveData.Money_Arctic;

        //테마
        Data.Theme = saveData.Theme;
        
        //캐릭터 구매 관련
        Data.Buy_Character = saveData.Buy_Character;
        Data.Cost_Character = saveData.Cost_Character;
        Data.Now_Character = saveData.Now_Character;

        //캐릭터 스탯 관리
        Data.Character_STAT = saveData.Character_STAT;

        //플레잉
        Data.play_gold = saveData.play_gold;
        Data.multi_coin = saveData.multi_coin;
        Data.active = saveData.active;
        Data.passive = saveData.passive;
        Data.speed = saveData.speed;
        Data.jump = saveData.jump;
        Data.down = saveData.down;
        Data.defense = saveData.defense;
        Data.max_hp = saveData.max_hp;
        Data.hp = saveData.hp;
        Data.damage = saveData.damage;
        Data.EXP = saveData.EXP;
        Data.now_Exp = saveData.now_Exp;
        Data.lv = saveData.lv;
        Data.lvup = saveData.lvup;
        Data.stage = saveData.stage;
        Data.combo = saveData.combo;
        Data.max_combo = saveData.max_combo;
        Data.multi_combo = saveData.multi_combo;
        Data.max_jump = saveData.max_jump;
        Data.luck = saveData.luck;
        Data.max_active = saveData.max_active;
        Data.use_active = saveData.use_active;
        Data.dodge_time = saveData.dodge_time;
        
        //패시브
        Data.magnet = saveData.magnet;
        Data.buwhal = saveData.buwhal;
        Data.auto_jump = saveData.auto_jump;
        Data.random_god = saveData.random_god;
        Data.auto_restore = saveData.auto_restore;
        Data.passive_active = saveData.passive_active;
        Data.passive_buwhal = saveData.passive_buwhal;



        Data.playing = saveData.playing;
        Data.restore_eff = saveData.restore_eff;

        Data.pattern = saveData.pattern;

        Data.change_chance = saveData.change_chance;

        // 재능 관련
        Data.Talent_HP = saveData.Talent_HP;
        Data.Talent_DEF = saveData.Talent_DEF;
        Data.Talent_LUK = saveData.Talent_LUK;
        Data.Talent_Restore = saveData.Talent_Restore;
        Data.Talent_LV = saveData.Talent_LV;

        // 시작 전 구매 아이템
        Data.Pre_Shield = saveData.Pre_Shield;
        Data.Pre_100 = saveData.Pre_100;
        Data.Pre_300 = saveData.Pre_300;
        Data.Exp_run = saveData.Exp_run;

        Debug.Log("LOAD!");

    }
    // 체력 바
    public void BAR_HP()
    {
        HP_Bar.fillAmount = GameManager.Data.hp / GameManager.Data.max_hp;
    }

    // 경험치 바
    public void BAR_EXP()
    {
        Debug.Log(GameManager.Data.now_Exp / GameManager.Data.EXP[GameManager.Data.lv]);
        EXP_Bar.fillAmount = (GameManager.Data.now_Exp / GameManager.Data.EXP[GameManager.Data.lv]);
    }
    // 코인 생성...
    public void MakeCoin(Transform other)
    {
        Instantiate(coin, new Vector3(other.position.x + 0.5f, other.position.y + 4.0f, other.position.z), Quaternion.identity);
    }

    // 트랩 파괴 시 콤보 적용
    public void Trap_Combo(Transform other)
    {
        MakeCoin(other.transform);
        Destroy(other.gameObject); // 그 장애물을 파! 괘!
        int per = Random.Range(0, 100);
        if (per < Data.luck) // 행운 수치에 따라 크리티컬 적용.
        {
            Data.combo += Data.multi_combo * 2;
            combo.GetComponent<Text>().color = Color.red;
            combo_Count.GetComponent<Text>().color = Color.red;
            combo.GetComponent<Text>().text = "ComboX2";
            combo_Count.GetComponent<Text>().text = $"{Data.combo}!";
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
            Data.combo += Data.multi_combo;
            combo.GetComponent<Text>().color = Color.white;
            combo_Count.GetComponent<Text>().color = Color.white;
            combo.GetComponent<Text>().text = "Combo";
            combo_Count.GetComponent<Text>().text = $"{Data.combo}!";
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

    IEnumerator ComboEffect()
    {
        for(int i=0; i<=100; i++)
        {
            if(i == 0)
            {
                combo.transform.localScale = new Vector3(1.5f, 1.5f, 0);
            }
            else
            {
                combo.transform.localScale -= new Vector3(0.005f, 0.005f, 0);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }


    private void OnApplicationQuit()
    {
        Data.playing = false;
        Data.active = DataManager.Active_Skil.None;
        Time.timeScale = 1;

        Data.lvup = false; // 레벨업 여부, true일 시 다음 장애물은 레벨업하는 장소로.
        Data.lv = 1; // 현재 레벨 최대 0~12렙까지
        Data.now_Exp = 0; // 현재 경험치 
        Data.stage = 0; // 스테이지
        Data.multi_coin = 0; // 코인 획득량 증가율
        Data.max_hp = 100.0f; // 최대 체력
        Data.hp = 100.0f;
        Data.speed = 8.0f; // 현재 속도
        Data.jump = 10.0f; // 현재 점프력
        Data.down = 20.0f; // 현재 하강 속도
        Data.defense = 0.0f; // 현재 방어력
        Data.damage = 20.0f; // 현재 피격 데미지
        Data.combo = 0; // 게임 진행 중 콤보 
        Data.max_combo = 0;
        Data.multi_combo = 1; // 콤보 배율
        Data.max_jump = 2; // 최대 점프 가능 횟수
        Data.luck = 0; // 행운 (회피와, 콤보 크리티컬에 기인한다)
        Data.max_active = 1; // 액티브 스킬 최대 사용가능 횟수
        Data.use_active = 0; // 액티브 스킬 현대 사용 횟수
        Data.dodge_time = 12; // 피격시 무적 시간 길이. default 12
        Data.restore_eff = 1.0f;

        Data.magnet = false; // 패시브 자석버그 유무
        Data.buwhal = 0; // 패시브 부활 유무
        Data.auto_jump = false; //패시브 오토점프 유무
        Data.random_god = false; // 패시브 작은 확률로 무적 유무
        Data.auto_restore = false;
        Data.change_chance = 0;
        Data.passive_active = false; // 패시브 액티브 사용횟수 + 1
        Data.passive_buwhal = false;

        // 시작 전 구매 아이템 초기화
        Data.Pre_Shield = false;
        Data.Pre_100 = false;
        Data.Pre_300 = false;
        Data.Exp_run = 0;

    Data.pattern = new List<int>();
        Data.Gold += (int)Data.play_gold;
        Data.play_gold = 0;
        Save();
    }
}
