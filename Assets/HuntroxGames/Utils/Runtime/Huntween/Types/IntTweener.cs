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
            currentValue = Huntween.Lerp(startValue, endValue, EasedPosition);
            valueSetter(currentValue);
            
        }
    }
}