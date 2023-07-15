using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class TransformHelper
    {
        /// <summary>
        /// 面向目标方向
        /// </summary>
        /// <param name="targetDirection">目标方向</param>
        /// <param name="transform">需要转向的对象</param>
        /// <param name="rotationSpeed">转向速度</param>
        public static void LookAtTarget(Vector3 targetDirection,Transform transform,float rotationSpeed)
        {
            if(targetDirection != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
            }
        }

        public static Transform FindChildByName(this Transform trans, string goName)
        {
            //一层一层往下找，没有就返回
            //先执行这里
            Transform childTF = trans.Find(goName);
            if (childTF != null)
                return childTF;

            int childAmount = trans.childCount;
            for(int i = 0;i< childAmount;i++)
            {
                childTF = FindChildByName(trans.GetChild(i),goName);
                if (childTF != null)
                    return childTF;
            }
            return null;
        }
        public static Transform FindChildByName(this GameObject go, string goName)
        {
            //一层一层往下找，没有就返回
            //先执行这里
            Transform childTF = go.transform.Find(goName);
            if (childTF != null)
                return childTF;

            int childAmount = go.transform.childCount;
            for(int i = 0;i< childAmount;i++)
            {
                childTF = FindChildByName(go.transform.GetChild(i),goName);
                if (childTF != null)
                    return childTF;
            }
            return null;
        }

        public static T FindChildByName<T>(this Transform trans, string goName)
        {
            //一层一层往下找，没有就返回
            //先执行这里
            Transform childTF = trans.Find(goName);
            if (childTF != null)
                return childTF.GetComponent<T>();

            int childAmount = trans.childCount;
            for (int i = 0; i < childAmount; i++)
            {
                T result = FindChildByName<T>(trans.GetChild(i), goName);
                if (result != null)
                    return result;
            }
            return default;
        }
    }


}
