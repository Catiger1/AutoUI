using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView
{
    void Open(ViewSerializationCfg cfg);

    void Close(ViewSerializationCfg cfg);

    void Refresh(ViewSerializationCfg cfg,Func<ViewSerializationCfg,bool> autoClose);

    void RefreshNextFrame(ViewSerializationCfg cfg);

    void AutoClose(ViewSerializationCfg cfg, Func<ViewSerializationCfg, bool> autoClose);
}
