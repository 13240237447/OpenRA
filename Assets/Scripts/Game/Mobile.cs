using System;
using BehaviorDesigner.Runtime;
using OpenRA.Traits;
using UnityEngine;
using Util;

namespace OpenRA
{
    public class Mobile : Actor,IPositionable,IFacing
    {
        public float BornFace => 45;

        public float TurnSpeed => 15;
        
        private BehaviorTree bt;
        
        private void Awake()
        {
            bt = GetComponent<BehaviorTree>();
            bt.SetVariable("moveTarget",new SharedVector3());
            bt.SetVariable("needMove",new SharedBool());
        }

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