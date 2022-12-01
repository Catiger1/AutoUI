using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView<T> where T:ViewConfigData
{
    void Open(T data,ViewFunc<T> func,ViewSerializationCfg cfg);

    void Close(T data);

    //void AddTo(IView<T> parent);
    void Refresh(T data,Func<T,bool> autoClose);

    void RefreshNextFrame(T data);

    void AutoClose(Func<T,bool> autoClose);
}
