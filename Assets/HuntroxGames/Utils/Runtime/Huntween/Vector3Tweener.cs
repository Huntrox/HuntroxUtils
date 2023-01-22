using UnityEngine;

namespace HuntroxGames.Utils
{
    public class Vector3Tweener : Tweener<Vector3>
    {
        public Vector3Tweener(TweenGetter<Vector3> getter, TweenSetter<Vector3> setter, Vector3 endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear) : base(getter, setter, endValue, duration, ease)
        {
        }

        protected override void UpdateValue()
        {
            currentValue = Vector3.Lerp(startValue, endValue, EasingFunctions.Evaluate(Position, ease));
            setter(currentValue);
        }
    }
}