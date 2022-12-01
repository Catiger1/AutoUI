using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View<T,Q> :IView<T> where T:ViewConfigData where Q:EventData
{
    public abstract void Open(T data,ViewFunc<T> func,ViewSerializationCfg cfg=null);
    public abstract void Close(T data);
    public virtual void Refresh(T data,Func<T,bool> autoClose=null){
        //每次刷新页面都检测下是否满足关闭条件
        if(autoClose!=null&&autoClose(data))
            Close(data);
    }
    public virtual void RefreshNextFrame(T data){}
    public virtual  void AutoClose(T data,Func<T,bool> autoClose){}
    public abstract void BindAndLoadPrefab(string path);
    public abstract void ExecuteEvent(Action<Q> events);
    public abstract void ClearCallFunc();
    public abstract void ProduceEffect(ViewSerializationCfg cfg);
}
