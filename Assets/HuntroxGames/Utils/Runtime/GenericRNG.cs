//#define OdinInspector
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if OdinInspector
using Sirenix.OdinInspector;
#endif

namespace HuntroxGames.Utils
{
    [Serializable]
    // ReSharper disable once InconsistentNaming
    public class GenericRNG<T> 
    {
        private List<T> rngItems;
        private Dictionary<int, (T type,int weight)> cachedElements;
#if OdinInspector
        [OnValueChanged("Init",true)]
#endif
        public List<RngElement<T>> tableEntries =new List<RngElement<T>>();
        //random hash code
        public const int EMPTY_HASH_CODE = 65465478;
        public GenericRNG()
        {
            cachedElements = new Dictionary<int,  (T type,int weight)>();
            rngItems = new List<T>();
        }
        public void Init()
        {
            cachedElements.Clear();
            foreach (var entry in tableEntries)
            {
                var code = entry.item != null ? entry.item.GetHashCode() : EMPTY_HASH_CODE;

                if (!cachedElements.ContainsKey(code))
                    cachedElements.Add(code, (entry.item,entry.weight));
            }
            rngItems.Clear();
            foreach (var pair in cachedElements)
            {
                for (int i = 0; i < pair.Value.weight; i++)
                {
                    rngItems.Add(pair.Value.type);
                }
            }
            for (var index = 0; index < tableEntries.Count; index++)
            {
                var entry = tableEntries[index];
                var code = entry.item != null ? entry.item.GetHashCode() : EMPTY_HASH_CODE;
                entry.chance = CalculateProbability(code)+"%";
                tableEntries[index] = entry;
            }
        }

        public double CalculateProbability(int item)
        {
            if(cachedElements.IsNullOrEmpty())
                return 0;
            if (cachedElements.ContainsKey(item))
            {
                double total = cachedElements.Values.Sum(n => n.weight);
                var percent = cachedElements[item].weight / total;
                return Math.Round(percent * 100, 2);
            }
            throw new Exception("Item: " + item + " Does not exist.");
        }

        public T NextRandomItem()
            => rngItems.Random();
        [ContextMenu(nameof(Clear))]
        public void Clear()
        {
            rngItems.Clear();
            cachedElements.Clear();
        }
    }
    
    [Serializable]
    public struct RngElement<T>
    {
#if OdinInspector
        [HorizontalGroup("weight")]
#endif
        public int weight;
#if OdinInspector
        [HorizontalGroup("weight"),HideLabel]
#endif
        [ReadOnly]public string chance;
        public T item;
    }
}