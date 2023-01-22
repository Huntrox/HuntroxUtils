using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HuntroxGames.Utils.UI
{
    public class PopupsManager : MonoBehaviour
    {

        [SerializeField] private Image bgImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private CanvasGroup boxGroup;
        [SerializeField] private Button okBtn;
        [SerializeField] private Button confirmBtn;
        [SerializeField] private Button cancelBtn;

        
        //[SerializeField] private TYPE type;

        private readonly Stack<PopupRequest> requests = new Stack<PopupRequest>();
        private PopupRequest activePopup = null;


        private void Update()
        {
            if (requests.Count > 1 && activePopup != null)
            {
                //push next popup
                var nextPopup = requests.Pop();
                StartCoroutine(Push(nextPopup));
            }
            
        }

        private IEnumerator Push(PopupRequest request)
        {
            activePopup = request;
            
            yield return null;
        }

        public PopupRequest ShowModal(string message, UnityAction callback, string confirmationBtnText = "Ok")
        {
            var request = new PopupRequest
            {
                title = "Dialogue Box",
                message = message,
                confirmAction = callback,
                cancelAction = null,
                confirmationBtnText = confirmationBtnText,
                type = PopupType.Dialogue
            };
            requests.Push(request);
            return request;
        }

        public PopupRequest ShowConfirmationBox(string message, UnityAction onConfirm, UnityAction onCancel,
            string confirmationBtnText = "Yes", string cancelBtnText = "Cancel")
        {
            var request = new PopupRequest
            {
                title = "Confirmation Box",
                message = message,
                confirmAction = onConfirm,
                cancelAction = onCancel,
                confirmationBtnText = confirmationBtnText,
                cancelBtnText = cancelBtnText,
                type = PopupType.Confirmation
            };

            
            requests.Push(request);
            return request;
        }

        private void Clear()
        {

            titleText.text = "";
            contentText.text = "";
            
            okBtn.onClick.RemoveAllListeners();
            confirmBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.RemoveAllListeners();
        }
    }

    public class PopupRequest
    {
        public string title;
        public string message;
        public string confirmationBtnText = "Ok";
        public string cancelBtnText = "Cancel";
        public UnityAction confirmAction;
        public UnityAction cancelAction;
        public PopupType type;

    }

    public enum PopupType
    {
        Dialogue,
        Confirmation,
        //Options

    }


}
