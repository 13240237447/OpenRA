using OpenRA.Traits;
using UnityEngine;

namespace OpenRA
{
    public class Turn : Activity
    {
        private IFacing facing;
        
        public float desiredFacing;

        public Turn(Actor self, float faceAngle)
        {
            facing = self.GetComponent<IFacing>();
            desiredFacing = faceAngle;
        }

        public override bool Tick(Actor self)
        {
            float targetFace = facing.GetFace();
            if (Mathf.Abs(targetFace - desiredFacing) < 0.01f)
            {
                return true;
            }
            facing.SetFace(Util.TickFacing(facing.GetFace(),desiredFacing,facing.TurnSpeed * Time.deltaTime));
            return false;
        }
    }
}