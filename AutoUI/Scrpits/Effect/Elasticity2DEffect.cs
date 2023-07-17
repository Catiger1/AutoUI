using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI.Effect
{
    public static class Elasticity2DEffect
    {
        static Keyframe[] showkeyframes = new Keyframe[5]{
            new Keyframe(0,0),
            new Keyframe(0.7f,1.2f),
            new Keyframe(0.8f,1f),
            new Keyframe(0.9f,1.1f),
            new Keyframe(1f,1f),
            };
        /// <summary>
        /// 弹性动效打开UI
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="time"></param>
        /// <param name="endvalue"></param>
        /// <param name="callfunc"></param>
        /// <param name="keyframes"></param>
        public static void Elasticity2DShow(this Transform tf,float time=0.5f,float endvalue = 1,Action callfunc = null,Keyframe[] keyframes=null)
        {
            var rect = tf.GetComponent<RectTransform>()?tf.GetComponent<RectTransform>():tf;
            rect.localScale = new Vector3(0,0,0);
            AnimationCurve curve = new AnimationCurve(keyframes==null?showkeyframes:keyframes);

            rect.DOScale(endvalue,time).SetEase(curve).onComplete=()=>{
                callfunc?.Invoke();};
        }
        /// <summary>
        /// 弹性动效隐藏UI
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="time"></param>
        /// <param name="endvalue"></param>
        /// <param name="callfunc"></param>
        public static void Elasticity2DHide(this Transform tf,float time=0.5f,float endvalue = 0,Action callfunc = null)
        {
            var rect = tf.GetComponent<RectTransform>()?tf.GetComponent<RectTransform>():tf;
            rect.DOScale(endvalue,time).SetEase(Ease.InBack).onComplete=()=>{callfunc?.Invoke();};
        }

    }
}

