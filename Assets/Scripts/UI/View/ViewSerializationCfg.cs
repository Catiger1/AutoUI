using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewShowType
{
    None,
    Elasticity,
}

public enum ViewHideType
{
    None,
    Elasticity,
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
    public bool AutoCloseEnable = false;
    public bool ButtonClose = true;
    public float AutoCloseTime=5f;
}
