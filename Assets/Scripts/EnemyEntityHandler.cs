using UnityEngine;

namespace Mechadroids {
    public class EnemyEntityHandler : IEntityHandler {
        private readonly GamePrefabs gamePrefabs;
        private readonly Transform enemySpawnPosition;

        private EnemyReference enemyReference;

        public EntityState EntityState { get; set; }

        public EnemyEntityHandler(GamePrefabs gamePrefabs, Transform enemySpawnPosition) {
            this.gamePrefabs = gamePrefabs;
            this.enemySpawnPosition = enemySpawnPosition;
        }

        public void Initialize() {
            enemyReference = Object.Instantiate(gamePrefabs.enemyReferencePrefab);
            enemyReference.transform.position = enemySpawnPosition.position;

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
            if(enemyReference != null) {
                Object.Destroy(enemyReference.gameObject);
                enemyReference = null;
            }
        }
    }
}
