using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
/// �ṩ���û��޸ĵĴ��ڹ���ű�
/// </summary>
public partial class WindowsManager
{
    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(gameObject);
    }
}