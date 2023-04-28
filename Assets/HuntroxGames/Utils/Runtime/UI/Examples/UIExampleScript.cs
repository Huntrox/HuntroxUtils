using HuntroxGames.Utils.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuntroxGames.Utils
{
    public class UIExampleScript : MonoBehaviour
    {

        [SerializeField] private AnchorPresets anchorPreset;
        [SerializeField] private Vector2 offset;
        [SerializeField] private RectTransform target;



        [Button()]
        private void TestAnchor()
        {
            if (target == null)
                return;
            target.SetRectAnchor(anchorPreset, offset);
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
