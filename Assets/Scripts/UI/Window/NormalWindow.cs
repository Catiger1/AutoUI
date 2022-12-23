//Generated Time: 2022/12/23 22:06:05
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Common;
public class NormalWindow: mWindow<NormalWindow,ViewSerializationCfg,EventData>
{
	public Transform BtnClose;
	private string BtnCloseName = "BtnClose";
	public NormalWindow SetBtnCloseName(string name)
	{
		BtnCloseName = name;
		return this;
	}
	private void BtnsInit(ViewSerializationCfg cfg)
	{
		BtnClose = viewPrefab.transform.FindChildByName(BtnCloseName);
		if(BtnClose!=null)
			 BtnClose.GetComponent<Button>().onClick.AddListener(()=>{Close(cfg);});
	}
	public override void OnCreate(ViewSerializationCfg cfg)
	{
		base.OnCreate(cfg);
		BtnsInit(cfg);
	}
}
