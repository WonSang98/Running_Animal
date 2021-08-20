using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status<T>
{
    public short level { get; set; }
    public T value { get; set; }

    public Status(short _level, T _value)
    {
        level = _level;
        value = _value;
    }

    Status()
    {

    }
}
