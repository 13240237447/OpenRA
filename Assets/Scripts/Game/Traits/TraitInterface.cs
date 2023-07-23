using System;
using OpenRA.Primitives;
using UnityEngine;


namespace OpenRA.Traits
{
    public sealed class RequireExplicitImplementationAttribute : Attribute { }

    public interface ITraitInfoInterface {}

    public abstract class TraitInfo : ITraitInfoInterface
    {
        public readonly string InstanceName = null;

        public abstract object Create(ActorInitializer init);
    }
    
    public sealed class TargetableType {
        TargetableType()
        {
            
        }
    }

    public interface ITargetableInfo : ITraitInfoInterface
    {
        BitSet<TargetableType> GetTargetTypes();
    }

    public interface ITargetable
    {
        BitSet<TargetableType> TargetTypes { get; }

        bool TargetableBy(Actor self, Actor byActor);
        
        bool RequiresForceFire { get; }
    }


    public interface ICustomMovementLayer
    {
        byte Index { get; }
        bool InteractsWithDefaultLayer { get; }
        bool ReturnToGroundLayerOnIdle { get; }

        bool EnabledForLocomotor();
    }


    public interface IPositionable
    {
        void SetPosition(Vector3 pos);

        Vector3 GetPosition();
    }

    public interface IFacing
    {
        /// <summary>
        /// 旋转速度
        /// </summary>
        public float TurnSpeed { get; }
        /// <summary>
        /// 初始角度
        /// </summary>
        public float BornFace { get;}
        
        public void SetFace(float angle);

        public float GetFace();
        
    }
}
