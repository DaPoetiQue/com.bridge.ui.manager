using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bridge.Core.Debug;
using Bridge.Core.Events;

namespace Bridge.Core.UI.Manager
{
    public class SelectableUIHandler : MonoDebug
    {
       #region Components

       [SerializeField] 
       private List<SelectableUIData.Selectable> selectableUI = new List<SelectableUIData.Selectable>();

       #endregion

       #region Unity Defaults

       private void Start()
       {
           Init();
       }

       #endregion
       
       #region Main

       private void Init()
       {
           if(selectableUI.Count <= 0)
           {
               Log(LogData.LogLevel.Error, this, "No selectable UI found.");
               return;
           }

           CreateSelectableUI();
       }

       private void CreateSelectableUI()
       {
            foreach(SelectableUIData.Selectable selectable in selectableUI)
           {
               if(!selectable.button)
               {
                    Log(LogData.LogLevel.Error, this, $"Selectable UI button not found for - {selectable.tagName}. Assign button component to selectable at index {selectableUI.IndexOf(selectable)}.");
                    return;
               }

                selectable.button.onClick.AddListener(() => OnSelection(selectable.selectionType, selectable.selectionView));
           }
       }

       private void OnSelection(AppEventsData.SelectionType selection, AppEventsData.AppViewState selectionView)
       {
           EventsManager.Instance.OnSelectableUIEvent.Invoke(selection, selectionView);

           Log(LogData.LogLevel.Debug, this, $"Selected - {selection.ToString()}. Load app view : {selectionView.ToString()}");
       }

       #endregion
    }
}
