using System;
using UnityEngine;

namespace OpenRA
{
    public enum WorldType { Regular, Shellmap, Editor }

    public sealed class World : MonoBehaviour
    {
        public Mobile TestMobile;
        public void Test()
        {
            
        }

        private void Update()
        {
            TestMobile.Tick();
        }
    }
}
