using UnityEngine;

public class CommandDispatcher :MonoSingleton<CommandDispatcher>
{
    CommandLinkList cmdlinkList;

    //命令调度是否为空，为空则空闲，后续最好改成数量限制做保护，或者说考虑一个GameObject只能几个命令
    public bool IsFree
    {
        get{return cmdlinkList.IsEmpty();}
    }

    public override void Init()
    {
        base.Init();
        cmdlinkList = new CommandLinkList();
    }

    public static Node<CommandData> PushCommand(CommandData data)
    {
        data.CreateTime = Time.time;
        return Instance.cmdlinkList.Add(data);
    }

    public static void ExecuteCommand(Node<CommandData> node)
    {
        Instance.cmdlinkList.ExecuteAndRemoveNode(node);
    }

    // Update is called once per frame
    void Update()
    {
        cmdlinkList.CommandLinkExecute();
    }
}
