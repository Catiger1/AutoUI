using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView<T> where T:ViewConfigData
{
    void Open(T data,ViewFunc<T> func);

    void Close(T data);

    //void AddTo(IView<T> parent);
    void Refresh(T data);

    void RefreshNextFrame(T data);

    void AutoClose(T data);
}
