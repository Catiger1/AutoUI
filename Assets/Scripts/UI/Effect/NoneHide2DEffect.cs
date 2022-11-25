using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Effect
{
    public class NoneHide2DEffect: I2DEffect
    {
        public void Execute(Transform tf, Action callfunc=null)
        {
            callfunc?.Invoke();
        }
    }
}
