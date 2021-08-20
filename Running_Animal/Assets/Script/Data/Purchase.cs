using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase
{
    //유저가 구매한 내역을 관리
    //1. 캐릭터 구매 내역.
    public bool[] Character;
    public bool[] Theme;
     
    public Purchase(
        bool[] _Chracter,
        bool[] _Theme)
    {
        Character = _Chracter;
        Theme = _Theme;
    }

    Purchase()
    {

    }

}
