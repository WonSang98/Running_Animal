using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase
{
    //������ ������ ������ ����
    //1. ĳ���� ���� ����.
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
