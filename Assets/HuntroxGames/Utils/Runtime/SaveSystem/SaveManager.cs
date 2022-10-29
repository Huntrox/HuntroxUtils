using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public class SaveManager : Singleton<SaveManager>
    {
        [SerializeField] private List<SerializableMonoBehavior> type;
        

        private List<SerializableMonoBehavior> destroyedSavableObjects = new List<SerializableMonoBehavior>();
        private List<SerializableMonoBehavior> liveSavableObjects = new List<SerializableMonoBehavior>();
        
        
        protected override void Awake()
        {
            base.Awake();
        }
        
    }

    public abstract class SerializableMonoBehavior : MonoBehaviour
    {
        public string Guid { get; }
        public virtual bool ShouldSave() => false;

        public virtual void Load(string json)
        {
            if (string.IsNullOrEmpty(json))
                return;
            
            var behaviorState = JsonUtility.FromJson<BehaviorState>(json);
            
            


            
            if(behaviorState.wasDestroyed)
                Destroy(gameObject);
        }

        public virtual string Save()
        {


            return null;
        }


        public virtual void OnDestroy()
        {
            
        }
        public virtual void OnEnable()
        {
            
        }
    }

    [System.Serializable]
    public struct BehaviorState
    {
        public bool wasDestroyed;
        public List<FieldState> states;
        public List<FieldState> customStates;
    }

    [System.Serializable]
    public struct FieldState
    {
        public object data;
        public string fieldName;
    }

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