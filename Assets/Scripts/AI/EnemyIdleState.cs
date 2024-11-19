using UnityEngine;

namespace Mechadroids {
    public class EnemyIdleState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private float idleDuration = 2f;
        private float idleTimer;

        public EnemyIdleState(IEntityHandler entityHandler, EnemyReference enemyReference) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
        }

        public void Enter() {
            idleTimer = 0f;
            // Optionally set idle animation
        }

        public void LogicUpdate() {
            idleTimer += Time.deltaTime;
            if(idleTimer >= idleDuration) {
                TransitionToPatrolState();
            }
        }

        public void PhysicsUpdate() {
            // No physics updates needed in idle state
        }

        public void Exit() {
            // Cleanup if necessary
        }

        private void TransitionToPatrolState() {
            Exit();
            // entityHandler.EntityState = new EnemyPatrolState(entityHandler, enemyReference);
            // entityHandler.EntityState.Enter();
        }
    }

}
