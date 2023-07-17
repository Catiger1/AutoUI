using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLinkList : LinkList<CommandData>
{
    //对每个节点执行，并把执行好的命令节点全部移除了
    public void CommandLinkExecute()
    {
        ProcessAllNodeWithDelete(
            (node)=>{
                //满足无条件执行或者条件执行
                if(node.Data.condition==null||node.Data.condition() && (Time.time-node.Data.CreateTime > node.Data.Delay))
                {
                    node.Data.command();
                    node.Data.isCommandExecute = true;
                }
                //满足不执行直接移除条件
                else if(node.Data.removeCondition!=null&&node.Data.removeCondition() && (!node.Data.DelayRemove ||(Time.time - node.Data.CreateTime > node.Data.Delay)))
                {
                    node.Data.isCommandExecute = true;
                }else{}
            },(node)=>{
                return node.Data.isCommandExecute;
            }
        );     
    }
    /// <summary>
    /// 执行并移除节点
    /// </summary>
    /// <param name="node"></param>
    public void ExecuteAndRemoveNode(Node<CommandData> node)
    {
        node.Data.command();
        Delete(node);
    }
}
