using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    public void Spawn()
    {
        // 현재 설정된 캐릭터 Prefab을 찾아 GameObject 소환
        GameManager.Play.Player = Instantiate(Resources.Load<GameObject>("Character/" + ((int)GameManager.Data.Preset.Character).ToString())) as GameObject;
        // Player 크기 설정
        GameManager.Play.Player.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        // Player 생성 위치 설정
        GameManager.Play.Player.transform.Translate(-6.495f, -1.5f, 0);
        // 계층도 창에서 Player 이름 설정.
        GameManager.Play.Player.transform.name = "Player";
        // Animator 실행
        GameManager.Play.Player.GetComponent<Animator>().SetBool("StartGame", true);

        Clear_DS();
    }

    public void FirstSet()
    {
        GetStatus();
        GetTalent();
        GetPreItem();
        GetDiff();
        ForestPattern();
    }

    public void ForestPattern()
    {
        GameManager.Play.DC.patternList = null;
        GameManager.Play.DC.patternList = new List<int>();
        for(int i=0; i< gameObject.GetComponent<TrapForest>().num_pattern; i++)
        {
            GameManager.Play.DC.patternList.Add(i);
        }
        GameManager.Play.ShuffleList(GameManager.Play.DC.patternList);
    }
    void GetStatus()
    {
        //현재 설정된 캐릭터의 능력치를 가져온다.
        Ability ab = GameManager.Data.Character_STAT[(int)GameManager.Data.Preset.Character].ability;
        GameManager.Play.Status.ability.MAX_HP.value = ab.MAX_HP.value;
        GameManager.Play.Status.ability.HP.value = ab.HP.value;
        GameManager.Play.Status.ability.SPEED.value = ab.SPEED.value;
        GameManager.Play.Status.ability.MAX_JUMP.value = ab.MAX_JUMP.value;
        GameManager.Play.Status.ability.JUMP.value = ab.JUMP.value;
        GameManager.Play.Status.ability.DOWN.value = ab.DOWN.value;
        GameManager.Play.Status.ability.DEF.value = ab.DEF.value;
        GameManager.Play.Status.ability.LUK.value = ab.LUK.value;
        GameManager.Play.Status.ability.RESTORE.value = ab.RESTORE.value;
    }

    void GetTalent()
    {
        //재능 적용
        GameManager.Play.Status.ability.MAX_HP.value += GameManager.Data.Talent.HP.value;
        GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
        GameManager.Play.Status.ability.DEF.value += GameManager.Data.Talent.DEF.value;
        GameManager.Play.Status.ability.LUK.value += (short)GameManager.Data.Talent.LUK.value;
        GameManager.Play.Status.ability.RESTORE.value += GameManager.Data.Talent.RESTORE.value;
    }
    void GetDiff()
    {
        //난이도 적용
        switch (GameManager.Data.Preset.Theme)
        {
            case Theme.THEME_CODE.Forest:
                GameManager.Play.DC.damage = Difficulty.Forest[GameManager.Data.Preset.Difficult].DMG;
                GameManager.Play.DC.goldMulti = Difficulty.Forest[GameManager.Data.Preset.Difficult].COIN;
                GameManager.Play.Status.ability.RESTORE.value -= Difficulty.Forest[GameManager.Data.Preset.Difficult].RESTORE;
                if (GameManager.Play.Status.ability.LUK.value - Difficulty.Forest[GameManager.Data.Preset.Difficult].LUK < 0)
                {
                    GameManager.Play.Status.ability.LUK.value = 0;
                }
                else
                {
                    GameManager.Play.Status.ability.LUK.value -= Difficulty.Forest[GameManager.Data.Preset.Difficult].LUK;
                }
                GameManager.Play.Status.ability.LUK.value -= Difficulty.Forest[GameManager.Data.Preset.Difficult].LUK;
                GameManager.Play.Status.ability.DEF.value += Difficulty.Forest[GameManager.Data.Preset.Difficult].DEF;
                GameManager.Play.Status.ability.SPEED.value += Difficulty.Forest[GameManager.Data.Preset.Difficult].SPEED;

                for (int idx = 0; idx < GameManager.Play.DC.expNeed.Length; idx++)
                {
                    GameManager.Play.DC.expNeed[idx] += Difficulty.Forest[GameManager.Data.Preset.Difficult].EXP;
                }

                break;
            case Theme.THEME_CODE.Desert:
                break;
            case Theme.THEME_CODE.Arctic:
                break;
        }
    }

    void GetPreItem()
    {
        //시작 전 구매한 아이템 적용하기.
        GameManager.Play.Status.ACTIVE = GameManager.Data.PreItem.Pre_Active; // 랜덤 액티브 스킬 구매시 적용.
        GameManager.Data.PreItem.Pre_Active = Active.ACTIVE_CODE.None; 
        PreRandom();
        // 시작 전 구매한 아이템의 적용
        if (GameManager.Data.PreItem.Pre_HP.USE)
        {
            GameManager.Play.Status.ability.MAX_HP.value *= 1.1f;
            GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
            GameManager.Data.PreItem.Pre_HP.USE = false;
        }
        if (GameManager.Data.PreItem.Pre_Shield.USE)
        {
            // 1회용 쉴드 구매 시
            GameManager.Play.Player.transform.Find(((int)Active.ACTIVE_CODE.Defense).ToString()).gameObject.SetActive(true);
            GameManager.Play.Player.tag = "Shield";
            GameManager.Data.PreItem.Pre_Shield.USE = false;
        }

        if (GameManager.Data.PreItem.Pre_100.USE)
        {
            //100미터 달리기 - 5초 달리기
            StartCoroutine(gameObject.GetComponent<Active>().OnRun(5.0f));
            GameManager.Data.PreItem.Pre_100.USE = false;

        }

        if (GameManager.Data.PreItem.Pre_300.USE)
        {
            //300미터 달리기 - 10초 달리기
            StartCoroutine(gameObject.GetComponent<Active>().OnRun(10.0f));
            GameManager.Data.PreItem.Pre_300.USE = false;
        }
    }

    void PreRandom()
    {
        switch (GameManager.Data.PreItem.Pre_Random)
        {
            case PreItem.Random_Item.HP15:
                GameManager.Play.Status.ability.MAX_HP.value *= 1.15f;
                GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
                break;
            case PreItem.Random_Item.HP30:
                GameManager.Play.Status.ability.MAX_HP.value *= 1.30f;
                GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
                break;
            case PreItem.Random_Item.LUK5:
                GameManager.Play.Status.ability.LUK.value += 5;
                break;
            case PreItem.Random_Item.LUK10:
                GameManager.Play.Status.ability.LUK.value += 10;
                break;
            case PreItem.Random_Item.SPEED15:
                GameManager.Play.Status.ability.SPEED.value *= 1.15f;
                break;
            case PreItem.Random_Item.SPEED30:
                GameManager.Play.Status.ability.SPEED.value *= 1.3f;
                break;
            case PreItem.Random_Item.JUMP20:
                GameManager.Play.Status.ability.JUMP.value *= 1.2f;
                break;
            case PreItem.Random_Item.JUMP40:
                GameManager.Play.Status.ability.JUMP.value *= 1.4f;
                break;
            case PreItem.Random_Item.GOLD25:
                GameManager.Play.DC.goldMulti += 0.25f;
                break;
            case PreItem.Random_Item.GOLD50:
                GameManager.Play.DC.goldMulti += 0.5f;
                break;
            case PreItem.Random_Item.COMBO2:
                GameManager.Play.DC.comboMulti *= 2;
                break;
            case PreItem.Random_Item.COMBO3:
                GameManager.Play.DC.comboMulti *= 3;
                break;
            case PreItem.Random_Item.JUMP_PLUS:
                GameManager.Play.Status.ability.MAX_JUMP.value += 1;
                break;
            case PreItem.Random_Item.DEF10:
                GameManager.Play.Status.ability.DEF.value -= 0.1f;
                break;
            case PreItem.Random_Item.DEF15:
                GameManager.Play.Status.ability.DEF.value -= 0.15f;
                break;
            case PreItem.Random_Item.EXP2:
                GameManager.Play.DC.expMulti *= 2;
                break;

        }
        GameManager.Data.PreItem.Pre_Random = PreItem.Random_Item.None;
    }

    //Stage 넘어가고 초기화 할 데이터 초기화 하기...
    public void Clear_DS()
    {
        DataShot temp = new DataShot();
        GameManager.Play.DS = temp.DeepCopy();
    }

    public void Clear_DC()
    {
        DataContinue temp = new DataContinue();
        GameManager.Play.DC = temp.DeepCopy();
    }

    public void Re_Stat()
    {
        GetStatus();
        GetTalent();
        GetPreItem();
        GetDiff();
        Clear_DC();
        Clear_DS();
        GameManager.Play.Playing = false;
    }

    public void Stop_SetPlayer()
    {
        StopAllCoroutines();
    }
}
