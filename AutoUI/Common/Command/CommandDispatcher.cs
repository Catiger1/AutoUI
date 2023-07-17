using UnityEngine;
/// <summary>
/// 命令调度器
/// </summary>
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
    /// <summary>
    /// 推送节点进入命令调度器链表中判断执行
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Node<CommandData> PushCommand(CommandData data)
    {
        data.CreateTime = Time.time;
        return Instance.cmdlinkList.Add(data);
    }
    /// <summary>
    /// 执行节点
    /// </summary>
    /// <param name="node"></param>
    public static void ExecuteCommand(Node<CommandData> node)
    {
        Instance.cmdlinkList.ExecuteAndRemoveNode(node);
    }

    //每帧循环检测链表中的每个节点的条件并判断是否执行
    void Update()
    {
        cmdlinkList.CommandLinkExecute();
    }
}
