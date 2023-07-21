using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public abstract class Singleton<T> where T : class, new()
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }

        public virtual void Release()
        {
            instance = null;
        }
    }
}