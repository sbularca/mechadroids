using UnityEngine;

namespace Mechadroids.UI {
    public class DebugMenuHandler {
        private readonly UIPrefabs uiPrefabs;
        private readonly InputHandler inputHandler;
        private DebugMenuReference debugMenu;

        public DebugMenuHandler(UIPrefabs uiPrefabs, InputHandler inputHandler) {
            this.uiPrefabs = uiPrefabs;
            this.inputHandler = inputHandler;
        }

        public void Initialize() {
            debugMenu = Object.Instantiate(uiPrefabs.debugMenuReferencePrefab);
            debugMenu.gameObject.SetActive(false);
        }

        private void Toggle() {
            debugMenu.gameObject.SetActive(!debugMenu.gameObject.activeSelf);
        }

        public void Tick() {
            if (inputHandler.InputActions.UI.Cancel.WasPerformedThisFrame()) {
                Toggle();
            }
        }

        public void Dispose() {
            Object.Destroy(debugMenu.gameObject);
        }
    }
}
