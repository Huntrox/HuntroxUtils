using UnityEngine;

namespace HuntroxGames.Utils
{
    public class Vector2Tweener : Tweener<Vector2>
    {
        public Vector2Tweener(TweenValueGetter<Vector2> valueGetter, TweenValueSetter<Vector2> valueSetter, Vector2 endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
            : base(valueGetter, valueSetter, endValue, duration, ease)
        {
        }

        protected override void UpdateValue()
        {
            currentValue = Vector2.Lerp(startValue, endValue, EasingFunctions.Evaluate(Position, ease));
            valueSetter(currentValue);
        }
    }
}