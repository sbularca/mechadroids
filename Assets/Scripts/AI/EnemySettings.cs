using System;
using UnityEngine;

namespace Mechadroids {
    // enemey settings that supports different kind of enemies. Route settings should be added here
    [CreateAssetMenu(menuName = "Mechadroids/EnemySettings", fileName = "EnemySettings", order = 0)]
    public class EnemySettings : ScriptableObject {
        public Enemy enemy;
        public Route routeSettings;
    }

    [Serializable]
    public class Enemy {
        public EnemyType enemyType;
        public EnemyReference enemyReferencePrefab;

        [Header("Health Settings")]
        public float totalHealth = 100f;

        [Header("Patrol Settings")]
        public float patrolSpeed = 2f;
        public float patrolRotationSpeed = 1f;

        [Header("Detection Settings")]
        public float detectionRadius = 10f;

        [Header("Attack Settings")]
        public float maxDistanceFromPlayer = 5f;
        public float attackRange = 30f;
        public float attackSpeed = 7f;
        public float attackRotationSpeed = 2f;
    }
}
