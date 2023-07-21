using OpenRA.Traits;
using UnityEngine;
using Util;

namespace OpenRA
{
    public class Mobile : Actor,IPositionable,IFacing
    {
        public float BornFace => 45;

        public float TurnSpeed => 15;
        
        public void SetPosition(Vector2 pos)
        {
            transform.position = new Vector3(pos.x,transform.position.y,pos.y);
        }

        public Vector2 GetPosition()
        {
            return new Vector2(transform.position.x,transform.position.z);
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