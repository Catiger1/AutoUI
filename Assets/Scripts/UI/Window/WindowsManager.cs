//Generated Time: 2023/1/3 13:32:12
public partial class WindowsManager: MonoSingleton<WindowsManager>
{
	public ViewSerializationCfg PopUpWindowCfg;
	public ViewSerializationCfg NormalWindowCfg;
	public override void Init()
	{
		base.Init();
		PopUpWindow.SetViewSerializationCfg(PopUpWindowCfg);
		NormalWindow.SetViewSerializationCfg(NormalWindowCfg);
	}
}
