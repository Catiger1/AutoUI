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
        public static IEnumerator DelayCoroutine(float DelayTime, Action callback,int times)
        {
            for (int i = 0; i < times; i++)
            {
                yield return new WaitForSeconds(DelayTime);
                callback?.Invoke();
            }
        }

        public static void DelayCallBack(this MonoBehaviour mono, float DelayTime, Action callback,int times = 1)
        {
            mono.StartCoroutine(DelayCoroutine(DelayTime, callback, times));
        }

        public static void ClearCoroutine(this MonoBehaviour mono)
        {
            mono.ClearCoroutine();
        }

        //递归找到所有带此组件的对象
        public static List<T> FindAllTargets<T>(this Transform tf) where T:MonoBehaviour
        {
            List<T> result = new List<T>();
            int childAmount = tf.childCount;
            for(int i = 0;i< childAmount;i++)
            {
                T component = tf.GetChild(i).GetComponent<T>();
                if(component!=null)
                   result.Add(component);

                result.AddRange(FindAllTargets<T>(tf.GetChild(i)));
            }
            
            return result;
        }
    }
}

