using System;
using JetBrains.Annotations;
using UnityEngine;


namespace HuntroxGames.Utils
{
    [PublicAPI]
    public static class Huntween
    {
        private static EasingFunctions.Ease defaultEase = EasingFunctions.Ease.Linear;


        public static Tweener<Quaternion> TweenTo(TweenValueGetter<Quaternion> getterFunc,
            TweenValueSetter<Quaternion> setterFunc, Quaternion endValue, float duration, object source)
        {
            var tween = new QuaternionTweener(getterFunc, setterFunc, endValue, duration, defaultEase);
            TweenManager.Instance.AddTween(tween, source);
            return tween;
        }
        public static Tweener<Vector2> TweenTo(TweenValueGetter<Vector2> getterFunc,
            TweenValueSetter<Vector2> setterFunc, Vector2 endValue, float duration, object source)
        {
            var tween = new Vector2Tweener(getterFunc, setterFunc, endValue, duration, defaultEase);
            TweenManager.Instance.AddTween(tween, source);
            return tween;
        }

        public static Tweener<Vector3> TweenTo(TweenValueGetter<Vector3> getterFunc,
            TweenValueSetter<Vector3> setterFunc, Vector3 endValue, float duration, object source)
        {
            var tween = new Vector3Tweener(getterFunc, setterFunc, endValue, duration, defaultEase);
            TweenManager.Instance.AddTween(tween, source);
            return tween;
        }

        public static Tweener<float> TweenTo(TweenValueGetter<float> getterFunc, TweenValueSetter<float> setterFunc,
            float endValue, float duration, object source)
        {
            var tween = new FloatTweener(getterFunc, setterFunc, endValue, duration, defaultEase);
            TweenManager.Instance.AddTween(tween, source);
            return tween;
        }

        public static Tweener<Color> TweenTo(TweenValueGetter<Color> getterFunc, TweenValueSetter<Color> setterFunc,
            Color endValue, float duration, object source)
        {
            var tween = new ColorTweener(getterFunc, setterFunc, endValue, duration, defaultEase);
            TweenManager.Instance.AddTween(tween, source);
            return tween;
        }


    }
}