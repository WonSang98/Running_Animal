using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{
    //재화 관련 DATA
    public int Cash; // 캐쉬 재화
    public int Gold; // 인게임 재화
    public int[] Speacial; // 스페셜 재화

    public Money(int _Cash, int _Gold, int[] _Speacial)
    {
        Cash = _Cash;
        Gold = _Gold;
        Speacial = _Speacial;
    }

    Money()
    {

    }
}
