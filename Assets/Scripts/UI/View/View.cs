using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View<T,Q> :IView<T>,IEventExecute<Q> where T:ViewConfigData where Q:EventData
{
    public abstract void Open(T data,ViewFunc<T> func);
    public abstract void Close(T data);
    public virtual void Refresh(T data){}
    public virtual void RefreshNextFrame(T data){}
    public virtual  void AutoClose(T data){}
    public abstract void BindAndLoadPrefab(string path);
    public abstract void ExecuteEvent(Action<Q> events);
    public abstract void ClearCallFunc();
}
