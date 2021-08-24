using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSuccess : MonoBehaviour
{
    Image[] Image_Success;
    Sprite[] Sprite_GoodCharacter;
    Button Button_SuccessMain;
    Text[] Text_Success;

    LoadScene LS;

    void Start()
    {
        GameObject END = GameObject.Find("END");
        LS = GameManager.Instance.GetComponent<LoadScene>();
        Image_Success = new Image[1];
        Image_Success[0] = END.transform.Find("Panel_Success/Image_SUCCESS").GetComponent<Image>();

        Sprite_GoodCharacter = Resources.LoadAll<Sprite>("Image/GUI/Play/CharacterDead"); //Temp
        Button_SuccessMain = END.transform.Find("Panel_Success/Button_MAIN").GetComponent<Button>();
        Button_SuccessMain.onClick.AddListener(() => gameObject.GetComponent<LoadScene>().EndGame());
        Text_Success = new Text[8];
        string path_TS = "Panel_Success/Result_Unit/";
        Text_Success[0] = END.transform.Find(path_TS + "Text_Stage/Text_Value").GetComponent<Text>();
        Text_Success[1] = END.transform.Find(path_TS + "Text_Stage_NoHit/Text_Value").GetComponent<Text>();
        Text_Success[2] = END.transform.Find(path_TS + "Text_Difficult/Text_Value").GetComponent<Text>();
        Text_Success[3] = END.transform.Find(path_TS + "Text_Combo/Text_Value").GetComponent<Text>();
        Text_Success[4] = END.transform.Find(path_TS + "Text_Trap/Text_Value").GetComponent<Text>();
        Text_Success[5] = END.transform.Find(path_TS + "Text_Result").GetComponent<Text>();
        Text_Success[6] = END.transform.Find("Panel_Success/Image_Gold/Text_Value").GetComponent<Text>();
        Text_Success[7] = END.transform.Find("Panel_Success/Image_Speacial/Text_Value").GetComponent<Text>();


        int result = GameManager.Play.DC.passTrap * ((GameManager.Play.DC.stage - GameManager.Play.DC.noHitStage) + (2 * GameManager.Play.DC.noHitStage)) * (GameManager.Data.Preset.Difficult + 1) + GameManager.Play.DC.comboMax * 10000 + (int)GameManager.Play.DC.goldNow * 10; // 게임 결과점수.
        int money_speacial = result / 10; // 특수재화 얻는 갯수.
        Text_Success[0].text = $"{GameManager.Play.DC.stage}";
        Text_Success[1].text = $"{GameManager.Play.DC.noHitStage}";
        Text_Success[2].text = Difficulty.DIFF_CODE[GameManager.Data.Preset.Difficult];
        Text_Success[3].text = $"{GameManager.Play.DC.comboMax}";
        Text_Success[4].text = $"{GameManager.Play.DC.passTrap}";
        Text_Success[5].text = $"{result}";
        Text_Success[6].text = $"{(int)GameManager.Play.DC.goldNow}G";
        Text_Success[7].text = $"{money_speacial}개";

        Image_Success[0].sprite = Sprite_GoodCharacter[(int)GameManager.Data.Preset.Character];

        GameManager.Data.Money.Gold += (int)GameManager.Play.DC.goldNow;
        GameManager.Data.Money.Speacial[0] += money_speacial;
    }
}
