using System;
using System.Collections;
using HuntroxGames.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HuntroxGames.Utils

{
    public class TweenExamples : MonoBehaviour
    {
        public ExtendedButton button;
        public EasingFunctions.Ease ease;
        public Transform trans;
        [Range(0,1)] public float test = 0;
        private Tween tween;
        public int testIntVar = 0;

        public void Start()
        {
            button.onClick.AddListener(() =>
            {
                StartCoroutine(TestTweenYield());
                tween.SetTweenPosition(test);
            });

            
            Huntween.TweenTo(() => testIntVar,  x => testIntVar = x, 10, 5f, this);
            
            tween = trans.TweenPosition(new Vector3(5, 5, 0), 5);
            button.onEnter.AddListener(() =>
            {
                button.GetComponent<RectTransform>().TweenScale(new Vector3(1.5f, 1.5f, 1.5f), 0.25f).SetEase(ease);
                button.GetComponent<Image>().TweenColor(Color.cyan, 0.25f).SetEase(ease);
            });
            button.onExit.AddListener(() =>
            {
                button.GetComponent<RectTransform>().TweenScale(Vector3.one, 0.25f);
                button.GetComponent<Image>().TweenColor(Color.white, 0.25f);
            });
            
        }
        private IEnumerator TestTweenYield()
        {
            Debug.Log("Started");
            yield return transform.TweenPosition(new Vector3(5, 5, 5), 5).SetEase(EasingFunctions.Ease.InOutSine)
                .Yield();
            Debug.Log("Finished yield");
        }
    }
}
