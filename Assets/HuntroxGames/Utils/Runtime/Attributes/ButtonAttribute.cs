using System;
using UnityEngine;

namespace HuntroxGames.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string callbackName;
        public ButtonAttribute(string callbackName = "")
        {
            this.callbackName = callbackName;
        }
    }
}