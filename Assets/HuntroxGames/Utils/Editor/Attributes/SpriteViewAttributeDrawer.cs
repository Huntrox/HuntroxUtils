using System.Collections;
using System.Collections.Generic;
using HuntroxGames.Utils;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SpriteViewAttribute))]
public class SpriteViewAttributeDrawer : PropertyDrawer
{

    private SpriteViewAttribute att;
    
    private const float OFFSET = 5f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        att ??= (SpriteViewAttribute)attribute;
        position.y -= (att.size / 2) - OFFSET;
        EditorGUI.LabelField(position, label);
        //position.width -= 16;
        position.y += att.size/2;
        position.x += position.width - att.size;
        position.width = att.size;
        position.height = att.size;
        EditorGUI.BeginChangeCheck();
        property.objectReferenceValue = (Sprite)EditorGUI.ObjectField(position, property.objectReferenceValue,typeof(Sprite),false);
        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        att ??= (SpriteViewAttribute)attribute;
        return  att.size + OFFSET;
    }
}
