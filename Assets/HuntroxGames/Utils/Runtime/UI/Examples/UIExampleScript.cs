using HuntroxGames.Utils.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuntroxGames.Utils
{
    public class UIExampleScript : MonoBehaviour
    {
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
