using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Max_Speed; // 캐릭터의 최고 속도 테마별로 다름.
    public int Max_Jump; // 캐릭터의 최고 점프 횟수
    public int Plus_Gold; // 테마별 골드 추가 수금
    public float init_Speed; // 캐릭터 초기속도
    public float Lucky; // 캐릭터 운빨
    
    public Character(float _max_speed, int _max_jump, int _plus_gold, float _Lucky, float _init_Speed = 8.0f)
    {
        Max_Speed = _max_speed;
        Max_Jump = _max_jump;
        Plus_Gold = _plus_gold;
        init_Speed = _init_Speed;
        Lucky = _Lucky;

    }


    
}
