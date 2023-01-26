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
    public class StringTweener : Tween
    {
        private int totalCharCount;
        private int currentCharIndex;
        public string startValue;
        public string currentValue;
        public string endValue;
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
            currentCharIndex = Mathf.RoundToInt(Mathf.Lerp(0, totalCharCount, EasingFunctions.Evaluate(Position, ease)));
            currentValue = endValue.Substring(0, currentCharIndex);
            valueSetter(currentValue);
        }
    }
}