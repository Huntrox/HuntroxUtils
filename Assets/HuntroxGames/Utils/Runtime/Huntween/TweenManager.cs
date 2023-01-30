using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public class TweenManager : Singleton<TweenManager>
    {
        //TODO: pool tweens
        
        private bool isActive;
        
        private readonly List<Tween> activeTweens = new List<Tween>();
        private readonly Dictionary<object, List<Tween>> tweenDict = new Dictionary<object, List<Tween>>();
        private readonly List<Tween> queueForAdd = new List<Tween>();

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
                activeTweens.Add(queueForAdd[i]);
            
            var finishedTweens = new List<Tween>();
            
            //loop through all active tweens in tweens dictionary  and update
                
            
                
            for (var i = 0; i < activeTweens.Count; i++)
            {
                var index = i;
                var tween = activeTweens[index];
                if (tween.source.IsNull())
                {
                    finishedTweens.Add(tween);
                    continue;
                }
                tween.Update(Huntween.DeltaTime);
                if (tween.isComplete || tween.IsKilled)
                {
                    finishedTweens.Add(tween);
                    if (tweenDict.TryGetValue(tween.source, out var list))
                    {
                        list.Remove(tween);
                        if (list.Count == 0)
                            tweenDict.Remove(tween.source);
                    }
                }
            }
            
            for (var index = 0; index < finishedTweens.Count; index++)
            {
                var tween = finishedTweens[index];
                activeTweens.Remove(tween);
            }


            queueForAdd.Clear();
        }

        public void AddTween(Tween t, object source = null)
        {
            t.isActive = true;
            t.source = (Component)source;
            queueForAdd.Add(t);
            if (source != null)
            {
                if (!tweenDict.ContainsKey(source))
                    tweenDict.Add(source, new List<Tween>());
                tweenDict[source].Add(t);
            }

        }
        public void KillTween(object source)
        {
            if (!tweenDict.TryGetValue(source, out var list)) return;
            foreach (var tween in list) tween.KillTween();
        }
        public void CompleteTween(object source)
        {
            if (!tweenDict.TryGetValue(source, out var list)) return;
            foreach (var tween in list) tween.CompleteTween();
        }
        
        public void KillAllTweens()
        {
            foreach (var tween in activeTweens) tween.KillTween();
        }
        public void CompleteAllTweens()
        {
            foreach (var tween in activeTweens) tween.CompleteTween();
        }
        public IEnumerable<Tween> GetActiveTweens(object source)
        {
            if (!tweenDict.TryGetValue(source, out var list)) yield break;
            foreach (var tween in list) yield return tween;
        }
  

        public  IEnumerator WaitForTween(Tween t)
        {
            while (t.isActive && !t.isComplete)
                yield return null;
        }

    }
}