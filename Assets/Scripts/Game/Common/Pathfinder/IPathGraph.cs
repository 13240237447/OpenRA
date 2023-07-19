using System;
using System.Collections.Generic;


namespace OpenRA.Common.Pathfinder
{

    public interface IPathGraph : IDisposable
    {
    }
    
    public static class PathGraph
    {
        public const int PathCostForInvalidPath = int.MaxValue;
        public const short MovementCostForUnreachableCell = short.MaxValue;
    }


    public readonly struct GraphEdge
    {
        public readonly CPos Source;

        public readonly CPos Destination;

        public readonly int Cost;

        public GraphEdge(CPos source, CPos destination, int cost)
        {
            if (source == destination)
                throw new ArgumentException($"{nameof(source)} and {nameof(destination)} must refer to different cells");
            if (cost < 0)
                throw new ArgumentOutOfRangeException(nameof(cost), $"{nameof(cost)} cannot be negative");
            if (cost == PathGraph.PathCostForInvalidPath)
                throw new ArgumentOutOfRangeException(nameof(cost), $"{nameof(cost)} cannot be used for an unreachable path");

            Source = source;
            Destination = destination;
            Cost = cost;
        }

        public GraphConnection ToConnection()
        {
            return new GraphConnection(Destination, Cost);
        }
        
        public override string ToString() => $"{Source} -> {Destination} = {Cost}";

    }

    public readonly struct GraphConnection
    {
        public sealed class CostComparer : IComparer<GraphConnection>
        {
            public static readonly CostComparer Instance = new CostComparer();
            CostComparer() { }
            public int Compare(GraphConnection x, GraphConnection y)
            {
                return x.Cost.CompareTo(y.Cost);
            }
        }

        public static readonly CostComparer ConnectionCostCompare = CostComparer.Instance;

        public readonly CPos Destination;

        public readonly int Cost;

        public GraphConnection(CPos destination, int cost)
        {
            if (cost < 0)
                throw new ArgumentOutOfRangeException(nameof(cost), $"{nameof(cost)} cannot be negative");
            if (cost == PathGraph.PathCostForInvalidPath)
                throw new ArgumentOutOfRangeException(nameof(cost), $"{nameof(cost)} cannot be used for an unreachable path");

            Destination = destination;
            Cost = cost;
        }

        public GraphEdge ToEdge(CPos source)
        {
            return new GraphEdge(source, Destination, Cost);
        }
        
        public override string ToString() => $"-> {Destination} = {Cost}";

    }
    
}
