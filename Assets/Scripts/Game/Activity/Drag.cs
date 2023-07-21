using System.Collections;
using System.Collections.Generic;
using OpenRA.Traits;
using UnityEngine;

namespace  OpenRA
{
    public class Drag : Activity
    {
        private Vector2 startPos;

        private Vector2 endPos;

        private float length;

        private IPositionable positionable;

        private float ticks;

        private IFacing facing;
        
        public Drag(Actor actor,Vector2 start, Vector2 end, float length)
        {
            this.positionable = actor.GetComponent<IPositionable>();
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
            bool isComplete = false;
            ticks += Time.deltaTime;
            if (ticks >= length)
            {
                ticks = length;
                isComplete = true;
                positionable.SetPosition(endPos);
            }
            else
            {
                positionable.SetPosition(Vector2.Lerp(startPos,endPos,ticks / length));
            }
            return isComplete;
        }
    }

}
