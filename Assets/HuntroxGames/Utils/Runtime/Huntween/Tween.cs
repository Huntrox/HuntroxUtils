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
        
        protected bool usingCurve;
        
        public float duration;

        
        public float Position => position;
        public bool IsKilled => isKilled;

        public Action onPlay;
        public Action onUpdate;
        public Action onComplete;

        public bool isActive;
        public bool isComplete;
        public Component source;
        public EasingFunctions.Ease ease;
        public AnimationCurve curve;
        public LoopType loopType;
        public int loopCount;
        public int currentLoop;
        
        

        
        protected float EasedPosition 
            => usingCurve ? curve.Evaluate(position) : EasingFunctions.Evaluate(position, ease);

        public YieldInstruction Yield()
        {
            return TweenManager.Instance.StartCoroutine(
                TweenManager.Instance.WaitForTween(this));
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
        public Tween SetEase(AnimationCurve ease)
        {
            this.curve = ease;
            this.usingCurve = true;
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
            if (elapsedTime < duration && !TargetReached())
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

        protected virtual bool TargetReached() => false;

    }
    [PublicAPI]
    public abstract class Tweener<T> : Tween where T : struct
    {
        public TweenValueGetter<T> valueGetter;
        public TweenValueSetter<T> valueSetter;
        public T startValue;
        public T currentValue;
        public T endValue;

        protected Tweener(TweenValueGetter<T> valueGetter, TweenValueSetter<T> valueSetter, T endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            this.valueGetter = valueGetter;
            this.valueSetter = valueSetter;
            this.endValue = endValue;
            this.duration = duration;
            this.ease = ease;
            this.startValue = this.valueGetter();
            isActive = true;
        }

        protected override void TweenComplete()
        {
            valueSetter(endValue);
            base.TweenComplete();
        }
        
    }

    public delegate T TweenValueGetter<out T>();
    public delegate void TweenValueSetter<in T>(T newValue);
    
    public enum LoopType
    {
        Restart,
        PingPong,
        Incremental
    }

}