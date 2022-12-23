//Generated Time: 2022/12/23 22:06:05
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Common;
public class PopUpWindow: mWindow<PopUpWindow,ViewSerializationCfg,EventData>
{
	public Transform BtnClose;
	private string BtnCloseName = "BtnClose";
	public PopUpWindow SetBtnCloseName(string name)
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
				CloseWindow();
			},
			condition = ()=>{
				return autoClose(data);
			}
		});
	}
}
