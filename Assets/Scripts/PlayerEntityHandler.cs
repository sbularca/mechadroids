using UnityEngine;

namespace Mechadroids {
    public class PlayerEntityHandler : IEntityHandler {
        private readonly PlayerPrefabs playerPrefabs;
        private readonly InputHandler inputHandler;
        private readonly Transform playerStartPosition;

        private PlayerReference playerReference;
        private HitIndicator hitIndicatorInstance;

        public EntityState EntityState { get; set; }

        public PlayerEntityHandler(PlayerPrefabs playerPrefabs, InputHandler inputHandler, Transform playerStartPosition) {
            this.playerPrefabs = playerPrefabs;
            this.inputHandler = inputHandler;
            this.playerStartPosition = playerStartPosition;
        }

        public void Initialize() {
            inputHandler.SetCursorState(false, CursorLockMode.Locked);

            playerReference = Object.Instantiate(playerPrefabs.playerReferencePrefab);
            playerReference.transform.position = playerStartPosition.position;

            hitIndicatorInstance = Object.Instantiate(playerPrefabs.hitIndicatorPrefab);
            hitIndicatorInstance.gameObject.SetActive(false);

            // Initialize the default state
            EntityState = new PlayerActiveState(this, inputHandler, playerReference, hitIndicatorInstance);
            EntityState.Enter();
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
