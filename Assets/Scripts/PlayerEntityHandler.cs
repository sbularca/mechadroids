using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Mechadroids {
    using UnityEngine;

    public class PlayerEntityHandler : IEntityHandler {
        private readonly GamePrefabs gamePrefabs;
        private readonly InputHandler inputHandler;
        private readonly Transform playerStartPosition;

        private PlayerReference playerReference;
        private HitIndicator hitIndicatorInstance;

        public EntityState EntityState { get; set; }

        public PlayerEntityHandler(GamePrefabs gamePrefabs, InputHandler inputHandler, Transform playerStartPosition) {
            this.gamePrefabs = gamePrefabs;
            this.inputHandler = inputHandler;
            this.playerStartPosition = playerStartPosition;
        }

        public void Initialize() {
            inputHandler.SetCursorState(false, CursorLockMode.Locked);

            playerReference = Object.Instantiate(gamePrefabs.playerReferencePrefab);
            playerReference.transform.position = playerStartPosition.position;

            hitIndicatorInstance = Object.Instantiate(gamePrefabs.hitIndicatorPrefab);
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
