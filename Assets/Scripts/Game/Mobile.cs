using OpenRA.Traits;
using UnityEngine;

namespace OpenRA
{
    public class Mobile : Actor,IPositionable,IFacing
    {
        private float angle = 45;
        public void SetPosition(Vector2 pos)
        {
            transform.position = pos;
        }

        public void SetFace(float angle)
        {
            
        }

        public float GetFace()
        {
            throw new System.NotImplementedException();
        }
    }
}