using UnityEngine;

namespace HuntroxGames.Utils
{
    public class QuaternionTweener : Tweener<Quaternion>
    {
        public QuaternionTweener(TweenValueGetter<Quaternion> valueGetter, TweenValueSetter<Quaternion> valueSetter, Quaternion endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear) 
            : base(valueGetter, valueSetter, endValue, duration, ease)
        {
        }

        protected override void UpdateValue()
        {
            currentValue = Huntween.Lerp(startValue, endValue, Position, ease);
            valueSetter(currentValue);
        }
    }
}