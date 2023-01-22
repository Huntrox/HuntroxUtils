using System;
using JetBrains.Annotations;
using UnityEngine;

namespace HuntroxGames.Utils
{
    [PublicAPI]
    public abstract class Tween
    {
        private float position;
        private float elapsedTime;
        private bool isKilled;
        
        public float duration;

        public float Position => position;
        public bool IsKilled => isKilled;

        public Action onPlay;
        public Action onUpdate;
        public Action onComplete;

        public bool isActive;
        public bool isComplete;
        
        public EasingFunctions.Ease ease;

        public YieldInstruction Yield()
        {
            return TweenManager.Instance.StartCoroutine(
                TweenManager.WaitForTween(this));
        }

        public Tween OnComplete(Action action)
        {
            onComplete = action;
            return this;
        }
        public Tween OnUpdate(Action action)
        {
            onUpdate = action;
            return this;
        }
        public Tween SetEase(EasingFunctions.Ease ease)
        {
            this.ease = ease;
            return this;
        }

        public void KillTween()
        {
            isActive = false;
            isKilled = true;
        }

        public void CompleteTween()
        {
            isComplete = true;
            SetTweenPosition(1);
        }

        public Tween SetTweenPosition(float pos)
        {
            position = pos;
            elapsedTime = duration * position;
            return this;
        }
        public virtual void Update(float delta)
        {
            if (!isActive || isComplete)
                return;
            if (elapsedTime < duration)
            {
                elapsedTime += delta;
                position = elapsedTime / duration;
                UpdateValue();
                onUpdate?.Invoke();
            }
            else
                TweenComplete();
        }

        protected abstract void UpdateValue();

        protected virtual void TweenComplete()
        {
            isComplete = true;
            onComplete?.Invoke();
        }

    }
    [PublicAPI]
    public abstract class Tweener<T> : Tween where T : struct
    {
        public TweenGetter<T> getter;
        public TweenSetter<T> setter;
        public T startValue;
        public T currentValue;
        public T endValue;

        protected Tweener(TweenGetter<T> getter, TweenSetter<T> setter, T endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            this.getter = getter;
            this.setter = setter;
            this.endValue = endValue;
            this.duration = duration;
            this.ease = ease;
            this.startValue = this.getter();
        }

        protected override void TweenComplete()
        {
            setter(endValue);
            base.TweenComplete();
        }
        
    }

    public delegate T TweenGetter<out T>();
    public delegate void TweenSetter<in T>(T newValue);

}