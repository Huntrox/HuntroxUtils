using UnityEngine;

namespace HuntroxGames.Utils
{
    public class FloatTweener : Tweener<float>
    {
        public FloatTweener(TweenGetter<float> getter, TweenSetter<float> setter, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
            : base(getter, setter, endValue, duration, ease)
        {
        }
        
        protected override void UpdateValue()
        {
            currentValue = Mathf.Lerp(startValue, endValue, EasingFunctions.Evaluate(Position, ease));
            setter(currentValue);
            
        }
    }
}