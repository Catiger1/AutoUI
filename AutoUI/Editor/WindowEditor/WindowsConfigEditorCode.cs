//Generated Time: 2023/7/15 21:58:24
public partial class WindowsConfigEditor
{
	public ViewSerializationCfg TestWindowCfg = new ViewSerializationCfg();
	private void LoadAllConfigByGameObject()
	{
		TestWindowCfg = WindowsManager.Instance.TestWindowCfg;
		serializedObject.Update();
	}
	private void LoadAllConfigByEditor()
	{
		dataManager.Load();
		TestWindowCfg = dataManager.GetCfgData(nameof(TestWindow));
		serializedObject.Update();
	}
	private void SetAllConfig()
	{
		SetConfig(nameof(TestWindowCfg),TestWindowCfg);
	}
	private void SaveConfig()
	{
		WindowsManager.Instance.TestWindowCfg = TestWindowCfg;
	}
	private void UpdateDataManager()
	{
		dataManager.Update(nameof(TestWindow),TestWindowCfg);
	}
}
