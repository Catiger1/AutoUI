using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : mWindow<PopUpWindow,ViewConfigData,EventData>
{
    public override void OnShow(ViewConfigData data)
    {
        base.OnShow(data);
        AutoClose(data,(viewData)=>{
            return  Time.time-LastOpenTime>=viewData.autoCloseTime;
        });
    }

    public override void AutoClose(ViewConfigData data,Func<ViewConfigData, bool> autoClose)
    {
        base.AutoClose(data,autoClose);
        CommandDispatcher.PushCommand(new CommandData(){
            command = ()=>{
                //关闭
                CloseWindow();
            },
            condition = ()=>{return autoClose(data);}
        });

    }
}
