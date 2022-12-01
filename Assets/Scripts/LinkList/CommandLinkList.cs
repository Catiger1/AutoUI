using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLinkList : LinkList<CommandData>
{
    //对每个节点执行，并把执行好的命令节点全删了
    public void CommandLinkExecute()
    {
        ProcessAllNodeWithDelete(
            (node)=>{
                if(node.Data.condition==null||node.Data.condition())
                {
                    node.Data.command();
                    node.Data.isCommandExecute = true;
                }
            },(node)=>{
                return node.Data.isCommandExecute;
            }
        );     
    }

}
