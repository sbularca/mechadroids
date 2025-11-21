using UnityEngine;

namespace Mechadroids.UI {
    public class UIPauseMenuHandler {
        private readonly UIPrefabs uiPrefabs;
        private readonly InputHandler inputHandler;
        private PauseMenuReference uiMenu;
        private bool isPauseMenuActive;

        public UIPauseMenuHandler(UIPrefabs uiPrefabs, InputHandler inputHandler) {
            this.uiPrefabs = uiPrefabs;
            this.inputHandler = inputHandler;
        }

        public void Initialize() {
            uiMenu = Object.Instantiate(uiPrefabs.pauseMenuReferencePrefab);
            uiMenu.gameObject.SetActive(false);
        }

        public void ToggleMenu(bool isActive) {
            uiMenu.gameObject.SetActive(isActive);
            isPauseMenuActive = isActive;
        }

        public void Tick() {
            if (inputHandler.InputActions.UI.Cancel.WasPerformedThisFrame()) {
                ToggleMenu(!isPauseMenuActive);
            }
        }
    }
}
