using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class UnityCommonTools
    {
        public static IEnumerator DelayCoroutine(int DelayTime, Action callback,int times)
        {
            for (int i = 0; i < times; i++)
            {
                yield return new WaitForSeconds(DelayTime);
                callback?.Invoke();
            }
        }

        public static void DelayCallBack(this MonoBehaviour mono, int DelayTime, Action callback,int times = 1)
        {
            mono.StartCoroutine(DelayCoroutine(DelayTime, callback, times));
        }

        public static void ClearCoroutine(this MonoBehaviour mono)
        {
            mono.ClearCoroutine();
        }

    }
}

