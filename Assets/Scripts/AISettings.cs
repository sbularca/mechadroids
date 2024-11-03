using System;
using UnityEngine;

namespace Mechadroids {

    public enum EnemyType {
        None,
        Tank,
        Walker,
        Wheeled,
        Fixed
    }

    [CreateAssetMenu(menuName = "Mechadroids/AISettings", fileName = "AISettings", order = 0)]
    public class AISettings : ScriptableObject {
        public EnemyGroup[] enemiesToSpawn;
    }

    [Serializable]
    public class EnemyGroup {
        public EnemySettings enemySettings;
        public int enemyCount = 1;
    }
}
