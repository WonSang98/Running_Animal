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
            Debug.Log("�ƾ�...��ȯ�Ǿ���");
            Load(); }
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

        Debug.Log("LOAD!");

    }

    
    private void OnApplicationQuit()
    {
        Save();
    }

}
