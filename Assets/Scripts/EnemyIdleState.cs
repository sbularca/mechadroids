using UnityEngine;

namespace Mechadroids {
    public class EnemyIdleState : EntityState {
        private readonly EnemyReference enemyReference;
        private float idleDuration = 2f;
        private float idleTimer;

        public EnemyIdleState(IEntityHandler entityHandler, EnemyReference enemyReference) : base(entityHandler) {
            this.enemyReference = enemyReference;
        }

        public override void Enter() {
            idleTimer = 0f;
            // Optionally set idle animation
        }

        public override void HandleInput() {
            // No input handling needed in idle state
        }

        public override void LogicUpdate() {
            idleTimer += Time.deltaTime;
            if(idleTimer >= idleDuration) {
                TransitionToPatrolState();
            }
        }

        public override void PhysicsUpdate() {
            // No physics updates needed in idle state
        }

        public override void Exit() {
            // Cleanup if necessary
        }

        private void TransitionToPatrolState() {
            Exit();
            entityHandler.EntityState = new EnemyPatrolState(entityHandler, enemyReference);
            entityHandler.EntityState.Enter();
        }
    }

}
