using UnityEngine;

namespace HuntroxGames.Utils
{
    public class Vector3Tweener : Tweener<Vector3>
    {
        public Vector3Tweener(TweenValueGetter<Vector3> valueGetter, TweenValueSetter<Vector3> valueSetter, Vector3 endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
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