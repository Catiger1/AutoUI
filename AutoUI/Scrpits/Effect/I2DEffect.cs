using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I2DEffect
{
    void Execute(Transform tf,Action callfunc=null);
}
