using UnityEngine;

namespace HuntroxGames.Utils
{
    public class Vector2Tweener : Tweener<Vector2>
    {
        public Vector2Tweener(TweenGetter<Vector2> getter, TweenSetter<Vector2> setter, Vector2 endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
            : base(getter, setter, endValue, duration, ease)
        {
        }

        protected override void UpdateValue()
        {
            currentValue = Vector2.Lerp(startValue, endValue, EasingFunctions.Evaluate(Position, ease));
            setter(currentValue);
        }
    }
}