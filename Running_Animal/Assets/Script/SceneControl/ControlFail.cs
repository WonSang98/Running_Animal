using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlFail : MonoBehaviour
{
    Image[] Image_Fail;
    Sprite[] Sprite_DeadCharacter;
    Sprite[] Sprite_DeadCause;
    Button Button_FailMain; // 실패시 메인으로 가는 버튼~
    Text[] Text_Fail;

    LoadScene LS;

    AudioClip clip;


    void Start()
    {
        LS = GameManager.Instance.GetComponent<LoadScene>();
        Image_Fail = new Image[2];
        GameManager.Sound.SFXPlay(clip);
        Image_Fail[0] = GameObject.Find("END/Panel_Fail/Image_DIE").GetComponent<Image>();
        Image_Fail[1] = GameObject.Find("END/Panel_Fail/Image_Trap/Image_Trap").GetComponent<Image>();
        Sprite_DeadCharacter = Resources.LoadAll<Sprite>("Image/GUI/Play/CharacterDead");
        Sprite_DeadCause = Resources.LoadAll<Sprite>("Image/GUI/Play/CauseDead");

        Button_FailMain = GameObject.Find("END/Panel_Fail/Button_MAIN").GetComponent<Button>();
        Button_FailMain.onClick.AddListener(() => LS.EndGame());

        Text_Fail = new Text[8];
        string path_TF = "END/Panel_Fail/Result_Unit/";
        Text_Fail[0] = GameObject.Find(path_TF + "Text_Stage/Text_Value").GetComponent<Text>();
        Text_Fail[1] = GameObject.Find(path_TF + "Text_Stage_NoHit/Text_Value").GetComponent<Text>();
        Text_Fail[2] = GameObject.Find(path_TF + "Text_Difficult/Text_Value").GetComponent<Text>();
        Text_Fail[3] = GameObject.Find(path_TF + "Text_Combo/Text_Value").GetComponent<Text>();
        Text_Fail[4] = GameObject.Find(path_TF + "Text_Trap/Text_Value").GetComponent<Text>();
        Text_Fail[5] = GameObject.Find(path_TF + "Text_Result").GetComponent<Text>();
        Text_Fail[6] = GameObject.Find("Panel_Fail/Image_Gold/Text_Value").GetComponent<Text>();
        Text_Fail[7] = GameObject.Find("Panel_Fail/Image_Speacial/Text_Value").GetComponent<Text>();

        int result = GameManager.Play.DC.passTrap * ((GameManager.Play.DC.stage - GameManager.Play.DC.noHitStage) + (2 * GameManager.Play.DC.noHitStage)) * (GameManager.Data.Preset.Difficult + 1) + GameManager.Play.DC.comboMax * 100 + (int)GameManager.Play.DC.goldNow * 10; // 게임 결과점수.
        int money_speacial = 0; // 특수재화 얻는 갯수.
        Text_Fail[0].text = GameManager.Play.DC.stage.ToString();
        Text_Fail[1].text = $"{GameManager.Play.DC.noHitStage}";
        Text_Fail[2].text = Difficulty.DIFF_CODE[GameManager.Data.Preset.Difficult];
        Text_Fail[3].text = $"{GameManager.Play.DC.comboMax}";
        Text_Fail[4].text = $"{GameManager.Play.DC.passTrap}";
        Text_Fail[5].text = $"{result}";
        Text_Fail[6].text = $"{(int)GameManager.Play.DC.goldNow}G";
        Text_Fail[7].text = $"{money_speacial}개";

        Image_Fail[0].sprite = Sprite_DeadCharacter[(int)GameManager.Data.Preset.Character];
        Image_Fail[1].sprite = Sprite_DeadCause[GameManager.Play.DC.lastHit];

        GameManager.Data.Money.Gold += (int)GameManager.Play.DC.goldNow;
        GameManager.Data.Money.Speacial[0] += money_speacial;
    }
    void LoadSound() //Sound Resoucres 경로 찾아와서 불러와놓기.
    {
        clip = Resources.Load<AudioClip>("Sound/Common/002_Paper");
    }
}
