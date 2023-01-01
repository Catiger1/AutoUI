//Generated Time: 2023/1/1 21:29:45
public class WindowsManager: MonoSingleton<WindowsManager>
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
