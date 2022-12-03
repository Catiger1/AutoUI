using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Effect;
using Assets.Scripts.Common;
public class mWindow<CT,T, Q> : View<T, Q> where CT:mWindow<CT,T,Q>,new() where T : ViewSerializationCfg,new() where Q : EventData
{
    //用于序列化参数控制，比如在unity检视面板选择打开跟关闭的方式
    public T  viewSerializationCfg;
    private ViewFunc<T> viewFunc;
    private bool isOpen = false;
    private bool isInit = false;
    public GameObject viewPrefab;
    public Transform effectView;
    private static mWindow<CT,T,Q> instance;
    private float lastOpenTime = 0;
    public float LastOpenTime
    {
        get{return lastOpenTime;}
    }
    //初始化分为
    //窗口预制件初始化
    //窗口特效初始化
    //窗口事件初始化
    // 
    // public enum InitFlag
    // {
    //     isViewPrefabInit = 1<<0,
    //     isViewEffectInit = 1<<1,
    //     isViewEventInit  = 1<<2,
    // }
    public static mWindow<CT,T,Q> Instance
    {
        get{
            if(instance == null)
                instance = new CT();
            return instance;
        }set{instance = value;}
    }
    private I2DEffect OpenEffect;
    private I2DEffect CloseEffect;
    private const string Path = "Windows/";
    public bool IsOpen
    {
        get{return isOpen;}
    }

    public static mWindow<CT,T,Q> SetViewFunc(ViewFunc<T> func=null)
    {            
        if(instance ==null)
            instance = new CT();
         
         instance.viewFunc=func;
         return instance;
    }
    //根据传入路径设置预制件路径并绑定
    public mWindow<CT,T,Q> SetPrefab(string prefabPath=Path)
    {
        if(instance ==null)
            instance = new CT();
        
        if(instance.viewPrefab==null)
           instance.BindAndLoadPrefab(prefabPath);

        return instance;
    }
    //编辑器序列化输入参数配置
    public static mWindow<CT,T,Q> SetViewSerializationCfg(T cfg=null)
    {
        if(instance ==null)
            instance = new CT();

        instance.viewSerializationCfg = cfg!=null?cfg:new T(); 
        instance.ProduceEffect(instance.viewSerializationCfg);

        if(!cfg.ViewPrefabPath.Equals("Windows/"))
        {
            instance.SetPrefab(cfg.ViewPrefabPath);
        }
        return instance;
    }
    //窗口初始化,缺什么使用默认值初始化什么
    //其中这里的func为添加
    private void ViewInit(T cfg=null,ViewFunc<T> func=null)
    {
        if(instance == null)
            instance = new CT();
        if(viewFunc==null)
            viewFunc = func==null?new ViewFunc<T>():func;
        else if(func!=null)
            viewFunc.Add(func);
        else{}

        if(viewSerializationCfg == null)
          SetViewSerializationCfg(cfg);

        if(viewPrefab==null)
          BindAndLoadPrefab(Path);
    
        instance?.OnCreate(cfg);
        instance?.OnAfterCreate(cfg);

        isInit = true;
    }

    public override void ProduceEffect(T cfg)
    {
        OpenEffect  = WindowsEffect2DFactory.Create2DEffect(cfg.showType);
        CloseEffect = WindowsEffect2DFactory.Create2DEffect(cfg.hideType);
    }
    //添加生命周期
    public virtual void OnCreate(T cfg)
    {
        viewFunc?.CreateCallFunc?.Invoke(cfg);
    }
    public virtual void OnAfterCreate(T cfg)
    {
        viewFunc?.AfterCreateFunc?.Invoke(cfg);
    }
    public virtual void OnBeforeShow(T cfg)
    {
        viewFunc?.BeforeShowCallFunc?.Invoke(cfg);
    }
    public virtual void OnShow(T cfg)
    {
        viewFunc?.ShowCallFunc?.Invoke(cfg);
        
    }
    public virtual void OnRecycle(T cfg)
    {
        viewFunc?.RecycleCallFunc?.Invoke(cfg);
    }
    public override void BindAndLoadPrefab(string path)
    {
        if(viewPrefab==null)
        {
            var type = typeof(CT).Name;
            GameObject go = Resources.Load<GameObject>(path+type);
            if(go!=null)
            {
                viewPrefab = GameObject.Instantiate(go);
                viewPrefab.SetActive(false);
                effectView = viewPrefab.FindChildByName(viewSerializationCfg.EffectViewName);
            }
            else
                Debug.LogError("Can't Not Find prefab in Path");
        }
    }
    public override void Close(T cfg)
    {
       if(viewPrefab!=null)
       {
           isOpen = false;
           if(CloseEffect!=null)
             CloseEffect.Execute(effectView,()=>{   
                viewFunc?.CloseCallFunc?.Invoke(cfg);
                instance.OnRecycle(cfg);
                viewPrefab.SetActive(false);
             });
       }
    }
    public override void ExecuteEvent(Action<Q> events)
    {
       
    }
    public override void Open(T cfg,ViewFunc<T> func=null)
    {
       if(func!=null)
         viewFunc?.Set(func);

       if(viewPrefab!=null)
       {
           instance?.OnBeforeShow(cfg);
           viewPrefab.SetActive(true);
           isOpen = true;
           if(OpenEffect!=null)
             OpenEffect.Execute(effectView,()=>
             {
                viewFunc?.ShowCallFunc?.Invoke(cfg);
                instance?.OnShow(cfg);
             });
           //记录打开时间
           lastOpenTime = Time.time;
       }
    }
    public static void OpenWindow(T cfg=null,ViewFunc<T> func=null,string prefabPath = Path)
    {       
        if(instance==null)
            instance=new CT();
        if(!instance.isInit)
            instance.ViewInit(cfg,func);
        if(cfg!=null)
            instance?.Open(cfg,func); 
        else
            instance?.Open(instance.viewSerializationCfg!=null?instance.viewSerializationCfg:new T(),func);
    }
    public static void CloseWindow(T cfg=null)
    {
        instance.Close(cfg);
    }
    public override void ClearCallFunc()
    {
        viewFunc = null;
    }
}
