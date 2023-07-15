
using UnityEngine;
using Assets.Scripts.Common;

public class Test : MonoBehaviour
{
    void Start()
    {
        WindowsManager.Instance.InitAllWindows();
        //InitWindows<TestWindow>();
    }


    void OnGUI()
    {
        if (GUILayout.Button("展示普通窗口"))
        {
            WindowsManager.Instance.OpenWindow(WindowsType.TestWindow);
        }
    }


}
