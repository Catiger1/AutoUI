//Generated Time: 2022/12/12 23:16:10
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
