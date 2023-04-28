using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HuntroxGames.Utils
{
    [PublicAPI]
    public static class ExtensionMethods
    {
        #region FloatExtension
        public static float Snap(this float value, float snappingValue)
        {
            if (snappingValue == 0) return value;
            return Mathf.Round(value / snappingValue) * snappingValue;
        }

        /// <summary>Round to nearest number of float points<br></br>
        /// <param><b>roundVal:</b> Number of decimal points to round value.(i.e. 2 = .00, 3 = .000, 4 = .0000, etc.)</param></summary>
        public static float RoundF(this float num, int roundVal)
            => (float)Math.Round((decimal)num, roundVal);

        /// <summary>Round float value to nearest half fraction (i.e. 2 = half, 3 = third, 4 = quarter, etc.)</summary>
        public static float RoundHalf(this float num, int roundVal) =>
            (float)Math.Round(num * roundVal, MidpointRounding.AwayFromZero) / roundVal;

        public static float ToSeconds(this float minutes)
            => minutes * 60f;

        /// <summary>
        /// Awards or penalizes percentage points based on input
        /// </summary>
        /// <param name="value"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static int AdjustByPercent(this int value, float percent)
            => Mathf.RoundToInt((value * percent) + value);

        public static float Ratio(this float value, float maxValue)
            => (int)value / Mathf.Clamp(maxValue, Mathf.Epsilon, int.MaxValue);

        public static float Ratio(this float value, float minValue, float maxValue)
            => (int)value / Mathf.Clamp(maxValue - minValue, Mathf.Epsilon, int.MaxValue);

        public static float LinearRemap(this float value,
            float valueRangeMin, float valueRangeMax,
            float newRangeMin, float newRangeMax) =>
            (value - valueRangeMin) / (valueRangeMax - valueRangeMin) * (newRangeMax - newRangeMin) + newRangeMin;

        

        #endregion

        public static bool MaskContains(this LayerMask mask, int layerNumber) 
            => mask == (mask | (1 << layerNumber));

        public static int WithRandomSign(this int value, float negativeProbability = 0.5f)
            => Random.value < negativeProbability ? -value : value;

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (component == null)
                return null;

            var type = typeof(T);
            var compon = component.gameObject.GetComponent(type);
            if (compon == null) compon = component.gameObject.AddComponent(type);

            return compon as T;
        }
        
        #region Color
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
        public static void SetAlpha(this UnityEngine.UI.Graphic graphic, float alpha)
        {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
        public static Color SetRed(this Color color,float red)
            => new Color(red,color.g,color.b,color.a);
        public static Color SetGreen(this Color color,float green)
            => new Color(color.r,green,color.b,color.a);
        public static Color SetBlue(this Color color,float blue)
            => new Color(color.r,color.g,blue,color.a);

        public static Color SetAlpha(this Color color,float alpha)
            => new Color(color.r,color.g,color.b,alpha);
      
        public static Color SetHue(this Color color, float h)
        {
            Color.RGBToHSV(color, out var _, out var s, out var v);
            color = Color.HSVToRGB(h, s, v);
            return color;
        }
        public static Color SetSaturation(this Color color, float s)
        {
            Color.RGBToHSV(color, out var h, out var _, out var v);
            color = Color.HSVToRGB(h, s, v);
            return color;
        }
        public static Color SetValue(this Color color, float v)
        {
            Color.RGBToHSV(color, out var h, out var s, out var _);
            color = Color.HSVToRGB(h, s, v);
            return color;
        }

        

        #endregion
        public static bool IsTouching(this Collider2D collider, Collider2D other) 
            => collider.IsTouching(other.bounds);
        public static bool IsTouching(this Collider2D collider, Bounds other) 
            => collider.bounds.Intersects(other);

        #region Transform
        public static bool IsFacingTarget(this Transform transform,Transform target,float dotThreshold = 0.5f) {
            var vectorToTarget = target.position - transform.position;
            vectorToTarget.Normalize();
            var dot = Vector3.Dot(transform.forward,vectorToTarget);
            return dot >= dotThreshold;
        }
        public static bool IsNull(this Component component) => (component == null);
        public static void LocalReset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (Transform child in transform) UnityEngine.Object.Destroy(child.gameObject);
        }
        public static void LookAtY(this Transform transform, Vector3 point)
        {
            var lookPos = point - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation;
        }
        public static void ChangeDirection(this Rigidbody rb, Vector3 direction) 
            => rb.velocity = direction.normalized * rb.velocity.magnitude;

        #endregion

        #region RectTransfrom


        public static void SetRectAnchor(this RectTransform rectTransform, AnchorPresets anchor, Vector2 offset)
        {
            rectTransform.pivot = anchorPivots[anchor];
            rectTransform.anchorMin = rectTransform.anchorMax = anchorPivots[anchor];
            rectTransform.anchoredPosition = offset;
                
                    if (anchor == AnchorPresets.StretchAll)
                    {
                        rectTransform.anchorMin = Vector2.zero;
                        rectTransform.anchorMax = Vector2.one;
                        rectTransform.sizeDelta = Vector2.zero;
                    }

        }

        private static Dictionary<AnchorPresets, Vector2> anchorPivots = new Dictionary<AnchorPresets, Vector2>()
        {
            { AnchorPresets.TopLeft, new Vector2(0, 1) },
            { AnchorPresets.TopCenter, new Vector2(0.5f, 1) },
            { AnchorPresets.TopRight, new Vector2(1, 1) },
            { AnchorPresets.MiddleLeft, new Vector2(0, 0.5f) },
            { AnchorPresets.MiddleCenter, new Vector2(0.5f, 0.5f) },
            { AnchorPresets.MiddleRight, new Vector2(1, 0.5f) },
            { AnchorPresets.BottomLeft, new Vector2(0, 0) },
            { AnchorPresets.BottomCenter, new Vector2(0.5f, 0) },
            { AnchorPresets.BottomRight, new Vector2(1, 0) },
            { AnchorPresets.StretchAll, new Vector2(0.5f, 0.5f) }
        };


        #endregion
    }
    public enum AnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        StretchAll
    }
}