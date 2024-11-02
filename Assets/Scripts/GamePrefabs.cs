using UnityEngine;

namespace Mechadroids {
    [CreateAssetMenu(menuName = "Mechadroids/Settings", fileName = "GamePrefabs", order = 0)]
    public class GamePrefabs : ScriptableObject {
        public PlayerReference playerReferencePrefab;
        public HitIndicator hitIndicatorPrefab;
        public EnemyReference enemyReferencePrefab;
    }
}
