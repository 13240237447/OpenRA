using System;
using UnityEngine;

namespace Util
{
    public class SingleTonRoot 
    {
        public static GameObject Instance { private set; get; }

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            Instance = new GameObject("SingleTonRoot");
            GameObject.DontDestroyOnLoad(Instance);
        }
    }
}