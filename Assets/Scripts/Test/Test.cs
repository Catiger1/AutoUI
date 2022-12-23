using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Effect;
using Assets.Scripts.Common;
public class Test : MonoBehaviour
{
    void OnGUI()
    {
        // if(GUILayout.Button("展示提示弹框"))
        // {
        //     //PopUpWindow.OpenWindow();
        // }
        // if(GUILayout.Button("展示普通窗口"))
        // {
        //     NormalWindow.OpenWindow();
        // }
        
    }

    private void Start() {
       Debug.Log(TestString("AutoClose(ViewSerializationCfg data,Func<ViewSerializationCfg,bool> autoClose)"));
    }
    //输入AutoClose(ViewSerializationCfg data,Func<ViewSerializationCfg,bool> autoClose)
    //输出base.AutoClose(data,autoClose)
    string TestString(string input)
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

}
