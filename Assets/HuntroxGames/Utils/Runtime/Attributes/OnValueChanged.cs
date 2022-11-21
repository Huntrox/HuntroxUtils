using System;
using UnityEngine;

namespace HuntroxGames.Utils
{
    [AttributeUsage(AttributeTargets.Field,AllowMultiple = false, Inherited = true)]
    public class OnValueChangedAttribute : Attribute
    {
        public string onValueChangedCallbackName;
        
        public OnValueChangedAttribute(string callback)
        {
            onValueChangedCallbackName = callback;
            
        }
    }
}
