using OpenRA.Traits;
using UnityEngine;
using Util;

namespace OpenRA
{
    public class Mobile : Actor,IPositionable,IFacing
    {
        public float BornFace => 45;

        public float TurnSpeed => 15;
        
        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetFace(float angle)
        {
            transform.SetRotateY(angle);
        }

        public float GetFace()
        {
            return transform.localRotation.eulerAngles.y;
        }

    }
}