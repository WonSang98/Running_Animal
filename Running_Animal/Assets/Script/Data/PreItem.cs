using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemSet
// ������ ���� ������ ��
// �ش� �������� �����ϴ��� (Boolean)
// �ش� �������� ���� ����  (int)
public struct ItemSet
{
    public bool USE;
    public int CNT;

    public ItemSet(bool _USE, int _CNT)
    {
        USE = _USE;
        CNT = _CNT;
    }
}

public class PreItem
{
    public enum Random_Item // ���� �� ���� ���� ������ ���
    {
        None = 0,
        HP15, // ü�� 15% ����
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

    public ItemSet Pre_HP;
    public ItemSet Pre_Shield;
    public ItemSet Pre_100;
    public ItemSet Pre_300;
    public Active.ACTIVE_CODE Pre_Active;
    public Random_Item Pre_Random;

    public PreItem(
        ItemSet _pre_hp,
        ItemSet _pre_shield,
        ItemSet _pre_100,
        ItemSet _pre_300, 
        Active.ACTIVE_CODE _pre_active,
        Random_Item _pre_random)
    {

        Pre_HP = _pre_hp;
        Pre_Shield = _pre_shield;
        Pre_100 = _pre_100;
        Pre_300 = _pre_300;
        Pre_Active = _pre_active;
        Pre_Random = _pre_random;
    }

    PreItem()
    {

    }
}
