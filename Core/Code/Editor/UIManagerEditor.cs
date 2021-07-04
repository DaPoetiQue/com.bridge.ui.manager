using UnityEditor;

namespace Bridge.Core.UI.Manager
{
    public class UIManagerEditor : UnityEditor.Editor
    {
        [MenuItem("Bridge/Create/UI Manager")]
        private static void CreateUIManager()
        {
            var uiManager = new UnityEngine.GameObject("_UI Manager");
            uiManager.AddComponent<UIManager>();

            if(Selection.activeGameObject != null) uiManager.transform.SetParent(Selection.activeGameObject.transform);

            UnityEngine.Debug.Log("<color=white>-->></color> <color=green> Success </color>:<color=white> A UI manager has been created successfully.</color>");
        }

        [MenuItem("Bridge/Create/UI Manager", true)]
        private static bool CanCreateUIManager()
        {
            return FindObjectOfType<UIManager>() == null;
        }
    }
}