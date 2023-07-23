using System.Collections;
using System.Collections.Generic;
using OpenRA.Traits;
using UnityEngine;
using UnityEngine.AI;

namespace  OpenRA
{
    public class Drag : Activity
    {
        private Vector3 startPos;

        private Vector3 endPos;

        private float length;

        private IPositionable positionable;

        private float ticks;

        private IFacing facing;

        private NavMeshAgent navMeshAgent;
        
        public Drag(Actor actor,Vector3 start, Vector3 end, float length)
        {
            this.positionable = actor.GetComponent<IPositionable>();
            navMeshAgent = actor.GetComponent<NavMeshAgent>();
            facing = actor.GetComponent<IFacing>();
            startPos = start;
            endPos = end;
            this.length = length;
            ticks = 0;
        }

        protected override void OnFirstRun(Actor self)
        {
            base.OnFirstRun(self);
            if (facing != null)
            {
                QueueChild(new Turn(self,facing.BornFace));
            }
        }

        public override bool Tick(Actor self)
        {
            if (!navMeshAgent.hasPath)
            {
                navMeshAgent.destination = endPos;
            }
            if (navMeshAgent.isStopped)
            {
                return true;
            }
            return false;
        }
    }

}
