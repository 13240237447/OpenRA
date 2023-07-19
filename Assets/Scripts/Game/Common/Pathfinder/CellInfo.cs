using System;


namespace OpenRA.Common.Pathfinder
{
    public enum CellStatus : byte
    {
        Unvisited,
        Open,
        Closed,
    }
    
    public readonly struct CellInfo
    {
        public readonly CellStatus Status;

        /// <summary>
        /// 从开始到此节点的消耗
        /// </summary>
        public readonly int CostSoFar;

        /// <summary>
        /// 该节点到目标有多远的估计值
        /// </summary>
        public readonly int EstimatedTotalCost;

        /// <summary>
        /// 最短路径的前一个节点
        /// </summary>
        public readonly CPos PreviousNode;

        public CellInfo(CellStatus status, int costSoFar, int estimatedTotalCost, CPos previousNode)
        {
            if (status == CellStatus.Unvisited)
            {
                throw new ArgumentException(
                    $"The default {nameof(CellInfo)} is the only such {nameof(CellInfo)} allowed for representing an {nameof(CellStatus.Unvisited)} location.",
                    nameof(status));
            }

            Status = status;
            CostSoFar = costSoFar;
            EstimatedTotalCost = estimatedTotalCost;
            PreviousNode = previousNode;
        }
        
        
        public override string ToString()
        {
            if (Status == CellStatus.Unvisited)
                return Status.ToString();

            return
                $"{Status} {nameof(CostSoFar)}={CostSoFar} " +
                $"{nameof(EstimatedTotalCost)}={EstimatedTotalCost} {nameof(PreviousNode)}={PreviousNode}";
        }
        
    }
}
