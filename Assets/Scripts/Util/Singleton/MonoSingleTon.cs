using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    /// <summary>
    /// 懒汉式单例 需要手动初始化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance => instance;

        public static void InstanceInit()
        {
            if (instance == null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
                instance.transform.SetParent(SingleTonRoot.Instance.transform);
            }
        }

        public static void Release()
        {
            Destroy(Instance.gameObject);
        }
    }
}