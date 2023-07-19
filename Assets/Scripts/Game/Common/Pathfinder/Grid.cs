using System;


namespace OpenRA.Common.Pathfinder
{
    public readonly struct Grid
    {
        /// <summary>
        /// Inclusive
        /// </summary>
        public readonly CPos TopLeft;

        /// <summary>
        /// Exclusive
        /// </summary>
        public readonly CPos BottomRight;

        /// <summary>
        /// 为true的话 格子只有一个层级，false的话会跨度所有层级
        /// </summary>  
        public readonly bool SingleLayer;

        public Grid(CPos topLeft, CPos bottomRight, bool singleLayer)
        {
            if (topLeft.Layer != bottomRight.Layer)
                throw new ArgumentException($"{nameof(topLeft)} and {nameof(bottomRight)} must have the same {nameof(CPos.Layer)}");

            TopLeft = topLeft;
            BottomRight = bottomRight;
            SingleLayer = singleLayer;
        }

        public int Width => BottomRight.X - TopLeft.X;

        public int Height => BottomRight.Y - TopLeft.Y;

        public bool Contains(CPos cell)
        {
            return cell.X >= TopLeft.X && cell.X < BottomRight.X && 
                   cell.Y < BottomRight.Y && cell.Y >= TopLeft.Y && 
                   (!SingleLayer || cell.Layer == TopLeft.Layer);
        }
        
        /// <summary>
        /// Checks if the line segment from <paramref name="start"/> to <paramref name="end"/>
        /// passes through the grid boundary. The cell layers are ignored.
        /// A line contained wholly within the grid that doesn't cross the boundary is not counted as intersecting.
        /// </summary>
        public bool IntersectsLine(CPos start, CPos end)
        {
            var s = new int2(start.X, start.Y);
            var e = new int2(end.X, end.Y);
            var tl = new int2(TopLeft.X, TopLeft.Y);
            var tr = new int2(BottomRight.X, TopLeft.Y);
            var bl = new int2(TopLeft.X, BottomRight.Y);
            var br = new int2(BottomRight.X, BottomRight.Y);
            return
                Exts.LinesIntersect(s, e, tl, tr) ||
                Exts.LinesIntersect(s, e, tl, bl) ||
                Exts.LinesIntersect(s, e, bl, br) ||
                Exts.LinesIntersect(s, e, tr, br);
        }
        
        public override string ToString()
        {
            return $"{TopLeft}->{BottomRight}";
        }
    }
}
