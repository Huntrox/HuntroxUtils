﻿using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using HuntroxGames.Utils.UI;

namespace HuntroxGames.Utils
{
    [CustomPropertyDrawer(typeof(UIOptionsScrollBar.OptionDataList), true)]
    class UIOptionListDrawer : PropertyDrawer
    {
        private ReorderableList m_ReorderableList;

        private void Init(SerializedProperty property)
        {
            if (m_ReorderableList != null)
                return;
            SerializedProperty array = property.FindPropertyRelative("m_Options");
            m_ReorderableList = new ReorderableList(property.serializedObject, array);
            m_ReorderableList.drawElementCallback = DrawOptionData;
            m_ReorderableList.drawHeaderCallback = DrawHeader;
            m_ReorderableList.elementHeight += 16;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);

            m_ReorderableList.DoList(position);
        }

        private void DrawHeader(Rect rect)
        {
            GUI.Label(rect, "Options");
        }

        private void DrawOptionData(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty itemData = m_ReorderableList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty itemText = itemData.FindPropertyRelative("m_Text");

            RectOffset offset = new RectOffset(0, 0, -1, -3);
            rect = offset.Add(rect);
            rect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(rect, itemText, GUIContent.none);
            rect.y += EditorGUIUtility.singleLineHeight;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);

            return m_ReorderableList.GetHeight();
        }
    }
}