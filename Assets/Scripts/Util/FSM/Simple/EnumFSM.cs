using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    /*
     *举例
     *   EnumFSM<LevelState> levelStateFsm = new EnumFSM<LevelState>();
     *   levelStateFsm.AddState(LevelState.LevelWin, () => new LeveWinState());
     *   levelStateFsm.AddState(LevelState.LevelFail, () => new LeveFailState());
     *   levelStateFsm.ChangeToState(LevelState.LevelWin);
     *   levelStateFsm.ChangeToState(LevelState.LevelFail);
     */
    /// <summary>
    /// 枚举状态机
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumFSM<T> where  T : Enum
    {
        
        private Dictionary<T, Func<EnumState<T>>> stateFactories = new Dictionary<T, Func<EnumState<T>>>();
        
        /// <summary>
        /// 当前状态
        /// </summary>
        public EnumState<T> CurrentState { private set; get; }

        /// <summary>
        /// 状态缓存
        /// </summary>
        private List<EnumState<T>> stateCache = new List<EnumState<T>>();

        /// <summary>
        /// 添加状态 懒汉式 需要的时候再初始化状态
        /// </summary>
        /// <param name="stateEnum">枚举</param>
        /// <param name="stateFactory">实例化状态的方法</param>
        /// <typeparam name="TState"></typeparam>
        public void AddState<TState>(TState stateEnum, Func<EnumState<T>> stateFactory) where TState : Enum
        {
            T enumValue = (T)Enum.Parse(typeof(T), stateEnum.ToString());
            if (stateFactories.ContainsKey(enumValue))
            {
                Debug.LogError("State have registered in state machine.");
                return;
            }
            stateFactories[enumValue] = stateFactory;
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="stateEnum"></param>
        /// <param name="state"></param>
        /// <typeparam name="TState"></typeparam>
        public void AddState<TState>(TState stateEnum, EnumState<T> state) where TState : Enum
        {
            T enumValue = (T)Enum.Parse(typeof(T), stateEnum.ToString());
            state.State = enumValue;
            stateCache.Add(state);
        }

        /// <summary>
        /// 转换到某个状态
        /// </summary>
        /// <param name="stateEnum"></param>
        public void ChangeToState(T stateEnum)
        {
            EnumState<T> nextState = GetState(stateEnum);
            if (nextState == null)
            {
                Debug.LogError($"Failed to instantiate {stateEnum} state.");
                return;
            }

            if (CurrentState != null)
            {
                CurrentState.OnLeave(nextState.State);
            }

            T previousStateEnum = CurrentState != null ? CurrentState.State : default;
            CurrentState = nextState;
            CurrentState.State = stateEnum;
            CurrentState.OnEnter(previousStateEnum);
            OnStateChange();
        }
        
        /// <summary>
        /// 状态发生改变
        /// </summary>
        public virtual void OnStateChange(){}

        /// <summary>
        /// 获取某个状态
        /// </summary>
        /// <param name="stateEnum"></param>
        /// <returns></returns>
        public EnumState<T> GetState(T stateEnum)
        {
            EnumState<T> rst = stateCache.Find((item) => item.State.Equals(stateEnum));
            if (rst == null)
            {
                rst = stateFactories[stateEnum]?.Invoke();
                if (rst != null)
                {
                    rst.OnInit();
                    stateCache.Add(rst);
                }
            }
            return rst;
        }

    }
    
}