using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pre_BuyItem : MonoBehaviour
{
    int normal_price = 500;
    int random_price = 1000;

    enum Random_Item
    {
        HP15 = 0, // ü�� 15% ����
        HP30, // ü�� 30% ����
        LUK5, // ��� 5 ����
        LUK10, // ��� 10 ����
        SPEED15, // �ӵ� 15%
        SPEED30, // �ӵ� 30%
        JUMP20, // ���� 20%
        JUMP40, // ���� 40%
        GOLD25, // ��� ȹ�淮 25% ����
        GOLD50, // ��� ȹ�淮 50% ����
        COMBO2, // �޺� ȹ�淮 2��
        COMBO3, // �޺� ȹ�淮 3��
        JUMP_PLUS, // ���� Ƚ�� 1ȸ �߰�
        DEF10, // ���� 10% �氨
        DEF15, // ���� 15% �氨
        EXP2, // ����ġ 2�� (�������� ���� �ӵ� UP)
    }

    private void Start()
    {
        GameManager.Data.max_hp += GameManager.Data.Talent_HP;
        GameManager.Data.hp = GameManager.Data.max_hp;
        GameManager.Data.defense += GameManager.Data.Talent_DEF;
        GameManager.Data.luck += GameManager.Data.Talent_LUK;
        GameManager.Data.restore_eff += (GameManager.Data.Talent_Restore - 1);

        // ���� Forest�϶�
        for (int i = 0; i < 255; i++)
        {
            GameManager.Data.pattern.Add(i);
        }

        GameManager.Data.pattern = GameManager.Instance.ShuffleList(GameManager.Data.pattern);
    }

    public void Buy_HP() // ü�� 10% ����.
    {
        if(GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.max_hp *= 1.1f;
            GameManager.Data.hp = GameManager.Data.max_hp;
        }
    }

    public void Buy_Shield() // ���� �� 1ȸ ����
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_Shield = true;
        }
    }

    public void Buy_100() //100���� �޸��� �̿��
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_100 = true;
        }
    }

    public void Buy_300() //300���� �޸��� �̿��
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            GameManager.Data.Pre_300 = true;
        }
    }

    public void Buy_Skil() //���� ��Ƽ�� ��ų ���ű�
    {
        if (GameManager.Data.Gold > normal_price)
        {
            GameManager.Data.Gold -= normal_price;
            int idx = Random.Range(1, Enum.GetNames(typeof(DataManager.Active_Skil)).Length);
            GameManager.Data.active = (DataManager.Active_Skil)idx;
        }
    }

    public void Buy_Random() // �α� �α� ���� ������ ������ ��í~
    {
        if (GameManager.Data.Gold > random_price)
        {
            GameManager.Data.Gold -= random_price;
            int idx = Random.Range(0, Enum.GetNames(typeof(Random_Item)).Length);
            GameManager.Data.active = (DataManager.Active_Skil)idx;
        }
    }
}
