using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{
    //��ȭ ���� DATA
    public int Cash; // ĳ�� ��ȭ
    public int Gold; // �ΰ��� ��ȭ
    public int[] Speacial; // ����� ��ȭ

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
