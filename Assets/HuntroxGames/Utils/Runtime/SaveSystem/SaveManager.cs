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
}