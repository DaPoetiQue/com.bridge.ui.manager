using System;
using UnityEngine;
using UnityEngine.UI;
using Bridge.Core.Events;

namespace Bridge.Core.UI.Manager
{
    public class SelectableUIData
    {

        #region Data

        [Serializable]
        public class Selectable
        {
            public string tagName;

            [Space(5)]
            public Button button;

            [Space(5)]
            public AppEventsData.SelectionType selectionType;

            [Space(5)]
            public AppEventsData.AppViewState selectionView;
        }

        #endregion
    }
}
