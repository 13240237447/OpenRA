using System.Reflection;
using OpenRA.Primitives;
using OpenRA.Traits;
using UnityEngine;


namespace OpenRA
{
    public interface IActorInitializer
    {
        World World { get; }

        T GetOrDefault<T>(TraitInfo info) where T : ActorInit;

        T Get<T>(TraitInfo info) where T : ActorInit;

        U GetValue<T, U>(TraitInfo info) where T : ValueActorInit<U>;

        U GetValue<T, U>(TraitInfo info, U fallback) where T : ValueActorInit<U>;

        bool Contains<T>(TraitInfo info) where T : ActorInit;

        T GetOrDefault<T>() where T : ActorInit, ISingleInstanceInit;

        T GetT<T>() where T : ActorInit, ISingleInstanceInit;

        U GetValue<T, U>() where T : ValueActorInit<U>, ISingleInstanceInit;

        T GetValue<T,U>(U fallback) where T : ValueActorInit<U>, ISingleInstanceInit;

        bool Contains<T>() where T : ActorInit, ISingleInstanceInit;
    }
    
    public class ActorInitializer : IActorInitializer
    {
        public readonly Actor Self;
        
        public World World => Self.World;

        internal TypeDictionary Dict;
        
        public ActorInitializer(Actor actor, TypeDictionary dict)
        {
            Self = actor;
            Dict = dict;
        }
        
        public T GetOrDefault<T>(TraitInfo info) where T : ActorInit
        {
            throw new System.NotImplementedException();
        }
        public T Get<T>(TraitInfo info) where T : ActorInit
        {
            throw new System.NotImplementedException();
        }
        public U GetValue<T, U>(TraitInfo info) where T : ValueActorInit<U>
        {
            throw new System.NotImplementedException();
        }
        public U GetValue<T, U>(TraitInfo info, U fallback) where T : ValueActorInit<U>
        {
            throw new System.NotImplementedException();
        }
        public bool Contains<T>(TraitInfo info) where T : ActorInit
        {
            throw new System.NotImplementedException();
        }
        public T GetOrDefault<T>() where T : ActorInit, ISingleInstanceInit
        {
            throw new System.NotImplementedException();
        }
        public T GetT<T>() where T : ActorInit, ISingleInstanceInit
        {
            throw new System.NotImplementedException();
        }
        public U GetValue<T, U>() where T : ValueActorInit<U>, ISingleInstanceInit
        {
            throw new System.NotImplementedException();
        }
        public T GetValue<T, U>(U fallback) where T : ValueActorInit<U>, ISingleInstanceInit
        {
            throw new System.NotImplementedException();
        }
        public bool Contains<T>() where T : ActorInit, ISingleInstanceInit
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class ActorInit
    {
        public readonly string InstanceName;

        protected ActorInit(string instanceName)
        {
            InstanceName = instanceName;
        }
        
        protected ActorInit() {}
        
    }
    
    public interface ISingleInstanceInit { }


    public abstract class ValueActorInit<T> : ActorInit
    {
        private readonly T value;

        protected ValueActorInit(TraitInfo info, T value) : base(info.InstanceName)
        {
            this.value = value;
        }

        protected ValueActorInit(string instanceName, T value): base(instanceName)
        {
            this.value = value;
        }

        protected ValueActorInit(T value)
        {
            this.value = value;
        }

        public virtual T Value => value;

        public virtual void Initialize(ScriptableObject so)
        {
            
        }

        public virtual void Initialize(T value)
        {
            var field = typeof(ValueActorInit<T>).GetField(nameof(value), BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
                field.SetValue(this, value);
        }
        

    }
}
