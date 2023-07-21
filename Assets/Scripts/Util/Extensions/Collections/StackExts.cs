using System.Collections.Generic;

namespace Util
{
    public static class StackExts
    {
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> items)
        {
            foreach (T item in items)
                stack.Push(item);
        }
    }
}
