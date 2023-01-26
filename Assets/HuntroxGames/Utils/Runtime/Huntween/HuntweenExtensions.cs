using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace HuntroxGames.Utils
{
    [PublicAPI]
    public static class HuntweenExtensions
    {



        public static void KillTween(this Component component)
            => TweenManager.Instance.KillTween(component);

        public static void CompleteTween(this Component component)
            => TweenManager.Instance.CompleteTween(component);

        #region Transform

        //Local Position Tweens
        public static Tween TweenLocalPosition(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localPosition,
                position => transform.localPosition = position, endValue, duration, transform);
            return tween;
        }
        public static Tween TweenLocalPositionX(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localPosition.x,
                position => transform.localPosition = new Vector3(position, transform.localPosition.y,
                    transform.localPosition.z), endValue, duration, transform);
            return tween;
        }
        public static Tween TweenLocalPositionY(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localPosition.y,
                position => transform.localPosition = new Vector3(transform.localPosition.x, position,
                    transform.localPosition.z), endValue, duration, transform);
            return tween;
        }
        public static Tween TweenLocalPositionZ(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localPosition.z,
                position => transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                    position), endValue, duration, transform);
            return tween;
        }
        //Position Tweens
        public static Tween TweenPosition(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.position, position => transform.position = position,
                endValue, duration, transform);
            return tween;
        }
        public static Tween TweenPositionX(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.position.x, 
                position => transform.position = new Vector3(position,transform.position.y,transform.position.z),
                endValue, duration, transform);
            return tween;
        }
        public static Tween TweenPositionY(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.position.y, 
                position => transform.position = new Vector3(transform.position.x,position,transform.position.z),
                endValue, duration, transform);
            return tween;
        }
        public static Tween TweenPositionZ(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.position.z, 
                position => transform.position = new Vector3(transform.position.x,transform.position.y,position),
                endValue, duration, transform);
            return tween;
        }
        // Scale Tweens
        public static Tween TweenScale(this Transform transform, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localScale, position => transform.localScale = position,
                endValue, duration, transform);
            return tween;
        }
        public static Tween TweenScaleX(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localScale.x, 
                position => transform.localScale = new Vector3(position,transform.localScale.y,transform.localScale.z),
                endValue, duration, transform);
            return tween;
        }
        public static Tween TweenScaleY(this Transform transform, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => transform.localScale.y, 
                position => transform.localScale = new Vector3(transform.localScale.x,position,transform.localScale.z),
                endValue, duration, transform);
            return tween;
        }

        #endregion

        #region RectTransform

        public static Tween TweenScale(this RectTransform rect, Vector3 endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => rect.localScale, position => rect.localScale = position, endValue,
                duration, rect);
            return tween;
        }
        //public static Tween TweenAnchoredPosition(

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
                a => image.color = new Color(image.color.r, image.color.g, image.color.b, a), endValue, duration,
                image);
            return tween;
        }

        public static Tween TweenAlpha(this CanvasGroup group, float endValue, float duration,
            EasingFunctions.Ease ease = EasingFunctions.Ease.Linear)
        {
            var tween = Huntween.TweenTo(() => group.alpha, a => group.alpha = a, endValue, duration, group);
            return tween;
        }

        #endregion

    }
}
