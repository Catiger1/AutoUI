using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindow : mWindow<PopUpWindow,ViewSerializationCfg,EventData>
{
    public override void OnShow(ViewSerializationCfg cfg)
    {
        base.OnShow(cfg);
        AutoClose(cfg,(viewCfg)=>{
            return  viewCfg.AutoCloseEnable&&(Time.time-LastOpenTime>=viewCfg.AutoCloseTime);
        });
    }

    public override void AutoClose(ViewSerializationCfg data,Func<ViewSerializationCfg, bool> autoClose)
    {
        base.AutoClose(data,autoClose);
        CommandDispatcher.PushCommand(new CommandData(){
            command = ()=>{
                //关闭
                CloseWindow();
            },
            condition = ()=>{
                return autoClose(data);
            }
        });
    }
}
