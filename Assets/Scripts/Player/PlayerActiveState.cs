using UnityEngine;

namespace Mechadroids {
    public class PlayerActiveState : IEntityState {
        private readonly InputHandler inputHandler;
        private readonly PlayerReference playerReference;
        private readonly HitIndicator hitIndicatorInstance;

        private float currentSpeed;
        private float turretAngle = 0f;
        private float barrelAngle = 0f;

        public PlayerActiveState(
            IEntityHandler entityHandler,
            InputHandler inputHandler,
            PlayerReference playerReference,
            HitIndicator hitIndicatorInstance) {
            this.inputHandler = inputHandler;
            this.playerReference = playerReference;
            this.hitIndicatorInstance = hitIndicatorInstance;
        }

        public void Enter() {
            // Any initialization when entering the state
        }

        public void LogicUpdate() {
            //handle player active state functionality
        }

        public void PhysicsUpdate() {
            // Implement physics update if needed
        }

        public void Exit() {
            // Clean up when exiting the state
        }
    }
}
