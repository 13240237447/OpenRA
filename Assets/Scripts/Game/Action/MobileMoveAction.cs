using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace OpenRA
{
    public class MobileMoveAction : BehaviorDesigner.Runtime.Tasks.Action
    {
        private SharedVector3 moveTarget = new SharedVector3();
        
        private NavMeshAgent agent;
        
        public override void OnAwake()
        {
            base.OnAwake();
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        public override void OnStart()
        {
            base.OnStart();
            if (agent)
            {
                agent.destination = moveTarget.Value;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (agent)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Running;
                }
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}