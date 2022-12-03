using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindowsManager: MonoSingleton<WindowsManager>
{
    public ViewSerializationCfg NormalWindowCfg;
    public override void Init()
    {
        base.Init();
        NormalWindow.SetViewSerializationCfg(NormalWindowCfg);
    }
}
