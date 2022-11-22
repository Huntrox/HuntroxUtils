using UnityEngine;

namespace HuntroxGames.Utils
{
    public class VarExample : MonoBehaviour
    {

        [SerializeField,OnValueChanged("OnIntChanged")] private int intField;
        [SerializeField,OnValueChanged("OnFloatChanged")] private float floatField;
        [SerializeField,OnValueChanged("OnBoolChanged")] private bool boolField;
        [SerializeField,OnValueChanged(nameof(OnStringChanged))] private string stringField;
        [SerializeField] private Vector2 vector2Field;
        [SerializeField] private GameObject gameObjectField;


        

        private void OnIntChanged()
            => Debug.Log("Int Field Has Changed");
        private void OnFloatChanged()
            => Debug.Log("Float Field Has Changed");
        private void OnBoolChanged()
            => Debug.Log("Bool Field Has Changed");
        private void OnStringChanged()
            => Debug.Log("String Field Has Changed");


        [Button]
        private void ButtonMethodWithParam(string param)
        {
            
        }
        
        [Button]
        private void TestButtonAttribute()
            => Debug.Log("TestButtonAttribute Clicked");

        [Button("MY BUTTON")]
        private void TestButtonAttributeWithName()
            => Debug.Log("MY BUTTON Clicked");
    }
}
