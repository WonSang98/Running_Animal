using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Max_Speed; // ĳ������ �ְ� �ӵ� �׸����� �ٸ�.
    public int Max_Jump; // ĳ������ �ְ� ���� Ƚ��
    public int Plus_Gold; // �׸��� ��� �߰� ����
    public float init_Speed; // ĳ���� �ʱ�ӵ�
    public float Lucky; // ĳ���� �
    
    public Character(float _max_speed, int _max_jump, int _plus_gold, float _Lucky, float _init_Speed = 8.0f)
    {
        Max_Speed = _max_speed;
        Max_Jump = _max_jump;
        Plus_Gold = _plus_gold;
        init_Speed = _init_Speed;
        Lucky = _Lucky;

    }


    
}
