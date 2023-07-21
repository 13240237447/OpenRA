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
        
        public Drag(Actor actor,Vector2 start, Vector2 end, float length)
        {
            this.positionable = actor.GetComponent<IPositionable>();
            startPos = start;
            endPos = end;
            this.length = length;
            ticks = 0;
        }

        public override bool Tick(Actor self)
        {
            bool isComplete = false;
            ticks += Time.deltaTime;
            if (ticks >= length)
            {
                ticks = length;
                isComplete = true;
            }
            positionable.SetPosition(Vector2.Lerp(startPos,endPos,length - ticks));
            return isComplete;
        }
    }

}
