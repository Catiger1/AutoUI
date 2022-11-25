using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Effect;
using Assets.Scripts.Common;
public class mWindow<CT,T, Q> : View<T, Q> where CT:mWindow<CT,T, Q>,new() where T : ViewConfigData where Q : EventData
{
    //用于序列化参数控制，比如在unity检视面板选择打开跟关闭的方式
    public ViewSerializationCfg  viewSerializationCfg;
    //用于自身传参输入配置
    public T viewConfigData;
    private ViewFunc<T> viewFunc;
    private bool isOpen = false;
    public static GameObject viewPrefab;
    private static mWindow<CT,T,Q> instance;
    private I2DEffect OpenEffect;
    private I2DEffect CloseEffect;
    private const string Path = "Windows/";
    public  ViewFunc<T> SetViewFunc
    {
        get{
            if(viewFunc==null)
                viewFunc = new ViewFunc<T>();
            return viewFunc;
        }
        set{ viewFunc = value;}
    }

    public bool IsOpen
    {
        get{return isOpen;}
    }
    public mWindow(string prefabPath = Path,ViewFunc<T> func=null)
    {
       Init(prefabPath,func);
       OnCreate();
       //这中间最好能延时一帧
       //if(viewPrefab!=null)
       //viewPrefab.GetComponent<MonoBehaviour>().DelayCallBack(0,()=>{OnAfterCreate();});
       OnAfterCreate();
    }

    void Init(string path,ViewFunc<T> func=null)
    {
        instance = this;
        viewFunc = func==null?new ViewFunc<T>():func;
        if(viewPrefab==null)
          BindAndLoadPrefab(path);
    }

    void ProduceEffect()
    {
        OpenEffect  = WindowsEffect2DFactory.Create2DEffect(viewSerializationCfg.showType);
        CloseEffect = WindowsEffect2DFactory.Create2DEffect(viewSerializationCfg.hideType);
    }
    //添加生命周期
    public virtual void OnCreate()
    {
        viewFunc?.CreateCallFunc?.Invoke(viewConfigData);
    }
    public virtual void OnAfterCreate()
    {
        viewFunc?.AfterCreateFunc?.Invoke(viewConfigData);
    }
    public virtual void OnBeforeShow()
    {
        viewFunc?.BeforeShowCallFunc?.Invoke(viewConfigData);
    }
    public virtual void OnShow()
    {
        viewFunc?.ShowCallFunc?.Invoke(viewConfigData);
    }
    public virtual void OnRecycle()
    {
        viewFunc?.RecycleCallFunc?.Invoke(viewConfigData);
    }
    
    public override void BindAndLoadPrefab(string path)
    {
        if(viewPrefab==null)
        {
            var type = GetType().Name;
            GameObject go = Resources.Load<GameObject>(path+type);
            if(go!=null)
                viewPrefab = GameObject.Instantiate(go);
        }
    }
    public override void Close(T data)
    {
       if(viewPrefab!=null)
       {
           viewPrefab.SetActive(false);
           isOpen = false;
           if(CloseEffect!=null)
             CloseEffect.Execute(viewPrefab.transform,()=>{
                viewFunc?.CloseCallFunc(data);
                OnRecycle();
             });
       }
    }

    public override void ExecuteEvent(Action<Q> events)
    {
       
    }

    public override void Open(T data,ViewFunc<T> func=null)
    {
       if(func!=null)
         viewFunc.Add(func);

       if(viewPrefab!=null)
       {
           OnBeforeShow();
           viewPrefab.SetActive(true);
           isOpen = true;
           if(OpenEffect!=null)
             OpenEffect.Execute(viewPrefab.transform,()=>
             {
                viewFunc?.ShowCallFunc(data);
                OnShow();
             });
       }
    }

    public static void OpenWindow(T data=null,ViewFunc<T> func=null,string prefabPath = Path)
    {
        if(instance==null)
         {
            instance = new CT();
         }  
        if(data!=null)
            instance?.Open(data,func); 
        else
            instance?.Open((T)new ViewConfigData(),func);
        
    }

    public override void ClearCallFunc()
    {
        viewFunc = null;
    }
}
