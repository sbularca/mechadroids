using UnityEngine;

namespace Mechadroids {
    public class EnemyAttackState : EntityState {
        private readonly EnemyReference enemyReference;
        private readonly Transform playerTransform;
        private float attackRange = 2f;
        private float attackCooldown = 1f;
        private float lastAttackTime;

        public EnemyAttackState(IEntityHandler entityHandler, EnemyReference enemyReference, Transform playerTransform) : base(entityHandler) {
            this.enemyReference = enemyReference;
            this.playerTransform = playerTransform;
        }

        public override void Enter() {
            // Optionally set attack animation
        }

        public override void HandleInput() {
            // AI decision making
        }

        public override void LogicUpdate() {
            float distanceToPlayer = Vector3.Distance(enemyReference.transform.position, playerTransform.position);

            if(distanceToPlayer > enemyReference.detectionRadius) {
                TransitionToPatrolState();
                return;
            }

            enemyReference.navMeshAgent.destination = playerTransform.position;

            if(distanceToPlayer <= attackRange) {
                enemyReference.navMeshAgent.isStopped = true;
                if(Time.time >= lastAttackTime + attackCooldown) {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
            else {
                enemyReference.navMeshAgent.isStopped = false;
            }
        }

        public override void PhysicsUpdate() {
            // No physics updates needed for attack
        }

        public override void Exit() {
            // Cleanup if necessary
        }

        private void AttackPlayer() {
            // Implement attack logic (e.g., reduce player's health)
            Debug.Log("Enemy attacks the player!");
        }

        private void TransitionToPatrolState() {
            Exit();
            entityHandler.EntityState = new EnemyPatrolState(entityHandler, enemyReference);
            entityHandler.EntityState.Enter();
        }
    }

}
