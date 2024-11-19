using UnityEngine;

namespace Mechadroids {

    /// <summary>
    /// Class that handles the states for each enemy behaviour. Each enemy will have its own entity handler
    /// </summary>
    public class EnemyEntityHandler : IEntityHandler {
        private readonly EnemySettings enemySettings;
        private readonly Transform parentHolder;
        private EnemyReference enemyReference;

        public IEntityState EntityState { get; set; }

        public EnemyEntityHandler(EnemySettings enemySettings, Transform parentHolder) {
            this.enemySettings = enemySettings;
            this.parentHolder = parentHolder;
        }

        public void Initialize() {
            enemyReference = Object.Instantiate(enemySettings.enemy.enemyReferencePrefab, parentHolder);
            enemyReference.transform.position = enemySettings.routeSettings.routePoints[0];

            // Initialize the default state (Idle State)
            EntityState = new EnemyIdleState(this, enemyReference);
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
            if (enemyReference != null) {
                Object.Destroy(enemyReference.gameObject);
                enemyReference = null;
            }
        }
    }

    // functionality for patrolling

        // private void SetNextPatrolDestination() {
        // if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
        // currentPatrolIndex %= enemyReference.enemySettings.routeSettings.routePoints.Length;
        // }
        //
        // private void MoveTowardsPatrolPoint() {
        //     if(enemyReference.enemySettings.routeSettings.routePoints.Length == 0) return;
        //
        //     Vector3 targetPoint = enemyReference.enemySettings.routeSettings.routePoints[currentPatrolIndex];
        //     targetPoint.y = enemyReference.transform.position.y;
        //     Vector3 direction = (targetPoint - enemyReference.transform.position).normalized;
        //
        //     // Move towards the target point
        //     enemyReference.transform.position += direction * enemyReference.enemySettings.enemy.patrolSpeed * Time.deltaTime;
        //
        //     // Rotate towards the target point
        //     RotateTowards(direction);
        //
        //     // Check if the enemy has reached the patrol point
        //     if(Vector3.Distance(enemyReference.transform.position, targetPoint) <= 0.1f) {
        //         currentPatrolIndex = (currentPatrolIndex + 1) % enemyReference.enemySettings.routeSettings.routePoints.Length;
        //         SetNextPatrolDestination();
        //     }
        // }
        //
        // private void RotateTowards(Vector3 direction) {
        //     if(direction.magnitude == 0) return;
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     enemyReference.transform.rotation = Quaternion.Slerp(
        //         enemyReference.transform.rotation,
        //         targetRotation,
        //         enemyReference.enemySettings.enemy.patrolRotationSpeed * Time.deltaTime
        //     );
        // }
        //
        // private bool IsPlayerInDetectionRange() {
        //     if(playerTransform == null) return false;
        //     float distance = Vector3.Distance(enemyReference.transform.position, playerTransform.position);
        //     return distance <= enemyReference.enemySettings.enemy.detectionRadius;
        // }

}
