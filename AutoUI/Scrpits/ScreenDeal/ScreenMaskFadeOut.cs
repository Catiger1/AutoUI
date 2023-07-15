using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenMaskFadeOut : MonoSingleton<ScreenMaskFadeOut>
{
    // Start is called before the first frame update
    Action callFunc;
    public float duration = 1f;
    void Start()
    {
        GetComponent<Image>().DOFade(0, duration).onComplete = () => {
            gameObject.SetActive(false);
            callFunc?.Invoke();
        };
    }

    public void SetCallFunc(Action callFunc)
    {
        this.callFunc = callFunc;
    }
}
