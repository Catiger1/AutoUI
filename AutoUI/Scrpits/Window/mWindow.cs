using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Effect;
using Assets.Scripts.Common;
using System.Runtime.InteropServices.ComTypes;

public abstract class mWindow: View {
    //用于序列化参数控制，比如在unity检视面板选择打开跟关闭的方式
    protected ViewSerializationCfg viewSerializationCfg;
    //窗口类型，由创建时代码自动生成的枚举类型
    public WindowsType WindowType { get;protected set; }
    //是否已经初始化
    public bool IsInit { get; set; }
    //弹窗的生命周期函数集合结构
    private ViewFunc viewFunc;
    //整个弹窗的窗口预制件
    protected GameObject viewPrefab;
    //受到弹窗打开/关闭动效影响的父节点的GameObject
    protected Transform effectView;
    private WindowsManager manager;
    //打开的时间，用于各种计算（比如弹窗自动关闭）
    public float LastOpenTime
    {
        get; set;
    }
    //窗口名
    public string WindowName{get;set;}

    //打开跟关闭动效，读取配置并以反射生成
    protected I2DEffect OpenEffect;
    protected I2DEffect CloseEffect;
    //自动关闭的节点，由此执行，并执行自动关闭的代码
    public Node<CommandData> autoCloseNode;
    /// <summary>
    /// 获取窗口预制件
    /// </summary>
    /// <returns></returns>
    public GameObject GetWindowPrefab()
    {
        return viewPrefab;
    }
    /// <summary>
    /// 设置窗口名跟类型
    /// </summary>
    /// <returns></returns>
    public mWindow SetWindowNameAndType()
    {
        WindowName = this.GetType().Name;
        WindowType = (WindowsType)Enum.Parse(typeof(WindowsType), WindowName);
        return this;
    }
    /// <summary>
    /// 窗口初始化
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="cfg"></param>
    /// <returns></returns>
    public mWindow WindowConfig(WindowsManager manager,ViewSerializationCfg cfg = null)
    {
        if (viewSerializationCfg == null)
            SetViewSerializationCfg(cfg);
        IsInit = true;
        this.manager = manager;
        return this;
    }

    /// <summary>
    /// 设置窗口生命周期函数
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
    /// <summary>
    /// 反射生成打开/关闭动效
    /// </summary>
    /// <param name="cfg"></param>
    protected override void ProduceEffect(ViewSerializationCfg cfg)
    {
        OpenEffect = WindowsEffect2DFactory.Create2DEffect(cfg.showType);
        CloseEffect = WindowsEffect2DFactory.Create2DEffect(cfg.hideType);
    }

    /// <summary>
    /// 窗口自动关闭时执行
    /// </summary>
    /// <param name="data"></param>
    protected virtual void OnAutoCreate(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.AutoCreateCallFunc, data);
    }
    /// <summary>
    /// 窗口创建时执行
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnCreate(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.CreateCallFunc, data);
    }
    /// <summary>
    /// 窗口展示前时执行（执行打开动效前）
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnBeforeShow(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.BeforeShowCallFunc, data);
    }
    /// <summary>
    /// 用于展示前的生命周期（代码部分生成使用）
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnShowByCodeGenerate(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.ShowByCodeGenerateCallFunc, data);
    }
    /// <summary>
    /// 用于展示的生命周期（打开动效执行后）
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnShow(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.ShowCallFunc, data);
    }
    /// <summary>
    /// 关闭前执行的生命周期（关闭动效执行前）
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnBeforeClose(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.BeforeCloseCallFunc, data);
    }
    /// <summary>
    /// 关闭时执行的生命周期（关闭动效执行完后）
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnClose(WindowData data)
    {
        viewFunc?.CallFunc(WindowsCallFuncType.CloseCallFunc, data);
    }
    /// <summary>
    /// 加载跟绑定对应窗口预制件
    /// </summary>
    /// <param name="path"></param>
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
    /// <summary>
    /// 带有自动关闭功能的窗口立刻执行自动关闭
    /// </summary>
    public void AutoCloseImmediately()
    {
        if(autoCloseNode!=null)
            CommandDispatcher.ExecuteCommand(autoCloseNode);
    }
    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <param name="data"></param>
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
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="data"></param>
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
    /// <summary>
    /// 清理生命周期函数
    /// </summary>
    public override void ClearCallFunc()
    {
        viewFunc.Clear();
    }
}
