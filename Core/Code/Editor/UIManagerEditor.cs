using UnityEditor;

namespace Bridge.Core.UI.Manager.Editor
{
    public class UIManagerEditor : UnityEditor.Editor
    {
        [MenuItem("3ridge/Create/UI Manager")]
        private static void CreateUIManager()
        {
            var uiManager = new UnityEngine.GameObject("_3ridge UI Manager");
            uiManager.AddComponent<UIManager>();

            if(Selection.activeGameObject != null) uiManager.transform.SetParent(Selection.activeGameObject.transform);

            UnityEngine.Debug.Log("<color=white>-->></color> <color=green> Success </color>:<color=white> A UI manager has been created successfully.</color>");
        }

        [MenuItem("3ridge/Create/UI Manager", true)]
        private static bool CanCreateUIManager()
        {
            return FindObjectOfType<UIManager>() == null;
        }
    }
}