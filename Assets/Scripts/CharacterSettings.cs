using UnityEngine;
using UnityEngine.Serialization;

namespace Mechadroids {
    [CreateAssetMenu(menuName = "Mechadroids/CharacterSettings", fileName = "CharacterSettings", order = 0)]
    public class CharacterSettings : ScriptableObject {
        [Header("Movement Parameters")]
        public float moveSpeed =3f;
        public float rotationSpeed = 10f;
        public float acceleration = 2f;
        public float deceleration = 2f;
        public float maxSlopeAngle = 45f;

        [Header("Turret Parameters")]
        public float turretRotationSpeed = 10f;
        public float barrelRotationSpeed = 10f;
        public float maxBarrelElevation = 25f;
        public float minBarrelElevation = -45f;

        public float minTurretAngle = -180f;
        public float maxTurretAngle = 180f;
    }
}
