using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Mechadroids.Editor {
    public static class EditorToolsMenu {
        [MenuItem("Mechadroids/Load Scenes")]
        private static void LoadScenes() {
            string[] scenePaths = {
                "Assets/Scenes/Entities.unity",
                "Assets/Scenes/Level.unity",
            };

            foreach (string scenePath in scenePaths) {
                Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                if (scene.IsValid()) {
                    Debug.Log($"Loaded scene: {scenePath}");
                } else {
                    Debug.LogError($"Failed to load scene: {scenePath}");
                }
            }
        }
    }
}
