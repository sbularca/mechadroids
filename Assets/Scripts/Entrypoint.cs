using Mechadroids.UI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Mechadroids {
    public class Entrypoint : MonoBehaviour {

        public AssetReferenceT<UIPrefabs> uiPrefabAssetRef;
        public AssetReferenceT<AISettings> aiSettingsAssetRef;
        public AssetReferenceT<PlayerPrefabs> playerPrefabsAssetRef;

        public Transform playerStartPosition;
        public CinemachineCamera followCamera;
        public Transform aiParentTransform;

        private InputHandler inputHandler;
        private PlayerEntityHandler playerEntityHandler;
        private AIEntitiesHandler aiEntitiesHandler;
        private bool initialized;
        private DebugMenuHandler debugMenuHandler;
        private PlayerPrefabs playerPrefabs;
        private AISettings aiSettings;
        private UIPrefabs uiPrefabs;

        public void Initialize() {
            // Load resources
            playerPrefabs = playerPrefabsAssetRef.LoadAssetAsync().WaitForCompletion();
            aiSettings = aiSettingsAssetRef.LoadAssetAsync().WaitForCompletion();
            uiPrefabs = uiPrefabAssetRef.LoadAssetAsync().WaitForCompletion();

            // Initialize systems
            inputHandler = new InputHandler();
            inputHandler.Initialize();
#if GAME_DEBUG
            debugMenuHandler = new DebugMenuHandler(uiPrefabs, inputHandler);
            debugMenuHandler.Initialize();
#endif
            playerEntityHandler = new PlayerEntityHandler(playerPrefabs, inputHandler, playerStartPosition, followCamera, debugMenuHandler);
            playerEntityHandler.Initialize();

            aiEntitiesHandler = new AIEntitiesHandler(aiSettings, aiParentTransform);
            aiEntitiesHandler.Initialize();

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
