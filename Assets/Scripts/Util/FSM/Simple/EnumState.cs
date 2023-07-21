using System;

namespace Util
{
    /// <summary>
    /// 枚举状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EnumState<T> where T : Enum
    {
        public T State { set; get; }

        public virtual void OnInit()
        {
            
        }
        
        public abstract void OnEnter(T lastState);

        public abstract void OnLeave(T currState);
    }
}