using Mechadroids.UI;
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
        private DebugMenuHandler debugMenuHandler;
        private UIPrefabs uiPrefabs;

        public void Initialize() {
            // Load resources
            playerPrefabs = Resources.Load<PlayerPrefabs>("PlayerPrefabs");
            aISettings = Resources.Load<AISettings>("AISettings");
            uiPrefabs = Resources.Load<UIPrefabs>("UIPrefabs");

            // Initialize systems
            inputHandler = new InputHandler();
            inputHandler.Initialize();

            playerEntityHandler = new PlayerEntityHandler(playerPrefabs, inputHandler, playerStartPosition, followCamera);
            playerEntityHandler.Initialize();

            aiEntitiesHandler = new AIEntitiesHandler(aISettings, aiParentTransform);
            aiEntitiesHandler.Initialize();

            debugMenuHandler = new DebugMenuHandler(uiPrefabs, inputHandler);
            debugMenuHandler.Initialize();

            initialized = true;
        }

        public void Update() {
            if (!initialized) {
                return;
            }
            playerEntityHandler.Tick();
            aiEntitiesHandler.Tick();
            debugMenuHandler.Tick();
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
            debugMenuHandler.Dispose();
        }
    }
}
