using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Effect
{
    /// <summary>
    /// ÎÞ¶¯Ð§Òþ²Ø
    /// </summary>
    public class NoneHide2DEffect: I2DEffect
    {
        public void Execute(Transform tf, Action callfunc=null)
        {
            var rect = tf.GetComponent<RectTransform>()?tf.GetComponent<RectTransform>():tf;
            rect.localScale = new Vector3(0,0,0);
            callfunc?.Invoke();
        }
    }
}
