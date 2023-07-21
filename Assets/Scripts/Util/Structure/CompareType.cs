using System;
using UnityEngine;

namespace Util
{
    public enum ECompareType
    {
        UnLimit,
        [InspectorName("=")]
        Equal,
        [InspectorName("<")]
        Less,
        [InspectorName(">")]
        Greater,
    }
    
    public static class CompareUtil
    {
        public static bool Compare<T>(T t1, T t2, ECompareType compareType)  where T : struct, IComparable<T>
        {
            switch (compareType)
            {
                case ECompareType.Equal:
                    return t1.Equals(t2);
                case ECompareType.Less:
                    return t1.CompareTo(t2) < 0;
                case ECompareType.Greater:
                    return t1.CompareTo(t2) > 0;
            }
            return true;
        }
    }
}