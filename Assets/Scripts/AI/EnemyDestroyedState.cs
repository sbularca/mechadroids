using UnityEngine;

namespace Mechadroids {
    public class EnemyDestroyedState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;

        public EnemyDestroyedState(IEntityHandler entityHandler, EnemyReference enemyReference) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
        }

        public void Enter() {
            // Play destruction effects or animations
            Object.Destroy(enemyReference.gameObject);
        }

        public void LogicUpdate() {
            // No logic needed when destroyed
        }

        public void PhysicsUpdate() {
            // No physics updates needed when destroyed
        }

        public void Exit() {
            // No exit actions needed when destroyed
        }
    }
}
