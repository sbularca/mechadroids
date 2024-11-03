using System.Collections.Generic;

namespace Mechadroids {
    public class AIEntitiesHandler {
        private readonly AISettings aiSettings;

        public Dictionary<int, EnemyEntityHandler> EnemyEntityHandlers { get; } = new();

        public AIEntitiesHandler(AISettings aiSettings) {
            this.aiSettings = aiSettings;
        }

        public void Initialize() {
            foreach(EnemyGroup enemy in aiSettings.enemiesToSpawn) {
                for(int i = 0; i < enemy.enemyCount; i++) {
                    EnemyEntityHandler enemyEntityHandler = new(enemy.enemySettings);
                    enemyEntityHandler.Initialize();
                    EnemyEntityHandlers.TryAdd(i, enemyEntityHandler);
                }
            }
        }

        public void Tick() {
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.Tick();
            }
        }

        public void PhysicsTick() {
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.PhysicsTick();
            }
        }

        public void Dispose() {
            foreach(KeyValuePair<int, EnemyEntityHandler> enemyEntityHandler in EnemyEntityHandlers) {
                enemyEntityHandler.Value.Dispose();
            }
            EnemyEntityHandlers.Clear();
        }
    }
}
