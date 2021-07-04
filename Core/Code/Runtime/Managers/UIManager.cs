using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bridge.Core.Debug;
using Bridge.Core.Events;

namespace Bridge.Core.UI.Manager
{
    public class UIManager : MonoDebug
    {
        #region Instances
        private static UIManager instance;

        public static UIManager Instance
        {
            get
            {
                if(instance == null) instance = FindObjectOfType<UIManager>();

                return instance;
            }
        }

        #endregion

        #region  Components

        [UnityEngine.SerializeField]
        private List<UIView> uiViewList = new List<UIView>();

        private List<UIView> generatedUIViewList = new List<UIView>();

        private Transform loadedUIViewContent;

        #endregion 

        #region Unity Functions

        private void Start()
        {
            Init();
        }

        #endregion

        #region Main

        public void Init()
        {
            CreateUIViews();

            EventsManager.Instance.OnAppViewChangedEvent.AddListener(OnAppViewStateChanged);
        }

        private void CreateUIViews()
        {
            if(uiViewList.Count <= 0)
            {
                Log(LogData.LogLevel.Error, this, "No UIView(s) found - Create and assign _UIManager's UI view list assets.");
                return;
            }

            if(!loadedUIViewContent) 
                loadedUIViewContent = new GameObject("_UI View Content").transform;

            if(uiViewList.Count > 0 && loadedUIViewContent)
            {
                foreach (UIView view in uiViewList)
                {
                    if(generatedUIViewList.Contains(view))
                    {
                         Log(LogData.LogLevel.Warning, this, $"UIView for : {view.nameTag}, has already been generated and add to the list at index - {uiViewList.IndexOf(view)}.");
                         return;
                    }

                    if(!generatedUIViewList.Contains(view))
                    {
                        if(view.prefab == null) 
                        {
                            Log(LogData.LogLevel.Error, this, $"UIView prefab for : {view.nameTag}, of view list not found - Assign UIManager's UIView prefab at index - {uiViewList.IndexOf(view)}.");
                            return;
                        }

                        if(view.prefab != null)
                        {
                            GameObject prefab = Instantiate<GameObject>(view.prefab, loadedUIViewContent);
                            prefab.name = view.nameTag;
                            prefab.SetActive(view.enableOnInit);

                            UIView generatedView = ScriptableObject.CreateInstance<UIView>();
                            generatedView.name = view.nameTag;
                            generatedView.nameTag = view.nameTag;
                            generatedView.prefab = prefab;
                            generatedView.viewState = view.viewState;
                            generatedView.enableOnInit = view.enableOnInit;
                            generatedView.isGlobal  = view.isGlobal;
                            
                            generatedUIViewList.Add(generatedView);

                            Log(LogData.LogLevel.Debug, this, $"A new UIView has been created for - {view.nameTag}");
                        }
                    }
                }
            }
        }

        private IEnumerator UpdateAppView(AppEventsData.AppViewState appView)
        {
            yield return new WaitForSeconds(0.15f);

            if(generatedUIViewList.Count > 0)
            {
                foreach(UIView view in generatedUIViewList)
                {
                    if(view.viewState != appView) view.prefab.SetActive(false);
                    if(view.viewState == appView) view.prefab.SetActive(true);
                }
            }

            yield return null;

            StopCoroutine("UpdateAppView");
        }

        #endregion

        #region Events

        public void OnAppViewStateChanged(AppEventsData.AppViewState appView)
        {
            Log(LogData.LogLevel.Debug, this, $"View changed - UI views switched to {appView.ToString()}.");

           if(generatedUIViewList.Count <= 0) 
            {
                Log(LogData.LogLevel.Error, this, $"No generated UIView content.");
                return;
            }

            StartCoroutine(UpdateAppView(appView));
        }

        #endregion
    }
}