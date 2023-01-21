using System;
using System.Collections;
using HuntroxGames.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HuntroxGames.Utils
{
    public class BasicLoadingScreen : MonoBehaviour, ILoadingScreen
    {

        [SerializeField] private CanvasGroup bgLayerGroup;
        [SerializeField] private CanvasGroup loadingGroup;
        [SerializeField] private Image progressBar;
        [SerializeField] private float fadeDuration = 0.25f;
        


        public bool IsLoading { get; set; }
        public float Progress { get; private set; }

        public void RestLoadingScreen()
        {
            bgLayerGroup.alpha = 0;
            loadingGroup.alpha = 0;
            progressBar.transform.localScale = new Vector3(0, 1, 1);
            bgLayerGroup.interactable = true;
            bgLayerGroup.blocksRaycasts = true;
        }

        public IEnumerator OnShow(Action callback)
        {
            StartCoroutine(CanvasGroupAlphaTween(bgLayerGroup, 1, fadeDuration));
            yield return StartCoroutine(CanvasGroupAlphaTween(loadingGroup, 1, fadeDuration));
            
        }

        public IEnumerator OnHide(Action callback)
        {
            StartCoroutine(CanvasGroupAlphaTween(bgLayerGroup, 0, fadeDuration));
            yield return StartCoroutine(CanvasGroupAlphaTween(loadingGroup, 0, fadeDuration));
            bgLayerGroup.alpha = 0;
            loadingGroup.alpha = 0;
            progressBar.transform.localScale = new Vector3(0, 1, 1);
            bgLayerGroup.interactable = false;
            bgLayerGroup.blocksRaycasts = false;
        }

        public void UpdateProgress(float progress)
        {
            Progress = progress;
            progressBar.transform.localScale = new Vector3(Progress, 1, 1);
        }


        private IEnumerator CanvasGroupAlphaTween(CanvasGroup group, float endValue, float duration)
        {
            var time = 0f;
            var start = group.alpha;
            while (time < duration)
            {
                time += Time.deltaTime;
                var t = time ;
                var alpha = Mathf.Lerp(start, endValue,EasingFunctions.Evaluate(time,EasingFunctions.Ease.InOutSine));
                Debug.Log($"alpha:{alpha} time:{t}");
                group.alpha = alpha;
                yield return null;
            }
        }
        
    }
}
