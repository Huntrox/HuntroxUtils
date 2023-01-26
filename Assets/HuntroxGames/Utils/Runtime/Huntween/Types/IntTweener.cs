using UnityEngine;

namespace HuntroxGames.Utils
{
    public class IntTweener : Tweener<int>
    {
        public IntTweener(TweenValueGetter<int> valueGetter, TweenValueSetter<int> valueSetter, int endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
            : base(valueGetter, valueSetter, endValue, duration, ease)
        {
        }
        
        protected override void UpdateValue()
        {
            currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, EasingFunctions.Evaluate(Position, ease)));
            valueSetter(currentValue);
            
        }
    }
}