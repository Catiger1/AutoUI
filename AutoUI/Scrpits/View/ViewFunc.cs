using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindowsCallFuncType
{
    AutoCreateCallFunc = 0,
    CreateCallFunc,
    AfterCreateFunc,
    BeforeShowCallFunc,
    ShowCallFunc,
    ShowByCodeGenerateCallFunc,
    BeforeCloseCallFunc,
    BtnCloseCallFunc,
    CloseCallFunc,
    AutoCloseCallFunc,
    CallFunc,
}

public class ViewFunc
{
    private Dictionary<WindowsCallFuncType, Action<WindowData>> callFuncDic;
    public ViewFunc()
    {
        throw new NotImplementedException("Null Configuration");
    }
    public ViewFunc(Dictionary<WindowsCallFuncType, Action<WindowData>> func)
    {
        if(func!=null)
            callFuncDic = func;
        else
            callFuncDic = new Dictionary<WindowsCallFuncType, Action<WindowData>>();
    }
    public void CallFunc(WindowsCallFuncType funcType, WindowData data =null)
    {
        if(callFuncDic.ContainsKey(funcType))
            callFuncDic[funcType].Invoke(data);
    }
    public void Add(Dictionary<WindowsCallFuncType, Action<WindowData>> func)
    {
        foreach(var item in func)
            callFuncDic[item.Key] += item.Value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    public void Cover(Dictionary<WindowsCallFuncType, Action<WindowData>> func)
    {
        foreach (var item in func)
            callFuncDic[item.Key] = item.Value;
    }

    public void Clear()
    {
        callFuncDic.Clear();
    }
}
