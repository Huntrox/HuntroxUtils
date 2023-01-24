using UnityEngine;

namespace HuntroxGames.Utils
{
    public static class HuntweenEditor 
    {
        [UnityEditor.InitializeOnLoadMethod]
        public static void Refresh() 
            => LoadOrCreateSettings();


        public static HuntweenSettings LoadOrCreateSettings()
        {
            var settings = Resources.Load<HuntweenSettings>("HuntweenSettings");
            if (settings != null) return settings;
            settings = ScriptableObject.CreateInstance<HuntweenSettings>();

            if (!UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources"))
                UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");
                
            UnityEditor.AssetDatabase.CreateAsset(settings, "Assets/Resources/HuntweenSettings.asset");
            UnityEditor.AssetDatabase.SaveAssets();
            return settings;
        }
    }
}
