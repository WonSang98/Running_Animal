using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    // �� ���� �����ؾ� �ϴ� �нú� ��ų���.
    public Dictionary<Passive.PASSIVE_CODE, bool> passive_once = new Dictionary<Passive.PASSIVE_CODE, bool>()
    {
        {Passive.PASSIVE_CODE.Active_Twice, false},
        {Passive.PASSIVE_CODE.Magenet, false},
        {Passive.PASSIVE_CODE.Resurrection, false},
        {Passive.PASSIVE_CODE.Auto_Jump, false},
        {Passive.PASSIVE_CODE.Random_God, false},
        {Passive.PASSIVE_CODE.Auto_Restore, false},
    };
}
