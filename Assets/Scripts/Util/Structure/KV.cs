using System;

namespace Util
{
    [Serializable]
    public class KV <T1,T2>
    {
        public T1 key;

        public T2 value;
    }
}
