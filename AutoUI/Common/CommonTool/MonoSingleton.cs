using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T>
{
    private static T instance = null;
    private static bool isInit = false;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    instance = new GameObject("Singleton of "+typeof(T)).AddComponent<T>();
                    instance.Init();
                    isInit = true;
                }
            }
            return instance;
        }
    }

    protected void Awake()
    {
        if (!isInit)
        {
            instance = this as T;
            //找到的不用配置，没找到的需要配置
            //DontDestroyOnLoad(instance);
            instance.Init();
            isInit = true;
        }
    }

    public virtual void Init() { }

    private void OnApplicationQuit()
    {
        instance = null;
    }

}
