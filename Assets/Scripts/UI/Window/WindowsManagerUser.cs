using System.Collections.Generic;

public partial class WindowsManager
{
    private Dictionary<string,bool> WindowOpenList = new Dictionary<string, bool>();
    public void AddWindowOpen(string windowName)
	{
		if(!WindowOpenList.ContainsKey(windowName))
			WindowOpenList[windowName] = true;
	}
	public void RemoveWindowOpen(string windowName)
	{
		if(WindowOpenList.ContainsKey(windowName))
			WindowOpenList.Remove(windowName);
	}
	public bool HasWindowOpen()
	{
		return WindowOpenList.Count>0;
	}
}