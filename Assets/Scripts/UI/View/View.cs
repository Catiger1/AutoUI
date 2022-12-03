using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View<T,Q> :IView<T> where T:ViewSerializationCfg where Q:EventData
{
    public abstract void Open(T cfg,ViewFunc<T> func);
    public abstract void Close(T cfg);
    public virtual void Refresh(T cfg,Func<T,bool> autoClose=null){
        //每次刷新页面都检测下是否满足关闭条件
        if(autoClose!=null&&autoClose(cfg))
            Close(cfg);
    }
    public virtual void RefreshNextFrame(T cfg){}
    public virtual  void AutoClose(T cfg,Func<T,bool> autoClose){}
    public abstract void BindAndLoadPrefab(string path);
    public abstract void ExecuteEvent(Action<Q> events);
    public abstract void ClearCallFunc();
    public abstract void ProduceEffect(T cfg);
}
