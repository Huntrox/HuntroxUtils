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
            var tween = new Vector3Tweener(() => transform.localPosition,
                position => transform.localPosition = position, endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }

        public static Tween TweenPosition(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = new Vector3Tweener(() => transform.position, position => transform.position = position,
                endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }

        public static Tween TweenScale(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = new Vector3Tweener(() => transform.localScale, position => transform.localScale = position,
                endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }

        #endregion

        #region RectTransform

        public static Tween TweenScale(this RectTransform rect, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = new Vector3Tweener(() => rect.localScale, position => rect.localScale = position, endValue,
                duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }

        #endregion

        #region UI

        public static Tween TweenColor(this Image image, Color endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = new ColorTweener(() => image.color, c => image.color = c, endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }

        public static Tween TweenAlpha(this Image image, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = new FloatTweener(() => image.color.a,
                a => image.color = new Color(image.color.r, image.color.g, image.color.b, a), endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }

        public static Tween TweenAlpha(this CanvasGroup group, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = new FloatTweener(() => group.alpha, a => group.alpha = a, endValue, duration, ease);
            TweenManager.Instance.AddTween(tween);
            return tween;
        }

        #endregion
        
    }
}
