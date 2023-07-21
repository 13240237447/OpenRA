using System;
using UnityEngine;

namespace OpenRA
{
    public class Actor : MonoBehaviour
    {
        public readonly World World;

        Activity currentActivity;
        
        public Activity CurrentActivity
        {
            get => Activity.SkipDoneActivities(currentActivity);
            private set => currentActivity = value;
        }


        public void Tick()
        {
            CurrentActivity = ActivityUtils.RunActivity(this, CurrentActivity);
        
        }

        private void OnDestroy()
        {
            CurrentActivity?.OnActorDisposeOuter(this);
        }
    }
}
