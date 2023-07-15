//Generated Time: 2023/7/15 21:58:24
using System;
using System.Collections.Generic;
using UnityEngine;
public enum WindowsType
{
	TestWindow,
}
public partial class WindowsManager: MonoSingleton<WindowsManager>
{
	public ViewSerializationCfg TestWindowCfg;
	private Dictionary<WindowsType, ViewSerializationCfg> cfgDic = new Dictionary<WindowsType, ViewSerializationCfg>();
	private Dictionary<WindowsType, mWindow> windowsDic = new Dictionary<WindowsType, mWindow>();
	public Dictionary<WindowsType, mWindow> WindowsDic { get { return windowsDic; } }
	private Dictionary<string, bool> windowsOpenList = new Dictionary<string, bool>();
	public Dictionary<string, bool> WindowOpenList { get; }
	private void AutoGenerateInit()
	{
		cfgDic.Add(WindowsType.TestWindow,TestWindowCfg);
	}
	public void InitWindows<T>(ViewSerializationCfg cfg = null, Dictionary<WindowsCallFuncType, Action<WindowData>> funcDic = null, WindowData data = null) where T : mWindow, new()
	{
		T window = new T();
		window.SetWindowNameAndType().WindowConfig(this, cfgDic[window.WindowType]).CreateWindowFunc(funcDic, data);
		windowsDic.Add(window.WindowType, window);
	}
	public void OpenWindow(WindowsType type)
	{
		if (windowsDic.ContainsKey(type))
			windowsDic[type].Open();
		else
			throw new Exception("Target window didn't not use function 'InitWindows<T>' to config window");
	}
	public void AddWindowOpen(string windowName)
	{
		if (!windowsOpenList.ContainsKey(windowName))
		windowsOpenList[windowName] = true;
	}
	public void RemoveWindowOpen(string windowName)
	{
		if (windowsOpenList.ContainsKey(windowName))
		windowsOpenList.Remove(windowName);
	}
	public bool HasWindowOpen()
	{
		return windowsOpenList.Count > 0;
	}
	public T GetWindow<T>(WindowsType type) where T : mWindow
	{
		if (windowsDic.ContainsKey(type))
			return windowsDic[type] as T;
		return null;
	}
	public GameObject GetWindowPrefab<T>(WindowsType type) where T : mWindow
	{
		if (windowsDic.ContainsKey(type))
			return windowsDic[type].GetWindowPrefab();
		return null;
	}
	public void InitAllWindows()
	{
		AutoGenerateInit();
		InitWindows<TestWindow>();
	}
}
