using System.Collections.Generic;

namespace HuntroxGames.Utils
{
    [System.Serializable]
    public struct BehaviorState
    {
        public bool wasDestroyed;
        public List<FieldState> states;
        public List<FieldState> customStates;
    }
}