using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Effect
{
    public class ElasticityHide2DEffect: I2DEffect
    {
        public void Execute(Transform tf, Action callfunc=null)
        {
            tf.Elasticity2DHide(0.5f,0,callfunc);
        }
    }
}
