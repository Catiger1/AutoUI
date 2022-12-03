using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

public class NormalWindow: mWindow<NormalWindow,ViewSerializationCfg,EventData>
{
    public Transform BtnClose;
    private string BtnCloseName = "BtnClose";
    public NormalWindow SetBtnCloseName(string name)
    {
        BtnCloseName = name;
        return this;
    }
    
    public override void OnCreate(ViewSerializationCfg cfg)
    {
        base.OnCreate(cfg);
        BtnsInit(cfg);
    }
    private void BtnsInit(ViewSerializationCfg cfg)
    {
        BtnClose = viewPrefab.transform.FindChildByName(BtnCloseName);
        if(BtnClose!=null)
            BtnClose.GetComponent<Button>().onClick.AddListener(()=>{Close(cfg);});
    }

    public override void OnShow(ViewSerializationCfg cfg)
    {
        base.OnShow(cfg);
    }
}
