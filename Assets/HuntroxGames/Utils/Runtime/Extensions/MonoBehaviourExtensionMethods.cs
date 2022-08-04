using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public static class MonoBehaviourExtensionMethods 
    {
        
        public static Coroutine DoAfter(this MonoBehaviour monoBehaviour, float duration, Action callback,bool unscaled = false)
        {
            return monoBehaviour.StartCoroutine(DoAfterCoroutine(duration, callback, unscaled));
            IEnumerator DoAfterCoroutine(float paramDuration, Action paramCallback,bool paramUnscaled)
            {
                if (paramUnscaled)
                    yield return new WaitForSecondsRealtime(paramDuration);
                else
                    yield return new WaitForSeconds(paramDuration);
                paramCallback();
            }
        }
        public static Coroutine WaitUntil(this MonoBehaviour monoBehaviour, Func<bool> condition, Action callback)
        {
            return monoBehaviour.StartCoroutine(WaitUntilCoroutine(condition, callback));
            IEnumerator WaitUntilCoroutine(Func<bool> paramCondition, Action paramCallback)
            {
                while (!paramCondition())
                    yield return null;
                paramCallback();
            }
        }
        public static Coroutine WaitWhile(this MonoBehaviour monoBehaviour, Func<bool> condition, Action callback)
        {
            return monoBehaviour.StartCoroutine(WaitWhileCoroutine(condition, callback));
            IEnumerator WaitWhileCoroutine(Func<bool> paramCondition, Action paramCallback)
            {
                while (paramCondition())
                    yield return null;
                paramCallback();
            }
        }
        
    }
}
