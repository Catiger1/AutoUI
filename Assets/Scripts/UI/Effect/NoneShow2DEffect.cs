using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Effect
{
    public class NoneShow2DEffect: I2DEffect
    {
        public void Execute(Transform tf, Action callfunc=null)
        {
            var rect = tf.GetComponent<RectTransform>()?tf.GetComponent<RectTransform>():tf;
            rect.localScale = new Vector3(1,1,1);
            callfunc?.Invoke();
        }
    }
}
