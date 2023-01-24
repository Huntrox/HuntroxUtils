using UnityEngine;

namespace HuntroxGames.Utils
{
    public class HuntweenSettings : ScriptableObject
    {
        public bool unscaledTime = false;
        public EasingFunctions.Ease defaultEase = EasingFunctions.Ease.Linear;
    }
}