using UnityEngine;

namespace Mechadroids {
    public class EnemyAttackState : IEntityState {
        private readonly IEntityHandler entityHandler;
        private readonly EnemyReference enemyReference;
        private readonly Transform playerTransform;
        private float attackCooldown = 1f;
        private float lastAttackTime;

        public EnemyAttackState(IEntityHandler entityHandler, EnemyReference enemyReference, Transform playerTransform) {
            this.entityHandler = entityHandler;
            this.enemyReference = enemyReference;
            this.playerTransform = playerTransform;
        }

        public IEntityState EntityState { get; set; }
        public void Enter() {
            // Optionally set attack animation
        }

        public void LogicUpdate() {
            if(playerTransform == null) {
                TransitionToPatrolState();
                return;
            }

            float distanceToPlayer = Vector3.Distance(enemyReference.transform.position, playerTransform.position);

            if(distanceToPlayer > enemyReference.enemySettings.enemy.detectionRadius) {
                TransitionToPatrolState();
                return;
            }

            MoveTowardsPlayer();

            if(distanceToPlayer <= enemyReference.enemySettings.enemy.attackRange) {
                if(Time.time >= lastAttackTime + attackCooldown) {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
        }

        public void PhysicsUpdate() {
            // Implement if physics are involved
        }

        public void Exit() {
            // Cleanup if necessary
        }

        private void MoveTowardsPlayer() {
            Vector3 direction = (playerTransform.position - enemyReference.transform.position).normalized;

            // Move towards the player
            if(Vector3.Distance(enemyReference.transform.position, playerTransform.position) > enemyReference.enemySettings.enemy.maxDistanceFromPlayer) {
                enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.attackSpeed * Time.deltaTime;
            }

            // Rotate towards the player
            RotateTowards(direction);
        }

        private void RotateTowards(Vector3 direction) {
            if(direction.magnitude == 0) return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyReference.transform.rotation = Quaternion.Slerp(
                enemyReference.transform.rotation,
                targetRotation,
                enemyReference.enemySettings.enemy.attackRotationSpeed * Time.deltaTime
            );
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
