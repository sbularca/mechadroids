using UnityEngine;
using UnityEngine.Serialization;

namespace Mechadroids {
    public class PlayerReference : MonoBehaviour {
        [Header("Tank Components")]
        public Transform tankBody;

        [Tooltip("Rotates horizontally relative to tankBody")]
        public Transform turretBase;

        [Tooltip("Rotates vertically relative to tankBody")]
        public Transform barrel;

        [Tooltip("Where the bullets come out")]
        public Transform barrelEnd;

        [Header("Aiming")]
        public LayerMask aimLayerMask; // Layers to consider for aiming

        [FormerlySerializedAs("characterSettings")] [Header("Character Specific Settings SO")]
        public PlayerSettings playerSettings;
    }
}
