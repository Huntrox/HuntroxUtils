using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuntroxGames.Utils.UI
{
    public class UIManager : Singleton<UIManager>
    {

        public new static UIManager Instance
        {
            get
            {
                if (instance) return instance;
                instance = FindObjectOfType<UIManager>();

                if (instance) return instance;
                var uiCanvas = Resources.Load<GameObject>("UI/UIManagerCanvas");
                instance = Instantiate(uiCanvas).GetComponent<UIManager>();
                instance.Init();

                return instance;
            }
        }

        
        [SerializeField] private ILoadingScreen loadingScreen;
        [SerializeField] private PopupsManager popupsManager;


        public ILoadingScreen LoadingScreen => loadingScreen;
            
        protected override void Awake()
        {
            loadingScreen = GetComponentInChildren<ILoadingScreen>();
            if (instance == null)
            {
                instance = this;

#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
#endif
                    DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (!loadingScreen.IsLoading)
                    Destroy(gameObject);
            }

            Init();
        }
        
        private void Init()
        {
            
        }
        
        #region Loading Screen
        
        private void LoadScene(string sceneName, Action onLoadingStarted, Action onLoadingFinished)
        {
            loadingScreen.RestLoadingScreen();
            StartCoroutine(Load());
            IEnumerator Load()
            {
                yield return new WaitForFixedUpdate();
                loadingScreen.IsLoading = true;
                onLoadingStarted?.Invoke();
                
                yield return StartCoroutine(loadingScreen.OnShow(null));
                
                var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
         
                while (!loadingOperation.isDone)
                {
                    loadingScreen.UpdateProgress(loadingOperation.progress);
                    yield return null;
                }
                loadingScreen.UpdateProgress(loadingOperation.progress);
                
                loadingScreen.IsLoading = false;
                onLoadingFinished?.Invoke();
                yield return StartCoroutine(loadingScreen.OnHide(null));
            }
            loadingScreen.transform.SetAsLastSibling();
            transform.SetAsLastSibling();
        }

        #endregion


        
        public static void LoadLevel(string sceneName, Action onLoadingStarted, Action onLoadingFinished)
            => instance.LoadScene(sceneName, onLoadingStarted, onLoadingFinished);
        

    }
}