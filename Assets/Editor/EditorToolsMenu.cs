using UnityEditor;
using UnityEditor.SceneManagement;

namespace Mechadroids.Editor {
    public static class EditorToolsMenu {
        static readonly string[] scenesToLoadInOrder = new[] {
            "Boot",
            "Entities",
            "Level"
        };

        [MenuItem("Mechadroids/Load Scenes")]
        private static void LoadScenes() {
            string path = "Assets/Scenes";
            foreach(string scene in scenesToLoadInOrder) {
                EditorSceneManager.OpenScene($"{path}/{scene}.unity", OpenSceneMode.Additive);
            }
        }
    }
}
