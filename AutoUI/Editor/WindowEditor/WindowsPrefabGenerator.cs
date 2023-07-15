using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class WindowsPrefabGenerator
{
    static string scriptsFolder = Application.dataPath;
    static string templatePrefabPath = "/WindowEditor/TemplateWindow.prefab";
    static string generatePrefabPath = "/AutoUI/Resources/";
    public static void Start(List<SerializableData> windowData) {
        foreach(var item in windowData)
        {
            GeneratedPrefabByTemplate(item);
        }
        AssetDatabase.Refresh();
    }

    static void GeneratedPrefabByTemplate(SerializableData data)
    {
        CopyTemplate(data);
    }

    static void CopyTemplate(SerializableData data)
    {
        string templatePath = scriptsFolder +"/AutoUI/Editor"+ templatePrefabPath;
        string newPrefabPath = scriptsFolder+generatePrefabPath+data.WindowsCfg.ViewPrefabPath+data.ClassName+".prefab";

        if(!File.Exists(templatePath))
        {
            Debug.LogError("Can't find TemplateWindow.prefab");
            return;
        }
        if(File.Exists(newPrefabPath))
        {
            Debug.Log("Prefab Has Exist");
            return;
        }
        //复制一份
        File.Copy(templatePath,newPrefabPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
       
       SetWindowPrefabByConf(data.WindowsCfg.ViewPrefabPath+data.ClassName,data);
    }

    static void SetWindowPrefabByConf(string path,SerializableData data)
    {
        //获取路径下预制件
        GameObject prefab = Resources.Load<GameObject>(path);
        if(prefab==null)
        {
            Debug.LogError("Can't Find Prefab in"+path);
            return;
        }
        var prefabPath = AssetDatabase.GetAssetPath(prefab);
        var instance = PrefabUtility.LoadPrefabContents(prefabPath);
        //如果没有按钮
        if(!data.WindowsCfg.ButtonClose)
        {
            Transform effectView = instance.transform.Find("EffectView");
            GameObject btnClose = effectView.Find("BtnClose").gameObject;
            GameObject.DestroyImmediate(btnClose,true);
        }
        PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
        PrefabUtility.UnloadPrefabContents(instance);
        //保存修改
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void RemovePrefab(SerializableData data)
    {
        string prefabPath = scriptsFolder+generatePrefabPath+data.WindowsCfg.ViewPrefabPath+data.ClassName+".prefab";
        string prefabMetaPath = scriptsFolder+generatePrefabPath+data.WindowsCfg.ViewPrefabPath+data.ClassName+".prefab.meta";
        if(File.Exists(prefabPath))
            File.Delete(prefabPath);
        if(File.Exists(prefabMetaPath))
            File.Delete(prefabMetaPath);
    }
}
