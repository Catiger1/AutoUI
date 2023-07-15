using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public partial class WindowsManager
{
    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(gameObject);
    }
}