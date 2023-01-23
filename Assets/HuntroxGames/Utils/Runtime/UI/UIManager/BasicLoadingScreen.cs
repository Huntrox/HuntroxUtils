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
            bgLayerGroup.TweenAlpha(1f, fadeDuration);
            yield return loadingGroup.TweenAlpha(1f, fadeDuration).Yield();
            
        }

        public IEnumerator OnHide(Action callback)
        {
            bgLayerGroup.TweenAlpha(0, fadeDuration);
            yield return loadingGroup.TweenAlpha(0, fadeDuration).Yield();
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



        
    }
}
