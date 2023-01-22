using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace HuntroxGames.Utils
{
    [PublicAPI]
    public static class Huntween
    {
        
        //public static Tween DoColor
        public static Tween DoFade(this Image image, float endValue, float duration, EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = new FloatTweener(() => image.color.a, a => image.color = new Color(image.color.r,image.color.g,image.color.b,a), endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }
        public static Tween DoFade(this CanvasGroup group, float endValue, float duration, EasingFunctions.Ease ease)
        {
            var tween = new FloatTweener(() => group.alpha, a => group.alpha = a, endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }
        
    }
}