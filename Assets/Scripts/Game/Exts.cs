using System;
using System.Collections;
using System.Collections.Generic;
using OpenRA;
using UnityEngine;

public static class Exts 
{
    
    public enum ISqrtRoundMode { Floor, Nearest, Ceiling }

    public static int ISqrt(int number, ISqrtRoundMode round = ISqrtRoundMode.Floor)
    {
        if (number < 0)
            throw new InvalidOperationException($"Attempted to calculate the square root of a negative integer: {number}");
        return (int)ISqrt(number, round);
    }
    
    public static V GetOrAdd<K, V>(this Dictionary<K, V> d, K k, Func<K, V> createFn)
    {
        if (!d.TryGetValue(k, out var ret))
            d.Add(k, ret = createFn(k));
        return ret;
    }
    
    public static string JoinWith<T>(this IEnumerable<T> ts, string j)
    {
        return string.Join(j, ts);
    }
    
    static int WindingDirectionTest(int2 v0, int2 v1, int2 p)
    {
        return Math.Sign((v1.X - v0.X) * (p.Y - v0.Y) - (p.X - v0.X) * (v1.Y - v0.Y));
    }
    
    public static bool LinesIntersect(int2 a, int2 b, int2 c, int2 d)
    {
        // If line segments AB and CD intersect:
        //  - the triangles ACD and BCD must have opposite sense (clockwise or anticlockwise)
        //  - the triangles CAB and DAB must have opposite sense
        // Segments intersect if the orientation (clockwise or anticlockwise) of the two points in each line segment are opposite with respect to the other
        // Assumes that lines are not collinear
        return WindingDirectionTest(c, d, a) != WindingDirectionTest(c, d, b) && WindingDirectionTest(a, b, c) != WindingDirectionTest(a, b, d);
    }

    
}
