using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

public class NormalWindow: mWindow<NormalWindow,ViewConfigData,EventData>
{
    public Transform BtnClose;
    private string BtnCloseName = "BtnClose";
    public NormalWindow SetBtnCloseName(string name)
    {
        BtnCloseName = name;
        return this;
    }

    public override void OnCreate(ViewConfigData data)
    {
        base.OnCreate(data);
        BtnsInit();
    }
    private void BtnsInit()
    {
        BtnClose = viewPrefab.transform.FindChildByName(BtnCloseName);
        if(BtnClose!=null)
            BtnClose.GetComponent<Button>().onClick.AddListener(()=>{Close(viewConfigData);});
    }

    public override void OnShow(ViewConfigData data)
    {
        base.OnShow(data);
    }
}
