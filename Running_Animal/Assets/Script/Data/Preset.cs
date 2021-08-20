using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preset
{
    //기존에 유저가 플레이 한 데이터를 저장.
    //ex) 선택되어 있던 캐릭터가 무엇이었나.
    //ex) 선택되어 있던 테마가 무엇이었나.

    public Character.CHARACTER_CODE Character;
    public Theme.THEME_CODE Theme;
    public int Difficult;

    public Preset(Character.CHARACTER_CODE _Character, Theme.THEME_CODE _Theme, int _Difficult)
    {
        Character = _Character;
        Theme = _Theme;
        Difficult = _Difficult;
    }

    Preset()
    {

    }

}
