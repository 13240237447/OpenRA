using System;
using OpenRA.Primitives;


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

}
