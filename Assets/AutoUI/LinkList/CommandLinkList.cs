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
                //满足无条件执行或者条件执行
                if(node.Data.condition==null||node.Data.condition())
                {
                    node.Data.command();
                    node.Data.isCommandExecute = true;
                }
                //满足不执行直接移除条件
                else if(node.Data.removeCondition!=null&&node.Data.removeCondition())
                {
                    node.Data.isCommandExecute = true;
                }else{}
            },(node)=>{
                return node.Data.isCommandExecute;
            }
        );     
    }

}
