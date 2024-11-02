using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechadroids {
    public class InputHandler {
        private InputActions inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseDelta { get; private set; }

        public void Initialize() {
            inputActions = new InputActions();
            inputActions.Player.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => MovementInput = Vector2.zero;
            inputActions.Player.Look.performed += ctx => MouseDelta = ctx.ReadValue<Vector2>();
            inputActions.Player.Look.canceled += ctx => MouseDelta = Vector2.zero;

            inputActions.Enable();
        }

        public void SetCursorState(bool visibility, CursorLockMode lockMode) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Dispose() {
            SetCursorState(true, CursorLockMode.None);
            inputActions.Disable();
        }
    }
}