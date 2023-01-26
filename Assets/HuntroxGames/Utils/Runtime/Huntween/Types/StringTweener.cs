using UnityEngine;

namespace HuntroxGames.Utils
{
    public class StringTweener : Tween
    {
        private readonly int totalCharCount;
        private int currentCharIndex;
        public string startValue;
        public string currentValue;
        public readonly string endValue;
        public TweenValueGetter<string> valueGetter;
        public TweenValueSetter<string> valueSetter;
            
        public StringTweener(TweenValueGetter<string> valueGetter, TweenValueSetter<string> valueSetter, string endValue,
            float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            this.valueGetter = valueGetter;
            this.valueSetter = valueSetter;
            this.endValue = endValue;
            this.duration = duration;
            this.ease = ease;
            this.startValue = this.valueGetter();
            this.valueSetter(string.Empty);
            totalCharCount = endValue.Length;
            isActive = true;
        }

        protected override void UpdateValue()
        {
            currentCharIndex = Huntween.Lerp(0, totalCharCount, EasedPosition);
            currentValue = endValue.Substring(0, currentCharIndex);
            valueSetter(currentValue);
        }
    }
}