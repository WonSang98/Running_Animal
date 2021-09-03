using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Passive : MonoBehaviour
{
    public enum PASSIVE_CODE
    {
        None = 0,
        LUK_UP, // 행운 증가
        Active_Twice, // 액티브 두 번 사용 2
        DEF_UP, // 방어력 증가
        HP_UP, // 최대 체력 증가
        MOV_UP, // 이동속도 증가
        MOV_DOWN, // 이동속도 감소
        JMP_UP, // 점프력 증가
        JMP_DOWN, // 점프력 감소
        DWN_UP, // 낙하속도 증가
        DWN_DOWN, // 낙하속도 감소
        Magenet, // 자석버그 11
        Combo_UP, // 콤보 획득량 증가
        Resurrection, // 부활 부활 철자 엄청기네 ㅋㅋㅋ  13
        Coin_UP, // 코인 획득량 증가
        Auto_Jump, // 자동 점프 15
        Random_God, // 낮은 확률로 랜덤 무적 16
        Hit_God_UP, // 피격시 무적시간 증가
        Max_Jump_Plus, // 점프 횟수 증가
        Auto_Restore, // 자동 체력 재생 19
        Heal_Eff// 회복 효율 증가
    }

    /*패시브 스킬 설명*/
    public String[,] Passive_Explane =
    {
        { "None", "영구결번" },
        { "행운 증가 [패시브]", "행운이 증가합니다."},
        { "한 번 더 [패시브]", "액티브 스킬을 한 번 더 사용할 수 있습니다."},
        { "방어력 증가 [패시브]", "방어력이 15% 증가합니다."  },
        { "하루 종일도 할 수 있어 [패시브]", $"최대 체력이 15% 상승합니다."},
        { "속도 증가 [패시브]", "속도가 증가합니다."},
        { "속도 감소 [패시브]", "속도가 감소합니다."},
        { "점프력 증가 [패시브]", "점프력이 증가합니다."},
        { "점프력 감소 [패시브]", "점프력이 감소합니다."},
        { "빠른 착지 [패시브]", "'DONW'버튼을 통해 내려오는 속도가 증가합니다."},
        { "느린 착지 [패시브]", "'DOWN'버튼을 통해 내려오는 속도가 감소합니다."},
        { "난 돈이 좋아! [패시브]", "골드를 캐릭터쪽으로 끌어당깁니다."},
        { "일석이조 [패시브]", "콤보 획득량이 2배가 됩니다."},
        { "집행유예 [패시브]", "체력이 0이 됐을 시, 최대 체력의 절반을 갖고 다시 일어섭니다." },
        { "통화팽창 [패시브]", "골드 획득량이 10% 상승합니다." },
        { "자율 점프 신발 [패시브]", "일정 확률로 점프합니다.\n단, 언제 될지는 모릅니다. 될 수도 있고 안 될 수도 있습니다.\n이로 인한 점프는 점프 횟수에 반영되지 않습니다." },
        { "슈뢰딩거의 고양이 [패시브]", "일정 확률로 잠시간 무적이 됩니다.\n단, 언제 될지는 모릅니다. 될 수도 있고 안 될 수도 있습니다." },
        { "피격시 무적시간 증가[패시브]", "피격시에 무적이 되는 시간이 늘어납니다." },
        { "허공답보 [패시브]", "최대 점프 가능한 횟수가 1회 증가합니다."},
        { "자가치유 [패시브]", $"1초 마다 전체 체력의 2% 만큼의 체력을 회복합니다."},
        { "체질 개선 [패시브]", "체력 회복 계열 아이템 및 스킬의 효율이 10% 증가합니다."}
    };

    //패시브 스킬 이미지
    public Sprite[] Passive_Sprites;
    private void Start()
    {
        Passive_Sprites = Resources.LoadAll<Sprite>("Passive_Buttons/");
    }
    public void set_passive(int num)
    {
        switch (num)
        {
            case 0:
                // None
                break;
            case 1:
                // 행운 증가
                GameManager.Play.Status.ability.LUK.value += 3;
                save_passive(1);
                break;
            case 2:
                // 액티브 스킬 두 번 사용
                GameManager.Play.DC.activeMax += 1;
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Active_Twice] = true;
                save_passive(2);
                break;
            case 3:
                // 방어력 증가
                GameManager.Play.Status.ability.DEF.value += GameManager.Play.Status.ability.DEF.value * 0.15f;
                save_passive(3);
                break;
            case 4:
                // 최대 체력 증가
                GameManager.Play.Status.ability.MAX_HP.value += GameManager.Play.Status.ability.MAX_HP.value * 0.15f;
                GameManager.Play.Status.ability.HP.value += GameManager.Play.Status.ability.HP.value * 0.15f;
                save_passive(4);
                break;
            case 5:
                // 이동속도 증가
                GameManager.Play.Status.ability.SPEED.value *= 1.2f; 
                save_passive(5);
                break;
            case 6:
                // 이동속도 감소
                GameManager.Play.Status.ability.SPEED.value *= 0.8f;
                save_passive(6);
                break;
            case 7:
                // 점프력 증가
                GameManager.Play.Status.ability.JUMP.value *= 1.2f;
                save_passive(7);
                break;
            case 8:
                // 점프력 감소
                GameManager.Play.Status.ability.JUMP.value *= 0.8f;
                save_passive(8);
                break;
            case 9:
                // 낙하속도 증가
                GameManager.Play.Status.ability.DOWN.value *= 1.2f;
                save_passive(9);
                break;
            case 10:
                // 낙하속도 감소
                GameManager.Play.Status.ability.DOWN.value *= 0.8f;
                save_passive(10);
                break;
            case 11:
                // 자석버그
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Magenet] = true;
                save_passive(11);
                break;
            case 12:
                // 콤보 획득량 증가
                GameManager.Play.DC.comboMulti *= 2;
                save_passive(12);
                break;
            case 13:
                // 부활
                GameManager.Play.DC.revive += 1;
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Resurrection] = true;
                save_passive(13);
                break;
            case 14:
                //코인 획득량 증가
                GameManager.Play.DC.goldMulti += 0.1f;
                save_passive(14);
                break;
            case 15:
                // 자동 점프
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Auto_Jump] = true;
                save_passive(15);
                break;
            case 16:
                // 낮은 확률로 랜덤 무적
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Random_God] = true;
                save_passive(16);
                break;
            case 17:
                // 피격시 무적시간 증가
                GameManager.Play.DC.dodge += (GameManager.Play.DC.dodge / 2);
                save_passive(17);
                break;
            case 18:
                // 점프 횟수 증가
                GameManager.Play.Status.ability.MAX_JUMP.value += 1;
                save_passive(18);
                break;
            case 19:
                // 자동 체력 재생
                GameManager.Skill.passive_once[Passive.PASSIVE_CODE.Auto_Restore] = true;
                save_passive(19);
                break;
            case 20:
                // 체력 회복량 증가.
                GameManager.Play.Status.ability.RESTORE.value += 0.1f;
                save_passive(20);
                break;
        }
    }

    public IEnumerator Auto_Jump()
    {
        while (true)
        {
            int per = Random.Range(0, 100);
            if (per < 20 + GameManager.Play.Status.ability.LUK.value)
            {
                GameManager.Play.Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, GameManager.Play.Status.ability.JUMP.value, 0);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    public IEnumerator Resurrection()
    {
        GameManager.Instance.GetComponent<UI_Play>().RemoveButton();
        StartCoroutine(gameObject.GetComponent<InterAction>().OnDodge(60));

        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                GameManager.Play.Status.ability.SPEED.value = 0.0f;
                GameManager.Play.Status.ability.HP.value = (GameManager.Play.Status.ability.MAX_HP.value / 2);
                gameObject.GetComponent<UI_Play>().BAR_HP();
            }
            if (i == 1)
            {
                GameManager.Play.Status.ability.SPEED.value = GameManager.Play.DC.pre_speed;
                GameManager.Instance.GetComponent<UI_Play>().RemakeButton();
                gameObject.GetComponent<InterAction>().Apply_Passive();
                GameManager.Play.Player.tag = "Player";
                
            }
            yield return new WaitForSeconds(3.0f);
        }
    }

    public IEnumerator Random_God()
    {
        while (true)
        {
            int per = Random.Range(0, 100);
            if (per < 10 + GameManager.Play.Status.ability.LUK.value)
            {
                StartCoroutine("Small_God");
            }
            yield return new WaitForSeconds(8f);
        }
    }

    public IEnumerator Small_God()
    {
        Color player_opacity;
        float time = 0;
        player_opacity = GameManager.Play.Player.GetComponent<SpriteRenderer>().color;
        player_opacity.a = 0.5f;
        GameManager.Play.Player.GetComponent<SpriteRenderer>().color = player_opacity;
        GameObject Player = GameManager.Play.Player.gameObject;
        while (time < 2)
        {
            time += Time.deltaTime;
            Player.tag = "God";
            yield return null;
        }
        player_opacity = GameManager.Play.Player.GetComponent<SpriteRenderer>().color;
        player_opacity.a = 1.0f;
        GameManager.Play.Player.GetComponent<SpriteRenderer>().color = player_opacity;
        Player.tag = "Player";
    }

    public IEnumerator Auto_Restore()
    {
        while (true)
        {
            //GameManager.Play.Status.ability.HP.value += 4 * GameManager.Play.Status.ability.RESTORE.value;
            GameManager.Play.Status.ability.HP.value += GameManager.Play.Status.ability.MAX_HP.value * 0.01f * GameManager.Play.Status.ability.RESTORE.value;
            if(GameManager.Play.Status.ability.HP.value > GameManager.Play.Status.ability.MAX_HP.value)
            {
                GameManager.Play.Status.ability.HP.value = GameManager.Play.Status.ability.MAX_HP.value;
            }
            gameObject.GetComponent<UI_Play>().BAR_HP();
            yield return new WaitForSeconds(1f);
        }
    }

    public void save_passive(int num)
    {
        GameManager.Play.DC.passiveGet.Add((PASSIVE_CODE)(num));
    }

    public void Stop_Passive()
    {
        StopAllCoroutines();
    }
}
