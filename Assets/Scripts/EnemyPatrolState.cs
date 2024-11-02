using UnityEngine;

namespace Mechadroids {
    public class EnemyPatrolState : EntityState {
        private readonly EnemyReference enemyReference;
        private int currentPatrolIndex;
        private Transform playerTransform;

        public EnemyPatrolState(IEntityHandler entityHandler, EnemyReference enemyReference) : base(entityHandler) {
            this.enemyReference = enemyReference;
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void Enter() {
            enemyReference.navMeshAgent.isStopped = false;
            SetNextPatrolDestination();
            // Optionally set patrol animation
        }

        public override void HandleInput() {
            // AI decision making
        }

        public override void LogicUpdate() {
            if(enemyReference.navMeshAgent.remainingDistance <= enemyReference.navMeshAgent.stoppingDistance) {
                SetNextPatrolDestination();
            }

            if(IsPlayerInDetectionRange()) {
                TransitionToAttackState();
            }
        }

        public override void PhysicsUpdate() {
            // No physics updates needed for patrol
        }

        public override void Exit() {
            // Cleanup if necessary
        }

        private void SetNextPatrolDestination() {
            if(enemyReference.patrolPoints.Length == 0) return;
            enemyReference.navMeshAgent.destination = enemyReference.patrolPoints[currentPatrolIndex].position;
            currentPatrolIndex = (currentPatrolIndex + 1) % enemyReference.patrolPoints.Length;
        }

        private bool IsPlayerInDetectionRange() {
            float distance = Vector3.Distance(enemyReference.transform.position, playerTransform.position);
            return distance <= enemyReference.detectionRadius;
        }

        private void TransitionToAttackState() {
            Exit();
            entityHandler.EntityState = new EnemyAttackState(entityHandler, enemyReference, playerTransform);
            entityHandler.EntityState.Enter();
        }
    }

}
