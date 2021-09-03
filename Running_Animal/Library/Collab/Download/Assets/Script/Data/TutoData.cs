using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoData 
{
    public bool tuto0;
    public bool tuto1;
    public bool tuto_preitem;
    public bool tuto_character;
    public bool tuto_talent;
    // Start is called before the first frame update
    
    public TutoData(bool _tuto0, bool _tuto1, bool _tuto_preitem, bool _tuto_character, bool _tuto_talent)
    {
        tuto0 = _tuto0;
        tuto1 = _tuto1;
        tuto_preitem = _tuto_preitem;
        tuto_character = _tuto_character;
        tuto_talent = _tuto_talent;
    }

    TutoData()
    {
    }
}
