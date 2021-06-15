using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;// 유일성이 보장.
    public static GameManager Instance { get { Init(); return s_Instance; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }
    //Play Theme
    
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

    }


    void Awake()
    {
        Init();
        string path = Application.persistentDataPath + "/save.xml";
        if (System.IO.File.Exists(path)) {Load(); }
    }

    void Update()
    {
        
    }

    public void Save()
    {
        Data saveData = new Data();

        saveData.Cash = Data.Cash;
        saveData.Gold = Data.Gold;
        saveData.Money_Forest = Data.Money_Forest;
        saveData.Money_Desert = Data.Money_Desert;
        saveData.Money_Arctic = Data.Money_Arctic;
        saveData.Theme = Data.Theme;
        saveData.Buy_Character = Data.Buy_Character;
        saveData.Now_Character = Data.Now_Character;

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

        Data.Cash = saveData.Cash;
        Data.Gold = saveData.Gold;
        Data.Money_Forest = saveData.Money_Forest;
        Data.Money_Desert = saveData.Money_Desert;
        Data.Money_Arctic = saveData.Money_Arctic;
        Data.Theme = saveData.Theme;
        Data.Buy_Character = saveData.Buy_Character;
        Data.Now_Character = saveData.Now_Character;

        Debug.Log("LOAD!");

    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
