using System.Collections.Generic;

namespace HuntroxGames.Utils
{
    [System.Serializable]
    public class SaveFile
    {
        public string saveDisplayName;
        public List<string> destroyedObjects;
        public List<string> createdObjects;
        public List<string> saveData;
        public string saveDate;
        public string levelName;
    }
}