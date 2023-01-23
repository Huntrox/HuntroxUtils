using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public class TweenManager : Singleton<TweenManager>
    {
        private readonly List<Tween> tweens = new List<Tween>();
        private readonly Dictionary<object, List<Tween>> tweenDict = new Dictionary<object, List<Tween>>();
        private bool isActive;
        private List<Tween> queueForAdd = new List<Tween>();

        protected override void Awake()
        {
            if (instance == null)
            {
                instance = this;

#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
#endif
                    DontDestroyOnLoad(gameObject);
            }
            else
                if(!isActive)
                    Destroy(gameObject);

            isActive = true;
        }

        private void Update()
        {

            for (int i = 0; i < queueForAdd.Count; i++) 
                tweens.Add(queueForAdd[i]);
            
            var finishedTweens = new List<Tween>();
            for (var i = 0; i < tweens.Count; i++)
            {
                var index = i;
                var tween = tweens[index];
                tween.Update(Time.deltaTime);
                if(tween.isComplete || tween.IsKilled)
                    finishedTweens.Add(tween);
            }
            
            for (var index = 0; index < finishedTweens.Count; index++)
            {
                var tween = finishedTweens[index];
                tweens.Remove(tween);
            }


            queueForAdd.Clear();
        }

        public void AddTween(Tween t, object source = null)
        {
            t.isActive = true;
            queueForAdd.Add(t);
        }
        public static IEnumerator WaitForTween(Tween t)
        {
            while (t.isActive && !t.isComplete)
                yield return null;
        }

    }
}