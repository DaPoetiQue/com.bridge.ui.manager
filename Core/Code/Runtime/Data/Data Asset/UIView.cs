using UnityEngine;
using Bridge.Core.Events;

namespace Bridge.Core.UI.Manager
{
    [UnityEngine.CreateAssetMenu]
    public class UIView : ScriptableObject
    {
        #region Components
        public string nameTag;
        public GameObject prefab;
        public AppEventsData.AppViewState viewState;
        public bool enableOnInit;
        public bool isGlobal;

        #endregion
    }
}