using UnityEngine;

namespace Util
{
    /// <summary>
    /// 饿汉式单例 自动初始化
    /// </summary>
    public class HungryMonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    instance.transform.SetParent(SingleTonRoot.Instance.transform);
                }

                return instance;
            }
        }
    }
}