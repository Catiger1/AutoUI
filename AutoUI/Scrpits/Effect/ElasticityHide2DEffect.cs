using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Effect
{
    /// <summary>
    /// 弹性动效隐藏窗口
    /// </summary>
    public class ElasticityHide2DEffect: I2DEffect
    {
        public void Execute(Transform tf, Action callfunc=null)
        {
            //这一段是因为DoFade有两种方式，一种是通过material来控制，会导致所有窗体背景材质颜色改变，所以要调回去
            //  List<Image> imgList = tf.FindAllTargets<Image>();
            //  foreach(var item in imgList)
            //  {
            //     item.material.color = new Color(1,1,1,1);
            //  }
            tf.Elasticity2DHide(0.5f,0,callfunc);
        }
    }
}
