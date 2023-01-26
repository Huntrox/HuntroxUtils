using UnityEngine;

namespace HuntroxGames.Utils
{
    public class FloatTweener : Tweener<float>
    {
        public FloatTweener(TweenValueGetter<float> valueGetter, TweenValueSetter<float> valueSetter, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
            : base(valueGetter, valueSetter, endValue, duration, ease)
        {
        }
        
        protected override void UpdateValue()
        {
            currentValue = Huntween.Lerp(startValue, endValue, EasedPosition);
            valueSetter(currentValue);
        }
    }
}