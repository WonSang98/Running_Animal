using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTutorial3 : MonoBehaviour
{
    TrapForest TrapForest;
    SetPlayer SetPlayer;
    UI_Play UI_Play;
    InterAction InterAction;

    private void Awake()
    {
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        TrapForest = GameObject.Find("@Managers").GetComponent<TrapForest>();
        SetPlayer = GameObject.Find("@Managers").GetComponent<SetPlayer>();
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
        InterAction = GameObject.Find("@Managers").GetComponent<InterAction>();
    }
    void Start()
    {
        //Trap Resource �ҷ�����.
        TrapForest.GetResource();
        //�÷��̾� ������ ��ġ�� ����
        SetPlayer.Spawn();
        if (GameManager.Play.Playing == false)
        {
            SetPlayer.FirstSet();
            GameManager.Play.Playing = true;
        }
        UI_Play.SetUI();

        GameManager.Play.DC.damage = 999999;
        for(int i=0; i<GameManager.Play.DC.expNeed.Length; i++)
        {
            GameManager.Play.DC.expNeed[i] += 10000;
        }
        StartCoroutine(TrapForest.TrapUpdate());
    }

    void Update()
    {
        GameManager.Play.DS.expNow = 0;
    }

    
    
}
