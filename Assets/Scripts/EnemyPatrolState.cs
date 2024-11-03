using UnityEngine;

namespace Mechadroids {
    public class EnemyPatrolState : EntityState {
        private readonly EnemyReference enemyReference;
        private int currentPatrolIndex;
        private Transform playerTransform;

        public EnemyPatrolState(IEntityHandler entityHandler, EnemyReference enemyReference) : base(entityHandler) {
            this.enemyReference = enemyReference;
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        public override void Enter() {
            // Optionally set patrol animation
            SetNextPatrolDestination();
        }

        public override void HandleInput() {
            // AI decision making
        }

        public override void LogicUpdate() {
            MoveTowardsPatrolPoint();

            if(IsPlayerInDetectionRange()) {
                TransitionToAttackState();
            }
        }

        public override void PhysicsUpdate() {
            // Implement if physics are involved
        }

        public override void Exit() {
            // Cleanup if necessary
        }

        private void SetNextPatrolDestination() {
            if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
            currentPatrolIndex %= enemyReference.enemySettings.routeSettings.routePoints.Length;
        }

        private void MoveTowardsPatrolPoint() {
            if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;

            Vector3 targetPoint = enemyReference.enemySettings.routeSettings.routePoints[currentPatrolIndex];
            Vector3 direction = (targetPoint - enemyReference.transform.position).normalized;

            // Move towards the target point
            enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.patrolSpeed * Time.deltaTime;

            // Rotate towards the target point
            RotateTowards(direction);

            // Check if the enemy has reached the patrol point
            if(Vector3.Distance(enemyReference.transform.position, targetPoint) <= 0.1f) {
                currentPatrolIndex = (currentPatrolIndex + 1) % enemyReference.enemySettings.routeSettings.routePoints.Length;
                SetNextPatrolDestination();
            }
        }

        private void RotateTowards(Vector3 direction) {
            if(direction.magnitude == 0) return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyReference.transform.rotation = Quaternion.Slerp(
                enemyReference.transform.rotation,
                targetRotation,
                enemyReference.enemySettings.enemy.patrolRotationSpeed * Time.deltaTime
            );
        }

        private bool IsPlayerInDetectionRange() {
            if(playerTransform == null) return false;
            float distance = Vector3.Distance(enemyReference.transform.position, playerTransform.position);
            return distance <= enemyReference.enemySettings.enemy.detectionRadius;
        }

        private void TransitionToAttackState() {
            Exit();
            entityHandler.EntityState = new EnemyAttackState(entityHandler, enemyReference, playerTransform);
            entityHandler.EntityState.Enter();
        }
    }
}
