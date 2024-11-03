using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mechadroids {
    public class Entrypoint : MonoBehaviour {
        public Transform playerStartPosition;
        public CinemachineCamera followCamera;

        private PlayerPrefabs playerPrefabs;
        private InputHandler inputHandler;
        private PlayerEntityHandler playerEntityHandler;
        private AISettings aISettings;
        private AIEntitiesHandler aiEntitiesHandler;

        public void Start() {
            LoadSceneAdditiveIfNotLoaded("Level");

            playerPrefabs = Resources.Load<PlayerPrefabs>("PlayerPrefabs");
            aISettings = Resources.Load<AISettings>("AISettings");

            inputHandler = new InputHandler();
            inputHandler.Initialize();

            playerEntityHandler = new PlayerEntityHandler(playerPrefabs, inputHandler, playerStartPosition, followCamera);
            playerEntityHandler.Initialize();

            aiEntitiesHandler = new AIEntitiesHandler(aISettings);
            aiEntitiesHandler.Initialize();
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
            aiEntitiesHandler.Tick();
        }

        public void FixedUpdate() {
            playerEntityHandler.PhysicsTick();
            aiEntitiesHandler.PhysicsTick();
        }

        public void OnDestroy() {
            inputHandler.Dispose();
            playerEntityHandler.Dispose();
            aiEntitiesHandler.Dispose();
        }
    }
}
