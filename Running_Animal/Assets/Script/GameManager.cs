using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;// ���ϼ��� ����.
    public static GameManager Instance { get { Init(); return s_Instance; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }
    //Play Theme
    
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
    }


    void Awake()
    {
        Init();
    }

    void Update()
    {
        
    }
}
