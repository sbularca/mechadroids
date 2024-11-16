using Mechadroids;
using UnityEditor;
using UnityEngine;

public class PlayerReference : MonoBehaviour {
    [Header("Tank Components")]
    public Transform tankBody;

    [Tooltip("Rotates horizontally relative to tankBody")]
    public Transform turretBase;

    [Tooltip("Rotates vertically relative to tankBody")]
    public Transform barrel;

    public Transform barrelEnd;

    [Header("Aiming")]
    public LayerMask aimLayerMask; // Layers to consider for aiming

    public CharacterSettings characterSettings;
}
