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
        
        public void QueueActivity(bool queued, Activity nextActivity)
        {
            if (!queued)
                CancelActivity();

            QueueActivity(nextActivity);
        }

        public void QueueActivity(Activity nextActivity)
        {
            if (CurrentActivity == null)
                CurrentActivity = nextActivity;
            else
                CurrentActivity.Queue(nextActivity);
        }

        public void CancelActivity()
        {
            CurrentActivity?.Cancel(this);
        }
    }
}
