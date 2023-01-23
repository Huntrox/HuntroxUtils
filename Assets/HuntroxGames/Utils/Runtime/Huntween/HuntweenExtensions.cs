using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HuntroxGames.Utils
{
    public static class HuntweenExtensions
    {
        
        #region Transform

        public static Tween TweenLocalPosition(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localPosition,
                position => transform.localPosition = position, endValue, duration, transform);
            return tween;
        }

        public static Tween TweenPosition(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.position, position => transform.position = position,
                endValue, duration, transform);
            return tween;
        }

        public static Tween TweenScale(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localScale, position => transform.localScale = position,
                endValue, duration, transform);
            return tween;
        }

        #endregion

        #region RectTransform

        public static Tween TweenScale(this RectTransform rect, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween =Huntween.TweenTo(() => rect.localScale, position => rect.localScale = position, endValue,
                duration, rect);
            return tween;
        }

        #endregion

        #region UI

        public static Tween TweenColor(this Image image, Color endValue, float duration)
        {
            var tween 
                = Huntween.TweenTo(() => image.color, c => image.color = c, endValue, duration, image);
            return tween;
        }

        public static Tween TweenAlpha(this Image image, float endValue, float duration)
        {
            var tween = Huntween.TweenTo(() => image.color.a,
                a => image.color = new Color(image.color.r, image.color.g, image.color.b, a), endValue, duration,image);
            return tween;
        }

        public static Tween TweenAlpha(this CanvasGroup group, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween =  Huntween.TweenTo(() => group.alpha, a => group.alpha = a, endValue, duration,group);
            return tween;
        }

        #endregion
        
    }
}
