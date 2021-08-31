using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status<T>
{
    public short setting { get; set; }
    public short level { get; set; }
    public T value { get; set; }

    public Status(short _setting, short _level, T _value)
    {
        setting = _setting;
        level = _level;
        value = _value;
    }

    Status()
    {

    }
}
