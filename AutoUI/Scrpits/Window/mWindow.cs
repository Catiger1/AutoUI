using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Effect;
using Assets.Scripts.Common;
using System.Runtime.InteropServices.ComTypes;

public abstract class mWindow: View {
    //用于序列化参数控制，比如在unity检视面板选择打开跟关闭的方式
    protected ViewSerializationCfg viewSerializationCfg;
    public WindowsType WindowType { get;protected set; }
    public bool IsInit { get; set; }
    private ViewFunc viewFunc;
    protected GameObject viewPrefab;
    protected Transform effectView;
    private WindowsManager manager;
    public float LastOpenTime
    {
        get; set;
    }
    public string WindowName{get;set;}
    protected I2DEffect OpenEffect;
    protected I2DEffect CloseEffect;

    public Node<CommandData> autoCloseNode;
    public GameObject GetWindowPrefab()
    {
        return viewPrefab;
    }
    public mWindow SetWindowNameAndType()
    {
        WindowName = this.GetType().Name;
        WindowType = (WindowsType)Enum.Parse(typeof(WindowsType), WindowName);
        return this;
    }
    public mWindow WindowConfig(WindowsManager manager,ViewSerializationCfg cfg = null)
    {
        if (viewSerializationCfg == null)
            SetViewSerializationCfg(cfg);

        IsInit = true;
        this.manager = manager;
        //viewPrefab.transform.SetParent(manager.transform);
        
        return this;
    }

    /// <summary>
    /// Set window life cycle function and Use create window life cycle function
    /// </summary>
    public void CreateWindowFunc(Dictionary<WindowsCallFuncType, Action<WindowData>> funcDic = null, WindowData data=null)
    {
        viewFunc = new ViewFunc(funcDic);
        OnAutoCreate(data);
        OnCreate(data);
        viewPrefab.SetActive(false);
    }

    //编辑器序列化输入参数配置
    public void SetViewSerializationCfg(ViewSerializationCfg cfg = null)
    {
        viewSerializationCfg = cfg != null ? cfg : new ViewSerializationCfg();
        ProduceEffect(viewSerializationCfg);
        BindAndLoadPrefab(viewSerializationCfg.ViewPrefabPath);
    }

    protected override void ProduceEffect(ViewSerializationCfg cfg)
    {
        OpenEffect = WindowsEffect2DFactory.Create2DEffect(cfg.showType);
        CloseEffect = WindowsEffect2DFactory.Create2DEffect(cfg.hideType);
    }

    //添加生命周期
    protected virtual void OnAutoCreate(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.AutoCreateCallFunc, data);
    }
    public virtual void OnCreate(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.CreateCallFunc, data);
    }
    public virtual void OnAfterCreate(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.AfterCreateFunc, data);
    }
    public virtual void OnBeforeShow(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.BeforeShowCallFunc, data);
    }
    public virtual void OnShowByCodeGenerate(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.ShowByCodeGenerateCallFunc, data);
    }
    public virtual void OnShow(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.ShowCallFunc, data);
    }
    public virtual void OnBeforeClose(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.BeforeCloseCallFunc, data);
    }
    public virtual void OnClose(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.CloseCallFunc, data);
    }
    public override void BindAndLoadPrefab(string path)
    {
        GameObject go = Resources.Load<GameObject>(path + WindowName);
        if (go != null)
        {
            viewPrefab = GameObject.Instantiate(go);
            effectView = viewPrefab.FindChildByName(viewSerializationCfg.EffectViewName);
            GameObject.DontDestroyOnLoad(viewPrefab);
        }
        else
            Debug.LogError("Can't Not Find prefab in Path");
    }
    public override void ExecuteEvent(Action<EventData> events)
    {

    }

    public void AutoCloseImmediately()
    {
        if(autoCloseNode!=null)
            CommandDispatcher.ExecuteCommand(autoCloseNode);
    }

    public override void Close(WindowData data=null)
    {
        if (viewPrefab != null)
        {
            OnBeforeClose(data);
            manager.RemoveWindowOpen(WindowName);
            if (CloseEffect != null)
                CloseEffect.Execute(effectView, () =>
                {
                    OnClose(data);
                    viewPrefab.SetActive(false);
                });
        }
    }

    public override void Open(WindowData data=null)
    {
        //如果是立刻打开窗口
        CommandDispatcher.PushCommand(new CommandData()
        {
            command = () =>
            {
                manager.AddWindowOpen(WindowName);
                OnBeforeShow(data);
                viewPrefab.SetActive(true);
                if (OpenEffect != null)
                    OpenEffect.Execute(effectView, () =>
                    {
                        OnShowByCodeGenerate(data);
                        OnShow(data);
                    });
                //记录打开时间
                LastOpenTime = Time.time;
            },
            condition = () =>
            {
                //命令执行条件是如果有窗口打开中，且设置为非立刻打开，则暂缓打开窗口，直到没有窗口打开中
                //后面这个值应该是新打开的窗口
                if (manager.HasWindowOpen() && !viewSerializationCfg.isOpenSoon)
                    return false;
                else
                    return true;

            }
        });
    }
    public override void ClearCallFunc()
    {
        viewFunc.Clear();
    }
}
