using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public class TweenManager : Singleton<TweenManager>
    {
        private readonly List<Tween> tweens = new List<Tween>();
        

        private void Update()
        {
            var finishedTweens = new List<Tween>();
            for (var i = 0; i < tweens.Count; i++)
            {
                var index = i;
                var tween = tweens[index];
                tween.Update(Time.deltaTime);
                if(tween.isComplete || tween.IsKilled)
                    finishedTweens.Add(tween);
            }

            // foreach (var tween in tweens)
            //     tweens.Remove(tween);
            
        }

        public void AddTween(Tween t)
        {
            t.isActive = true;
            tweens.Add(t);
        }
        public static IEnumerator WaitForTween(Tween t)
        {
            while (t.isActive && !t.isComplete)
                yield return null;
        }

    }
}