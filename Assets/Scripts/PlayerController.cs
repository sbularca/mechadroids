using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("Tank Components")]
    public Transform tankBody;

    public Transform turretBase; // Rotates horizontally relative to tankBody
    public Transform barrel; // Rotates vertically
    public Transform barrelEnd;

    [Header("Movement Parameters")]
    public float moveSpeed = 5f;

    public float rotationSpeed = 100f;
    private float currentSpeed = 0f;
    private float acceleration = 2f;
    private float deceleration = 2f;

    [Header("Turret Parameters")]
    public float turretRotationSpeed = 100f; // Adjusted for mouse input

    public float barrelRotationSpeed = 100f; // Adjusted for mouse input
    public float minBarrelAngle = -5f; // Minimum elevation angle
    public float maxBarrelAngle = 30f; // Maximum elevation angle
    public float minTurretAngle = -90f; // Minimum horizontal angle relative to tankBody
    public float maxTurretAngle = 90f; // Maximum horizontal angle relative to tankBody

    [Header("Aiming")]
    public LayerMask aimLayerMask; // Layers to consider for aiming

    public GameObject hitIndicatorPrefab; // Prefab of the hit indicator (e.g., a small quad with a texture)

    // Input actions
    private InputActions inputActions;
    private Vector2 movementInput;
    private Vector2 mouseDelta;

    private float turretAngle = 0f; // Current horizontal angle of turret relative to tank body
    private float barrelAngle = 0f; // Current elevation angle of barrel

    private GameObject hitIndicatorInstance;
    private Vector3 prevHit;

    private void Awake() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        inputActions = new InputActions();
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;
        inputActions.Player.Look.performed += ctx => mouseDelta = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => mouseDelta = Vector2.zero;
        
        hitIndicatorInstance = Instantiate(hitIndicatorPrefab);
        hitIndicatorInstance.gameObject.SetActive(false);
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        if(hitIndicatorInstance != null) {
            Destroy(hitIndicatorInstance);
            hitIndicatorInstance = null;
        }
    }

    private void Update() {
        HandleMovement();
        HandleTurretAiming();
        UpdateHitIndicator();
    }

    private void HandleMovement() {
        if(movementInput.y != 0) {
            currentSpeed += movementInput.y * acceleration * Time.deltaTime;
        } else {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }
        currentSpeed = Mathf.Clamp(currentSpeed, -moveSpeed, moveSpeed);
        tankBody.Translate(Vector3.forward * (currentSpeed * Time.deltaTime));
        float rotationAmount = movementInput.x * rotationSpeed * Time.deltaTime;
        tankBody.Rotate(Vector3.up, rotationAmount);
    }

    private void HandleTurretAiming() {
        Vector2 mouseInput = mouseDelta;

        // Update turret horizontal angle
        turretAngle += mouseInput.x * turretRotationSpeed * Time.deltaTime;
        turretAngle = Mathf.Clamp(turretAngle, minTurretAngle, maxTurretAngle);

        // Update barrel elevation angle
        barrelAngle -= mouseInput.y * barrelRotationSpeed * Time.deltaTime; // Inverted because moving mouse up should raise the barrel
        barrelAngle = Mathf.Clamp(barrelAngle, minBarrelAngle, maxBarrelAngle);

        // Apply turret rotation relative to tank body
        Quaternion turretRotation = tankBody.rotation * Quaternion.Euler(0f, turretAngle, 0f);
        turretBase.rotation = turretRotation;

        // Apply barrel rotation
        Quaternion barrelRotation = Quaternion.Euler(barrelAngle, 0f, 0f);
        barrel.localRotation = barrelRotation;
    }

    private void UpdateHitIndicator() {
        var ray = new Ray(barrelEnd.position, barrelEnd.forward);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask)) {
            hitIndicatorInstance.gameObject.SetActive(true);
            hitIndicatorInstance.transform.position = hitInfo.point + hitInfo.normal * 0.01f; // Slight offset to prevent z-fighting
            hitIndicatorInstance.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
        } else {
            if(hitIndicatorInstance != null) {
                hitIndicatorInstance.gameObject.SetActive(false);
            }
        }
    }
}