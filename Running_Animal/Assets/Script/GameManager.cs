using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;// ���ϼ��� ����.
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
            //���� Manager Object�� ���� �� ���� ����.
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
            Debug.Log("�ƾ�...��ȯ�Ǿ���");
            Load(); }

        if(SceneManager.GetActiveScene().name == "Play")
        {
            Debug.Log("���� �ҷ����� �Ϸ�");
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

        //��ȭ
        saveData.Cash = Data.Cash;
        saveData.Gold = Data.Gold;
        saveData.Money_Forest = Data.Money_Forest;
        saveData.Money_Desert = Data.Money_Desert;
        saveData.Money_Arctic = Data.Money_Arctic;

        //�׸�
        saveData.Theme = Data.Theme;
        
        //ĳ���� ���� ����
        saveData.Buy_Character = Data.Buy_Character;
        saveData.Cost_Character = Data.Cost_Character;
        saveData.Now_Character = Data.Now_Character;

        //ĳ���� ���� ����
        saveData.Character_STAT = Data.Character_STAT;
        
        // ���� �÷���
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
        saveData.multi_exp = Data.multi_exp;
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

        // �нú�
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

        // ��� ����
        saveData.Talent_HP = Data.Talent_HP;
        saveData.Talent_DEF = Data.Talent_DEF;
        saveData.Talent_LUK = Data.Talent_LUK;
        saveData.Talent_Restore = Data.Talent_Restore;
        saveData.Talent_LV = Data.Talent_LV;

        // ���� �� ���� ������.
        saveData.Pre_HP = Data.Pre_HP;
        saveData.Pre_Shield = Data.Pre_Shield;
        saveData.Pre_100 = Data.Pre_100;
        saveData.Pre_300 = Data.Pre_300;
        saveData.Pre_Random = Data.Pre_Random;
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

        //��ȭ
        Data.Cash = saveData.Cash;
        Data.Gold = saveData.Gold;
        Data.Money_Forest = saveData.Money_Forest;
        Data.Money_Desert = saveData.Money_Desert;
        Data.Money_Arctic = saveData.Money_Arctic;

        //�׸�
        Data.Theme = saveData.Theme;
        
        //ĳ���� ���� ����
        Data.Buy_Character = saveData.Buy_Character;
        Data.Cost_Character = saveData.Cost_Character;
        Data.Now_Character = saveData.Now_Character;

        //ĳ���� ���� ����
        Data.Character_STAT = saveData.Character_STAT;

        //�÷���
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
        Data.multi_exp = saveData.multi_exp;
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
        
        //�нú�
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

        // ��� ����
        Data.Talent_HP = saveData.Talent_HP;
        Data.Talent_DEF = saveData.Talent_DEF;
        Data.Talent_LUK = saveData.Talent_LUK;
        Data.Talent_Restore = saveData.Talent_Restore;
        Data.Talent_LV = saveData.Talent_LV;

        // ���� �� ���� ������
        Data.Pre_HP = saveData.Pre_HP;
        Data.Pre_Shield = saveData.Pre_Shield;
        Data.Pre_100 = saveData.Pre_100;
        Data.Pre_300 = saveData.Pre_300;
        Data.Pre_Random = saveData.Pre_Random;
        Data.Exp_run = saveData.Exp_run;

        Debug.Log("LOAD!");

    }
    // ü�� ��
    public void BAR_HP()
    {
        HP_Bar.fillAmount = GameManager.Data.hp / GameManager.Data.max_hp;
    }

    // ����ġ ��
    public void BAR_EXP()
    {
        Debug.Log(GameManager.Data.now_Exp / GameManager.Data.EXP[GameManager.Data.lv]);
        EXP_Bar.fillAmount = (GameManager.Data.now_Exp / GameManager.Data.EXP[GameManager.Data.lv]);
    }
    // ���� ����...
    public void MakeCoin(Transform other)
    {
        Instantiate(coin, new Vector3(other.position.x + 0.5f, other.position.y + 4.0f, other.position.z), Quaternion.identity);
    }

    // Ʈ�� �ı� �� �޺� ����
    public void Trap_Combo(Transform other)
    {
        MakeCoin(other.transform);
        Destroy(other.gameObject); // �� ��ֹ��� ��! ��!
        int per = Random.Range(0, 100);
        if (per < Data.luck) // ��� ��ġ�� ���� ũ��Ƽ�� ����.
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

    public void Set_Stat()
    {
        int idx = (int)Data.Now_Character;
        //���� ���õ� ĳ���� ���� + ������� �÷��� ���� ����.
        
        // 
        Data.max_hp = Data.Character_STAT[idx].MAX_HP + Data.Talent_HP;
        Data.hp = Data.max_hp;
        Data.speed = Data.Character_STAT[idx].SPEED;
        Data.jump = Data.Character_STAT[idx].JUMP_POWER;
        Data.down = Data.Character_STAT[idx].DOWN_POWER;
        Data.max_jump = Data.Character_STAT[idx].JUMP_COUNT;
        Data.defense = Data.Character_STAT[idx].DEF + Data.Talent_DEF;
        Data.luck = Data.Character_STAT[idx].LUK + Data.Talent_LUK;
        Data.active = Data.Character_STAT[idx].ACTIVE;
        Data.restore_eff = Data.Talent_Restore;

    }
    private void OnApplicationQuit()
    {
        Re_Stat();
    }

    public void Re_Stat()
    {
        Data.playing = false;
        Time.timeScale = 1;

        Set_Stat();

        Data.lvup = false; // ������ ����, true�� �� ���� ��ֹ��� �������ϴ� ��ҷ�.
        Data.lv = 1; // ���� ���� �ִ� 0~12������
        Data.now_Exp = 0; // ���� ����ġ 
        Data.multi_exp = 1;
        Data.stage = 0; // ��������
        Data.multi_coin = 1; // ���� ȹ�淮 ������
        Data.damage = 20.0f; // ���� �ǰ� ������
        Data.combo = 0; // ���� ���� �� �޺� 
        Data.max_combo = 0;
        Data.multi_combo = 1; // �޺� ����
        Data.max_active = 1; // ��Ƽ�� ��ų �ִ� ��밡�� Ƚ��
        Data.use_active = 0; // ��Ƽ�� ��ų ���� ��� Ƚ��
        Data.dodge_time = 12; // �ǰݽ� ���� �ð� ����. default 12

        Data.magnet = false; // �нú� �ڼ����� ����
        Data.buwhal = 0; // �нú� ��Ȱ ����
        Data.auto_jump = false; //�нú� �������� ����
        Data.random_god = false; // �нú� ���� Ȯ���� ���� ����
        Data.auto_restore = false;
        Data.change_chance = 0;
        Data.passive_active = false; // �нú� ��Ƽ�� ���Ƚ�� + 1
        Data.passive_buwhal = false;

        // ���� �� ���� ������ �ʱ�ȭ
        Data.Pre_HP = false;
        Data.Pre_Shield = false;
        Data.Pre_100 = false;
        Data.Pre_300 = false;
        Data.Pre_Random = DataManager.Random_Item.None;
        Data.Exp_run = 0;

        Data.pattern = new List<int>();
        Data.Gold += (int)Data.play_gold;
        Data.play_gold = 0;
        Save();
    }
}
