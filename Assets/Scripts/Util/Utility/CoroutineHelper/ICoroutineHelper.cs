using System;
using System.Collections;

namespace Util
{
    public interface ICoroutineHelper
    {
        UnityEngine.Coroutine StartCoroutine(UnityEngine.Coroutine routine, Action callBack);
        UnityEngine.Coroutine StartCoroutine(IEnumerator routine);
        UnityEngine.Coroutine StartCoroutine(Action handler);
        UnityEngine.Coroutine StartCoroutine(Action handler, Action callback);
        /// <summary>
        /// 延时协程；
        /// </summary>
        /// <param name="delay">延时的时间</param>
        /// <param name="callBack">延时后的回调函数</param>
        /// <returns>协程对象</returns>
        UnityEngine.Coroutine DelayCoroutine(float delay, Action callBack);
        /// <summary>
        /// 条件协程；
        /// </summary>
        /// <param name="handler">目标条件</param>
        /// <param name="callBack">条件达成后执行的回调</param>
        /// <returns>协程对象</returns>
        UnityEngine.Coroutine PredicateCoroutine(Func<bool> handler, Action callBack);
        /// <summary>
        /// 嵌套协程；
        /// </summary>
        /// <param name="predicateHandler">条件函数</param>
        /// <param name="nestHandler">条件成功后执行的嵌套协程</param>
        /// <returns>Coroutine></returns>
        UnityEngine.Coroutine PredicateNestCoroutine(Func<bool> predicateHandler, Action nestHandler);
        void StopAllCoroutines();
        void StopCoroutine(IEnumerator routine);
        void StopCoroutine(UnityEngine.Coroutine routine);
    }
}
