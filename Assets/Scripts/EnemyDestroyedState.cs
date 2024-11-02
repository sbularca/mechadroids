using UnityEngine;

namespace Mechadroids {
    public class EnemyDestroyedState : EntityState {
        private readonly EnemyReference enemyReference;

        public EnemyDestroyedState(IEntityHandler entityHandler, EnemyReference enemyReference) : base(entityHandler) {
            this.enemyReference = enemyReference;
        }

        public override void Enter() {
            enemyReference.navMeshAgent.isStopped = true;
            // Play destruction effects or animations
            Object.Destroy(enemyReference.gameObject);
        }

        public override void HandleInput() {
            // No input needed when destroyed
        }

        public override void LogicUpdate() {
            // No logic needed when destroyed
        }

        public override void PhysicsUpdate() {
            // No physics updates needed when destroyed
        }

        public override void Exit() {
            // No exit actions needed when destroyed
        }
    }

}
