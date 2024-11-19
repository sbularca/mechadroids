using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mechadroids {
    /// <summary>
    /// Project bootstrapper monobehaviour
    /// </summary>
    public class GameLoader : MonoBehaviour {
        public string[] scenesToLoadInOrder;
        private IEnumerator Start() {
            // first we load all scenes
            foreach(var scene in scenesToLoadInOrder) {
                yield return StartCoroutine(LoadSceneAdditiveIfNotLoaded(scene));
            }

            // We set the first scene specified in the array as the active scene by convention
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesToLoadInOrder[0]));

            // ...then we initialize the entrypoint and consecutively the game
            Entrypoint entrypoint = FindFirstObjectByType<Entrypoint>();
            entrypoint.Initialize();
        }

        private IEnumerator LoadSceneAdditiveIfNotLoaded(string sceneName) {
            Scene scene = SceneManager.GetSceneByName(sceneName);

            if(!scene.isLoaded) {
                yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                Debug.Log($"Scene '{sceneName}' was not loaded and has now been loaded additively.");
            }
            else {
                Debug.Log($"Scene '{sceneName}' is already loaded.");
            }
        }
    }
}
