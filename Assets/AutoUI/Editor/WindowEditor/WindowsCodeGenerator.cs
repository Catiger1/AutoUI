using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

//2跟3对应fileName里的数据
public enum WindowFileType
{
    GenerateCodeFile = 2,
    UserFile = 3,
}
public class WindowsCodeGenerator
{
    static string scriptsFolder = Application.dataPath;
    static string[] fileName = new string[]{
        "/AutoUI/Editor/WindowEditor/WindowsConfigEditorCode.cs",
        "/AutoUI/UI/Window/WindowsManager.cs",
        "/AutoUI/UI/Window/",
        "/AutoUI/User/"
    };
    public static void AddCodeWriteFunc(string Path,Action<StreamWriter,string[]> writefunc,string[] writeParams,bool needDelete = true)
    {
        if (File.Exists(Path))
        {
            //如果需要删除，则删除文件重新生成，如果不需要删除且文件已经存在，则结束生成
            if(needDelete)
                File.Delete(Path);
            else 
                return;
        }
        FileStream stream = File.Open(Path,FileMode.OpenOrCreate);
        StreamWriter writer = new StreamWriter(stream);
        writefunc(writer,writeParams);
        writer.Close();
        stream.Close();
    }
    
    public static bool Start()
    {
        string[] data = WindowsConfigEditor.Data.GetWindowEnum();
        AddCodeWriteFunc(scriptsFolder+fileName[0],WriteWindowsConfigEditor,data);
        AddCodeWriteFunc(scriptsFolder+fileName[1],WriteWindowsManager,data);
        
        //生成窗口文件
        for(int i=0;i<data.Length;i++)
        {
            AddCodeWriteFunc(scriptsFolder+fileName[2]+data[i]+".cs",WriteWindowFile,new string[1]{data[i]});
            AddCodeWriteFunc(scriptsFolder+fileName[3]+data[i]+"User.cs",WriteUserWindowFile,new string[1]{data[i]},false);
        }
        //生成代码后刷新编辑器,这里可以考虑优化,频繁刷新编辑器会造成卡顿
        AssetDatabase.Refresh();
        return true;
    }

    public static void RemoveWindowFile(string windowName,WindowFileType fileType)
    {
        string filePath = scriptsFolder+fileName[(int)fileType]+windowName+".cs";
        if (File.Exists(filePath))
            File.Delete(filePath);

        if(File.Exists(filePath+".meta"))
            File.Delete(filePath+".meta");
    }

    static void RepeatWrite(StreamWriter stream,string format,string[] variableName)
    {
        for(int i=0;i<variableName.Length;i++)
            stream.WriteLine(string.Format("\t"+format,variableName[i]));
    }
    static void WritePropertyLine(StreamWriter stream,string format)
    {
        stream.WriteLine("\t"+format);
    }
    
    //获取函数名中的参数
    //输入WriteFunc(StreamWriter stream,string statementName)
    //输出base.WriteFunc(stream,statementName);
    static string GetParamsName(string input)
    {
        string[] paramsArray = input.Split('(');
        paramsArray[1] = paramsArray[1].Remove(paramsArray[1].Length-1,1);
        int startIndex = paramsArray[1].IndexOf('<');
        if(startIndex!=-1)
        {
            int endIndex = paramsArray[1].IndexOf('>');
            paramsArray[1] = paramsArray[1].Remove(startIndex,endIndex-startIndex);
        }
        string[] newParamsArray = paramsArray[1].Split(',');
        string result=paramsArray[0]+"(";

        if(newParamsArray[0].Equals(""))
            return input;

        for(int i=0;i<newParamsArray.Length;i++)
        {
            string[] resultArray = newParamsArray[i].Split(' ');
            result += resultArray[1];
            if(i!=newParamsArray.Length-1)
                result +=",";
        }
        result+=")";
        return result;
    }
    //重复写某个东西，只是变量名不同，再加上函数格式
    static void WriteFunc(StreamWriter stream,string statementName,string funcName,string format,string[] variableName,string[] startLines = null,string[] addLines=null,bool isOverride=false,string returnName = " void ")
    {
        string overrideString = isOverride?" override":"";
        stream.WriteLine("\t"+statementName+overrideString+returnName+funcName);
        stream.WriteLine("\t{");
        if(isOverride)
            stream.WriteLine("\t\tbase."+GetParamsName(funcName)+";");

        if(startLines!=null)
        {
            for(int i=0;i<startLines.Length;i++)
            {
                stream.WriteLine("\t\t"+startLines[i]);
            }
        }
        if(format!=null && !format.Equals(""))
            RepeatWrite(stream,"\t"+format,variableName);
        //直接添加
        if(addLines!=null)
        {
            for(int i=0;i<addLines.Length;i++)
            {
                stream.WriteLine("\t\t"+addLines[i]);
            }
        }
        stream.WriteLine("\t}");
    }
    static void WriteDateTime(StreamWriter stream)
    {
        //先记录生成时间
        stream.WriteLine("//Generated Time: "+System.DateTime.Now.ToString());
    }
    static void WriteWindowsConfigEditor(StreamWriter stream,string[] variableName)
    {
        WriteDateTime(stream);
        stream.WriteLine("public partial class WindowsConfigEditor");
        stream.WriteLine("{");
        RepeatWrite(stream,"public ViewSerializationCfg {0}Cfg = new ViewSerializationCfg();",variableName);
        WriteFunc(stream,"private","LoadAllConfigByGameObject()","{0}Cfg = WindowsManager.Instance.{0}Cfg;",variableName,null,new string[1]{"serializedObject.Update();"});
        WriteFunc(stream,"private","LoadAllConfigByEditor()","{0}Cfg = dataManager.GetCfgData(nameof({0}));",variableName,new string[1]{"dataManager.Load();"},new string[1]{"serializedObject.Update();"});
        WriteFunc(stream,"private","SetAllConfig()","SetConfig(nameof({0}Cfg),{0}Cfg);",variableName);
        WriteFunc(stream,"private","SaveConfig()","WindowsManager.Instance.{0}Cfg = {0}Cfg;",variableName);
        WriteFunc(stream,"private","UpdateDataManager()","dataManager.Update(nameof({0}),{0}Cfg);",variableName);
        stream.WriteLine("}");
    }

    static void WriteWindowsManager(StreamWriter stream,string[] variableName)
    {
        WriteDateTime(stream);
        stream.WriteLine("public partial class WindowsManager: MonoSingleton<WindowsManager>");
        stream.WriteLine("{");
        RepeatWrite(stream,"public ViewSerializationCfg {0}Cfg;",variableName);
        WriteFunc(stream,"public","Init()","{0}.SetViewSerializationCfg({0}Cfg);",variableName,null,null,true);
        stream.WriteLine("}");
    }
    static void WriteWindowFile(StreamWriter stream,string[] variableName)
    {
        if(variableName==null||variableName.Length<=0)
            return;
        string windowName = variableName[0];
        ViewSerializationCfg cfg = WindowsConfigEditor.Data.GetCfgData(variableName[0]);
        if(cfg==null)
        {
            Debug.LogError("Can't Find ViewSerializationCfg Data In DataManager");
            return;
        }
        WriteDateTime(stream);
        //写入命名空间
        stream.WriteLine("using System;");
        stream.WriteLine("using UnityEngine;");
        stream.WriteLine("using UnityEngine.UI;");
        stream.WriteLine("using Assets.Scripts.Common;");
        //写入命名空间

        stream.WriteLine("public partial class {0}: mWindow<{0},ViewSerializationCfg,EventData>",variableName[0]);
        stream.WriteLine("{");
        //如果有关闭按钮
        if(cfg.ButtonClose)
        {
            stream.WriteLine("\tpublic Transform BtnClose;");
            stream.WriteLine("\tprivate string BtnCloseName = \"BtnClose\";");
            WriteFunc(stream,"public","SetBtnCloseName(string name)",null,variableName,null,new string[2]{"BtnCloseName = name;","return this;"},false," "+windowName+" ");
            WriteFunc(stream,"private","BtnsInit(ViewSerializationCfg cfg)",null,variableName,null,new string[3]{"BtnClose = viewPrefab.transform.FindChildByName(BtnCloseName);","if(BtnClose!=null)","\t BtnClose.GetComponent<Button>().onClick.AddListener(()=>{Close(cfg);});"});
            WriteFunc(stream,"public","OnCreate(ViewSerializationCfg cfg)",null,variableName,null,new string[1]{"BtnsInit(cfg);"},true);
        }
        //如果自动关闭开启
        if(cfg.AutoCloseEnable)
        {
            WriteFunc(stream,"public","OnShow(ViewSerializationCfg cfg)",null,variableName,null,new string[3]{" AutoClose(cfg,(viewCfg)=>{","return  viewCfg.AutoCloseEnable&&(Time.time-LastOpenTime>=viewCfg.AutoCloseTime);","});"},true);
            WriteFunc(stream,"public","AutoClose(ViewSerializationCfg data,Func<ViewSerializationCfg, bool> autoClose)",null,variableName,null,new string[8]{
                "CommandDispatcher.PushCommand(new CommandData(){",
                "\tcommand = ()=>{",
                "\t\tCloseWindow();",
                "\t},",
                "\tcondition = ()=>{",
                "\t\treturn autoClose(data);",
                "\t}",
                "});",},true);
        }

        stream.WriteLine("}");
    }

    //如果文件不存在则创建提供给用户编写的窗口文件
    static void WriteUserWindowFile(StreamWriter stream,string[] variableName)
    {
        stream.WriteLine("public partial class {0}",variableName[0]);
        stream.WriteLine("{");

        stream.WriteLine("}");
    }

    
}
