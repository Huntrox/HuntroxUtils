using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HuntroxGames.Utils
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class HuntroxInspector : Editor
    {

        private Dictionary<string, List<MethodInfo>> dict;
        private List<SerializedProperty> serializedProperties = new List<SerializedProperty>();
        private IEnumerable<FieldInfo> fields;
        private IEnumerable<MethodInfo> buttonMethods;

        protected virtual void OnEnable()
        {
            dict = new Dictionary<string, List<MethodInfo>>();
            fields = GetAllFields(target, f => f.GetCustomAttributes(typeof(OnValueChangedAttribute), true).Length > 0);
            buttonMethods = GetAllMethods(target, m => m.GetCustomAttributes(typeof(ButtonAttribute), true).Length > 0);
            foreach (var field in fields)
            {
                List<MethodInfo> methods = new List<MethodInfo>();
                foreach (var @obj in field.GetCustomAttributes(typeof(OnValueChangedAttribute), true))
                {
                    var att = (OnValueChangedAttribute)@obj;
                    var method = target.GetType().GetMethod(att.onValueChangedCallbackName,
                        BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                    methods.Add(method);
                }

                if (methods.Count > 0)
                    dict.Add(field.Name, methods);
            }
        }

        public override void OnInspectorGUI()
        {
            GetSerializedProperties(ref serializedProperties);
            DrawSerializedProperties();
            DrawButtons();
        }

        private void DrawButtons()
        {
            if(!buttonMethods.Any())
                return;
            
            foreach (var method in buttonMethods)
            {
                Button(serializedObject.targetObject, method);
            }
        }


        private static void Button(UnityEngine.Object target,MethodInfo info)
        {
            if (!info.GetParameters().All(p => p.IsOptional))
            {
                var msg = $"{nameof(ButtonAttribute)} works only on methods with no parameters";
                EditorGUILayout.HelpBox(msg, MessageType.Warning);
                return;
            }
            var attribute = (ButtonAttribute)info.GetCustomAttributes(typeof(ButtonAttribute), true)[0];
            var methodName = string.IsNullOrEmpty(attribute.callbackName) ? ObjectNames.NicifyVariableName(info.Name) : attribute.callbackName;
            
            if (GUILayout.Button(methodName, EditorStyleExtensions.ButtonStyle))
            {
                object[] defaultParams = info.GetParameters().Select(p => p.DefaultValue).ToArray();
                var methodResult = (IEnumerator)info.Invoke(target, defaultParams);
                
                if (methodResult != null && target is MonoBehaviour behaviour)
                {
                    behaviour.StartCoroutine(methodResult);
                }
            }
        } 

        public static IEnumerable<FieldInfo> GetAllFields(object target, Func<FieldInfo, bool> predicate)
        {
            if (target == null)
            {
                Debug.LogError("The target object is null. Check for missing scripts.");
                yield break;
            }

            List<Type> types = new List<Type>()
            {
                target.GetType()
            };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<FieldInfo> fieldInfos = types[i]
                    .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic |
                               BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (var fieldInfo in fieldInfos)
                {
                    yield return fieldInfo;
                }
            }
        }
        public static IEnumerable<MethodInfo> GetAllMethods(object target, Func<MethodInfo, bool> predicate)
        {
            if (target == null)
            {
                Debug.LogError("The target object is null. Check for missing scripts.");
                return null;
            }

            IEnumerable<MethodInfo> methodInfos = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(predicate);

            return methodInfos;
        }


        protected void GetSerializedProperties(ref List<SerializedProperty> outSerializedProperties)
        {
            outSerializedProperties.Clear();
            using (var iterator = serializedObject.GetIterator())
            {
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        outSerializedProperties.Add(serializedObject.FindProperty(iterator.name));
                    } while (iterator.NextVisible(false));
                }
            }
        }
        
        protected void DrawSerializedProperties()
        {
            serializedObject.Update();
            foreach (var property in serializedProperties)
            {
                if (property.name.Equals("m_Script", StringComparison.Ordinal))
                {
                    using (new EditorGUI.DisabledScope(disabled: true)) 
                        EditorGUILayout.PropertyField(property);

                }
                else
                {
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(property, includeChildren: true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (!dict.TryGetValue(property.name, out var methods)) continue;
                        foreach (var method in methods)
                            method.Invoke(target, null);
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

    }


    


}
