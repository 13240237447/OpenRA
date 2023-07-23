using System.Numerics;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace OpenRA
{
    public class MoveConditional : Conditional
    {
        private SharedBool needMove = new SharedBool();

        public override TaskStatus OnUpdate()
        {
            if (needMove.Value)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}