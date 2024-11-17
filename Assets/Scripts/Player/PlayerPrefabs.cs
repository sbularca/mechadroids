using UnityEngine;

namespace Mechadroids {
    [CreateAssetMenu(menuName = "Mechadroids/PlayerPrefabs", fileName = "PlayerPrefabs", order = 0)]
    public class PlayerPrefabs : ScriptableObject {
        public PlayerReference playerReferencePrefab;
        public HitIndicator hitIndicatorPrefab;
    }
}
