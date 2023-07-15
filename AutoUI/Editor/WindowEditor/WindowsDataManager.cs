using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Plastic.Newtonsoft.Json;

public class WindowsDataManager 
{
    static Dictionary<string,ViewSerializationCfg> windowsDataDic = new Dictionary<string, ViewSerializationCfg>();
    static string savePath = "WindowsEditorData"; 

    //返回加载成功或者失败
    public bool Load()
    {
        string data = EditorPrefs.GetString(savePath);
        if(!data.Equals(""))
        {  
            windowsDataDic = JsonConvert.DeserializeObject<Dictionary<string,ViewSerializationCfg>>(data);
            return true;
        }
        return false;
    }
    //更新数据到DataManager里，后保存的数据才能是正确数据
    public void Update(string name,ViewSerializationCfg cfg)
    {   
        if(IsExist(name))
            windowsDataDic[name]=cfg;
    }
    public string[] GetWindowEnum()
    {
        List<string> list = new List<string>();
        foreach(var item in windowsDataDic)
        {
            list.Add(item.Key);
        }
        return list.ToArray();
    }
    public void Save()
    {
        EditorPrefs.SetString(savePath,JsonConvert.SerializeObject(windowsDataDic));
    }
    public ViewSerializationCfg GetCfgData(string name)
    {
        return windowsDataDic.ContainsKey(name)?windowsDataDic[name]:null;
    }
    public bool IsExist(string name)
    {
        return windowsDataDic.ContainsKey(name);
    }
    public bool Add(string name,ViewSerializationCfg cfg)
    {
        if(!IsExist(name))
        {
            windowsDataDic.Add(name,cfg);
            return true;
        }

        return false;
    }

    public bool Remove(string name)
    {
        if(IsExist(name))
        {
            windowsDataDic.Remove(name);
            return true;
        }

        return false;
    }
}
