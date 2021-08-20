using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;// 유일성이 보장.
    public static GameManager Instance { get { Init(); return s_Instance; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    PlayManager _play = new PlayManager();
   public static PlayManager Play { get { return Instance._play; } }

    SkillManager _Skill = new SkillManager();
    public static SkillManager Skill { get { return Instance._Skill; } }

    
    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");


            if (go == null)
            {
                //만약 Manager Object를 생성 한 적이 없다.
                go = new GameObject { name = "@Managers" };
                go.AddComponent<GameManager>();
                go.AddComponent<TrapForest>();
                go.AddComponent<SetPlayer>();
                go.AddComponent<UI_Play>();
                go.AddComponent<InterAction>();
                go.AddComponent<Active>();
                go.AddComponent<Passive>();
                go.AddComponent<LoadScene>();
            };
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<GameManager>();
        }
    }


    void Awake()
    {
        Init();
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path)) {
            Debug.Log("아아...소환되었따");
            Load(); }
    }


    
    public void Save()
    {
        Data saveData = new Data();
        //재화 관리
        saveData.Money = Data.Money;
        //구매 관리
        saveData.Purchase = Data.Purchase;
        //캐릭터 스탯(캐릭터 강화 데이터 저장)
        saveData.Character_STAT = Data.Character_STAT;
        //재능 관리
        saveData.Talent = Data.Talent;
        //시작 전 구매 아이템 데이터
        saveData.PreItem = Data.PreItem;
        //프리셋 유저기반 데이터

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

        //재화 관리
        Data.Money = saveData.Money;
        //구매 관리
        Data.Purchase = saveData.Purchase;
        //캐릭터 스탯(캐릭터 강화 데이터 저장)
        Data.Character_STAT = saveData.Character_STAT;
        //재능 관리
        Data.Talent = saveData.Talent;
        //시작 전 구매 아이템 데이터
        Data.PreItem = saveData.PreItem;
        //프리셋 유저기반 데이터

        Debug.Log("LOAD!");

    }

    
    private void OnApplicationQuit()
    {
        Save();
    }

}
