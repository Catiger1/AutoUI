using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
/// 提供给用户修改的窗口管理脚本
/// </summary>
public partial class WindowsManager
{
    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(gameObject);
    }
}