using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;// ���ϼ��� ����.
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
                //���� Manager Object�� ���� �� ���� ����.
                go = new GameObject { name = "@Managers" };
                go.AddComponent<GameManager>();
                go.AddComponent<TrapForest>(); // �ڷ�ƾ 
                go.AddComponent<SetPlayer>(); // �ڷ�ƾ
                go.AddComponent<UI_Play>(); // �ڷ�ƾ
                go.AddComponent<InterAction>(); // �ڷ�ƾ
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
            Load(); }
    }

    // Component�� ������ ��ũ��Ʈ���� Coroutine ����.
    public void AllStop()
    {
        gameObject.GetComponent<TrapForest>().Stop_TrapForest();
        gameObject.GetComponent<SetPlayer>().Stop_SetPlayer();
        gameObject.GetComponent<UI_Play>().Stop_UiPlay();
        gameObject.GetComponent<InterAction>().Stop_InterAction();
        gameObject.GetComponent<Active>().Stop_Active();
        gameObject.GetComponent<Passive>().Stop_Passive();
        gameObject.GetComponent<LoadScene>().Stop_LoadScene();
    }
    
    public void Save()
    {
        Data saveData = new Data();
        //��ȭ ����
        saveData.Money = Data.Money;
        //���� ����
        saveData.Purchase = Data.Purchase;
        //ĳ���� ����(ĳ���� ��ȭ ������ ����)
        saveData.Character_STAT = Data.Character_STAT;
        //��� ����
        saveData.Talent = Data.Talent;
        //���� �� ���� ������ ������
        saveData.PreItem = Data.PreItem;
        //������ ������� ������
        saveData.Preset = Data.Preset;

        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<Data>(saveData, path);

        Debug.Log(path);
    }

    public void Load() 
    {
        Data saveData = new Data();
        string path = Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<Data>(path);

        //��ȭ ����
        Data.Money = saveData.Money;
        //���� ����
        Data.Purchase = saveData.Purchase;
        //ĳ���� ����(ĳ���� ��ȭ ������ ����)
        Data.Character_STAT = saveData.Character_STAT;
        //��� ����
        Data.Talent = saveData.Talent;
        //���� �� ���� ������ ������
        Data.PreItem = saveData.PreItem;
        //������ ������� ������
        Data.Preset = saveData.Preset;


    }

    
    private void OnApplicationQuit()
    {
        GameManager.Instance.AllStop();
        GameManager.Play.Playing = false;
        Save();
    }

}
