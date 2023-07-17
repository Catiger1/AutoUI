using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Effect
{
    /// <summary>
    /// 窗口动效生成工厂
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
            /// 反射创建窗口动效显示实例
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static I2DEffect Create2DEffect(ViewShowType type)
            {   
                string className = type.ToString()+"Show2DEffect";
                return CreateObject<I2DEffect>(className);
            }
            /// <summary>
            /// 反射创建窗口动效隐藏实例
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
