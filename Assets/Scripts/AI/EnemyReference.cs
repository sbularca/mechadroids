using UnityEngine;
using UnityEngine.AI;
namespace Mechadroids {
    // most of the enemy will point towards a set of enemy settings
    public class EnemyReference : MonoBehaviour {
        public int id;
        public EnemySettings enemySettings;
    }
}
