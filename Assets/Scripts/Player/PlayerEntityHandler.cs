using Mechadroids.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace Mechadroids {
    public class PlayerEntityHandler : IEntityHandler {
        private readonly PlayerPrefabs playerPrefabs;
        private readonly InputHandler inputHandler;
        private readonly Transform playerStartPosition;
        private readonly CinemachineCamera followCamera;
        private readonly DebugMenuHandler debugMenuHandler;

        private PlayerReference playerReference;
        private HitIndicator hitIndicatorInstance;

        public IEntityState EntityState { get; set; }

        public PlayerEntityHandler(PlayerPrefabs playerPrefabs,
            InputHandler inputHandler,
            Transform playerStartPosition,
            CinemachineCamera followCamera,
            DebugMenuHandler debugMenuHandler) {
            this.playerPrefabs = playerPrefabs;
            this.inputHandler = inputHandler;
            this.playerStartPosition = playerStartPosition;
            this.followCamera = followCamera;
            this.debugMenuHandler = debugMenuHandler;
        }

        public void Initialize() {
            inputHandler.SetCursorState(false, CursorLockMode.Locked);

            playerReference = Object.Instantiate(playerPrefabs.playerReferencePrefab);
            playerReference.transform.position = playerStartPosition.position;
            followCamera.Follow = playerReference.transform;

            hitIndicatorInstance = Object.Instantiate(playerPrefabs.hitIndicatorPrefab);
            hitIndicatorInstance.gameObject.SetActive(false);
            EntityState = new PlayerActiveState(this, inputHandler, playerReference, hitIndicatorInstance);
            EntityState.Enter();

#if GAME_DEBUG
            InitializeDebugMenu();
#endif
        }

        private void InitializeDebugMenu() {
            debugMenuHandler.AddUIElement(UIElementType.Single, "MoveSpeed", new float [] { playerReference.playerSettings.moveSpeed }, (newValue) => {
                playerReference.playerSettings.moveSpeed = newValue[0];
            });

        }

        public void Tick() {
            EntityState.HandleInput();
            EntityState.LogicUpdate();
        }

        public void PhysicsTick() {
            EntityState.PhysicsUpdate();
        }

        public void LateTick() {
            // Implement if needed
        }

        public void Dispose() {
            inputHandler.Dispose();
            if (hitIndicatorInstance != null) {
                Object.Destroy(hitIndicatorInstance.gameObject);
                hitIndicatorInstance = null;
            }
        }
    }

    // code that handles the player functionality. Should to the correct state

        // private void HandleMovement() {
        //     if(inputHandler.MovementInput.y != 0) {
        //         currentSpeed += inputHandler.MovementInput.y * playerReference.playerSettings.acceleration * Time.deltaTime;
        //     }
        //     else {
        //         currentSpeed = Mathf.MoveTowards(currentSpeed, 0, playerReference.playerSettings.deceleration * Time.deltaTime);
        //     }
        //
        //     currentSpeed = EntityHelper.HandleSlope(playerReference.tankBody, playerReference.playerSettings.maxSlopeAngle, currentSpeed);
        //
        //     currentSpeed = Mathf.Clamp(currentSpeed, -playerReference.playerSettings.moveSpeed, playerReference.playerSettings.moveSpeed);
        //     playerReference.tankBody.Translate(Vector3.forward * (currentSpeed * Time.deltaTime));
        //
        //     float rotationAmount = inputHandler.MovementInput.x * playerReference.playerSettings.rotationSpeed * Time.deltaTime;
        //     playerReference.tankBody.Rotate(Vector3.up, rotationAmount);
        // }
        //
        // private void HandleTurretAiming() {
        //     Vector2 mouseInput = inputHandler.MouseDelta;
        //
        //     // Update turret horizontal angle
        //     turretAngle += mouseInput.x * playerReference.playerSettings.turretRotationSpeed * Time.deltaTime;
        //     turretAngle = Mathf.Clamp(turretAngle, playerReference.playerSettings.minTurretAngle, playerReference.playerSettings.maxTurretAngle);
        //
        //     // Update barrel elevation angle
        //     barrelAngle -= mouseInput.y * playerReference.playerSettings.barrelRotationSpeed * Time.deltaTime; // Inverted because moving mouse up should raise the barrel
        //     barrelAngle = Mathf.Clamp(barrelAngle, playerReference.playerSettings.minBarrelElevation, playerReference.playerSettings.maxBarrelElevation);
        //
        //     // Apply turret rotation relative to tank body
        //     Quaternion turretRotation = playerReference.tankBody.rotation * Quaternion.Euler(0f, turretAngle, 0f);
        //     playerReference.turretBase.rotation = turretRotation;
        //
        //     // Apply barrel rotation
        //     Quaternion barrelRotation = Quaternion.Euler(barrelAngle, 0f, 0f);
        //     playerReference.barrel.localRotation = barrelRotation;
        // }
        //
        // private void UpdateHitIndicator() {
        //     var ray = new Ray(playerReference.barrelEnd.position, playerReference.barrelEnd.forward);
        //     if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, playerReference.aimLayerMask)) {
        //         hitIndicatorInstance.gameObject.SetActive(true);
        //         hitIndicatorInstance.transform.position = hitInfo.point + hitInfo.normal * 0.01f;
        //         hitIndicatorInstance.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
        //     }
        //     else {
        //         if(hitIndicatorInstance != null) {
        //             hitIndicatorInstance.gameObject.SetActive(false);
        //         }
        //     }
        // }
}
