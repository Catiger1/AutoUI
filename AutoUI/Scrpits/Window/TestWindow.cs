//Generated Time: 2023/7/15 21:58:24
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Common;
public partial class TestWindow: mWindow
{
	public Transform BtnClose;
	private string BtnCloseName = "BtnClose";
	public TestWindow SetBtnCloseName(string name)
	{
		BtnCloseName = name;
		return this;
	}
	private void BtnsInit(WindowData data)
	{
		BtnClose = viewPrefab.transform.FindChildByName(BtnCloseName);
		if(BtnClose!=null)
			 BtnClose.GetComponent<Button>().onClick.AddListener(()=>{Close(data);});
	}
	protected override void OnAutoCreate(WindowData data)
	{
		base.OnAutoCreate(data);
		BtnsInit(data);
	}
}
