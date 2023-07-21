using System;

namespace OpenRA
{
    public class CallFunc : Activity
    {
        public CallFunc(Action a) { this.a = a; }
        public CallFunc(Action a, bool interruptible)
        {
            this.a = a;
            IsInterruptible = interruptible;
        }

        readonly Action a;

        public override bool Tick(Actor self)
        {
            a.Invoke();
            return true;
        }
    }
}