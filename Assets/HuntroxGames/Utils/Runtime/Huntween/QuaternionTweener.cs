using UnityEngine;

namespace HuntroxGames.Utils
{
    public class QuaternionTweener : Tweener<Quaternion>
    {
        public QuaternionTweener(TweenGetter<Quaternion> getter, TweenSetter<Quaternion> setter, Quaternion endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear) 
            : base(getter, setter, endValue, duration, ease)
        {
        }

        protected override void UpdateValue()
        {
            currentValue = Quaternion.Lerp(startValue, endValue, EasingFunctions.Evaluate(Position, ease));
            setter(currentValue);
        }
    }
}