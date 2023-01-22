using System;
using HuntroxGames.Utils.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HuntroxGames.Utils
{
    public class UIExampleScript : MonoBehaviour
    {
        public Image image;
        public EasingFunctions.Ease ease;
        [ContextMenu("TEST")]
        public void Start()
        {
            image.DoFade(0f, 2f,ease);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                UIManager.LoadLevel(SceneManager.GetActiveScene().name, (() =>
                {
                    Debug.Log("Loading Started");
                }), () =>
                {
                    Debug.Log("Loading Finished!");
                });
            }
        }
    }
}
