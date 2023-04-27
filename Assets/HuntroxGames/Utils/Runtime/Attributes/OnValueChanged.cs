using System;

namespace HuntroxGames.Utils
{
    [AttributeUsage(AttributeTargets.Field,AllowMultiple = false, Inherited = true)]
    public class OnValueChangedAttribute : Attribute
    {
        public readonly string onValueChangedCallbackName;
        
        public OnValueChangedAttribute(string callback) 
            => onValueChangedCallbackName = callback;
    }
}
