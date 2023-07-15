using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View
{
    public abstract void Open(WindowData data);
    public abstract void Close(WindowData data);
    public virtual void Refresh(WindowData data,Func<WindowData, bool> autoClose=null){
        //每次刷新页面都检测下是否满足关闭条件
        if(autoClose!=null&&autoClose(data))
            Close(data);
    }
    public virtual void RefreshNextFrame(WindowData data){}
    public virtual  void AutoClose(WindowData data,Func<ViewSerializationCfg, bool> autoClose){}
    public abstract void BindAndLoadPrefab(string path);
    public abstract void ExecuteEvent(Action<EventData> events);
    public abstract void ClearCallFunc();
    protected abstract void ProduceEffect(ViewSerializationCfg cfg);
}
