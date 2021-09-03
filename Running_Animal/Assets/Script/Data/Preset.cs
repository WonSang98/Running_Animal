using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preset
{
    //������ ������ �÷��� �� �����͸� ����.
    //ex) ���õǾ� �ִ� ĳ���Ͱ� �����̾���.
    //ex) ���õǾ� �ִ� �׸��� �����̾���.

    public Character.CHARACTER_CODE Character;
    public Theme.THEME_CODE Theme;
    public int Difficult;

    public Preset(Character.CHARACTER_CODE _Character, Theme.THEME_CODE _Theme, int _Difficult)
    {
        Character = _Character;
        Theme = _Theme;
        Difficult = _Difficult;
    }

    public Preset DeepCopy()
    {
        Preset pr = new Preset();
        pr.Character = this.Character;
        pr.Theme = this.Theme;
        pr.Difficult = this.Difficult;

        return pr;
    }

    Preset()
    {

    }

}
