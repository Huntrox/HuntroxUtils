﻿using UnityEngine;

namespace HuntroxGames.Utils
{
    public class ColorTweener : Tweener<Color>
    {
        public ColorTweener(TweenValueGetter<Color> valueGetter, TweenValueSetter<Color> valueSetter, Color endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear) 
            : base(valueGetter, valueSetter, endValue, duration, ease)
        {
        }

        protected override void UpdateValue()
        {
            currentValue = Huntween.Lerp(startValue, endValue, EasedPosition);
            valueSetter(currentValue);
        }
        
        protected override bool TargetReached() 
            => endValue == currentValue;
    }
}