using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager: MonoSingleton<WindowsManager>
{
    public override void Init()
    {
        base.Init();

        NormalWindow.SetViewSerializationCfg(new ViewSerializationCfg(){
            showType = ViewShowType.None,
            hideType = ViewHideType.None
        });

        
    }
}
