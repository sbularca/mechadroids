using Mechadroids.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace Mechadroids {
    public class PlayerEntityHandler : IEntityHandler {
        private readonly PlayerPrefabs playerPrefabs;
        private readonly InputHandler inputHandler;
        private readonly Transform playerStartPosition;
        private readonly CinemachineCamera followCamera;
        private readonly DebugMenuHandler debugMenuHandler;

        private PlayerReference playerReference;
        private HitIndicator hitIndicatorInstance;

        public IEntityState EntityState { get; set; }

        public PlayerEntityHandler(PlayerPrefabs playerPrefabs,
            InputHandler inputHandler,
            Transform playerStartPosition,
            CinemachineCamera followCamera,
            DebugMenuHandler debugMenuHandler) {
            this.playerPrefabs = playerPrefabs;
            this.inputHandler = inputHandler;
            this.playerStartPosition = playerStartPosition;
            this.followCamera = followCamera;
            this.debugMenuHandler = debugMenuHandler;
        }

        public void Initialize() {
            inputHandler.SetCursorState(false, CursorLockMode.Locked);

            playerReference = Object.Instantiate(playerPrefabs.playerReferencePrefab);
            playerReference.transform.position = playerStartPosition.position;
            followCamera.Follow = playerReference.transform;

            hitIndicatorInstance = Object.Instantiate(playerPrefabs.hitIndicatorPrefab);
            hitIndicatorInstance.gameObject.SetActive(false);
            EntityState = new PlayerActiveState(this, inputHandler, playerReference, hitIndicatorInstance);
            EntityState.Enter();

#if GAME_DEBUG
            InitializeDebugMenu();
#endif
        }

        private void InitializeDebugMenu() {
            debugMenuHandler.AddUIElement(UIElementType.Single, "MoveSpeed", new float [] { playerReference.playerSettings.moveSpeed }, (newValue) => {
                playerReference.playerSettings.moveSpeed = newValue[0];
            });

        }

        public void Tick() {
            EntityState.HandleInput();
            EntityState.LogicUpdate();
        }

        public void PhysicsTick() {
            EntityState.PhysicsUpdate();
        }

        public void LateTick() {
            // Implement if needed
        }

        public void Dispose() {
            inputHandler.Dispose();
            if (hitIndicatorInstance != null) {
                Object.Destroy(hitIndicatorInstance.gameObject);
                hitIndicatorInstance = null;
            }
        }
    }
}
