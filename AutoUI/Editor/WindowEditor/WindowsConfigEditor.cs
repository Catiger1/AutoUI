using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SerializableData
{
    public string ClassName;
    public ViewSerializationCfg WindowsCfg;
}

public partial class WindowsConfigEditor : EditorWindow
{
    static string titleName = "AutoWindowsEditor";
    static WindowsDataManager dataManager = new WindowsDataManager();
    float progressValue = 0;
    bool editorRefresh = false;
    bool isNeedDetelePrefab = true;
    bool isNeedDeteleUserCodeFile = true;
    public static WindowsDataManager Data
    {   
        get{
            return dataManager;
        }
    }
    int curSelect = 0;
    public static WindowsConfigEditor window;
    SerializedObject serializedObject;

    //ToolBar选择
    string[] ToolBarTitleArray = {"Create","Edit"};
    int toolbarSelect = 0;
    //
    
    //Create界面
    const string AddConfigListName = "AddList";
    SerializedProperty addSerializedProperty;
    public List<SerializableData> AddList = new List<SerializableData>(){new SerializableData()};
    //
    private void OnEnable() 
    { 
        dataManager.Load();
        serializedObject  = new SerializedObject(this);
        addSerializedProperty = serializedObject.FindProperty(AddConfigListName);
        LoadAllConfigByGameObject();
    }

     [MenuItem("AutoUI/AutoWindows %t")]
    public static void OpenWindowsEditor()
    {
        HasWindowManager();
        window = GetWindow<WindowsConfigEditor>();
        window.titleContent = new GUIContent(titleName);
    }

    public static void HasWindowManager()
    {
        var windowManager = FindObjectOfType<WindowsManager>();
        if(windowManager==null||!windowManager.name.Equals("WindowsManager"))
        {
            Debug.Log("Can't Find WindowsManager and Create WindowsManager");
            GameObject go = new GameObject(typeof(WindowsManager).Name);
            go.AddComponent<WindowsManager>();
        }
    }

    private void SetConfig(string name,ViewSerializationCfg cfg)
    {
        SerializedProperty serializedProperty = serializedObject.FindProperty(name);
        EditorGUILayout.PropertyField(serializedProperty, new GUIContent(name),GUILayout.Width(300));
        //保存序列化状态
        serializedObject.ApplyModifiedProperties();
    }

    private void AddConfigDic()
    {
        EditorGUILayout.PropertyField(addSerializedProperty, new GUIContent("Add List"),GUILayout.Width(450));
    }

    ///删除下拉框设置
    private void RemovePopupSet(string[] PopupData)
    {
        
        isNeedDetelePrefab = GUILayout.Toggle(isNeedDetelePrefab,"Need Detele Prefab");
        isNeedDeteleUserCodeFile = GUILayout.Toggle(isNeedDetelePrefab,"Need Detele User Code File");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Select Window Need Detele");
        
        //这里序列化成一个选择框
        if(PopupData.Length<=0)
            EditorGUILayout.Popup(0,new string[1]{"None"});
        else
            curSelect = EditorGUILayout.Popup(curSelect,PopupData);
        
        if(GUILayout.Button("Remove")&&PopupData.Length>0)
        {
            string removeWindowName = PopupData[curSelect];
            //是否需要移除预制件
            if(isNeedDetelePrefab)
            {
                //这里要移除文件
                WindowsPrefabGenerator.RemovePrefab(new SerializableData()
                {
                    ClassName = removeWindowName,
                    WindowsCfg = dataManager.GetCfgData(removeWindowName)
                });
            }
            WindowsCodeGenerator.RemoveWindowFile(removeWindowName,WindowFileType.GenerateCodeFile);
            if(isNeedDeteleUserCodeFile)
            {
                WindowsCodeGenerator.RemoveWindowFile(removeWindowName+"User",WindowFileType.UserFile);
            }
            dataManager.Remove(removeWindowName);
            dataManager.Save();
            //开始刷新并生成代码
            if(WindowsCodeGenerator.Start())
            {
                editorRefresh = true;
            }
        }
        GUILayout.EndHorizontal();
    }

    private int ToolBarSet()
    {  
       toolbarSelect = GUILayout.Toolbar(toolbarSelect,ToolBarTitleArray,GUILayout.Width(200));
       return toolbarSelect;
    }
    ///View
    private void EditView()
    {
        RemovePopupSet(dataManager.GetWindowEnum());
        SetAllConfig();
        UpdateDataManager();
        //LoadAllConfigByGameObject();
        SaveConfig();
        EditViewRefreshBtn();
    }
    private void EditViewRefreshBtn()
    {
        if(GUILayout.Button("Refresh Code"))
        {
            dataManager.Save();
            AssetDatabase.Refresh();
            if(WindowsCodeGenerator.Start())
                editorRefresh = true;
        }
    }

    private void GenerateCode()
    {
        WindowsCodeGenerator.Start();
    }

    private void CreateView()
    {
        GUILayout.BeginVertical();
        AddConfigDic();
        //序列化要增加的窗口参数
        if(GUILayout.Button("Add",GUILayout.Height(50)))
        {
            serializedObject.ApplyModifiedProperties();
            foreach (var item in AddList)
            {
               if(item.ClassName.Equals(""))
               {
                  AddList.Remove(item);
                  continue;
               }
               dataManager.Add(item.ClassName,item.WindowsCfg);
            }
            dataManager.Save();

            //开始生成代码，如果代码生成完成就弹出提示
            if(WindowsCodeGenerator.Start())
            {
                WindowsPrefabGenerator.Start(AddList);
                editorRefresh =true;
            }
        }
        GUILayout.Space(20);
        GUILayout.EndVertical();
    }
    ///
    private void OnGUI()
    {
        DrawEditorGUI();
    }

    private void DrawEditorGUI()
    {
        //刷新的时候不执行其他，专心刷新
        ToolBarSet();
        if(toolbarSelect == 1)
        {
            EditView();
        }
        else
        {
            CreateView();
        }
        
        if(editorRefresh&&Event.current.type != EventType.Repaint)
            RefreshEditor();
    }

    //编辑器操作很多东西都需要刷新，比如生成代码后代码改变需要刷新用AssetDatabase.Refresh();编辑器后等一会
    private void RefreshEditor()
    {
        EditorUtility.DisplayProgressBar("Code is being generated", "Refresh Editor", progressValue); //显示进度条
        progressValue += 0.2f;
        progressValue = Mathf.Clamp01(progressValue); //约束value值到0~1

        if (progressValue == 1)
        {
            LoadAllConfigByEditor();
            SetAllConfig();
            UpdateDataManager();
            SaveConfig();
            editorRefresh = false;
            progressValue = 0;
            EditorUtility.ClearProgressBar(); //关闭进度条
            ShowNotification(new GUIContent("代码修改完成"));
            window = GetWindow<WindowsConfigEditor>();
        }
    }
}
