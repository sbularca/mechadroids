using UnityEngine;

namespace Mechadroids {
    public class EnemyEntityHandler : IEntityHandler {
        private readonly EnemySettings enemySettings;
        private EnemyReference enemyReference;

        public EntityState EntityState { get; set; }

        public EnemyEntityHandler(EnemySettings enemySettings) {
            this.enemySettings = enemySettings;
        }

        public void Initialize() {
            enemyReference = Object.Instantiate(enemySettings.enemy.enemyReferencePrefab); //should handle this with pooling
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

}
