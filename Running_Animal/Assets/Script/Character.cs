using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Max_Speed; // ĳ������ �ְ� �ӵ� �׸����� �ٸ�.
    public float Plus_Speed; // ĳ������ �����Ӵ� �����ӵ�
    public int Max_Jump; // ĳ������ �ְ� ���� Ƚ��
    public int Plus_Gold; // �׸��� ��� �߰� ����
    public float init_Speed;
    
    public Character(float _max_speed, float _plus_speed, int _max_jump, int _plus_gold, float _init_Speed = 8.0f)
    {
        Max_Speed = _max_speed;
        Plus_Speed = _plus_speed;
        Max_Jump = _max_jump;
        Plus_Gold = _plus_gold;
        init_Speed = _init_Speed;

    }


    
}
