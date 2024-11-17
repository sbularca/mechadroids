using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Mechadroids.Editor {
    public static class EditorToolsMenu {
        [MenuItem("Mechadroids/Load Scenes")]
        private static void LoadScenes() {
            Loader loader = Object.FindFirstObjectByType<Loader>();
            foreach(string scene in loader.scenesToLoadInOrder) {
                if(!SceneManager.GetSceneByName(scene).isLoaded) {
                    EditorSceneManager.OpenScene($"Assets/Scenes/{scene}.unity", OpenSceneMode.Additive);
                }
            }
        }
    }
}
