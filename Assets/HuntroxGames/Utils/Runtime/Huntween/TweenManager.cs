using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public class TweenManager : Singleton<TweenManager>
    {
        private readonly List<Tween> tweens = new List<Tween>();


        private void Update()
        {
            for (int i = 0; i < tweens.Count; i++)
            {
                int index = i;
                var tween = tweens[index];
                tween.Update(Time.deltaTime);
            }
        }

        public void AddTween(Tween t)
        {
            t.isActive = true;
            tweens.Add(t);
        }
        public static IEnumerator WaitForTween(Tween t)
        {
            while (t.isActive && !t.isComplete)
                yield return null;
        }

    }

    public abstract class Tween
    {
        protected float position;
        protected float elapsedTime;

        public float duration;
        
        public float Position => position;
        public Action onPlay;
        public Action onUpdate;
        public Action onComplete;

        public bool isActive;
        public bool isComplete;
        public EasingFunctions.Ease ease;

        public YieldInstruction WaitForTween()
        {
            return TweenManager.Instance.StartCoroutine(
                TweenManager.WaitForTween(this));
        }

        public abstract void Update(float delta);

    }

    public class Tweener<T> : Tween where T : struct
    {
        public TweenGetter<T> getter;
        public TweenSetter<T> setter;
        public T startValue;
        public T currentValue;
        public T endValue;

        public Tweener(TweenGetter<T> getter, TweenSetter<T> setter, T endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            this.getter = getter;
            this.setter = setter;
            this.endValue = endValue;
            this.duration = duration;
            this.ease = ease;
            this.startValue = this.getter();
        }

        public override void Update(float delta)
        {
        }
    }

    public class FloatTweener : Tweener<float>
    {
        public FloatTweener(TweenGetter<float> getter, TweenSetter<float> setter, float endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear) 
            : base(getter, setter, endValue, duration, ease)
        {
        }

        
        public override void Update(float delta)
        {
            if(!isActive || isComplete)
                return;
            if (elapsedTime < duration)
            {
                elapsedTime += delta;
                position = elapsedTime / duration;
                currentValue = Mathf.Lerp(startValue, endValue,EasingFunctions.Evaluate(position, ease));
                setter(currentValue);
                Debug.Log(currentValue);
                onUpdate?.Invoke();
            }
            else
            {
                isComplete = true;
                Debug.Log("Completed");
                onComplete?.Invoke();
            }
        }
    }

    public delegate T TweenGetter<out T>();

    public delegate void TweenSetter<in T>(T newValue);


}