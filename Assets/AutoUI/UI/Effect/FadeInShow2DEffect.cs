using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Effect
{
    public class FadeInShow2DEffect : I2DEffect
    {
        private float duration = 1f;
        public void Execute(Transform tf, Action callfunc = null)
        {
             tf.localScale = new Vector3(1,1,1);
             //找到所有image，再DoFade
             List<Image> imgList = tf.FindAllTargets<Image>();

             List<Text> textList = tf.FindAllTargets<Text>();
             foreach(var item in textList)
             {
                item.DOFade(1,duration).SetEase(Ease.OutBack);
             }

             foreach(var item in imgList)
             {
                item.DOFade(1,duration).SetEase(Ease.OutBack).onComplete=()=>{callfunc?.Invoke();};
             }
        }
    }
}
