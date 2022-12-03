using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView<T> where T:ViewSerializationCfg
{
    void Open(T cfg,ViewFunc<T> func);

    void Close(T cfg);

    //void AddTo(IView<T> parent);
    void Refresh(T cfg,Func<T,bool> autoClose);

    void RefreshNextFrame(T cfg);

    void AutoClose(T cfg,Func<T,bool> autoClose);
}
