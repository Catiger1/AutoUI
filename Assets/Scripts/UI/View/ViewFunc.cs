using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFunc<T> :IFillFunc<ViewFunc<T>> where T:ViewSerializationCfg
{
    public Action<T> CreateCallFunc;
    public Action<T> AfterCreateFunc;
    public Action<T> BeforeShowCallFunc;
    public Action<T> ShowCallFunc;
    public Action<T> BeforeCloseCallFunc;
    public Action<T> BtnCloseCallFunc;
    public Action<T> CloseCallFunc;
    public Action<T> AutoCloseCallFunc;
    public Action<T> RecycleCallFunc;
    public Action<T> CallFunc;

    public void Add(ViewFunc<T> func)
    {
        if(func.CreateCallFunc!=null)
            CreateCallFunc += func.CreateCallFunc;

        if(func.AfterCreateFunc!=null)
            AfterCreateFunc += func.AfterCreateFunc;

        if(func.BeforeShowCallFunc!=null)
            BeforeShowCallFunc += func.BeforeShowCallFunc;

        if(func.ShowCallFunc!=null)
            ShowCallFunc += func.ShowCallFunc;    

        if(func.BeforeCloseCallFunc!=null)
            BeforeCloseCallFunc += func.BeforeCloseCallFunc;

        if(func.BtnCloseCallFunc!=null)
            BtnCloseCallFunc += func.BtnCloseCallFunc;

        if(func.CloseCallFunc!=null)
            CloseCallFunc += func.CloseCallFunc;

        if(func.AutoCloseCallFunc!=null)
            AutoCloseCallFunc += func.AutoCloseCallFunc;

        if(func.RecycleCallFunc!=null)
            RecycleCallFunc += func.RecycleCallFunc;

        if(func.CallFunc!=null)
            CallFunc += func.CallFunc;
    }

    public void Set(ViewFunc<T> func)
    {
        if(func.CreateCallFunc!=null)
            CreateCallFunc = func.CreateCallFunc;

        if(func.AfterCreateFunc!=null)
            AfterCreateFunc = func.AfterCreateFunc;

        if(func.BeforeShowCallFunc!=null)
            BeforeShowCallFunc = func.BeforeShowCallFunc;

        if(func.ShowCallFunc!=null)
            ShowCallFunc = func.ShowCallFunc;    

        if(func.BeforeCloseCallFunc!=null)
            BeforeCloseCallFunc = func.BeforeCloseCallFunc;

        if(func.BtnCloseCallFunc!=null)
            BtnCloseCallFunc = func.BtnCloseCallFunc;

        if(func.CloseCallFunc!=null)
            CloseCallFunc = func.CloseCallFunc;

        if(func.AutoCloseCallFunc!=null)
            AutoCloseCallFunc = func.AutoCloseCallFunc;

        if(func.RecycleCallFunc!=null)
            RecycleCallFunc = func.RecycleCallFunc;

        if(func.CallFunc!=null)
            CallFunc = func.CallFunc;
    }

    public void Clear()
    {
        CreateCallFunc = null;
        AfterCreateFunc = null;
        BeforeShowCallFunc = null;
        ShowCallFunc = null;
        BeforeCloseCallFunc = null;
        BtnCloseCallFunc = null;
        CloseCallFunc = null;
        AutoCloseCallFunc = null;
        RecycleCallFunc = null;
        CallFunc = null;
    }
}
