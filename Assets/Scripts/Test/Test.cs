using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Effect;
using Assets.Scripts.Common;
public class Test : MonoBehaviour
{
    void OnGUI()
    {
        if(GUILayout.Button("展示"))
        {
            PopUpWindow.SetViewSerializationCfg(new ViewSerializationCfg(){
                showType = ViewShowType.Elasticity,
                hideType = ViewHideType.Elasticity
            });
            PopUpWindow.OpenWindow(new ViewConfigData()
            {
                autoCloseTime = 5
            });
            //NormalWindow.OpenWindow();
        }
    }

}
