using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mechadroids {
    public class Entrypoint : MonoBehaviour {
        public Transform playerStartPosition;
        public CinemachineCamera followCamera;
        public Transform aiParentTransform;

        private PlayerPrefabs playerPrefabs;
        private InputHandler inputHandler;
        private PlayerEntityHandler playerEntityHandler;
        private AISettings aISettings;
        private AIEntitiesHandler aiEntitiesHandler;
        private bool initialized;

        public void Initialize() {
            // Load resources
            playerPrefabs = Resources.Load<PlayerPrefabs>("PlayerPrefabs");
            aISettings = Resources.Load<AISettings>("AISettings");

            // Initialize systems
            inputHandler = new InputHandler();
            inputHandler.Initialize();

            playerEntityHandler = new PlayerEntityHandler(playerPrefabs, inputHandler, playerStartPosition, followCamera);
            playerEntityHandler.Initialize();

            aiEntitiesHandler = new AIEntitiesHandler(aISettings, aiParentTransform);
            aiEntitiesHandler.Initialize();

            initialized = true;
        }

        public void Update() {
            if (!initialized) {
                return;
            }
            playerEntityHandler.Tick();
            aiEntitiesHandler.Tick();
        }

        public void FixedUpdate() {
            if (!initialized) {
                return;
            }
            playerEntityHandler.PhysicsTick();
            aiEntitiesHandler.PhysicsTick();
        }

        public void OnDestroy() {
            if (!initialized) {
                return;
            }
            inputHandler.Dispose();
            playerEntityHandler.Dispose();
            aiEntitiesHandler.Dispose();
        }
    }
}
