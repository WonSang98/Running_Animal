using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme
{
    public enum THEME_CODE
    {
        Forest = 0,
        Desert,
        Arctic
    }

    public static int[] COST = { 0, 999, 999 }; // 테마 해금 비용
}
