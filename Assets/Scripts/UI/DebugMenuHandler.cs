using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

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

        private void ToggleMenu() {
            debugMenu.gameObject.SetActive(!debugMenu.gameObject.activeSelf);
            if(debugMenu.gameObject.activeSelf) {
                inputHandler.SetCursorState(true, CursorLockMode.None);
            }
            else {
                inputHandler.SetCursorState(false, CursorLockMode.Locked);
            }
        }

        public void Tick() {
            if(inputHandler.InputActions.UI.Cancel.WasPerformedThisFrame()) {
                ToggleMenu();
            }
        }

        /// <summary>
        ///  Gen
        /// </summary>
        /// <param name="type"></param>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        /// <param name="onValueChanged"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddUIElement<T>(UIElementType type, string variableName, T[] value, Action<T[]> onValueChanged) {
            UIElementReference uiElementReference = uiPrefabs.GetUIElementReference(type);
            // since this is a debug menu feature, we do not need to bother with pooling and saving memory since most likely this code will be removed from the release version
            UIElementReference uiElement = Object.Instantiate(uiElementReference, debugMenu.contentHolder);
            uiElement.SetName(variableName);
            switch(type) {
                case UIElementType.Single:
                    uiElement.SetValue(value[0]);
                    break;
                case UIElementType.Multiple:
                    uiElement.SetValue(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            foreach(TMP_InputField inputField in uiElement.InputFields) {
                inputField.onSubmit.AddListener((newValue) => {
                    T[] newValueArray = new T[value.Length];
                    for(int i = 0; i < newValueArray.Length; i++) {
                        try {
                            newValueArray[i] = (T)Convert.ChangeType(newValue, typeof(T));
                        }
                        catch {
                            Debug.LogWarning($"Wrong type of input, try again!");
                            return;
                        }
                    }
                    onValueChanged.Invoke(newValueArray);
                });
            }
        }

        public void Dispose() {
            if(debugMenu != null) {
                Object.Destroy(debugMenu.gameObject);
            }
        }
    }
}
