using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mechadroids {
    public class Entrypoint : MonoBehaviour {
        public Transform playerStartPosition;

        private GamePrefabs gamePrefabs;
        private InputHandler inputHandler;
        private PlayerEntityHandler playerEntityHandler;
        public void Start() {
            LoadSceneAdditiveIfNotLoaded("Level");

            gamePrefabs = Resources.Load<GamePrefabs>("GamePrefabs");

            inputHandler = new InputHandler();
            inputHandler.Initialize();

            playerEntityHandler = new PlayerEntityHandler(gamePrefabs, inputHandler, playerStartPosition);
            playerEntityHandler.Initialize();
        }

        void LoadSceneAdditiveIfNotLoaded(string sceneName) {
            Scene scene = SceneManager.GetSceneByName(sceneName);

            if(!scene.isLoaded) {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                Debug.Log($"Scene '{sceneName}' was not loaded and has now been loaded additively.");
            }
            else {
                Debug.Log($"Scene '{sceneName}' is already loaded.");
            }
        }

        public void Update() {
            playerEntityHandler.Tick();
        }

        public void OnDestroy() {
            inputHandler.Dispose();
            playerEntityHandler.Dispose();
        }
    }
}
