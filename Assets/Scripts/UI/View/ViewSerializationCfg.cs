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
[SerializeField]
public class ViewSerializationCfg
{
    public ViewShowType showType = ViewShowType.None;
    public ViewHideType hideType = ViewHideType.None;
    public string EffectViewName = "EffectView";
}
