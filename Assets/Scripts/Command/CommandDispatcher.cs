using System.Collections;
using System.Collections.Generic;
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

    public static void PushCommand(CommandData data)
    {
        Instance.cmdlinkList.Add(data);
    }

    // Update is called once per frame
    void Update()
    {
        cmdlinkList.CommandLinkExecute();
    }

    private void Start() {
        // CommandDispatcher.PushCommand(new CommandData(){
        //     command = ()=>{Debug.Log("Test");},
        //     condition = ()=>{return ss;}
        // });
        
        CommandDispatcher.PushCommand(new CommandData(){
            command = ()=>{Debug.Log("Test1");},
            
        });

        CommandDispatcher.PushCommand(new CommandData(){
            command = ()=>{Debug.Log("Test2");},
            
        });

        CommandDispatcher.PushCommand(new CommandData(){
            command = ()=>{Debug.Log("Test3");},
            
        });
    }
}
