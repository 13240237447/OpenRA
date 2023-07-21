using System;

namespace Util
{
    public static class Thinker
    {
        /*
         * 举例：
         *********************************************************************************
         1. 每隔5秒打印一次消息
            private float DebugTest()
            {
               Debug.Log("Every 5 seconds call once");
               return 5;
            }
            Thinker.SetThink(DebugTest);
        ********************************************************************************
         2. 15秒后调用某个方法
             private float DelayTest()
            {
                Debug.Log("DelayTest");
                return 0;
            }
            Thinker.SetThink(DelayTest,15);
         *********************************************************************************
         3. 每0.5s检测一次 某个事件是否成功触发 
              private float EventTest()
             {
                 if (a != b)
                 {
                     return 0.5f;
                 }
                 return 0;
             }
         */


        /// <summary>
        /// 设置一个定时器
        /// </summary>
        /// <param name="thinkFunc">定时器回调 返回下次触发间隔 如果为0则结束</param>
        /// <param name="delay">首次触发延迟</param>
        /// <param name="isIgnoreTimeScale">是否忽略timeScale</param>
        public static void SetThink(Func<float> thinkFunc,float delay = 0,bool isIgnoreTimeScale = true)
        {
            TickTimer tickTimer = isIgnoreTimeScale ? ThinkerManager.Instance.scaleTimer : ThinkerManager.Instance.unscaleTimer;
            if (delay > 0)
            {
                tickTimer.AddDelayTask(delay, () =>
                {
                    SetThink(thinkFunc,0,isIgnoreTimeScale);
                });
            }
            else
            {
                float nextSecond = thinkFunc.Invoke();
                void TickFunc()
                {
                    if (nextSecond > 0)
                    {
                        SetThink(thinkFunc,0,isIgnoreTimeScale);
                    }
                }
                tickTimer.AddDelayTask(nextSecond, TickFunc);
            }
        }

        /// <summary>
        /// 设置一个定时器
        /// </summary>
        /// <param name="thinkFunc">定时器回调 返回下次触发间隔 如果为0则结束</param>
        /// <param name="bindObj">绑定一个物体 物体为null时将不在触发thinkFunc</param>
        /// <param name="delay">首次触发延迟</param>
        /// <param name="isIgnoreTimeScale">是否忽略timeScale</param>
        public static void SetThink(Func<float> thinkFunc, Object bindObj,float delay = 0, bool isIgnoreTimeScale = true)
        {
            float ExThinkFunc()
            {
                if (bindObj == null)
                {
                    return 0;
                }
                return thinkFunc();
            }
            SetThink(ExThinkFunc, delay, isIgnoreTimeScale);
        }

        
        public static void SetThink(Action thinkFunc, Object bindObj, float delay = 0,
            bool isIgnoreTimeScale = true)
        {
            float ExThinkFunc()
            {
                if (bindObj == null)
                {
                    return 0;
                }
                thinkFunc();
                return 0;
            }
            SetThink(ExThinkFunc, delay, isIgnoreTimeScale);
        }
    }
}