using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FGMmpMananger
{
    public static Action _Initialisation;
    public static event Action Initialisation
    {
        add
        {
            _Initialisation += value;
        }
        remove
        {
            _Initialisation -= value;
        }
    }
}
