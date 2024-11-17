using UnityEngine;

namespace Mechadroids {
    public class InputHandler {
        private InputActions inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        public InputActions InputActions => inputActions;

        public void Initialize() {
            inputActions ??= new InputActions();
            inputActions.Player.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += _ => MovementInput = Vector2.zero;
            inputActions.Player.Look.performed += ctx => MouseDelta = ctx.ReadValue<Vector2>();
            inputActions.Player.Look.canceled += _ => MouseDelta = Vector2.zero;
            inputActions.Enable();
        }

        public void SetCursorState(bool visibility, CursorLockMode lockMode) {
            Cursor.visible = visibility;
            Cursor.lockState = lockMode;
        }

        public void Dispose() {
            SetCursorState(true, CursorLockMode.None);
            inputActions.Disable();
        }
    }
}
