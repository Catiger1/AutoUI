using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Effect;
using Assets.Scripts.Common;
public class Test : MonoBehaviour
{
    void OnGUI()
    {
        if(GUILayout.Button("展示提示弹框"))
        {
            PopUpWindow.OpenWindow();
        }
        if(GUILayout.Button("展示普通窗口"))
        {
            NormalWindow.OpenWindow();
        }
    }

}
