using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour {
    // References
    [Header("Tank Components")]
    public Transform tankBody;

    public Transform turretBase; // Rotates horizontally relative to tankBody
    public Transform barrel; // Rotates vertically

    [Header("Movement Parameters")]
    public float moveSpeed = 5f;

    public float rotationSpeed = 100f;
    private float currentSpeed = 0f;
    private float acceleration = 2f;
    private float deceleration = 2f;

    [Header("Turret Parameters")]
    public float turretRotationSpeed = 10f;

    public float barrelRotationSpeed = 10f;
    public float minBarrelAngle = -5f; // Minimum elevation angle
    public float maxBarrelAngle = 30f; // Maximum elevation angle
    public float minTurretAngle = -90f; // Minimum horizontal angle relative to tankBody
    public float maxTurretAngle = 90f; // Maximum horizontal angle relative to tankBody
    public LayerMask aimLayerMask; // Layers to consider for aiming

    [Header("Camera Reference")]
    public Camera mainCamera;

    // Input actions
    private InputActions inputActions;
    private Vector2 movementInput;

    private void Awake() {
        //Cursor.visible = false;
        inputActions = new InputActions();

        // Subscribe to the Move action
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    private void Update() {
        HandleMovement();
        HandleTurretAiming();
    }

    private void HandleMovement() {
        // Accelerate or decelerate
        if(movementInput.y != 0) {
            currentSpeed += movementInput.y * acceleration * Time.deltaTime;
        } else {
            // Gradually slow down when no input
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Clamp the speed
        currentSpeed = Mathf.Clamp(currentSpeed, -moveSpeed, moveSpeed);

        // Move the tank
        tankBody.Translate(Vector3.forward * (currentSpeed * Time.deltaTime));

        // Rotation remains the same
        float rotationAmount = movementInput.x * rotationSpeed * Time.deltaTime;
        tankBody.Rotate(Vector3.up, rotationAmount);
    }

    private void HandleTurretAiming() {
        if(mainCamera == null) {
            Debug.LogWarning("Main Camera is not assigned in the CharacterController script.");
            return;
        }

        // Create a ray from the mouse position into the world
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Raycast to find the point where the mouse is pointing
        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask)) {
            if(hitInfo.collider.CompareTag("Player")) {
                Debug.Log("Hit the player itself");
                return;
            }

            Vector3 targetPoint = hitInfo.point;

            // Rotate turret base (horizontal rotation relative to tank body)
            Vector3 turretDirection = targetPoint - turretBase.position;
            turretDirection.y = 0f; // Ignore vertical component for horizontal rotation

            // Tank body's forward direction
            Vector3 bodyForward = tankBody.forward;

            // Calculate the angle between tank body's forward and the target direction
            float angleToTarget = Vector3.SignedAngle(bodyForward, turretDirection, Vector3.up);

            // Clamp the angle to the allowed turret rotation limits
            angleToTarget = Mathf.Clamp(angleToTarget, minTurretAngle, maxTurretAngle);

            // Calculate the desired turret rotation relative to the tank body
            Quaternion targetRotation = tankBody.rotation * Quaternion.Euler(0f, angleToTarget, 0f);

            // Smoothly rotate the turret towards the target rotation
            turretBase.rotation = Quaternion.Lerp(
                turretBase.rotation,
                targetRotation,
                turretRotationSpeed * Time.deltaTime);

            // Rotate barrel (vertical rotation)
            // Direction from barrel to target point
            Vector3 barrelDirection = targetPoint - barrel.position;

            // Transform barrelDirection into the local space of the turretBase
            Vector3 localBarrelDirection = turretBase.InverseTransformDirection(barrelDirection);

            // Calculate the angle between the local forward direction and the barrelDirection
            float elevationAngle = Mathf.Atan2(localBarrelDirection.y, localBarrelDirection.z) * Mathf.Rad2Deg;

            // Clamp the elevation angle to the allowed barrel rotation limits
            elevationAngle = Mathf.Clamp(elevationAngle, minBarrelAngle, maxBarrelAngle);

            // Apply the rotation to the barrel
            Quaternion desiredBarrelRotation = Quaternion.Euler(elevationAngle, 0f, 0f);

            barrel.localRotation = Quaternion.Lerp(
                barrel.localRotation,
                desiredBarrelRotation,
                barrelRotationSpeed * Time.deltaTime);
        }
    }
}