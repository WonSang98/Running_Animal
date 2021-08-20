using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    // 한 번만 등장해야 하는 패시브 스킬목록.
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
