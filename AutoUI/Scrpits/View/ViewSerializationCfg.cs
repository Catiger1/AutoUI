using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视图展示动效类型
/// </summary>
public enum ViewShowType
{
    None,
    Elasticity,
    FadeIn,
}
/// <summary>
/// 视图隐藏动效类型
/// </summary>
public enum ViewHideType
{
    None,
    Elasticity,
    FadeOut,
}

//窗口的序列化设置
[Serializable]
public class ViewSerializationCfg
{
    public bool isOpenSoon = false;
    public ViewShowType showType = ViewShowType.None;
    public ViewHideType hideType = ViewHideType.None;
    public string EffectViewName = "EffectView";
    public string ViewPrefabPath = "Windows/";
    public bool ButtonClose = true;
    public bool AutoCloseEnable = false;
    public float AutoCloseTime=5f;
}
