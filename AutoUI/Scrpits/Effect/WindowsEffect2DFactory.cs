using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Effect
{
    /// <summary>
    /// ���ڶ�Ч���ɹ���
    /// </summary>
    public static class WindowsEffect2DFactory
    {
            private static Dictionary<string, object> cache;
            private const string EffectScrpitsNamespace = "Assets.Scripts.UI.Effect.";
            static WindowsEffect2DFactory()
            {
                cache = new Dictionary<string, object>();
            }
            /// <summary>
            /// ���䴴�����ڶ�Ч��ʾʵ��
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static I2DEffect Create2DEffect(ViewShowType type)
            {   
                string className = type.ToString()+"Show2DEffect";
                return CreateObject<I2DEffect>(className);
            }
            /// <summary>
            /// ���䴴�����ڶ�Ч����ʵ��
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static I2DEffect Create2DEffect(ViewHideType type)
            {   
                string className = type.ToString()+"Hide2DEffect";
                return CreateObject<I2DEffect>(className);
            }

            private static T CreateObject<T>(string className)where T:class
            {
                if (!cache.ContainsKey(className))
                {
                    Type type = Type.GetType(EffectScrpitsNamespace+className);
                    object instance = Activator.CreateInstance(type);
                    cache.Add(className, instance);
                }
                return cache[className] as T;
            }
    }
}
