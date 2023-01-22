using UnityEngine;

namespace HuntroxGames.Utils
{
    public class ColorTweener : Tweener<Color>
    {
        public ColorTweener(TweenGetter<Color> getter, TweenSetter<Color> setter, Color endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear) 
            : base(getter, setter, endValue, duration, ease)
        {
        }

        protected override void UpdateValue()
        {
            currentValue = Color.Lerp(startValue, endValue, EasingFunctions.Evaluate(Position, ease));
            setter(currentValue);
        }
    }
}