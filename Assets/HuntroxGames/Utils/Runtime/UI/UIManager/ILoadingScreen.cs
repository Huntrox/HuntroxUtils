using System;
using System.Collections;
using UnityEngine;

namespace HuntroxGames.Utils.UI
{
    public interface ILoadingScreen
    {
        bool IsLoading  { get; set; }
        float Progress { get; }
        Transform transform { get; }
        void RestLoadingScreen();
        void OnShow(Action callback);
        IEnumerator OnHide(Action callback);
        void UpdateProgress(float progress);
    }
}
