using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTutorial3 : MonoBehaviour
{
    TrapForest TrapForest;
    SetPlayer SetPlayer;
    UI_Play UI_Play;
    InterAction InterAction;
    LoadScene LS;

    private void Awake()
    {
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        TrapForest = GameObject.Find("@Managers").GetComponent<TrapForest>();
        SetPlayer = GameObject.Find("@Managers").GetComponent<SetPlayer>();
        UI_Play = GameObject.Find("@Managers").GetComponent<UI_Play>();
        InterAction = GameObject.Find("@Managers").GetComponent<InterAction>();
        LS = GameManager.Instance.GetComponent<LoadScene>();
    }
    void Start()
    {
        GameManager.Data.TutoData.tuto0 = true;
        GameObject.Find("UI").transform.Find("Panel_Pause/Button_Yes").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("UI").transform.Find("Panel_Pause/Button_Yes").GetComponent<Button>().onClick.AddListener(() => LS.OnStopTutorial());
        //Trap Resource 불러오기.
        TrapForest.GetResource();
        //플레이어 지정된 위치에 생성
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
