using System;
using UnityEngine;

namespace Util
{
    internal class ThinkerManager : HungryMonoSingleTon<ThinkerManager>
    {
        public TickTimer unscaleTimer;

        public TickTimer scaleTimer;

        private void Awake()
        {
            unscaleTimer = new TickTimer();
            scaleTimer = new TickTimer();
        }

        private void Update()
        {
            scaleTimer.TickRefresh();
            if (Time.timeScale > 0)
            {
                unscaleTimer.TickRefresh();
            }
        }
    }
}