using UnityEngine;
using UnityEngine.AI;
namespace Mechadroids {
    public class EnemyReference : MonoBehaviour {
        [Header("AI Components")]
        public NavMeshAgent navMeshAgent;
        public Transform[] patrolPoints;

        [Header("Detection Settings")]
        public float detectionRadius = 10f;

        private void Awake() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            // Initialize other components if needed
        }
    }
}
