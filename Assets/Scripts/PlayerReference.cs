using UnityEngine;

public class PlayerReference : MonoBehaviour {
    [Header("Tank Components")]
    public Transform tankBody;

    [Tooltip("Rotates horizontally relative to tankBody")]
    public Transform turretBase;

    [Tooltip("Rotates vertically relative to tankBody")]
    public Transform barrel;

    public Transform barrelEnd;

    [Header("Movement Parameters")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float acceleration = 2f;
    public float deceleration = 2f;

    [Header("Turret Parameters")]
    public float turretRotationSpeed = 10f;
    public float barrelRotationSpeed = 10f;
    public float minBarrelAngle = -20f;
    public float maxBarrelAngle = 30f;
    public float minTurretAngle = -90f;
    public float maxTurretAngle = 90f;

    [Header("Aiming")]
    public LayerMask aimLayerMask; // Layers to consider for aiming
}
