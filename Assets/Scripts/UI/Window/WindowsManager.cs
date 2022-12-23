//Generated Time: 2022/12/23 22:06:05
public class WindowsManager: MonoSingleton<WindowsManager>
{
	public ViewSerializationCfg NormalWindowCfg;
	public ViewSerializationCfg PopUpWindowCfg;
	public override void Init()
	{
		base.Init();
		NormalWindow.SetViewSerializationCfg(NormalWindowCfg);
		PopUpWindow.SetViewSerializationCfg(PopUpWindowCfg);
	}
}
