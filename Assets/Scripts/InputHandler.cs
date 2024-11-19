using UnityEngine;

namespace Mechadroids {
    /// <summary>
    /// Handles input state from the Input System
    /// </summary>
    public class InputHandler {
        private InputActions inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        public InputActions InputActions => inputActions;

        public void Initialize() {
            // initialize input here
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
