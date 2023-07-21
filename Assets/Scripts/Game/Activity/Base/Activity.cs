using System;

namespace OpenRA
{
    public enum ActivityState
    {
        Queued,
        Active,
        Canceling,
        Done
    }

    public interface IActivityInterface
    {
    }

    public class Activity : IActivityInterface
    {
        public ActivityState State { get; private set; }

        Activity childActivity;
        
        protected Activity ChildActivity
        {
            get => SkipDoneActivities(childActivity);
            private set => childActivity = value;
        }
        
        Activity nextActivity;
        
        public Activity NextActivity
        {
            get => SkipDoneActivities(nextActivity);
            private set => nextActivity = value;
        }
        
        internal static Activity SkipDoneActivities(Activity first)
        {
            while (first != null && first.State == ActivityState.Done)
                first = first.nextActivity;
            return first;
        }
        
        public bool IsInterruptible { get; protected set; }
        
        public bool ChildHasPriority { get; protected set; }
        
        public bool IsCanceling => State == ActivityState.Canceling;

        bool finishing;
        bool firstRunCompleted;
        bool lastRun;
        
        public Activity()
        {
            IsInterruptible = true;
            ChildHasPriority = true;
        }

        public Activity TickOuter(Actor self)
        {
            if (State == ActivityState.Done)
                throw new InvalidOperationException($"Actor {self} attempted to tick activity {GetType()} after it had already completed.");
            
            if (State == ActivityState.Queued)
            {
                OnFirstRun(self);
                firstRunCompleted = true;
                State = ActivityState.Active;
            }
            
            if (!firstRunCompleted)
                throw new InvalidOperationException($"Actor {self} attempted to tick activity {GetType()} before running its OnFirstRun method.");
            
            if (ChildHasPriority)
            {
                lastRun = TickChild(self) && (finishing || Tick(self));
                finishing |= lastRun;
            }
            else
                lastRun = Tick(self);
            
            var ca = ChildActivity;
            if (ca != null && ca.State == ActivityState.Queued)
            {
                if (ChildHasPriority)
                    lastRun = TickChild(self) && finishing;
                else
                    TickChild(self);
            }

            if (lastRun)
            {
                State = ActivityState.Done;
                OnLastRun(self);
                return NextActivity;
            }

            return this;
        }
        
        
        protected bool TickChild(Actor self)
        {
            ChildActivity = ActivityUtils.RunActivity(self, ChildActivity);
            return ChildActivity == null;
        }
        
        
        public virtual bool Tick(Actor self)
        {
            return true;
        }
        
        public virtual void Cancel(Actor self, bool keepQueue = false)
        {
            if (!keepQueue)
                NextActivity = null;

            if (!IsInterruptible)
                return;

            ChildActivity?.Cancel(self);

            // Directly mark activities that are queued and therefore didn't run yet as done
            State = State == ActivityState.Queued ? ActivityState.Done : ActivityState.Canceling;
        }

        public void Queue(Activity activity)
        {
            var it = this;
            while (it.nextActivity != null)
                it = it.nextActivity;
            it.nextActivity = activity;
        }

        public void QueueChild(Activity activity)
        {
            if (childActivity != null)
                childActivity.Queue(activity);
            else
                childActivity = activity;
        }
      

        
        protected virtual void OnFirstRun(Actor self) { }

        protected virtual void OnLastRun(Actor self) { }

        protected virtual void OnActorDispose(Actor self) { }

        internal void OnActorDisposeOuter(Actor self)
        {
            ChildActivity?.OnActorDisposeOuter(self);
            OnActorDispose(self);
        }
        
    }
}