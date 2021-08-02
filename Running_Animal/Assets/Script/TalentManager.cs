using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentManager : MonoBehaviour
{
    GameObject[] panels = new GameObject[4]; // ���� �г�.

    // ��� ���׷��̵� ���
    int[] costs = { 500, 1000, 1500, 2000, 2500, 3000, 4000, 5000, 6000, 7000};
    // ��� ���׷��̵� �� �ɷ�ġ
    float[] HP = { 20, 40, 60, 80, 100, 120, 140, 160, 180, 200};
    float[] DEF = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    int[] LUK = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    float[] Restore = { 1.1f, 1,15f, 1.2f, 1.25f, 1.3f, 1.35f, 1.4f, 1.45f, 1.5f};

    Text[] txts = new Text[4];

    void Start()
    {
        panels[0] = GameObject.Find("UI/Panel_HP");
        panels[1] = GameObject.Find("UI/Panel_DEF");
        panels[2] = GameObject.Find("UI/Panel_LUK");
        panels[3] = GameObject.Find("UI/Panel_RESTORE");

        txts[0] = GameObject.Find("UI/Panel_HP/Text").GetComponent<Text>();
        txts[1] = GameObject.Find("UI/Panel_DEF/Text").GetComponent<Text>();
        txts[2] = GameObject.Find("UI/Panel_LUK/Text").GetComponent<Text>();
        txts[3] = GameObject.Find("UI/Panel_RESTORE/Text").GetComponent<Text>();

        for (int i = 0; i < 4; i++) panels[i].SetActive(false);

        txts[0].text = $"LV : {GameManager.Data.Talent_LV[0]}\n" +
                       $"�ִ� HP�� {HP[GameManager.Data.Talent_LV[0] - 1]} ��ŭ �߰��մϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[0] - 1]}";

        txts[1].text = $"LV : {GameManager.Data.Talent_LV[1]}\n" +
                       $"������ {DEF[GameManager.Data.Talent_LV[1] - 1]} ��ŭ �߰��մϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[1] - 1]}";

        txts[2].text = $"LV : {GameManager.Data.Talent_LV[2]}\n" +
                       $"����� {LUK[GameManager.Data.Talent_LV[2] - 1]} ��ŭ �߰��մϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[2] - 1]}";

        txts[3].text = $"LV : {GameManager.Data.Talent_LV[3]}\n" +
                       $"ȸ���������� ȿ���� {Restore[GameManager.Data.Talent_LV[3] - 1]} ��ŭ �����ϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[3] - 1]}";
    }

    void Update()
    {
        
    }
    void ClosePanels()
    {
        for (int i = 0; i < 4; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void OnInfoHP()
    {
        ClosePanels();
        panels[0].SetActive(true);
    }

    public void OnInfoDEF()
    {
        ClosePanels();
        panels[1].SetActive(true);
    }
    public void OnInfoLUK()
    {
        ClosePanels();
        panels[2].SetActive(true);
    }

    public void OnInfoRestore()
    {
        ClosePanels();
        panels[3].SetActive(true);
    }

    public void UpgradeHP()
    {
        if(GameManager.Data.Gold > costs[GameManager.Data.Talent_LV[0] - 1] && GameManager.Data.Talent_LV[0] < 9)
        {
            GameManager.Data.Gold -= costs[GameManager.Data.Talent_LV[0] - 1];
            GameManager.Data.Talent_LV[0] += 1;
            GameManager.Data.Talent_HP = HP[GameManager.Data.Talent_LV[0] - 1];

            txts[0].text = $"LV : {GameManager.Data.Talent_LV[0]}\n" +
                       $"�ִ� HP�� {HP[GameManager.Data.Talent_LV[0] - 1]} ��ŭ �߰��մϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[0] - 1]}";
        }
    }

    public void UpgradeDEF()
    {
        if (GameManager.Data.Gold > costs[GameManager.Data.Talent_LV[1] - 1] && GameManager.Data.Talent_LV[1] < 9)
        {
            GameManager.Data.Gold -= costs[GameManager.Data.Talent_LV[1] - 1];
            GameManager.Data.Talent_LV[1] += 1;
            GameManager.Data.Talent_DEF = DEF[GameManager.Data.Talent_LV[1] - 1];

            txts[1].text = $"LV : {GameManager.Data.Talent_LV[1]}\n" +
                       $"������ {DEF[GameManager.Data.Talent_LV[1] - 1]} ��ŭ �߰��մϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[1] - 1]}";
        }
    }

    public void UpgradeLUK()
    {
        if (GameManager.Data.Gold > costs[GameManager.Data.Talent_LV[2] - 1] && GameManager.Data.Talent_LV[2] < 9)
        {
            GameManager.Data.Gold -= costs[GameManager.Data.Talent_LV[2] - 1];
            GameManager.Data.Talent_LV[2] += 1;
            GameManager.Data.Talent_LUK = LUK[GameManager.Data.Talent_LV[2] - 1];

            txts[2].text = $"LV : {GameManager.Data.Talent_LV[2]}\n" +
                       $"����� {LUK[GameManager.Data.Talent_LV[2] - 1]} ��ŭ �߰��մϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[2] - 1]}";
        }
    }

    public void UpgradeRstore()
    {
        if (GameManager.Data.Gold > costs[GameManager.Data.Talent_LV[3] - 1] && GameManager.Data.Talent_LV[3] < 9)
        {
            GameManager.Data.Gold -= costs[GameManager.Data.Talent_LV[3] - 1];
            GameManager.Data.Talent_LV[3] += 1;
            GameManager.Data.Talent_Restore = Restore[GameManager.Data.Talent_LV[3] - 1];

            txts[3].text = $"LV : {GameManager.Data.Talent_LV[3]}\n" +
                       $"ȸ���������� ȿ���� {Restore[GameManager.Data.Talent_LV[3] - 1]} ��ŭ �����ϴ�.\n" +
                       $"�������� �ʿ��� Gold {costs[GameManager.Data.Talent_LV[3] - 1]}";
        }
    }

}
